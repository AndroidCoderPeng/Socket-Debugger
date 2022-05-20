using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Socket_Debugger.Model;
using SQLite;

namespace Socket_Debugger.Utils
{
    public class SqLiteHelper
    {
        private static readonly string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SocketConnection.db");

        private static SqLiteHelper _sqLiteHelper;
        private SQLiteConnection _connection;

        private SqLiteHelper()
        {
        }

        public static SqLiteHelper GetInstance()
        {
            return _sqLiteHelper ?? (_sqLiteHelper = new SqLiteHelper());
        }

        public void InitDataBase()
        {
            GetSqLiteConnection().CreateTable<ConnectionModel>();
        }

        private SQLiteConnection GetSqLiteConnection()
        {
            return _connection ?? (_connection = new SQLiteConnection(ConfigPath));
        }

        // 存入连接配置
        public void AddConfig(ConnectionModel model)
        {
            _connection.Insert(model);
        }

        // 根据连接类型查询数据集合
        public ObservableCollection<ConnectionModel> QueryConnectionModelsByType(string type)
        {
            ObservableCollection<ConnectionModel> collectionResult = new ObservableCollection<ConnectionModel>();
            TableQuery<ConnectionModel> tableQuery = _connection.Table<ConnectionModel>()
                .Where(param => param.ConnType.Equals(type));
            foreach (ConnectionModel model in tableQuery)
            {
                collectionResult.Add(model);
            }

            return collectionResult;
        }

        // 根据uuid查询数据
        public ConnectionModel QueryConnectionModelByUuid(string uuid)
        {
            return _connection
                .Query<ConnectionModel>("select * from ConnectionModel")
                .First(param => param.Uuid.Equals(uuid));
        }

        //根据uuid删除
        public void DeleteConnectionModelByUuid(string uuid)
        {
            ConnectionModel connectionModel = QueryConnectionModelByUuid(uuid);
            if (connectionModel != null)
            {
                _connection.Delete(connectionModel);
            }
        }

        //根据ConnectionModel实体类删除
        public void DeleteConnectionModel(ConnectionModel model)
        {
            _connection.Delete(model);
        }

        //根据uuid修改数据
        public void UpdateConnectionModelByUuid(string uuid)
        {
            ConnectionModel connectionModel = QueryConnectionModelByUuid(uuid);
            if (connectionModel != null)
            {
                _connection.Update(connectionModel);
            }
        }

        //根据ConnectionModel实体类修改数据
        public void UpdateConnectionModel(ConnectionModel model)
        {
            _connection.Update(model);
        }
    }
}
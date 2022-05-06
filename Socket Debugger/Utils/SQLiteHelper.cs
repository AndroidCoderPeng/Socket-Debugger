using System;
using System.IO;
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

        public void AddConfig(string uuid, string connType, string comment, string connHost, string connPort,
            string message)
        {
            ConnectionModel config = new ConnectionModel
            {
                Uuid = uuid,
                ConnType = connType,
                Comment = comment,
                ConnHost = connHost,
                ConnPort = connPort,
                Message = message
            };
            _connection.Insert(config);
        }
    }
}
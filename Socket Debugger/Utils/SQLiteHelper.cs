using System;
using System.IO;
using Socket_Debugger.Model;
using SQLite;

namespace Socket_Debugger.Utils
{
    public class SqLiteHelper
    {
        private static readonly string ConfigPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "SocketConfig.db");

        private static SqLiteHelper _sqLiteHelper;
        private SQLiteConnection _connection;

        private SqLiteHelper()
        {
        }

        public static SqLiteHelper GetInstance()
        {
            return _sqLiteHelper ?? (_sqLiteHelper = new SqLiteHelper());
        }

        private SQLiteConnection GetSqLiteConnection()
        {
            return _connection ?? (_connection = new SQLiteConnection(ConfigPath));
        }

        public void InitDataBase()
        {
            GetSqLiteConnection().CreateTable<SocketConfig>();
        }

        public void AddConfig(string uuid, string connType, string comment, string connHost, string connPort,
            string message)
        {
            SocketConfig config = new SocketConfig();
            config.Uuid = uuid;
            config.ConnType = connType;
            config.Comment = comment;
            config.ConnHost = connHost;
            config.ConnPort = connPort;
            config.Message = message;
            GetSqLiteConnection().Insert(config);
        }
    }
}
using SQLite;

namespace Socket_Debugger.Model
{
    public class SocketConfig
    {
        [PrimaryKey] public ulong Id { get; set; }

        public string ConnType { get; set; }

        public string Comment { get; set; }

        public string ConnHost { get; set; }

        public string ConnPort { get; set; }

        public string Message { get; set; }
    }
}
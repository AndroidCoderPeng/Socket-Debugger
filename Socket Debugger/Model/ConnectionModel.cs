using SQLite;

namespace Socket_Debugger.Model
{
    public class ConnectionModel
    {
        [PrimaryKey, AutoIncrement] public int Id { get; set; }

        public string Uuid { get; set; }

        public string Comment { get; set; }

        public string ConnType { get; set; }

        public string ConnHost { get; set; }

        public string ConnPort { get; set; }

        public string MsgType { get; set; }

        public string Message { get; set; }

        public int TimePeriod { get; set; }
    }
}
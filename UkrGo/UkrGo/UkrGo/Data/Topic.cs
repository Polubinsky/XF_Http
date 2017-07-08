using SQLite;

namespace UkrGo.Data
{
    public class Topic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string TopicName { get; set; }

        public string URL { get; set; }

        public override string ToString()
        {
            return string.Format("[Topic: ID={0}, TopicName={1}, URL={2}]", ID, TopicName, URL);
        }
    }
}

namespace Ex2_2020.Models {
    public class Topic {
        private int _id;
        private string _topicname;

        public Topic(int id, string topicname) {
            _id = id;
            _topicname = topicname;
        }

        public int Id {
            get => _id;
            set => _id = value;
        }

        public string Topicname {
            get => _topicname;
            set => _topicname = value;
        }
    }
}
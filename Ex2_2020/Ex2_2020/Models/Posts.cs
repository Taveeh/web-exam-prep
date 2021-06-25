using System;

namespace Ex2_2020.Models {
    public class Posts {
        private int _id;
        private string _user;
        private int _topicid;
        private string _text;
        private int _date;

        public Posts(int id, string user, int topicid, string text, int date) {
            _id = id;
            _user = user;
            _topicid = topicid;
            _text = text;
            _date = date;
        }

        public int Id {
            get => _id;
            set => _id = value;
        }

        public string User {
            get => _user;
            set => _user = value;
        }

        public int Topicid {
            get => _topicid;
            set => _topicid = value;
        }

        public string Text {
            get => _text;
            set => _text = value;
        }

        public int Date {
            get => _date;
            set => _date = value;
        }
    }
}
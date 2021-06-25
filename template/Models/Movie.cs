namespace template.Models
{
    public class Movie
    {
        private int _id;
        private string _title;
        private int _duration;

        public Movie(int id, string title, int duration)
        {
            _id = id;
            _title = title;
            _duration = duration;
        }

        public int Id => _id;

        public string Title => _title;

        public int Duration => _duration;
    }
}
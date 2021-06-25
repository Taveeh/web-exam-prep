namespace template.Models {
    public class Author
    {
        private int _id;
        private string _name;
        private string _documentList;
        private string _movieList;

        public Author(int id, string name, string documentList, string movieList)
        {
            _id = id;
            _name = name;
            _documentList = documentList;
            _movieList = movieList;
        }

        public int Id => _id;

        public string Name => _name;

        public string DocumentList => _documentList;

        public string MovieList => _movieList;
    }
    
    
}
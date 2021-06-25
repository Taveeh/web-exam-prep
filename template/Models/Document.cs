namespace template.Models
{
    public class Document
    {
        private int _id;
        private string _name;
        private string _contents;

        public Document(int id, string name, string contents)
        {
            _id = id;
            _name = name;
            _contents = contents;
        }

        public int Id => _id;

        public string Name => _name;

        public string Contents => _contents;
    }
}
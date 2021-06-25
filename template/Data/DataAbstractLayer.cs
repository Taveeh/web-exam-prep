using System.Collections.Generic;
using Npgsql;
using template.Models;

namespace template.Data {
    public class DataAbstractLayer {
        private static string host = "localhost";
        private static string username = "postgres";
        private static string password = "2369";
        private static string database = "Movies";
        private string connectionString = $"Host={host};Username={username};Password={password};Database={database}";
        
        public DataAbstractLayer() {
        }

        public List<Author> GetAllAuthors() {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "SELECT * FROM Author";
            List<Author> elements = new List<Author>();
            var cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();
            while (npgsqlDataReader.Read()) {
                elements.Add(new Author(
                    npgsqlDataReader.GetInt32(0),
                    npgsqlDataReader.GetString(1),
                    npgsqlDataReader.GetString(2),
                    npgsqlDataReader.GetString(3)
                    ));
            }
        
            return elements;
        }
        
        public Author GetAuthorByName(string name) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "SELECT * FROM Author where name = @name";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("name", name);
            NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();
            if (npgsqlDataReader.Read()) {
                return new Author(
                    npgsqlDataReader.GetInt32(0),
                    npgsqlDataReader.GetString(1),
                    npgsqlDataReader.GetString(2),
                    npgsqlDataReader.GetString(3)
                );
            }
            return null;
        }

        public Movie GetMovieById(int id)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "SELECT * FROM Movie where id = @id";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();
            if (npgsqlDataReader.Read())
            {
                return new Movie(
                    npgsqlDataReader.GetInt32(0),
                    npgsqlDataReader.GetString(1),
                    npgsqlDataReader.GetInt32(2)
                );
            }
            return null;
        }
        
        public Document GetDocumentById(int id)
        {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "SELECT * FROM Document where id = @id";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();
            if (npgsqlDataReader.Read())
            {
                return new Document(
                    npgsqlDataReader.GetInt32(0),
                    npgsqlDataReader.GetString(1),
                    npgsqlDataReader.GetString(2)
                );
            }
            return null;
        }

        public void AddDocument(string name, string contents) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "INSERT INTO Document(name, contents) VALUES (@name, @contents)";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Parameters.AddWithValue("contents", contents);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void AddChild(int id, int parentId) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "INSERT INTO Tabel(id, parentId) VALUES (@id, @parentId)";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("parentId", parentId);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Remove(int id) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "DELETE FROM Tabel WHERE id = @id";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void Update(int id, string text) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "UPDATE Tabel SET text = @text WHERE id = @id";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Parameters.AddWithValue("text", text);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

    }
}
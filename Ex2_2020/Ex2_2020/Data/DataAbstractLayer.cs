using System;
using System.Collections.Generic;
using Ex2_2020.Models;
using Npgsql;

namespace Ex2_2020.Data {
    public class DataAbstractLayer {
        private static string host = "localhost";
        private static string username = "postgres";
        private static string password = "branza123";
        private static string database = "Ex3_2020";
        private string connectionString = $"Host={host};Username={username};Password={password};Database={database}";
        
        public DataAbstractLayer() {
        }

        public List<Posts> getAllPosts() {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "SELECT * FROM posts";
            var cmd = new NpgsqlCommand(sql, conn);
            List<Posts> postsList = new List<Posts>();
            
            NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();
            while (npgsqlDataReader.Read()) {
                postsList.Add(new Posts(
                    npgsqlDataReader.GetInt32(0),
                    npgsqlDataReader.GetString(1),
                    npgsqlDataReader.GetInt32(2),
                    npgsqlDataReader.GetString(3),
                    npgsqlDataReader.GetInt32(4)
                ));
            }

            return postsList;
        }

        public void UpdatePost(string user, int date, int id, string text) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "UPDATE posts SET text = @text, username = @user, postdate = @date WHERE id = @id";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("text", text);
            cmd.Parameters.AddWithValue("user", user);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("id", id);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void AddPost(int id, string text, string user, int date, int topicid) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "INSERT INTO posts(id, username, topicid, postdate, text) VALUES (@id, @user, @topicid, @date, @text)";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("id", id);

            cmd.Parameters.AddWithValue("text", text);
            cmd.Parameters.AddWithValue("user", user);
            cmd.Parameters.AddWithValue("date", date);
            cmd.Parameters.AddWithValue("topicid", topicid);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void AddTopic(string name) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "INSERT INTO topics(topicname) VALUES (@name)";
            var cmd = new NpgsqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("name", name);
            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public int GetTopicIdWithName(string name) {
            var conn = new NpgsqlConnection(connectionString);
            conn.Open();
            var sql = "SELECT T.id FROM topics T WHERE T.topicname = \'" + name + "\'";
            var cmd = new NpgsqlCommand(sql, conn);
            NpgsqlDataReader npgsqlDataReader = cmd.ExecuteReader();

            if (npgsqlDataReader.Read()) {
                return npgsqlDataReader.GetInt32(0);
            }
            return -1;
        }
    }
}
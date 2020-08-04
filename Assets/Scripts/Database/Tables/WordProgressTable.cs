using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class WordProgressTable : SqliteHelper
    {
        private const string TABLE_NAME = "WordProgress";

        private const string WORD = "word";
        private const string TIMES_HIT = "times_hit";
        private const string TIMES_ATTEMPTED = "times_attempted";
        private const string USER_ID = "user_id";
        private const string CATEGORY_ID = "category_id";


        public WordProgressTable() : base()
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText =
                $@"CREATE TABLE IF NOT EXISTS {TABLE_NAME}
                (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    {WORD} STRING DEFAULT NULL,
                    {TIMES_HIT} INTEGER DEFAULT 0,
                    {TIMES_ATTEMPTED} INTEGER DEFAULT 0,
                    {USER_ID} INTEGER DEFAULT 0,
                    {CATEGORY_ID} INTEGER DEFAULT 0, 
                    FOREIGN KEY({USER_ID}) REFERENCES Users(id) ON DELETE CASCADE,
                    FOREIGN KEY({CATEGORY_ID}) REFERENCES WordCategories(id) ON DELETE NO ACTION
                );";
            cmd.ExecuteNonQuery();
        }

        public void Create(WordProgressModel wordProgress)
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText =
                $@"INSERT INTO {TABLE_NAME}
                (
                    {WORD},
                    {TIMES_HIT},
                    {TIMES_ATTEMPTED},
                    {USER_ID}
                )
                VALUES
                (
                    '{wordProgress.word}',
                    {wordProgress.timesHit},
                    {wordProgress.timesAttempted},
                    {wordProgress.userId}
                );";
            cmd.ExecuteNonQuery();
        }

        public IDataReader GetAllData()
        {
            return base.GetAllData(TABLE_NAME);
        }

        public List<WordProgressModel> GetByUserId(int userId)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = $@"SELECT * FROM {TABLE_NAME} WHERE (user_id={userId});";
            IDataReader reader = cmd.ExecuteReader();

            List<WordProgressModel> WordProgressData = new List<WordProgressModel>();

            while (reader.Read())
            {
                WordProgressData.Add(
                    new WordProgressModel
                    {
                        id = Convert.ToInt32(reader[0]),
                        word = reader[1].ToString(),
                        timesHit = Convert.ToInt32(reader[2]),
                        timesAttempted = Convert.ToInt32(reader[3]),
                        userId = Convert.ToInt32(reader[4]),
                        categoryId = Convert.ToInt32(reader[5])
                    }
                );
            }

            return WordProgressData;
        }

        public void UpdateWordProgress(WordProgressModel wordProgress)
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText =
                $@"UPDATE {TABLE_NAME}
                SET
                    {TIMES_HIT}={wordProgress.timesHit},
                    {TIMES_ATTEMPTED}={wordProgress.timesAttempted}
                WHERE id={wordProgress.id}";
            cmd.ExecuteNonQuery();
        }

        public void DeleteAllData()
        {
            base.DeleteAllData(TABLE_NAME);
        }
    }
}

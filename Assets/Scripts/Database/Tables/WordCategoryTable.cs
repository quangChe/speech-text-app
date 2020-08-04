using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class WordCategoryTable : SqliteHelper
    {
        private const string TABLE_NAME = "WordCategories";

        private const string USER_ID = "user_id";
        private const string CATEGORY_NAME = "category_name";
        private const string STARS_COLLECTED = "stars_collected";

        public WordCategoryTable() : base()
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText =
                $@"CREATE TABLE IF NOT EXISTS {TABLE_NAME}
                (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    {USER_ID} INTEGER DEFAULT 0,
                    {CATEGORY_NAME} INTEGER DEFAULT NULL, 
                    {STARS_COLLECTED} STRING DEFAULT DEFAULT 0,
                    FOREIGN KEY({USER_ID}) REFERENCES Users(id) ON DELETE CASCADE
                );";
            cmd.ExecuteNonQuery();
        }

        public void Create(WordCategoryModel wordCategory)
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText = 
                $@"INSERT INTO {TABLE_NAME}
                (
                    {USER_ID},
                    {CATEGORY_NAME},
                    {STARS_COLLECTED}
                )
                VALUES
                (
                    {wordCategory.userId},
                    '{wordCategory.categoryName}',
                    {wordCategory.starsCollected}
                );";
            cmd.ExecuteNonQuery();
        }

        public IDataReader GetAllData()
        {
            return base.GetAllData(TABLE_NAME);
        }

        public List<WordCategoryModel> GetByUserId(int userId)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = $@"SELECT * FROM {TABLE_NAME} WHERE (user_id={userId});";
            IDataReader reader = cmd.ExecuteReader();

            List<WordCategoryModel> WordCategoryData = new List<WordCategoryModel>();

            while (reader.Read())
            {
                WordCategoryData.Add(
                    new WordCategoryModel
                    {
                        id = Convert.ToInt32(reader[0]),
                        userId = Convert.ToInt32(reader[1]),
                        categoryName = reader[2].ToString(),
                        starsCollected = Convert.ToInt32(reader[3])
                    }
                );
            }

            return WordCategoryData;
        }

        public void UpdateWordCategory(WordCategoryModel wordCategory)
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText =
                $@"UPDATE {TABLE_NAME}
                SET
                    {CATEGORY_NAME}={wordCategory.categoryName},
                    {STARS_COLLECTED}='{wordCategory.starsCollected}'
                WHERE id={wordCategory.id}";
            cmd.ExecuteNonQuery();
        }

        public void DeleteAllData()
        {
            base.DeleteAllData(TABLE_NAME);
        }
    }
}

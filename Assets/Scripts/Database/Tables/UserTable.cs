using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Database
{
    public class UserTable : SqliteHelper
    {
        private const string TABLE_NAME = "Users";

        private const string NAME = "name";
        private const string TOTAL_REPS = "total_reps";
        private const string TOTAL_STARS = "total_stars";
        private const string LAST_LOGIN = "last_login";

        public UserTable() : base()
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText =
                $@"CREATE TABLE IF NOT EXISTS {TABLE_NAME}
                (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    {NAME} TEXT DEFAULT NULL,
                    {TOTAL_REPS} INTEGER DEFAULT 0, 
                    {TOTAL_STARS} INTEGER DEFAULT 0,
                    {LAST_LOGIN} DATETIME DEFAULT CURRENT_TIMESTAMP, 
                );";
            cmd.ExecuteNonQuery();
        }

        public void Create(UserModel user)
        {
            IDbCommand cmd = GetDbCommand();
            cmd.CommandText =
                $@"INSERT INTO {TABLE_NAME}
                (
                    {NAME},
                    {TOTAL_REPS},
                    {TOTAL_STARS}
                ) 
                VALUES
                (
                    '{user.name}',
                    {user.totalReps},
                    {user.totalStars}
                );";
            cmd.ExecuteNonQuery();
        }

        public IDataReader GetAllData()
        {
            return base.GetAllData(TABLE_NAME);
        }

        public UserModel GetById(int id)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = $@"SELECT * FROM {TABLE_NAME} WHERE id={id};";
            IDataReader reader = cmd.ExecuteReader();

            UserModel user = null;

            while (reader.Read())
            {
                user = new UserModel
                {
                    id = Convert.ToInt32(reader[0]),
                    name = reader[1].ToString(),
                    totalReps = Convert.ToInt32(reader[2]),
                    totalStars = Convert.ToInt32(reader[3]),
                    lastLogin = reader[4].ToString()
                };
            }
            return user;
        }

        public void UpdateLoginDate(int userId)
        {
            DateTime now = DateTime.UtcNow;
            string timeNow = now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText =
                $@"UPDATE {TABLE_NAME}
                SET {LAST_LOGIN}='{timeNow}'
                WHERE id={userId};";
            cmd.ExecuteNonQuery();
        }

        public void UpdateUser(UserModel user)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText =
                $@"UPDATE {TABLE_NAME}
                SET
                    {NAME}='{user.name}',
                    {TOTAL_REPS}={user.totalReps}, 
                    {TOTAL_STARS}={user.totalStars}
                WHERE id={user.id};";
            cmd.ExecuteNonQuery();
        }

        public void DeleteAllData()
        {
            base.DeleteAllData(TABLE_NAME);
        }
    }
}


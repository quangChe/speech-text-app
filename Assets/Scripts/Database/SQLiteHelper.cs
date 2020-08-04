using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Data.Sqlite;
using UnityEngine;
using System.Data;

namespace Database
{
    public class SqliteHelper
    {
        private const string dbName = "db_speech_hero";

        public string dbPath;
        public IDbConnection dbConnection;

        public SqliteHelper()
        {
            dbPath = "URI=file:" + Application.persistentDataPath + "/" + dbName;
            Debug.Log(dbPath);
            dbConnection = new SqliteConnection(dbPath);
            dbConnection.Open();
            Debug.Log("Opened a connection to " + dbName + " database");
        }

        ~SqliteHelper()
        {
            dbConnection.Close();
            Debug.Log("Connection to " + dbName + " database closed.");
        }

        public IDbCommand GetDbCommand()
        {
            return dbConnection.CreateCommand();
        }

        public IDataReader GetAllData(string table_name)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = "SELECT * FROM " + table_name;
            return cmd.ExecuteReader();
        }

        public void DeleteAllData(string table_name)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = "DROP TABLE IF EXISTS " + table_name;
            cmd.ExecuteNonQuery();
        }

        public IDataReader GetNumOfRows(string table_name)
        {
            IDbCommand cmd = dbConnection.CreateCommand();
            cmd.CommandText = "SELECT COALESCE(MAX(id)+1, 0) FROM " + table_name;
            return cmd.ExecuteReader();
        }

        public void Close()
        {
            dbConnection.Close();
        }
    }
}
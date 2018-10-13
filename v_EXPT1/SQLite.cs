using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.Data.SQLite;

namespace Exp_T1
{
    class SQLite
    {
        public static void Connect(string name)
        {
            //SQLite m = new SQLite();
            //SQLite.Connect(name);

            ///Creating a database file
            SQLiteConnection.CreateFile("MyDatabase.sqlite");

            ///Connecting to a database
            SQLiteConnection m_dbConnection;

            m_dbConnection =
                new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            ///Creating a table
            //string sql = "create table highscores (name varchar(20), score int)";
            string sql = "CREATE TABLE highscores (name VARCHAR(20), score INT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

            ///Filling our table
            //string sql = "insert into highscores (name, score) values ('Me', 9001)";

            /*
            string sql = "insert into highscores (name, score) values ('Me', 3000)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('Myself', 6000)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "insert into highscores (name, score) values ('And I', 9001)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
             * 
             * 
             * 
             * 
             * 
             * 
             * 
             */
        }

        public static void GetData()
        {
            /*
            ///Getting the high scores out of our database
            string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
             * 
             * 
             SQLiteDataReader reader = command.ExecuteReader();
             * 
             * 
             string sql = "select * from highscores order by score desc";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
             * 
             */
        }
    }
}

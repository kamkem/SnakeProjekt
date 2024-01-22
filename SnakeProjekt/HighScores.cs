using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection.PortableExecutable;
using System.Data.SqlTypes;

namespace SnakeProjekt
{
    class HighScores
    {
       // SqlConnection conn;

        static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Bobon\\source\\repos\\SnakeProjekt\\SnakeProjekt\\HighScoreDatabase.mdf;Integrated Security=True";
        public HighScores() 
        {
            //conn = new SqlConnection();
            //conn.ConnectionString = Path.Combine("Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename=", Directory.GetCurrentDirectory(), "HighScoreDatabase.mdf", ";Integrated Security = True");
            //conn.ConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\Bobon\\source\\repos\\SnakeProjekt\\SnakeProjekt\\HighScoreDatabase.mdf;Integrated Security=True";
            //conn.Open();
        }

        public string readDatabase()
        {

            string output = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand command = new SqlCommand("SELECT * FROM HighScoreTable", conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            output += reader[0].ToString();
                            output += reader[1].ToString();
                            output += reader[2].ToString() + "\n";
                        }
                    }
                }
                conn.Close();
            }

            return output;
        }

        public static void addHighScore(GameMap gameMap, int newHighScore, string name)
        {


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string map = gameMap.ToString();
                string query = "INSERT INTO HighScoreTable (Name, Score, Level, Map) VALUES ((@name), (@newHighScore), (@level), (@map))";

                using (SqlCommand command4 = new SqlCommand(query, conn))
                {
                    command4.Parameters.AddWithValue("@name", name);
                    command4.Parameters.AddWithValue("newHighScore", newHighScore);
                    command4.Parameters.AddWithValue("@level", GameProperties.level);
                    command4.Parameters.AddWithValue("@map", GameProperties.gameMapSelected.ToString());

                    command4.ExecuteNonQuery();
                }
            }
        }   

        public static DataTable generateHighScoreTable(GameMap gameMap, bool isNewHighScore)
        {
            DataTable dataTable = new DataTable();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string map = gameMap.ToString();
                string query = "Select TOP 5 Name, Score FROM HighScoreTable WHERE Map = @map ORDER BY Score DESC";

                using (SqlCommand command3 = new SqlCommand(query, conn))
                {
                    command3.Parameters.AddWithValue("@map", map);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command3))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                return dataTable;
            }
        }


        public static List<int> getHighScores(GameMap gameMap) //on level and map?
        {

            int highscore = 0;
            List<int> topValues = new List<int>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string map = gameMap.ToString();
                string query = "Select TOP 5 Score FROM HighScoreTable WHERE Map = @map ORDER BY Score DESC";

                using (SqlCommand command2 = new SqlCommand(query, conn))
                {
                    command2.Parameters.AddWithValue("@map", map);

                    using (SqlDataReader reader2 = command2.ExecuteReader())
                    {
                        while (reader2.Read())
                        {
                            //object result = command2.ExecuteScalar();
                            object result = reader2["Score"];
                            highscore = result == DBNull.Value ? 0 : Convert.ToInt32(result);
                            topValues.Add(highscore);
                        }

                    }

                    //fill missing values with 0s
                    while (topValues.Count < 5)
                    {
                        topValues.Add(0);
                    }

                    conn.Close();
                }


                return topValues;
            }
        }
    }
}

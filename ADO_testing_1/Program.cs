using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using TagLib;
using System.Data.SqlClient;
using System.Data;

namespace ADO_testing_1
{
    class Program
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        static SqlConnection connection = new SqlConnection(connectionString);

        static void Main(string[] args)
        {
            //InsertArtist();
            //GetArtistsWithCountries();
            //GetCountries();
            //GetConnectionInfo();
            //string directory = Console.ReadLine();
            string directory = @"C:\Mus";

            var testAu = new AudioFilesList(directory);

            AudioFile Au1 = testAu.Tracks[0];
            
            Console.WriteLine(Au1.Year);

            Console.Read();
        }







        public static void GetConnectionInfo()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Подключение открыто");

                // Вывод информации о подключении
                Console.WriteLine("Свойства подключения:");
                Console.WriteLine("\tСтрока подключения: {0}", connection.ConnectionString);
                Console.WriteLine("\tБаза данных: {0}", connection.Database);
                Console.WriteLine("\tСервер: {0}", connection.DataSource);
                Console.WriteLine("\tВерсия сервера: {0}", connection.ServerVersion);
                Console.WriteLine("\tСостояние: {0}", connection.State);
                Console.WriteLine("\tWorkstationld: {0}", connection.WorkstationId);
            }

            Console.WriteLine("Подключение закрыто... Нажать Enter");

            Console.Read();
        }


        public static void InsertArtist()
        {
            Console.Write("Введите имя исполнителя:");
            string name = Console.ReadLine();

            Console.Write("Введите страну исполнителя на английском:");
            string country = Console.ReadLine();

            if (AddArtist(name, country))
            {
                Console.WriteLine("Исполнитель добавлен:");
                GetArtistsWithCountries();
            }
            else
            {
                Console.WriteLine("Ничего не добавлено. Выберите страну из списка:");
                Console.WriteLine();
                GetCountries();
                Console.WriteLine();
                InsertArtist();
            }
            Console.Read();
        }

        // добавление исполнителя
        private static bool AddArtist(string name, string country)
        {
            // название процедуры
            string sqlExpression = "sp_InsertArtist";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = name
                };
                // добавляем параметр
                command.Parameters.Add(nameParam);
                // параметр для ввода страны
                SqlParameter countryParam = new SqlParameter
                {
                    ParameterName = "@Country",
                    Value = country
                };
                command.Parameters.Add(countryParam);

                if (command.ExecuteNonQuery() <= 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }


        private static void GetArtistsWithCountries()
        {
            // название процедуры
            string sqlExpression = "sp_GetArtistsWithCountries";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}\t{1} \t\t\t{2}", reader.GetName(0), reader.GetName(1), reader.GetName(2));
                    Console.WriteLine("-----------------------------------------");
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string artist = reader.GetString(1);
                        string country = reader.GetString(2);
                        Console.WriteLine("{0}\t{1} \t\t{2}", id, artist, country);
                    }
                }
                reader.Close();
            }
        }

        public static void GetCountries()
        {
            // название процедуры
            string sqlExpression = "sp_GetCountries";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = CommandType.StoredProcedure;
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    Console.WriteLine("{0}", reader.GetName(1));
                    Console.WriteLine("-----------------------------------------");
                    while (reader.Read())
                    {
                        string country = reader.GetString(1);
                        Console.WriteLine("{0}", country);
                    }
                }
                reader.Close();
            }
        }
    }
}

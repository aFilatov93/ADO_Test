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
            bool switcher = true;

            DBManipulation.GetConnectionInfo();
            /*
            while (switcher)
            {
                string directory = Console.ReadLine();

                try
                {
                    AudioFilesList testAu = new AudioFilesList(directory);
                    if (testAu.Tracks.Count > 0)
                    {
                        testAu.ShowTags();
                        //switcher = false;
                    }
                    else Console.WriteLine("Folders doesn't contain .mp3 files");
                }
                catch (NullReferenceException ex)
                {
                    ex = new NullReferenceException();
                    Console.WriteLine("Null Reference");
                }
            }
            */
            Console.Read();
        }

    }
}

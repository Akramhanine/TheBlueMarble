using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using BlueMarble.Data;

namespace BlueMarble.ImportUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            string csvFile = Directory.GetCurrentDirectory() + @"\Images.csv";

            Console.WriteLine("CSV File: {0}", csvFile);

            StreamReader reader = new StreamReader(csvFile);
            try
            {
                //don't need this
                string header = reader.ReadLine();
                Regex missionRx = new Regex(@"^([A-Z]+)([0-9]+\w*)");

                do
                {
                    string nextLine = reader.ReadLine();
                    string[] tokens = nextLine.Split(',');

                    if (tokens.Length != 12)
                    {
                        throw new Exception("NOT ENOUGH TOKENZ");
                    }

                    string missionDesc = tokens[0];

                    MatchCollection missionData = missionRx.Matches(missionDesc);
                    Match missionMatch = missionData[0];

                    if (missionMatch.Groups.Count != 3)
                    {
                        throw new Exception("Unable to parse mission data.");
                    }

                    Group datasetAcronym = missionMatch.Groups[1];
                    Console.WriteLine("Dataset Acronym: {0}", datasetAcronym);

                    MarbleDataBase database = new MarbleDataBase();

                    BlueMarble.Data.ImageDataset dataSet = new BlueMarble.Data.ImageDataset
                    {
                        Source = 1,
                        Description = datasetAcronym.Value
                    };

                   // database.Dataset.Add(dataSet);
                    database.SaveChanges();

                    /*var datasets = from d in database.ImageDataset select d;
                    foreach (Dataset d in datasets)
                    {
                        Console.WriteLine("{0} {1} {2}", d.DatasetID, d.Source, d.Description);
                    }*/

                } while (false);// (reader.Peek() >= 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex);
            }
            finally
            {
                reader.Close();
            }



            Console.ReadLine();
        }
    }
}

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
        static Dictionary<string, int> _datasetSource = new Dictionary<string, int>();

        static Regex missionRx = new Regex(@"^([A-Z]+)([0-9]+\w*)");

        static int ticker = 0;

        static int maxEntries = 0;

        static void Main(string[] args)
        {
            string csvFile = Directory.GetCurrentDirectory() + @"\Images.csv";

            Console.WriteLine("CSV File: {0}", csvFile);

            StreamReader reader = new StreamReader(csvFile);
            try
            {
                //don't need this
                string header = reader.ReadLine();

                _datasetSource.Add("AS", 1);

                using (MarbleDataBase database = new MarbleDataBase())
                {
                    do
                    {
                        string nextLine = reader.ReadLine();
                        string[] tokens = nextLine.Split(',');

                        if (tokens.Length != 12)
                        {
                            throw new Exception("NOT ENOUGH TOKENZ");
                        }

                        string missionDesc = tokens[0];

                        ImageData imageData = new ImageData
                        {
                            Rollnum = int.Parse(tokens[1]),
                            Framenum = int.Parse(tokens[2]),
                            Width = int.Parse(tokens[3]),
                            Height = int.Parse(tokens[4]),
                            Filesize = int.Parse(tokens[5]),
                            //CloudCoveragePercentage = int.Parse(tokens[6]),
                            Latitude = double.Parse(tokens[7]),
                            Longitude = double.Parse(tokens[8]),
                            //tokens[9]
                            //tokens[10]
                            Lowresurl = tokens[11]
                        };

                        imageData.DatasetID = GetDataSourceID(database, missionDesc);

                        int cloudCoverage = 0;
                        int.TryParse(tokens[6], out cloudCoverage);
                        imageData.CloudCoveragePercentage = cloudCoverage;

                        imageData.Highresurl = imageData.Lowresurl.Replace("lowres", "highres");

                        database.Imagedata.Add(imageData);

                        ticker++;
                        maxEntries++;

                        if (ticker > 100)
                        {
                            database.SaveChanges();
                            Console.WriteLine("{0} {1}", imageData.ImageDataID, imageData.Lowresurl);
                            ticker = 0;
                        }
                    } while (reader.Peek() >= 0 && maxEntries < 300);

                    database.SaveChanges();
                }
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

        static int GetDataSourceID(MarbleDataBase database, string missionDesc)
        {
            var missions = from d in database.Dataset
                           where d.Description == missionDesc
                           select d;

            int datasetId = 0;
            foreach (Dataset ds in missions)
            {
                datasetId = ds.DatasetID;
                break;
            }

            if (datasetId > 0)
            {
                return datasetId;
            }
            else
            {
                //must create a new dataset
                MatchCollection missionData = missionRx.Matches(missionDesc);
                Match missionMatch = missionData[0];

                if (missionMatch.Groups.Count != 3)
                {
                    throw new Exception("Unable to parse mission data.");
                }

                string datasetAcronym = missionMatch.Groups[1].Value;

                int datasetSourceId = 0;
                if (!_datasetSource.ContainsKey(datasetAcronym))
                {
                    datasetSourceId = _datasetSource.Count;

                    //add a source
                    _datasetSource.Add(datasetAcronym, datasetSourceId);
                }
                else
                {
                    datasetSourceId = _datasetSource[datasetAcronym];
                }

                Dataset dataSet = new Dataset
                {
                    Source = datasetSourceId,
                    Description = datasetAcronym
                };

                database.Dataset.Add(dataSet);
                database.SaveChanges();

                return dataSet.DatasetID;
            }
        }
    }
}

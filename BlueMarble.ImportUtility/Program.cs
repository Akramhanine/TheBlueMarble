using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;

using BlueMarble.Data;
using Microsoft.Samples.EntityDataReader;

namespace BlueMarble.ImportUtility
{
    class Program
    {
        enum ProcessorType
        {
            DataSet,
            ImageData,
            Location,
            Feature,
            ImageXFeature
        };

        static ProcessorType ProcessingType = ProcessorType.DataSet;

        static void Main(string[] args)
        {
            string csvFile = Directory.GetCurrentDirectory() + @"\images.csv";

            Console.WriteLine("CSV File: {0}", csvFile);
            List<ImageData> images = new List<ImageData>();
            DatasetImporter dataSetImporter = new DatasetImporter();
            ImageDataImporter imageDataImporter = new ImageDataImporter();
            LocationImporter locationImporter = new LocationImporter();
            FeatureImporter featureImporter = new FeatureImporter();
            ImageXFeatureImporter imagexFeatureImporter = new ImageXFeatureImporter();

            STARTPROCESSING:
            StreamReader reader = new StreamReader(csvFile);
            MarbleDataBase database = new MarbleDataBase();
            try
            {
                //don't need this line
                string header = reader.ReadLine();
                
                //this speeds it up
                database.Configuration.AutoDetectChangesEnabled = false;

                //init processor
                switch (ProcessingType)
                {
                    case ProcessorType.DataSet:
                        Console.WriteLine("Data Set Init");
                        dataSetImporter.Init(database);
                        break;
                    case ProcessorType.ImageData:
                        Console.WriteLine("Image Data Init");
                        imageDataImporter.Init(database);
                        break;
                    case ProcessorType.Location:
                        Console.WriteLine("Location Init");
                        locationImporter.Init(database);
                        break;
                    case ProcessorType.Feature:
                        Console.WriteLine("Feature Init");
                        featureImporter.Init(database);
                        break;
                    case ProcessorType.ImageXFeature:
                        Console.WriteLine("Imagexfeature Init");
                        imagexFeatureImporter.Init(database);
                        break;
                }

                do
                {
                    string nextLine = reader.ReadLine();
                    string[] tokens = nextLine.Split(',');

                    if (tokens.Length != 12)
                    {
                        throw new Exception("NOT ENOUGH TOKENS");
                    }
                    string missionDesc = tokens[0];

                    //process data record
                    switch (ProcessingType)
                    {
                        case ProcessorType.DataSet:
                            dataSetImporter.ProcessRecord(tokens);
                            break;
                        case ProcessorType.ImageData:
                            imageDataImporter.ProcessRecord(tokens);
                            break;
                        case ProcessorType.Location:
                            locationImporter.ProcessRecord(tokens);
                            break;
                        case ProcessorType.Feature:
                            featureImporter.ProcessRecord(tokens);
                            break;
                        case ProcessorType.ImageXFeature:
                            imagexFeatureImporter.ProcessRecord(tokens);
                            break;
                    }
                } while (reader.Peek() >= 0);                
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR");
                Console.WriteLine(ex);
            }
            finally
            {
                reader.Close();

                //finalize data record
                switch (ProcessingType)
                {
                    case ProcessorType.DataSet:
                        Console.WriteLine("Close up dataset");
                        dataSetImporter.CloseUp();
                        break;
                    case ProcessorType.ImageData:
                        Console.WriteLine("Close up image");
                        imageDataImporter.CloseUp();
                        break;
                    case ProcessorType.Location:
                        Console.WriteLine("Close up locations");
                        locationImporter.CloseUp();
                        break;
                    case ProcessorType.Feature:
                        Console.WriteLine("Close up features");
                        featureImporter.CloseUp();
                        break;
                    case ProcessorType.ImageXFeature:
                        Console.WriteLine("Close up imagexfeature");
                        imagexFeatureImporter.CloseUp();
                        break;
                }

                database.Dispose();
            }

            if (ProcessingType == ProcessorType.DataSet)
            {
                ProcessingType = ProcessorType.ImageData;
                goto STARTPROCESSING;
            }
			if (ProcessingType == ProcessorType.ImageData)
			{
				ProcessingType = ProcessorType.Location;
				goto STARTPROCESSING;
			}
			if (ProcessingType == ProcessorType.Location)
			{
				ProcessingType = ProcessorType.Feature;
				goto STARTPROCESSING;
			}
			if (ProcessingType == ProcessorType.Feature)
			{
				ProcessingType = ProcessorType.ImageXFeature;
				goto STARTPROCESSING;
			}

            Console.ReadLine();
        }
    }
}

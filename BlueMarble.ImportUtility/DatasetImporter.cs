using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using BlueMarble.Data;

namespace BlueMarble.ImportUtility
{
    public class DatasetImporter : ImporterBase
    {
        static Regex missionRx = new Regex(@"^([A-Z]+)([0-9]+\w*)");

        List<Dataset> datasets = new List<Dataset>();
        Dictionary<string, int> _datasetSource = new Dictionary<string, int>();

        public override void ProcessRecord(string[] tokens)
        {
            string missionDesc = tokens[0];

            if (!datasets.Exists(d => d.Description == missionDesc))
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
                    Description = missionDesc
                };

                datasets.Add(dataSet);
                _database.Dataset.Add(dataSet);
            }
        }
    }
}

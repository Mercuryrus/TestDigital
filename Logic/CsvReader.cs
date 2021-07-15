using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDigital.Enums;
using TestDigital.Models;

namespace TestDigital.Logic
{
    class CsvReader
    {
        public List<CsvReadModel> ReadCsv(string path)
        {

            List<string> files = new List<string>();
            foreach (string i in Directory.GetFiles(path, "*.csv"))
            {
                files.Add(i);
            }
            var dir = Directory.GetFiles(path, "*.csv").Select(x => Path.GetFileNameWithoutExtension(x)).ToArray();
            List<string> filepath = new List<string>();
            
            for (int i = 0; i < files.Count-1; i++)
            {
                if (dir[i].All(char.IsDigit))
                {
                    if ((int.Parse(dir[i]) != i + 1) && (int.Parse(dir[i]) < 100))
                        break;
                    else
                    {
                        filepath.Add(files[i]);
                    }
                }
            }
            

            List<CsvReadModel> csvs = new List<CsvReadModel>();
            
            foreach (var i in filepath)
            {
                var lines = File.ReadAllLines(i);
                CsvReadModel csvReadModel = GetCsvReadModel(lines);
                csvs.Add(csvReadModel);
            }
            
            return csvs;
        }

        private CsvReadModel GetCsvReadModel(string[] lines)
        {
            CsvReadModel csvReadModel = new CsvReadModel();
            csvReadModel.Value = new List<List<Score>>();

            foreach (var line in lines)
            {
                List<Score> row = line.Split(";").Select(x => csvReadModel.dicTranslate[x]).ToList();
                csvReadModel.Value.Add(row);
            }

            csvReadModel.Row = csvReadModel.Value.Count;
            csvReadModel.Col = csvReadModel.Value.Max(x => x.Count);

            return csvReadModel;
        }
    }
}
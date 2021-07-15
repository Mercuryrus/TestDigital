using System.Collections.Generic;
using System.Linq;
using TestDigital.Enums;
using TestDigital.Models;

namespace TestDigital.Logic
{
    public class CsvMath
    {
        public List<List<int>> GetMainCsv(List<CsvReadModel> csvReadModels)
        {
            List<List<int>> result = new List<List<int>>();
            
            int maxRow = csvReadModels.Max(x => x.Row);
            int maxCol = csvReadModels.Max(x => x.Col);

            result = GetNormalizeCsv(csvReadModels.First().Value, maxRow, maxCol);
            int currentCoef = 1;

            for (int i = 1; i < csvReadModels.Count; i++)
            {
                List<List<int>> normalizeCsv = GetNormalizeCsv(csvReadModels[i].Value, maxRow, maxCol);
                result = GetSum(result, normalizeCsv, currentCoef);
                currentCoef *= -1;
            }

            return result;
        }

        private List<List<int>> GetNormalizeCsv(List<List<Score>> csvTable, int maxRow, int maxCol)
        {
            List<List<int>> normalizeCsv = new List<List<int>>();

            for (int i = 0; i < maxRow; i++)
            {
                List<int> normalizeRow = new List<int>();
                if (i >= csvTable.Count)
                {
                    normalizeRow = GetZeroRow(maxCol);
                    normalizeCsv.Add(normalizeRow);
                    continue;
                }

                normalizeRow = GetNormalizeRow(csvTable[i], maxCol);
                normalizeCsv.Add(normalizeRow);
            }

            return normalizeCsv;
        }

        private List<int> GetZeroRow(int maxCol)
        {
            List<int> zeroRow = new List<int>();

            for (int i = 0; i < maxCol; i++)
                zeroRow.Add((int)Score.Zero);

            return zeroRow;
        }

        private List<int> GetNormalizeRow(List<Score> csvRow, int maxCol)
        {
            List<int> normalizeRow = new List<int>();

            for (int j = 0; j < maxCol; j++)
            {
                if (j >= csvRow.Count)
                {
                    normalizeRow.Add((int)Score.Zero);
                    continue;
                }

                normalizeRow.Add((int)csvRow[j]);
            }

            return normalizeRow;
        }

        private List<List<int>> GetSum(List<List<int>> currentRes, List<List<int>> csv, int currentCoef)
        {
            List<List<int>> sumRes = new List<List<int>>();

            for (int i = 0; i < currentRes.Count; i++)
            {
                List<int> row = new List<int>();

                for (int j = 0; j < currentRes[i].Count; j++)
                {
                    int result = currentRes[i][j] + (currentCoef * csv[i][j]);
                    row.Add(result);
                }

                sumRes.Add(row);    
            }

            return sumRes;
        }
    }
}

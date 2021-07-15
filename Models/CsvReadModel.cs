using System.Collections.Generic;
using TestDigital.Enums;

namespace TestDigital.Models
{
    public class CsvReadModel
    {
        public List<List<Score>> Value { get; set; }
        public int Col { get; set; }
        public int Row { get; set; }

        public Dictionary<string, Score> dicTranslate { get; set; }
        
        public CsvReadModel()
        {
            dicTranslate = new Dictionary<string, Score>()
            {
                { "",       Score.Zero },
                { "red",    Score.Red },
                { "green",  Score.Green },
                { "black",  Score.Black },
                { "white",  Score.White },
                { "blue",   Score.Blue },
            };
        }
    }
}
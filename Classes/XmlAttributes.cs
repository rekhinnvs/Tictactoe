using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Classes
{
    public class XmlAttributes
    {
        public int StepID { get; set; }
        public string StepTime { get; set; }

        public string PlayerID { get; set; }
        public string PlayerType { get; set; }

        public string PlaySign { get; set; }
        public string PlayLocation { get; set; }

        public string FileName { get; set; }
    }
}

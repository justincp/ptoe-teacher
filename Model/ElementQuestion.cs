using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTOEQuiz
{ 
    public class ElementQuestion
    {
        private string symbol;
        private string name;
        private string response="";

        public string Name { get => name; set => name = value; }
        public string Symbol { get => symbol; set => symbol = value; }
        public string Response { get => response; set => response = value; }
    }
}


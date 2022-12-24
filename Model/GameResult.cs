using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PTOEQuiz
{
    public class GameResult
    {
        [Key]
        public int GameId { get; set; }

        public string Name { get; set; }

        public bool GameMode { get; set; }

        public int Mastered {get; set;}

        public DateTime CreatedTime { get; set; }

        public string IPaddress { get; set; }

    }
}

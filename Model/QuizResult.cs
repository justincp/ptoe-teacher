using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PTOEQuiz
{
    public class QuizResult
    {
        [Key]
        public int QuizId { get; set; }

        public string Name { get; set; }

        public bool QuizMode { get; set; }

        public int Mastered {get; set;}

        public DateTime CreatedTime { get; set; }

        public string IPaddress { get; set; }

    }
}

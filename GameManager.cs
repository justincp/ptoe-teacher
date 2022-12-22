using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace BlazingPizza
{
    public class GameManager
    {

        private List<ElementQuestion> allQuestions;
        private List<ElementQuestion> currentQuestions;
        private List<ElementQuestion> masteredQuestions;
        private ElementQuestion question;

        public int WORKING_POOL_SIZE = 4;
        public int QUIZ_MINUTES = 1;
        public bool POSITIVE_MODE = false;
        private static readonly string[] CORRECT_POSITIVES = { "Correct,You're smart!", "Correct,You're good!", "Correct,That was really smart!" };
        private static readonly string[] INCORRECT_POSITIVES = { "Incorrect, Keep trying!" };
        private static readonly string[] CORRECT_NEGATIVES = { "Correct, It's about time." };
        private static readonly string[] INCORRECT_NEGATIVES = { "Incorrect,You're dumb!", "Incorrect,You're bad!", "Incorrect Are You Even Trying", "Incorrect,Youre bad at this!" };

        private string strReinforcement;

        public ElementQuestion? Question { get => question; }

        public GameManager()
        {
            // initialize
            allQuestions = new List<ElementQuestion>();
            currentQuestions = new List<ElementQuestion>();
            masteredQuestions = new List<ElementQuestion>();

            //import elements
            using (System.IO.StreamReader reader = new System.IO.StreamReader(@"Resources\elements.csv"))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    //Define pattern
                    Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");

                    //Separating columns to array
                    string[] X = CSVParser.Split(line);

                    //Debug.Log(X[0]);
                    ElementQuestion eq = new()
                    {
                        Name = X[0],
                        Symbol = X[1]
                    };
                    allQuestions.Add(eq);

                }
            }

            // set the reinforcement mode
            POSITIVE_MODE = true; // Settings1.Default.PositiveMode;
            QUIZ_MINUTES = 1; //  Settings1.Default.QuizMinutes;
            WORKING_POOL_SIZE = 4; // Settings1.Default.WorkingPoolSize;

            // load up the current pool
            FillupCurrentQuestions();

        }

        public int TotalMastered()
        {
            return masteredQuestions.Count;
        }

        public void NextQuestion()
        {
            // get our next question
            int randomQuestionIndex = new Random().Next(0, currentQuestions.Count);
            question = currentQuestions[randomQuestionIndex];
            this.strReinforcement= "";

        }

        public bool CheckAnswer(string userResponse)
        {
            bool correct;

            correct = string.Equals(userResponse, Question.Name, StringComparison.OrdinalIgnoreCase);

            if (correct)
            {
                // move the element to mastered
                masteredQuestions.Add(Question);
                currentQuestions.Remove(Question);
                FillupCurrentQuestions();

                if (POSITIVE_MODE)
                {
                    strReinforcement = CORRECT_POSITIVES[new Random().Next(0, GameManager.CORRECT_POSITIVES.Length)];
                }
                else
                {
                    strReinforcement = CORRECT_NEGATIVES[new Random().Next(0, GameManager.CORRECT_NEGATIVES.Length)];
                }
            }
            else
            {
                if (POSITIVE_MODE)
                {
                    strReinforcement = INCORRECT_POSITIVES[new System.Random().Next(0, GameManager.INCORRECT_POSITIVES.Length)];
                }
                else
                {
                    strReinforcement = INCORRECT_NEGATIVES[new Random().Next(0, GameManager.INCORRECT_NEGATIVES.Length)];
                }
            }

            return correct;
        }

        public string Reinforcment()
        {
            return strReinforcement;
        }

        private void FillupCurrentQuestions()
        {
            while (currentQuestions.Count < WORKING_POOL_SIZE)
            {
                if (allQuestions.Count == 0) break; // we're out of questions!

                int randomQuestionIndex = 0;
                //int randomQuestionIndex = new Random().Next(0, allQuestions.Count);
                currentQuestions.Add(allQuestions[randomQuestionIndex]);
                allQuestions.Remove(allQuestions[randomQuestionIndex]);
            }
        }

    }

}
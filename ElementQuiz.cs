﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace PTOEQuiz
{
    public class ElementQuiz
    {

        private List<ElementQuestion> allQuestions;
        private List<ElementQuestion> currentQuestions;
        private List<ElementQuestion> masteredQuestions;
        private ElementQuestion _question;

        private int WORKING_POOL_SIZE = 7;
        public int QUIZ_SECONDS = 60;
        public bool POSITIVE_MODE = false;
        private static readonly string[] CORRECT_POSITIVES = { "Correct, good choice!", "Correct, you're good!", "Correct, that was really smart!","Perfect!","Correct, you're amazing!" };
        private static readonly string[] INCORRECT_POSITIVES = { "Incorrect, keep trying!","Incorrect, you'll get it next time.", "Incorrect, you're getting there.", "Incorrect, you can do it." };
        private static readonly string[] CORRECT_NEGATIVES = { "Correct, it's about time.", "Finally.", "Yep.", "Good enough." };
        private static readonly string[] INCORRECT_NEGATIVES = { "Incorrect, that was bad!", "Incorrect, poor choice!", "Wrong, are you even trying?", "Incorrect, dumb answer!", "No! No! No!", "Not good at all.", "No, are you ever going to get this?" };
    
        private string strReinforcement = "";

        public ElementQuestion Question { get => _question; }

        public ElementQuiz()
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
            POSITIVE_MODE = (new Random().Next(0, 2)) > 0;  // Settings1.Default.PositiveMode;
            //QUIZ_MINUTES = Settings1.Default.QuizMinutes;
            //WORKING_POOL_SIZE = Settings1.Default.WorkingPoolSize;

            // load up the current pool
            FillupCurrentQuestions();

            // start with the current question
            NextQuestion();

        }

        public int TotalMastered()
        {
            if (masteredQuestions == null)
            {
                return 0;
            }
            else
            {
                return masteredQuestions.Count;
            }
        }

        public void NextQuestion()
        {
            if (currentQuestions.Count > 0)
            {
                // get our next question
                int randomQuestionIndex = new Random().Next(0, currentQuestions.Count);
                _question = currentQuestions[randomQuestionIndex];
                this.strReinforcement = "";
            }
            else
            {
                // no questions left
                _question = null;
            }
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
                    strReinforcement = CORRECT_POSITIVES[new Random().Next(0, ElementQuiz.CORRECT_POSITIVES.Length)];
                }
                else
                {
                    strReinforcement = CORRECT_NEGATIVES[new Random().Next(0, ElementQuiz.CORRECT_NEGATIVES.Length)];
                }
            }
            else
            {
                if (POSITIVE_MODE)
                {
                    strReinforcement = INCORRECT_POSITIVES[new System.Random().Next(0, ElementQuiz.INCORRECT_POSITIVES.Length)];
                }
                else
                {
                    strReinforcement = INCORRECT_NEGATIVES[new Random().Next(0, ElementQuiz.INCORRECT_NEGATIVES.Length)];
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
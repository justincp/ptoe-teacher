using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PTOEQuiz.Services
{
    public class GameState
    {
        public bool ShowingConfigureDialog { get; private set; }

        public bool ShowingResponseDialog { get; private set; }

        public GameManager game { get; set; }

        public ElementQuestion Question;

        public string Response;

        public bool gameFinished;


        public void ShowQuizDialog()
        {

            ShowingConfigureDialog = true;
        }

        public void CancelConfigurePizzaDialog()
        {


            ShowingConfigureDialog = false;
        }

        public void ConfirmConfigurePizzaDialog()
        {


            ShowingConfigureDialog = false;
        }

        public void CheckAnswerDialog()
        {
            game.NextQuestion();
            Question = game.Question;
            ShowingConfigureDialog = false;
            //ShowingResponseDialog = true;

        }

        public void OnKeyDown()
        {
            game.CheckAnswer(game.Question.Response); // TODO: could just be private variable

            //ShowingConfigureDialog = false;
            //ShowingResponseDialog = true;
        }


        public async void GameOver()
        {
            ShowingConfigureDialog = false;
            Debug.WriteLine(game.TotalMastered());
            gameFinished = true;
        }


    }
}

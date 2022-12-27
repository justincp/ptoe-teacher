using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace PTOEQuiz.Services
{
    public class QuizState
    {
        public bool ShowingConfigureDialog { get; private set; }

        public ElementQuiz Quiz { get; set; }

        public ElementQuestion Question { get; set; }

        public bool QuizFinished { get; set; }


        public void ShowQuizDialog()
        {
            ShowingConfigureDialog = true;
        }

        public async void QuizOver()
        {
            ShowingConfigureDialog = false;
            Debug.WriteLine(Quiz.TotalMastered());
            QuizFinished = true;

            await Task.Yield();
        }


    }
}

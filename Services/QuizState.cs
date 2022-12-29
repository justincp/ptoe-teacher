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

        internal void ShowQuizDialog()
        {
            ShowingConfigureDialog = true;
        }

        internal async void QuizOver()
        {
            ShowingConfigureDialog = false;
            
            QuizFinished = true;

            await Task.Yield();
        }


    }
}

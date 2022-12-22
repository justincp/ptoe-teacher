using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazingPizza
{
    public class OrderState
    {
        public bool ShowingConfigureDialog { get; private set; }

        public bool ShowingResponseDialog { get; private set; }

        public GameManager game { get; set; }

        public Pizza ConfiguringPizza { get; private set; }
        public Order Order { get; private set; } = new Order();

        public ElementQuestion Question;

        public string Response;

        public bool gameFinished;

        public void ShowConfigurePizzaDialog(PizzaSpecial special)
        {
            ConfiguringPizza = new Pizza()
            {
                Special = special,
                SpecialId = special.Id,
                Size = Pizza.DefaultSize,
                Toppings = new List<PizzaTopping>(),
            };

            ShowingConfigureDialog = true;
        }

        public void ShowQuizDialog()
        {

            ShowingConfigureDialog= true;
        }

        public void CancelConfigurePizzaDialog()
        {
            ConfiguringPizza = null;

            ShowingConfigureDialog = false;
        }

        public void ConfirmConfigurePizzaDialog()
        {
            Order.Pizzas.Add(ConfiguringPizza);
            ConfiguringPizza = null;

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

        public void RemoveConfiguredPizza(Pizza pizza)
        {
            Order.Pizzas.Remove(pizza);
        }
        
        public void ResetOrder()
        {
            Order = new Order();
        }

        public async void GameOver()
        {
            ShowingConfigureDialog = false;
            Debug.WriteLine(game.TotalMastered());
            gameFinished = true;
        }


    }
}

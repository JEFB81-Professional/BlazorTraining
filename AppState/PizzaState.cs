namespace LearnBlazor.AppState
{
    /// <summary>
    /// State container for pizza-related state
    /// </summary>
    public class PizzaState
    {

        public int PizzasSoldToday { get; private set; }

        public void SellPizza()
        {
            PizzasSoldToday++;
        }
    }
}
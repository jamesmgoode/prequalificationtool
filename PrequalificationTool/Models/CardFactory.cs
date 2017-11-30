namespace PrequalificationTool.Models
{
    public static class CardFactory
    {
        public static Card BarclayCard()
        {
            return new Card
            {
                CardName = "Barclaycard Credit Card",
                Apr = 15.5,
                PromoMessage = "Barclaycard credit cards are good!"
            };
        }

        public static Card VanquisCard()
        {
            return new Card
            {
                CardName = "Vanquis Card",
                Apr = 20.5,
                PromoMessage = "Vanquis is also fine."
            };
        }
    }
}

namespace PrequalificationTool.Models
{
    public static class CardFactory
    {
        public static Card BarclayCard()
        {
            return new Card
            {
                CardName = "Barclaycard",
                Apr = 15.5,
                PromoMessage = "Yay barclaycard isn't that bad!"
            };
        }

        public static Card VanquisCard()
        {
            return new Card
            {
                CardName = "Vanquis",
                Apr = 20.1,
                PromoMessage = "When you're too poor for a real bank."
            };
        }
    }
}

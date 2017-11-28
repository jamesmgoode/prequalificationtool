using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrequalificationTool.Models
{
    public class Card
    {
        public string CardName { get; set; }
        public double Apr { get; set; }
        public string PromoMessage { get; set; }

        public Card(string cardName, double apr, string promoMessage)
        {
            CardName = cardName;
            Apr = apr;
            PromoMessage = promoMessage;
        }
    }
}

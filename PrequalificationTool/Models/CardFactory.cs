using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrequalificationTool.Models
{
    public static class CardFactory
    {
        public static Card BarclayCard()
        {
            return new Card("Barclaycard", 15.5, "Yay barclaycard isn't that bad!");
        }

        public static Card VanquisCard()
        {
            return new Card("Vanquis", 20.1, "When you're too poor for a real bank.");
        }
    }
}

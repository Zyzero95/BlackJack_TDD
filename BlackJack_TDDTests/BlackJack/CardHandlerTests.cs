using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack_TDD;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDDTests.BlackJack
{
    [TestClass]
    public class CardHandlerTests
    {
        [TestMethod()]
        public void DarCardTestItem1()
        {
            CardsHandler.CreateDeck();
            var c = CardsHandler.DrawCard();
            Assert.IsNotNull(c, c.Value.ToString());
        }
        [TestMethod()]
        public void DarCardTestItem2()
        {
            CardsHandler.CreateDeck();
            var c = CardsHandler.DrawCard();
            Assert.IsNotNull(c, c.Suit.ToString());
        }
    }
}

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
        public void darCardTestItem1()
        {
            CardsHandler.CreateDeck();
            var c = CardsHandler.DrawCard();
            Assert.Fail(c.Item1);
        }
        [TestMethod()]
        public void darCardTestItem2()
        {
            CardsHandler.CreateDeck();
            var c = CardsHandler.DrawCard();
            Assert.Fail(c.Item2);
        }
    }
}

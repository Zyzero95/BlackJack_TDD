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
            var deck = new CardsHandler();
            var c = deck.DrawCard();
            Assert.IsNotNull(c, c.Value.ToString());
        }
        [TestMethod()]
        public void DarCardTestItem2()
        {
            var deck = new CardsHandler();
            var c = deck.DrawCard();
            Assert.IsNotNull(c, c.Suit.ToString());
        }
        [TestMethod()]
        public void ShuffleDeck()
        {
            var deck = new CardsHandler();
            string stack = string.Join(".",deck.SetOfCards);
            string list = string.Join(".", deck.cards);
            Assert.AreEqual(stack, list);
        }
    }
}

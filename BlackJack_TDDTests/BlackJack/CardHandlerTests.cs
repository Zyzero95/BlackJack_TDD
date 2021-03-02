using BlackJack_TDD;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var stack = string.Join(".", deck.SetOfCards);
            var list = string.Join(".", deck.cards);
            Assert.AreNotEqual(stack, list);
        }
    }
}
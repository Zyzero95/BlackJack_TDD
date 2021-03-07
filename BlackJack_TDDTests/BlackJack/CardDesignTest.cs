using BlackJack_TDD;
using BlackJack_TDD.BlackJack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackJack_TDDTests.BlackJack
{
    [TestClass]
    public class CardDesignTest
    {
        [TestMethod()]
        public void DesignTest()
        {
            var cardDesign = new CardDesign();
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var card = deck.DrawCard();
            cardDesign.Design(card);

            Assert.IsNotNull(cardDesign);
        }

        [TestMethod()]
        public void FlipCardTest()
        {
            var cardDesign = new CardDesign();
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var card = deck.DrawCard();
            cardDesign.FlipCard(card);

            Assert.IsFalse(card.isVisible);
        }
    }
}

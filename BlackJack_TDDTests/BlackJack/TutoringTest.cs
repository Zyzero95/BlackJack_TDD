using BlackJack_TDD;
using BlackJack_TDD.BlackJack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackJack_TDDTests.BlackJack
{
    [TestClass]
    public class TutoringTest
    {
        [TestMethod()]
        public void StartOfRoundTest()
        {
            var deck = new CardsHandler();
            var dealer = new Dealer(deck);
            Core.CardDeck = deck;
            Core.Players.Add(new Player(deck));
            Core.Dealer = dealer;
            dealer.StartOfRound();
            Tutoring.StartOfRound = true;

            Assert.AreEqual(dealer.Hand.Count, 2);
        }

        [TestMethod()]
        public void CheatTest()
        {

        }
    }
}

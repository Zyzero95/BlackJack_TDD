namespace BlackJack_TDDTests.BlackJack
{
    using BlackJack_TDD;
    using BlackJack_TDD.BlackJack;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SpecaialCaseTest
    {
        [TestMethod]
        public void FiveAceAceKingTest()
        {
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var player = new Player(deck);
            player.Hand.Clear();
            player.Hand.Add(new Card { Suit = Card.CardSuit.Clubs, Value = Card.CardValue.Five });
            player.Hand.Add(new Card { Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Ace });
            player.Hand.Add(new Card { Suit = Card.CardSuit.Clubs, Value = Card.CardValue.Ace });
            player.Hand.Add(new Card { Suit = Card.CardSuit.Hearts, Value = Card.CardValue.King });
            player.Turn("stand");
            //fick 21 en gång...
            Assert.AreEqual(17, player.HandValue);
        }
    }
}
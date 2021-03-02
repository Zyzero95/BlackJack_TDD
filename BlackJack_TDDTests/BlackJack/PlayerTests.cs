using BlackJack_TDD.BlackJack;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackJack_TDD.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void SetBetLessThenMin()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            Assert.IsFalse(player.SetBet(10).Succses);
        }

        [TestMethod()]
        public void SetBetLessMoreThenMax()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            Assert.IsFalse(player.SetBet(2698).Succses);
        }

        [TestMethod()]
        public void SetBetinbetween()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            Assert.IsTrue(player.SetBet(100).Succses);
        }

        [TestMethod()]
        public void SetBetskipTurn()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            Assert.IsTrue(player.SetBet(0).Succses);
            Assert.IsFalse(player.IsPlaying);
        }

        [TestMethod()]
        public void Nochoice()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            var result = player.Turn("ffdsfkfpiskf");
            Assert.AreEqual(false, result.Succses, null, result.Exception);
        }

        [TestMethod()]
        public void Hit()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            player.Hand.Add(deck.DrawCard());
            player.Hand.Add(deck.DrawCard());
            var result = player.Turn("hit");
            Assert.AreEqual(true, result.Succses, null, result.Exception);
        }

        [TestMethod()]
        public void Stand()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            player.Hand.Add(deck.DrawCard());
            player.Hand.Add(deck.DrawCard());
            var result = player.Turn("stand");
            Assert.AreEqual(true, result.Succses, null, result.Exception);
        }

        [TestMethod()]
        public void DoubleWithTooLittleOnSaldo()
        {
            var deck = new CardsHandler();
            var player = new Player(deck, 60);
            player.SetBet(50);
            player.Hand.Add(deck.DrawCard());
            player.Hand.Add(deck.DrawCard());
            var result = player.Turn("Double");
            Assert.AreEqual(false, result.Succses, null, result.Exception);
        }

        [TestMethod()]
        public void DoubleWithTooManyCards()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            player.SetBet(50);
            player.Hand.Add(deck.DrawCard());
            player.Hand.Add(deck.DrawCard());
            player.Hand.Add(deck.DrawCard());
            var result = player.Turn("Double");
            Assert.AreEqual(false, result.Succses, null, result.Exception);
        }

        [TestMethod()]
        public void Doublesuccsesfull()
        {
            var deck = new CardsHandler();
            var dealer = new Dealer(deck);
            var player = new Player(deck);
            Core.Players.Add(player);
            player.SetBet(50);
            dealer.StartOfRound();
            var result = player.Turn("Double");
            Assert.AreEqual(true, result.Succses, null, result.Exception);
            Assert.AreEqual(3, player.Hand.Count, null, result.Exception);
            Assert.AreEqual(false, player.IsPlaying, null, result.Exception);
        }

        [TestMethod()]
        public void SplittooLuitleSaldo()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            Core.Players.Add(player);
            player.Saldo = 60;
            player.SetBet(50);
            player.Hand.Clear();
            player.Hand.Add(new Card { Suit = Card.CardSuit.Clubs, Value = Card.CardValue.Queen });
            player.Hand.Add(new Card { Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Queen });
            var result = player.Turn("split");
            Assert.AreEqual(false, result.Succses, message: result.Exception);
        }

        [TestMethod()]
        public void SplitDiffrentCards()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            Core.Players.Add(player);
            player.SetBet(50);
            player.Hand.Clear();
            player.Hand.Add(new Card { Suit = Card.CardSuit.Clubs, Value = Card.CardValue.Seven });
            player.Hand.Add(new Card { Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Queen });
            var result = player.Turn("split");
            Assert.AreEqual(false, result.Succses, message: result.Exception);
        }

        [TestMethod()]
        public void Splituccsesfull()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            Core.Players.Add(player);
            player.SetBet(50);
            player.Hand.Clear();
            player.Hand.Add(new Card { Suit = Card.CardSuit.Clubs, Value = Card.CardValue.Queen });
            player.Hand.Add(new Card { Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Queen });
            var result = player.Turn("split");
            Assert.AreEqual(true, result.Succses, message: result.Exception);
            Assert.AreEqual(2, player.Hand.Count, message: result.Exception);
            Assert.AreEqual(2, player.Splithand.Count, message: result.Exception);
        }
    }
}
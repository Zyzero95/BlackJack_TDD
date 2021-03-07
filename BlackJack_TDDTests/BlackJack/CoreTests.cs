using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BlackJack_TDD.BlackJack.Tests
{
    [TestClass()]
    public class CoreTests
    {
        [TestMethod()]
        public void CalculatewinblakcJackwin()
        {
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var dealer = new Dealer(deck);
            var player = new Player(deck);
            Core.Players.Add(player);
            Core.Dealer = dealer;
            dealer.HandValue = 21;
            player.HandValue = 21;
            player.Saldo = 100;
            player.SetBet(100);
            Core.Calculatewin();
            Assert.AreEqual(250, player.Saldo);
        }

        [TestMethod()]
        public void CalculatewinNormalWin()
        {
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var dealer = new Dealer(deck);
            var player = new Player(deck);
            Core.Players.Add(player);
            Core.Dealer = dealer;
            dealer.HandValue = 17;
            player.HandValue = 20;
            player.Saldo = 100;
            player.SetBet(100);
            Core.Calculatewin();
            Assert.AreEqual(200, player.Saldo);
        }

        [TestMethod()]
        public void CalculatewindealerBust()
        {
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var dealer = new Dealer(deck);
            var player = new Player(deck);
            Core.Players.Add(player);
            Core.Dealer = dealer;
            dealer.HandValue = 20;
            player.HandValue = 18;
            player.Saldo = 100;
            player.SetBet(100);
            Core.Calculatewin();
            Assert.AreEqual(0, player.Saldo);
        }

        [TestMethod()]
        public void CalculatewinBust()
        {
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var dealer = new Dealer(deck);
            var player = new Player(deck);
            Core.Players.Add(player);
            Core.Dealer = dealer;
            dealer.HandValue = 19;
            player.HandValue = 26;
            player.Saldo = 100;
            player.SetBet(100);
            Core.Calculatewin();
            Assert.AreEqual(0, player.Saldo);
        }

        [TestMethod()]
        public void CalculatewinLost()
        {
            var deck = new CardsHandler();
            Core.CardDeck = deck;
            var dealer = new Dealer(deck);
            var player = new Player(deck);
            Core.Players.Add(player);
            Core.Dealer = dealer;
            dealer.HandValue = 19;
            player.HandValue = 18;
            player.Saldo = 100;
            player.SetBet(100);
            Core.Calculatewin();
            Assert.AreEqual(0, player.Saldo);
        }
    }
}
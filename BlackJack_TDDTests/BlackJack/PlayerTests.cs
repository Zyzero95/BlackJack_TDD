using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack_TDD;
using System;
using System.Collections.Generic;
using System.Text;
using BlackJack_TDD.BlackJack;

namespace BlackJack_TDD.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void Nochoice()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            var result = player.Turn("ffdsfkfpiskf");
            Assert.AreEqual(false, result.Succses,null,result.Exception);
        }
        [TestMethod()]
        public void Hit()
        {
            var deck = new CardsHandler();
            var player = new Player(deck);
            player.Hand.Add(deck.DrawCard());
            player.Hand.Add(deck.DrawCard());
            var result = player.Turn("hit");
            Assert.AreEqual(true, result.Succses,null,result.Exception);
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
            var player = new Player(deck,60);
            player.SetBet(50);
            player.Hand.Add(deck.DrawCard());
            player.Hand.Add(deck.DrawCard());
            var result = player.Turn("Double");
            Assert.AreEqual(false, result.Succses,null, result.Exception);
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
            Assert.AreEqual(true, result.Succses,null,result.Exception);
        }
    }
}
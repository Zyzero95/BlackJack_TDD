using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlackJack_TDD;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void nochoice()
        {
            CardsHandler.CreateDeck();
            var player = new Player();
            player.Cards.Add(CardsHandler.DrawCard());
            player.Cards.Add(CardsHandler.DrawCard());
            player.Turn("hit");
            var result = player.Turn("ffdsfkfpiskf");
            Assert.AreEqual(false, result.Succses,null,result.Exception);
        }
        [TestMethod()]
        public void Hit()
        {
            CardsHandler.CreateDeck();
            var player = new Player();
            player.Cards.Add(CardsHandler.DrawCard());
            player.Cards.Add(CardsHandler.DrawCard());
            player.Turn("hit");
            Assert.AreEqual(true, player.Turn("hit").Succses);
        }
        [TestMethod()]
        public void Stand()
        {
            CardsHandler.CreateDeck();
            var player = new Player();
            player.Cards.Add(CardsHandler.DrawCard());
            player.Cards.Add(CardsHandler.DrawCard());
            player.Turn("hit");
            Assert.AreEqual(true, player.Turn("hit").Succses);
        }
        [TestMethod()]
        public void DoubleWithTooLittleOnSaldo()
        {
            CardsHandler.CreateDeck();
            var player = new Player(60);
            player.setBet(50);
            player.Cards.Add(CardsHandler.DrawCard());
            player.Cards.Add(CardsHandler.DrawCard());
            player.Turn("hit");
            var result = player.Turn("Double");
            Assert.AreEqual(false, result.Succses,null, result.Exception);
        }
        [TestMethod()]
        public void DoubleWithTooManyCards()
        {
            CardsHandler.CreateDeck();
            var player = new Player();
            player.Cards.Add(CardsHandler.DrawCard());
            player.Cards.Add(CardsHandler.DrawCard());
            player.Cards.Add(CardsHandler.DrawCard());
            player.Turn("hit");
            var result = player.Turn("Double");
            Assert.AreEqual(false, result.Succses, null, result.Exception);
        }
        [TestMethod()]
        public void Doublesuccsesfull()
        {
            CardsHandler.CreateDeck();
            var player = new Player();
            player.Cards.Add(CardsHandler.DrawCard());
            player.Cards.Add(CardsHandler.DrawCard());
            player.Turn("hit");
            var result = player.Turn("Double");
            Assert.AreEqual(true, result.Succses, null, result.Exception);
        }
    }
}
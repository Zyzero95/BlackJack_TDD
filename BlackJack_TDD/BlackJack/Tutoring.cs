using BlackJack_TDD.BlackJack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack_TDD
{
    public class Tutoring
    {

        public static bool StartOfRound
        {
            get { return startOfRound; }
            set
            {
                startOfRound = value;
                if (value)
                {
                    RemoveCards();
                }
            }
        }
        private static bool startOfRound;
        private static List<Card> DeckOfCard = Core.CardDeck.cards;
        public bool helpterSwitch;

        private Player player;
        private Dealer Dealer = Core.Dealer;

        public Tutoring(Player player)
        {
            this.player = player;
        }

        internal static void RemoveOneCard(Card card)
        {
            DeckOfCard.Remove(card);
        }

        internal static void ClearCard()
        {
            DeckOfCard = Core.CardDeck.cards;
        }

        private static void RemoveCards()
        {
            foreach (var player in Core.Players)
            {
                foreach (var card in player.Hand)
                {
                    DeckOfCard.Remove(card);
                }
            }
            DeckOfCard.Remove(Core.Dealer.Hand[0]);
            foreach (var card in DeckOfCard.Where(c => c.Value == Card.CardValue.YellowCard).ToList())
            {
                DeckOfCard.Remove(card);
            }
        }


        public string Cheat()
        {
            var porbility = CalculatePobabilitiy();

            int untilBlackJack;
            if (player.HandValue < 15)
            {
                return "You should hit";
            }
            else
            {
                untilBlackJack = 21 - player.HandValue;
                var listOfVaildDraws = DeckOfCard.Where(x => x.Score <= untilBlackJack).ToList();
                if (Core.Dealer.HandValue > 16 && player.HandValue < Core.Dealer.HandValue)
                {
                    return "You should hit";
                }
                else if (player.HandValue >= 20)
                {
                    return "You should stand";
                }
                else if (listOfVaildDraws.Count >= DeckOfCard.Count / 2)
                {
                    return helpterSwitch ? "You should hit" : "You should stand";
                }
                else
                {
                    return !helpterSwitch ? "You should hit" : "You should stand";
                }

            }
        }

        private Pobability CalculatePobabilitiy()
        {
            var porabibltyofDrawCard = new List<CardPorbability>();
            var dealerCurrentScore = Core.Dealer.Hand[0].Score == 1 ? 11 : Core.Dealer.Hand[0].Score;
            var alreadyCalculatedValues = new List<Card>();
            foreach (var card in DeckOfCard)
            {
                if (alreadyCalculatedValues.FindAll(x => x.Score == card.Score).Count == 0)
                {
                    alreadyCalculatedValues.Add(card);
                    var allCardsWithSameValue = DeckOfCard.FindAll(x => x.Score == card.Score);
                    porabibltyofDrawCard.Add(new CardPorbability { Probability = (double)allCardsWithSameValue.Count / (double)DeckOfCard.Count, Score = card.Score });
                }
            }
            var DealerTo16 = 16 - dealerCurrentScore;
            var DealerTo18 = 18 - dealerCurrentScore;
            var DealerTo21 = 21 - dealerCurrentScore;
            double probailityDealerDrawCardUnder16 = 0;
            double probailityDealerGetbetween16And18 = 0;
            double probailityDealerGetbetween18And21 = 0;
            double probailityDealerDrawCardOver21 = 0;
            Pobability under16 = null;
            //Poability dealer get bewtween 16 and 18
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score > DealerTo16 && x.Score <= DealerTo18))
            {
                probailityDealerGetbetween16And18 += card.Probability;
            }
            //Poability dealer get bewtween 18 and 21
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score > DealerTo18 && x.Score <= DealerTo21))
            {
                probailityDealerGetbetween18And21 += card.Probability;
            }
            //Poability dealer get over 21
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score > DealerTo21))
            {
                probailityDealerDrawCardOver21 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score <= DealerTo16))
            {
                under16 = CalculatePobabilitiUnder16(card, porabibltyofDrawCard, DealerTo16, card.Probability);
                probailityDealerGetbetween16And18 += under16.Between16And18;
                probailityDealerGetbetween18And21 += under16.Between18And21;
                probailityDealerDrawCardOver21 += under16.Over21;
            }

            return new Pobability
            {
                Under16 = probailityDealerDrawCardUnder16,
                Between16And18 = probailityDealerGetbetween16And18,
                Between18And21 = probailityDealerGetbetween18And21,
                Over21 = probailityDealerDrawCardOver21
            };
        }

        private Pobability CalculatePobabilitiUnder16(CardPorbability card, List<CardPorbability> cardPorbabilities,int dealerTo16, double porbability)
        {
            dealerTo16 -= card.Score;
            var dealerTo18 = dealerTo16 + 2;
            var dealerTo21 = dealerTo16 + 5;
            Pobability under16 = null;

            double probailityDealerGetbetween16And18 = 0;
            double probailityDealerGetbetween18And21 = 0;
            double probailityDealerDrawCardOver21 = 0;

            //Poability dealer get bewtween 16 and 18 third card
            foreach (var card1 in cardPorbabilities.Where(x => x.Score > dealerTo16 && x.Score <= dealerTo18))
            {
                probailityDealerGetbetween16And18 += card1.Probability * porbability;
            }
            
            //Poability dealer get bewtween 18 and 21 third card
            foreach (var card1 in cardPorbabilities.Where(x => x.Score > dealerTo18 && x.Score <= dealerTo21))
            {
                probailityDealerGetbetween18And21 += card1.Probability * porbability;
            }
            
            //Poability dealer get over 21 third card
            foreach (var card1 in cardPorbabilities.Where(x => x.Score > dealerTo21))
            {
                probailityDealerDrawCardOver21 += card1.Probability * porbability;
            }
            //under 16
            var probailityDealerDrawCardUnder16 = porbability - probailityDealerGetbetween16And18 - probailityDealerGetbetween18And21 - probailityDealerDrawCardOver21;
            if (probailityDealerDrawCardUnder16 > 0.001)
            {
                probailityDealerDrawCardUnder16 = 0;
                foreach (var card1 in cardPorbabilities.Where(x => x.Score <= dealerTo16))
                {
                    under16 = CalculatePobabilitiUnder16(card1, cardPorbabilities, dealerTo16, porbability * card1.Probability);
                    probailityDealerGetbetween16And18 += under16.Between16And18;
                    probailityDealerGetbetween18And21 += under16.Between18And21;
                    probailityDealerDrawCardOver21 += under16.Over21;
                }
            }
            return new Pobability
            {
                Under16 = probailityDealerDrawCardUnder16,
                Between16And18 = probailityDealerGetbetween16And18,
                Between18And21 = probailityDealerGetbetween18And21,
                Over21 = probailityDealerDrawCardOver21
            };
        }
    }

    internal class CardPorbability
    {
        public int Score { get; set; }
        public double Probability { get; set; }

        public override string ToString()
        {
            return $"Score={Score} Probability={Probability}";
        }
    }

    internal class Pobability
    {
        public double Under16 { get; set; }
        public double Between16And18 { get; set; }
        public double Between18And21 { get; set; }
        public double Over21 { get; set; }
        public override string ToString()
        {
            return $"Under16= {Under16} Between16And18={Between16And18} Between18And21={Between18And21} Over21 = {Over21}";
        }
    }

}
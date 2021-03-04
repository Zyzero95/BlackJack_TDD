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
            var probabilityDealer = CalculatePobabilitiy((int)Core.Dealer.Hand[0].Value);
            var probabilityPlayer = CalculatePobabilitiy(player.HandValue);

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

        private Probability CalculatePobabilitiy(int HandScore)
        {
            var porabibltyofDrawCard = new List<CardPorbability>();
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


            var scoreTo17 = 17 - HandScore;
            double probabilityUnder17 = 0;
            double probabilityToGet17 = 0;
            double probabilityToGet18 = 0;
            double probabilityToGet19 = 0;
            double probabilityToGet20 = 0;
            double probabilityToGet21 = 0;
            double probabilityOver21 = 0;
            Probability probability = null;
            //Poability dealer get bewtween 16 and 18
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17))
            {
                probabilityToGet17 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 +1 ))
            {
                probabilityToGet18 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 +2))
            {
                probabilityToGet19 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 +3))
            {
                probabilityToGet20 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 +4))
            {
                probabilityToGet21 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score > scoreTo17 +4))
            {
                probabilityOver21 += card.Probability;
            }

            foreach (var card in porabibltyofDrawCard.Where(x => x.Score < scoreTo17))
            {
                probability = CalculatePobabilitiUnder16(card, porabibltyofDrawCard, scoreTo17 , card.Probability);
                probabilityToGet17 += probability.Seventeen;
                probabilityToGet18 += probability.Eighteen;
                probabilityToGet19 += probability.Ninteen;
                probabilityToGet20 += probability.Twenty;
                probabilityToGet21 += probability.Twentyone;
                probabilityOver21 += probability.OverTwentyone;
            }
            probabilityUnder17 = 1 - probabilityToGet17 - probabilityToGet18 - probabilityToGet19 - probabilityToGet20 - probabilityToGet21 - probabilityOver21;
            return new Probability
            {
                UnderSeventeen = probabilityUnder17,
                Seventeen = probabilityToGet17,
                Eighteen = probabilityToGet18,
                Ninteen = probabilityToGet19,
                Twenty = probabilityToGet20,
                Twentyone = probabilityToGet21,
                OverTwentyone = probabilityOver21
            };
        }

        private Probability CalculatePobabilitiUnder16(CardPorbability card, List<CardPorbability> cardProbability, int scoreto17, double probability)
        {
            Probability probabilityRetrun = null;

            var scoreTo17 = scoreto17 - card.Score;
            double probabilityUnder17 = 0;
            double probabilityToGet17 = 0;
            double probabilityToGet18 = 0;
            double probabilityToGet19 = 0;
            double probabilityToGet20 = 0;
            double probabilityToGet21 = 0;
            double probabilityOver21 = 0;
            //Poability dealer get bewtween 16 and 18
            foreach (var card1 in cardProbability.Where(x => x.Score == scoreTo17))
            {
                probabilityToGet17 += card1.Probability * probability;
            }
            foreach (var card1 in cardProbability.Where(x => x.Score == scoreTo17 + 1))
            {
                probabilityToGet18 += card1.Probability * probability;
            }
            foreach (var card1 in cardProbability.Where(x => x.Score == scoreTo17 + 2))
            {
                probabilityToGet19 += card1.Probability * probability;
            }
            foreach (var card1 in cardProbability.Where(x => x.Score == scoreTo17 + 3))
            {
                probabilityToGet20 += card1.Probability * probability;
            }
            foreach (var card1 in cardProbability.Where(x => x.Score == scoreTo17 + 4))
            {
                probabilityToGet21 += card1.Probability * probability;
            }
            foreach (var card1 in cardProbability.Where(x => x.Score > scoreTo17 + 4))
            {
                probabilityOver21 += card1.Probability * probability;
            }
            //under 16
            probabilityUnder17 = probability - probabilityToGet17 - probabilityToGet18 - probabilityToGet19 - probabilityToGet20 - probabilityToGet21 - probabilityOver21;
            if (probabilityUnder17 > 0.0001)
            {
                probabilityUnder17 = 0;
                foreach (var card1 in cardProbability.Where(x => x.Score < scoreTo17))
                {
                    probabilityRetrun = CalculatePobabilitiUnder16(card1, cardProbability, scoreto17 , probability * card1.Probability);
                    probabilityToGet17 += probabilityRetrun.Seventeen;
                    probabilityToGet18 += probabilityRetrun.Eighteen;
                    probabilityToGet19 += probabilityRetrun.Ninteen;
                    probabilityToGet20 += probabilityRetrun.Twenty;
                    probabilityToGet21 += probabilityRetrun.Twentyone;
                    probabilityOver21 += probabilityRetrun.OverTwentyone;
                }
            }
            return new Probability
            {
                UnderSeventeen = probabilityUnder17,
                Seventeen = probabilityToGet17,
                Eighteen = probabilityToGet18,
                Ninteen = probabilityToGet19,
                Twenty = probabilityToGet20,
                Twentyone = probabilityToGet21,
                OverTwentyone = probabilityOver21
            };
        }
    }

    internal class CardPorbability
    {
        public int Score { get; set; }
        public double Probability { get; set; }

        public override string ToString()
        {
            return $"Score={Score} probability={Probability}";
        }
    }

    internal class Probability
    {
        public double UnderSeventeen { get; set; }
        public double Seventeen { get; set; }
        public double Eighteen { get; set; }
        public double Ninteen { get; set; }
        public double Twenty { get; set; }
        public double Twentyone { get; set; }
        public double OverTwentyone { get; set; }
        public override string ToString()
        {
            return $"UnderSeventeen={UnderSeventeen} Seventeen={Seventeen} Eighteen={Eighteen} Ninteen={Ninteen} Twenty={Twenty} Twentyone={Twentyone} OverTwentyone={OverTwentyone}";
        }
    }

}
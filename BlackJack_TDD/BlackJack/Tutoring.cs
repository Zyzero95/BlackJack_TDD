using BlackJack_TDD.BlackJack;
using System.Collections.Generic;
using System.Linq;

namespace BlackJack_TDD
{
    public class Tutoring
    {
        /// <summary>
        /// when set to true the clear will run for bot
        /// </summary>
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
        /// <summary>
        /// if it is true help or not
        /// </summary>
        public bool helpterSwitch;

        private static bool startOfRound;
        private static List<Card> DeckOfCard = new List<Card>(Core.CardDeck.cards);
        private Player player;

        public Tutoring(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// Remove one card fron the local deck
        /// </summary>
        /// <param name="card">card that have been drawn</param>
        internal static void RemoveOneCard(Card card)
        {
            DeckOfCard.Remove(card);
        }

        /// <summary>
        /// reset the local crad deck
        /// </summary>
        internal static void ClearCard()
        {
            DeckOfCard = new List<Card>(Core.CardDeck.cards);
        }

        /// <summary>
        /// removes multiple cards, should be run on start of evry round
        /// </summary>
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

        /// <summary>
        /// Caclutet the porbability of to get all values bewtween 17 and 21 and then sugest what to do
        /// </summary>
        /// <returns>string of what you should do</returns>
        public string Cheat()
        {
            var probabilityDealer = CalculatePobabilitiy((int)Core.Dealer.Hand[0].Value);
            var probabilityPlayer = CalculatePobabilitiy(player.HandValue);


            if (probabilityPlayer.OverTwentyone > 0.70)
            {
                return helpterSwitch ? "You should stand" : "You should hit";
            }
            else if (probabilityPlayer.OverTwentyone < 0.30)
            {
                return helpterSwitch ? "You should hit" : "You should stand";
            }
            else if(probabilityDealer.OverTwentyone > 0.80)
            {
                return helpterSwitch ? "You should stand" : "You should hit";
            }
            else if (player.HandValue == 17 && probabilityDealer.OverTwentyone > 0.60)
            {
                return helpterSwitch ? "You should stand" : "You should hit";
            }
            else if((player.HandValue == 16 || player.HandValue == 17 || player.HandValue == 18 || player.HandValue == 19) && (probabilityPlayer.Twenty + probabilityPlayer.Twentyone) > (probabilityDealer.Twenty + probabilityDealer.Twentyone))
            {
                return helpterSwitch ? "You should hit" : "You should stand";
            }
            else if(player.HandValue == 18 && (probabilityDealer.OverTwentyone + probabilityDealer.Seventeen) > (probabilityDealer.Ninteen + probabilityDealer.Twenty + probabilityDealer.OverTwentyone))
            {
                return helpterSwitch ? "You should stand" : "You should hit";
            }
            else if (player.HandValue == 18 && (probabilityDealer.OverTwentyone + probabilityDealer.Seventeen) < (probabilityDealer.Ninteen + probabilityDealer.Twenty + probabilityDealer.OverTwentyone))
            {
                return helpterSwitch ? "You should hit" : "You should stand";
            }          
            else if (player.HandValue == 19 && (probabilityDealer.OverTwentyone + probabilityDealer.Seventeen + probabilityDealer.Eighteen) > (probabilityDealer.Twenty + probabilityDealer.OverTwentyone))
            {
                return helpterSwitch ? "You should stand" : "You should hit";
            }
            else if (player.HandValue == 19 && (probabilityDealer.OverTwentyone + probabilityDealer.Seventeen + probabilityDealer.Eighteen) < (probabilityDealer.Twenty + probabilityDealer.OverTwentyone))
            {
                return helpterSwitch ? "You should hit" : "You should stand";
            }
            else{
                return "I tihnk you should hit but my data aint sure";
            }
        }

        /// <summary>
        /// Calculate the porbabilty of draw diffrent cards
        /// </summary>
        /// <param name="HandScore">known score of hand/param>
        /// <returns>obj with all probabilitys for drawing to a spicifikit value nomether how many round it takes</returns>
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
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 + 1))
            {
                probabilityToGet18 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 + 2))
            {
                probabilityToGet19 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 + 3))
            {
                probabilityToGet20 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score == scoreTo17 + 4))
            {
                probabilityToGet21 += card.Probability;
            }
            foreach (var card in porabibltyofDrawCard.Where(x => x.Score > scoreTo17 + 4))
            {
                probabilityOver21 += card.Probability;
            }

            foreach (var card in porabibltyofDrawCard.Where(x => x.Score < scoreTo17))
            {
                probability = CalculatePobabilitiUnder17(card, porabibltyofDrawCard, scoreTo17, card.Probability);
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

        /// <summary>
        /// Caclute the porbability to get an value if you already draw one card
        /// </summary>
        /// <param name="card"></param>
        /// <param name="cardProbability"></param>
        /// <param name="scoreto17"></param>
        /// <param name="probability"></param>
        /// <returns></returns>
        private Probability CalculatePobabilitiUnder17(CardPorbability card, List<CardPorbability> cardProbability, int scoreto17, double probability)
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
            if (probabilityUnder17 > 0.000001)
            {
                probabilityUnder17 = 0;
                foreach (var card1 in cardProbability.Where(x => x.Score < scoreTo17))
                {
                    probabilityRetrun = CalculatePobabilitiUnder17(card1, cardProbability, scoreto17, probability * card1.Probability);
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
    }
}
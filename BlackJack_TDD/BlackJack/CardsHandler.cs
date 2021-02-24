//Viktor Löfgren
using System;
using System.Collections.Generic;
using System.Text;

namespace BlackJack_TDD
{
    public class CardsHandler
    {
        public static List<List<string>> CardDeck = new List<List<string>>();

        public static List<string> Clubs = new List<string>();
        public static List<string> Spades = new List<string>();
        public static List<string> Hearts = new List<string>();
        public static List<string> Diamonds = new List<string>();
        public static List<string> TempList = new List<string>();

        // Creates a set of cards for every suit in the deck.
        public static void CreateCards()
        {
            for(int i = 0; i < 3; i++)
            {
                for(int j = 0; j < 13; j++)
                {
                    if(i == 0 && j == 0)
                    {
                        Clubs.Add("Ace");
                    }
                    else if(i == 0 && j == 10)
                    {
                        Clubs.Add("Jacks");
                    }
                    else if (i == 0 && j == 11)
                    {
                        Clubs.Add("Queen");
                    }
                    else if (i == 0 && j == 12)
                    {
                        Clubs.Add("King");
                    }
                    else if(i == 0)
                    {
                        Clubs.Add((j + 1).ToString());
                    }

                    if (i == 1 && j == 0)
                    {
                        Spades.Add("Ace");
                    }
                    else if (i == 1 && j == 10)
                    {
                        Spades.Add("Jacks");
                    }
                    else if (i == 1 && j == 11)
                    {
                        Spades.Add("Queen");
                    }
                    else if (i == 1 && j == 12)
                    {
                        Spades.Add("King");
                    }
                    else if (i == 1)
                    {
                        Spades.Add((j + 1).ToString());
                    }

                    if (i == 2 && j == 0)
                    {
                        Hearts.Add("Ace");
                    }
                    else if (i == 2 && j == 10)
                    {
                        Hearts.Add("Jacks");
                    }
                    else if (i == 2 && j == 11)
                    {
                        Hearts.Add("Queen");
                    }
                    else if (i == 2 && j == 12)
                    {
                        Hearts.Add("King");
                    }
                    else if (i == 2)
                    {
                        Hearts.Add((j + 1).ToString());
                    }

                    if (i == 3 && j == 0)
                    {
                        Diamonds.Add("Ace");
                    }
                    else if (i == 3 && j == 10)
                    {
                        Diamonds.Add("Jacks");
                    }
                    else if (i == 3 && j == 11)
                    {
                        Diamonds.Add("Queen");
                    }
                    else if (i == 3 && j == 12)
                    {
                        Diamonds.Add("King");
                    }
                    else if (i == 3)
                    {
                        Diamonds.Add((j + 1).ToString());
                    }
                }
            }
        }
        //Creates a deck of cards.
        public static void CreateDeck()
        {
            CreateCards();
            CardDeck.Add(Clubs);
            CardDeck.Add(Spades);
            CardDeck.Add(Hearts);
            CardDeck.Add(Diamonds);
        }

        //Enables the dealer to draw a card for either a player or the table.
        public static (string, string) DrawCard()
        {
            string cardName;
            string suitName;
            Random r = new Random();
            Random l = new Random();
            int suit = r.Next(0, (CardDeck.Count -1));
            int card = l.Next(0, Clubs.Count);
            cardName = CardDeck[suit][card];
            if(suit == 0)
            {
                suitName = "Clubs";
            }
            else if(suit == 1)
            {
                suitName = "Spades";
            }
            else if(suit == 2)
            {
                suitName = "Hearts";
            }
            else
            {
                suitName = "Diamonds";
            }
            CardDeck[suit].RemoveAt(card);
            return (cardName, suitName);
        }

        public static void ShuffleDeck()
        {
            Random r = new Random();
            int randomNumber = 0;
            for(int i = 0; i < CardDeck.Count; i++)
            {
                for (int j = CardDeck[i].Count; j > 0; j--)
                {
                    randomNumber = r.Next(0, j);
                    TempList.Add(CardDeck[i][randomNumber]);
                    CardDeck[i].RemoveAt(randomNumber);
                }
                for (int k = 0; k < TempList.Count; k++)
                {
                    CardDeck[i].Add(TempList[k]);
                }
                TempList.Clear();
            }
        }
    }
}

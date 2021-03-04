using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gubanc.include
{
    public class RubikGubanc
    {
        private int CARDS_COUNT = 0;

        public RubikGubanc()
        {
            CardCollections cardCollections = CardCollections.GetCardCollections();

            cardCollections.CardCollection.Add(new Card(name: "1", top: "PZ", right: "SP", bottom: "KZ", left: "KS"));
            cardCollections.CardCollection.Add(new Card(name: "2", top: "KS", right: "ZK", bottom: "PS", left: "PZ"));
            cardCollections.CardCollection.Add(new Card(name: "3", top: "PZ", right: "KP", bottom: "SZ", left: "SK"));
            cardCollections.CardCollection.Add(new Card(name: "4", top: "ZP", right: "SZ", bottom: "KP", left: "KS"));
            cardCollections.CardCollection.Add(new Card(name: "5", top: "ZS", right: "PZ", bottom: "KS", left: "KP"));
            cardCollections.CardCollection.Add(new Card(name: "6", top: "KS", right: "PK", bottom: "ZS", left: "ZP"));
            cardCollections.CardCollection.Add(new Card(name: "7", top: "ZK", right: "SZ", bottom: "PK", left: "PS"));
            cardCollections.CardCollection.Add(new Card(name: "8", top: "SK", right: "ZS", bottom: "PK", left: "PZ"));
            cardCollections.CardCollection.Add(new Card(name: "9", top: "KZ", right: "PK", bottom: "SZ", left: "SP"));

            CARDS_COUNT = cardCollections.CardCollection.Count;
        }

        public bool Gubancol(int cardSpace)
        {
            Card card = null;

            if (cardSpace == 9)
            {
                Console.WriteLine("");
                Console.WriteLine("Befejezve");
                return true;
            }

            for (int i = 0; i < CARDS_COUNT; i++)
            {
                card = LoadCard(i);

                if (!card.IsConnected)
                {
                    card.Reset();

                    while (card.IsRotateEnabled)
                    {
                        if (TryAdd(card, cardSpace))
                        {
                            card.IsConnected = true;
                            card.CardSpace = cardSpace;
                            Console.WriteLine("    SIKERES ILLESZTÉS - {0}    kártyahely: {1}    tájolás: {2}", card.Name, cardSpace + 1, card.Orientation);
                            Console.WriteLine("{0}. lap illesztése", cardSpace + 1);

                            if (Gubancol(cardSpace + 1))
                            {
                                return true;
                            }
                            else
                            {
                                Rotate(card, cardSpace);
                                DropCard(card);
                            }
                        }
                        else
                        {
                            Rotate(card, cardSpace);
                        }
                    }
                }
            }
            return false;
        }

        private void Rotate(Card card, int cardSpace)
        {
            card.Rotate();
            Console.WriteLine("    FORGATÁS - {0}    kártyahely: {1}    tájolás: {2}", card.Name, cardSpace + 1, card.Orientation);
        }

        private Card LoadCard(int index)
        {
            CardCollections cardCollections = CardCollections.GetCardCollections();

            return cardCollections.CardCollection[index];
        }

        private void DropCard(Card card)
        {
            CardCollections cardCollections = CardCollections.GetCardCollections();

            cardCollections.Solution.Remove(card);
            card.IsConnected = false;
            Console.WriteLine("    ELDOBÁS - {0}", card.Name);
        }

        private bool TryAdd(Card card, int cardSpace)
        {
            Card left = null;
            Card top = null;
            bool result = false;
            CardCollections cardCollections = CardCollections.GetCardCollections();

            Console.WriteLine("    ILLESZTÉS - {0}    kártyahely: {1}    tájolás: {2}", card.Name, cardSpace + 1, card.Orientation);

            switch (cardSpace)
            {
                case 1:
                case 2:
                    left = cardCollections.Solution[cardSpace - 1];
                    break;
                case 3:
                case 6:
                    top = cardCollections.Solution[cardSpace - 3];
                    break;
                case 4:
                case 5:
                case 7:
                case 8:
                    left = cardCollections.Solution[cardSpace - 1];
                    top = cardCollections.Solution[cardSpace - 3];
                    break;
                default:
                    break;
            }

            if (left == null && top == null)
            {
                result = true;
            }
            else if (left == null)
            {
                result = (top.Bottom == card.Top);
            }
            else if (top == null)
            {
                result = (left.Right == card.Left);
            }
            else
            {
                result = ((top.Bottom == card.Top) && (left.Right == card.Left));
            }

            if (result)
                cardCollections.Solution.Add(card);

            return result;
        }
    }
}

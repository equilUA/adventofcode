using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;

namespace star7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Star7!");

            var inputData = File.ReadAllLines(@"C:\Stars\adventofcode\star7\input.txt");

            var hands = new List<Hand>();

            foreach (var line in inputData)
            {
                var data = line.Split(' ');
                var cards = data[0].Trim().ToList();
                var bid = long.Parse(data[1].Trim());

                hands.Add(new Hand { CardsOnHand = cards, Bid = bid });
            }

            long partOne = 0;

            var orderedHands = hands.OrderBy(x=>x).ToList();

            for (int i = 1; i <= orderedHands.Count; i++)
            {
                partOne += orderedHands[i - 1].Bid * i;
            }

            Console.WriteLine($"Part two: {partOne}");
        }
        internal class Hand : IComparable<Hand>
        {
            List<char> _order = new List<char>() { 'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J' };

            public List<char> _cardsOnHand = new List<char>();
            public List<char> CardsOnHand
            {
                get
                { return _cardsOnHand; }
                set
                {
                    _cardsOnHand = value;
                }
            }
            public long Bid { get; set; }

            public int CompareTo(Hand? other)
            {
                if (CardsKind < other.CardsKind)
                {
                    return 1;
                }
                else if (CardsKind > other.CardsKind)
                {
                    return -1;
                }

                for (int i = 0; i < CardsOnHand.Count; i++)
                {
                    if (_order.IndexOf(CardsOnHand[i]) < _order.IndexOf(other.CardsOnHand[i]))
                    {
                        return 1;
                    }
                    else if (_order.IndexOf(CardsOnHand[i]) > _order.IndexOf(other.CardsOnHand[i]))
                    {
                        return -1;
                    }
                }

                return 0;
            }

            public CardsKind CardsKind => GetCardsKind();

            private CardsKind GetCardsKind()
            {
                var groupedCards = CardsOnHand.GroupBy(x => x);

                if (CardsOnHand.All(x => x.Equals(CardsOnHand[0])) || CardsOnHand.Contains('J') && CardsOnHand.Distinct().Count() == 2)
                {
                    return CardsKind.FiveOfKind;
                }
                else if (groupedCards.Any(group => group.Count() == 4) ||
                         groupedCards.Any(group => group.Count() == 3 && group.Key != 'J' && CardsOnHand.Contains('J')) || 
                         groupedCards.Any(group => group.Count() == 2 && group.Key != 'J' && CardsOnHand.Count(x => x == 'J') == 2) || 
                         groupedCards.Any(group => group.Count() == 1 && group.Key != 'J' && CardsOnHand.Count(x => x == 'J') == 3)) 
                {
                    return CardsKind.FourOfKind;
                }
                else if (groupedCards.Any(group => group.Count() == 3) && groupedCards.Any(group => group.Count() == 2) ||
                         (groupedCards.Where(group => group.Count() == 2).Count() == 2) && CardsOnHand.Contains('J'))
                {
                    return CardsKind.FullHouse;
                }
                else if (groupedCards.Any(group => group.Count() == 3) ||
                         groupedCards.Any(group => group.Count() == 2) && CardsOnHand.Contains('J')) 
                {
                    return CardsKind.ThreeOfKind;
                }
                else if (groupedCards.Where(group => group.Count() == 2).Count() == 2) 
                {
                    return CardsKind.TwoPair;
                }
                else if (groupedCards.Any(group => group.Count() == 2) || CardsOnHand.Contains('J')) 
                {
                    return CardsKind.OnePair;
                }
                else
                {
                    return CardsKind.HighCard;
                }
            }
        }

        internal enum CardsKind
        {
            FiveOfKind,
            FourOfKind,
            FullHouse,
            ThreeOfKind,
            TwoPair,
            OnePair,
            HighCard
        }
    }
}

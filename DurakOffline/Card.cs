using System;
using System.Collections.Generic;
using System.Text;

namespace DurakOffline
{
    public class Card
    {
        public Suit Suit { get; }
        public Rank Rank { get; }
        public bool IsTrump { get; set; }

        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            IsTrump = false;
        }

        public bool CanBeat(Card attackingCard, Card trumpCard)
        {
            if (attackingCard == null)
                return false;

            if (trumpCard == null)
                return false;

            if (attackingCard.Suit == trumpCard.Suit)
            {
                return this.Suit == trumpCard.Suit && (int)this.Rank > (int)attackingCard.Rank;
            }

            else
            {
                if (this.Suit == attackingCard.Suit)
                {
                    return (int)this.Rank > (int)attackingCard.Rank;
                }
                else if (this.Suit == trumpCard.Suit)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public override string ToString()
        {
            string suitSymbol = Suit switch
            {
                Suit.Hearts => "♥",
                Suit.Diamonds => "♦",
                Suit.Clubs => "♣",
                Suit.Spades => "♠",
                _ => "",
            };

            string rankStr = Rank switch
            {
                Rank.Six => "6",
                Rank.Seven => "7",
                Rank.Eight => "8",
                Rank.Nine => "9",
                Rank.Ten => "10",
                Rank.Jack => "В",
                Rank.Queen => "Д",
                Rank.King => "К",
                Rank.Ace => "Т",
                _ => ""
            };

            return $"{rankStr}{suitSymbol}";
        }
    }
}

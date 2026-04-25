using System;
using System.Collections.Generic;
using System.Text;

namespace DurakOffline
{
    /// <summary>
    /// Класс, представляющий игральную карту
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Масть карты (Черви, Буби, Крести, Пики)
        /// </summary>
        public Suit Suit { get; }

        /// <summary>
        /// Достоинство карты (6,7,8,9,10,В,Д,К,Т)
        /// </summary>
        public Rank Rank { get; }

        /// <summary>
        /// Флаг, указывающий, является ли карта козырем
        /// </summary>
        public bool IsTrump { get; set; }

        /// <summary>
        /// Конструктор карты
        /// </summary>
        /// <param name="suit">Масть карты</param>
        /// <param name="rank">Достоинство карты</param>
        public Card(Suit suit, Rank rank)
        {
            Suit = suit;
            Rank = rank;
            IsTrump = false;
        }

        /// <summary>
        /// Определяет, может ли текущая карта побить атакующую карту
        /// </summary>
        /// <param name="attackingCard">Атакующая карта, которую нужно побить</param>
        /// <param name="trumpCard">Карта-козырь (определяет козырную масть)</param>
        /// <returns>True, если текущая карта может побить атакующую, иначе False</returns>
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

        /// <summary>
        /// Возвращает строковое представление карты
        /// </summary>
        /// <returns>Строка вида "6♥", "В♠"
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

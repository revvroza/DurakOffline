using System;
using System.Collections.Generic;
using System.Text;

namespace DurakOffline
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; private set; }
        public bool IsAI { get; set; }
        public int CardCount => Hand.Count;

        public Player(string name, bool isAI = false)
        {
            Name = name;
            IsAI = isAI;
            Hand = new List<Card>();
        }

        public void TakeCards(List<Card> cards)
        {
            Hand.AddRange(cards);
        }

        public void DiscardAllCards()
        {
            Hand.Clear();
        }

        public bool RemoveCard(Card card)
        {
            return Hand.Remove(card);
        }

        public List<Card> GetCardsBySuit(Suit suit)
        {
            return Hand.Where(c => c.Suit == suit).ToList();
        }

        public List<Card> GetCardsThatCanBeat(Card attackingCard, Card trumpCard)
        {
            return Hand.Where(card => card.CanBeat(attackingCard, trumpCard)).ToList();
        }

        public List<Card> GetAttackCards()
        {
            return new List<Card>(Hand);
        }

        public List<Card> GetCardsByRank(Rank rank)
        {
            return Hand.Where(c => c.Rank == rank).ToList();
        }

        public bool HasCards()
        {
            return Hand.Count > 0;
        }

        public Card GetLowestCard()
        {
            if (!HasCards()) return null;
            return Hand.OrderBy(c => (int)c.Rank).First();
        }

        public Card GetLowestWinningCard(Card attackingCard, Card trumpCard)
        {
            var winningCards = GetCardsThatCanBeat(attackingCard, trumpCard);
            if (!winningCards.Any()) return null;
            return winningCards.OrderBy(c => (int)c.Rank).First();
        }

        public override string ToString()
        {
            return $"{Name} ({CardCount} карт)";
        }
    }
}

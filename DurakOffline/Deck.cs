using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Linq;

namespace DurakOffline
{
    public class Deck
    {
        private List<Card> _cards;
        private Random _random;
        public Card TrumpCard { get; private set; }
        public int Count => _cards.Count;

        public Deck()
        {
            _cards = new List<Card>();
            _random = new Random();
            Initialize();
        }

        private void Initialize()
        {
            foreach (Suit suit in Enum.GetValues(typeof(Suit)))
            {
                foreach (Rank rank in Enum.GetValues(typeof(Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }
        }

        public void Shuffle()
        {
            for (int i = 0; i < _cards.Count; i++)
            {
                int randomIndex = _random.Next(i, _cards.Count);
                var temp = _cards[i];
                _cards[i] = _cards[randomIndex];
                _cards[randomIndex] = temp;
            }
        }

        public void RevealTrump()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("Колода пуста");

            TrumpCard = _cards[_cards.Count - 1];
            TrumpCard.IsTrump = true;
        }

        public List<Card> DealCards(int count)
        {
            if (count > _cards.Count)
                throw new InvalidOperationException("Недостаточно карт в колоде");

            var cards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                cards.Add(_cards[_cards.Count - 1]);
                _cards.RemoveAt(_cards.Count - 1);
            }
            return cards;
        }

        public void AddCardsToBottom(List<Card> cards)
        {
            _cards.InsertRange(0, cards);
        }

        public List<Card> GetCards()
        {
            return new List<Card>(_cards);
        }
    }
}

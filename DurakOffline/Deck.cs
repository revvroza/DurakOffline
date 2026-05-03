using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakOffline
{
    /// <summary>
    /// Класс, управляющий колодой карт
    /// </summary>
    public class Deck
    {
        /// <summary>Список карт в колоде</summary>
        private List<Card> _cards;

        /// <summary>Генератор случайных чисел для перемешивания</summary>
        private Random _random;

        /// <summary>Карта-козырь (определяет козырную масть)</summary>
        public Card TrumpCard { get; private set; }

        /// <summary>Количество оставшихся карт в колоде</summary>
        public int Count => _cards.Count;

        /// <summary>
        /// Конструктор колоды
        /// </summary>
        /// <remarks>Создаёт полную колоду из 36 карт (6-Туз × 4 масти)</remarks>
        public Deck()
        {
            _cards = new List<Card>();
            _random = new Random();
            Initialize();
        }

        /// <summary>
        /// Инициализация колоды: создание всех 36 карт
        /// </summary>
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

        /// <summary>
        /// Перемешивание колоды (алгоритм Фишера-Йетса)
        /// </summary>
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

        /// <summary>
        /// Открытие козыря (последняя карта в колоде)
        /// </summary>
        /// <exception cref="InvalidOperationException">Колода пуста</exception>
        public void RevealTrump()
        {
            if (_cards.Count == 0)
                throw new InvalidOperationException("Колода пуста");

            int randomIndex = _random.Next(_cards.Count);
            TrumpCard = _cards[randomIndex];
            TrumpCard.IsTrump = true;

            _cards.RemoveAt(randomIndex);
            _cards.Add(TrumpCard);
        }

        /// <summary>
        /// Раздача указанного количества карт из колоды
        /// </summary>
        /// <param name="count">Количество карт для раздачи</param>
        /// <returns>Список разданных карт</returns>
        /// <exception cref="InvalidOperationException">Недостаточно карт в колоде</exception>
        public List<Card> DealCards(int count)
        {
            if (count > _cards.Count)
                throw new InvalidOperationException("Недостаточно карт в колоде");

            var cards = new List<Card>();
            for (int i = 0; i < count; i++)
            {
                cards.Add(_cards[0]); 
                _cards.RemoveAt(0);
            }
            return cards;
        }

        /// <summary>
        /// Добавление карт в начало колоды (в отбой)
        /// </summary>
        /// <param name="cards">Список карт для добавления</param>
        public void AddCardsToBottom(List<Card> cards)
        {
            _cards.InsertRange(0, cards);
        }

        /// <summary>
        /// Получение копии текущей колоды (для отладки)
        /// </summary>
        /// <returns>Копия списка карт в колоде</returns>
        public List<Card> GetCards()
        {
            return new List<Card>(_cards);
        }
    }
}
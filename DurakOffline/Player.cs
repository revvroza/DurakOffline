using System;
using System.Collections.Generic;
using System.Text;

namespace DurakOffline
{
    /// <summary>
    /// Класс, представляющий игрока 
    /// </summary>
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; private set; }
        public int CardCount => Hand.Count;

        /// <summary>
        /// Конструктор игрока
        /// </summary>
        /// <param name="name">Имя игрока</param>
        public Player(string name, bool isAI = false)
        {
            Name = name;
            Hand = new List<Card>();
        }

        /// <summary>
        /// Добавление карт в руку игрока
        /// </summary>
        /// <param name="cards">Список карт для добавления</param>
        public void TakeCards(List<Card> cards)
        {
            Hand.AddRange(cards);
        }

        /// <summary>
        /// Сброс всех карт из руки (очистка)
        /// </summary>
        public void DiscardAllCards()
        {
            Hand.Clear();
        }

        /// <summary>
        /// Удаление конкретной карты из руки
        /// </summary>
        /// <param name="card">Карта для удаления</param>
        /// <returns>True, если карта была найдена и удалена, иначе False</returns>
        public bool RemoveCard(Card card)
        {
            return Hand.Remove(card);
        }

        /// <summary>
        /// Получение всех карт указанной масти
        /// </summary>
        /// <param name="suit">Масть для фильтрации</param>
        /// <returns>Список карт указанной масти</returns>
        public List<Card> GetCardsBySuit(Suit suit)
        {
            return Hand.Where(c => c.Suit == suit).ToList();
        }

        public List<Card> GetCardsThatCanBeat(Card attackingCard, Card trumpCard)
        {
            return Hand.Where(card => card.CanBeat(attackingCard, trumpCard)).ToList();
        }

        /// <summary>
        /// Получение всех карт для атаки (все карты в руке)
        /// </summary>
        /// <returns>Копия списка карт в руке</returns>
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

        /// <summary>
        /// Строковое представление игрока
        /// </summary>
        /// <returns>Строка вида "Игрок 1 (6 карт)"</returns>
        public override string ToString()
        {
            return $"{Name} ({CardCount} карт)";
        }
    }
}

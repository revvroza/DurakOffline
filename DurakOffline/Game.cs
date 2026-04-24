using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakOffline
{
    public class Game
    {
        private Deck _deck;
        private Player _player1;
        private Player _player2;
        private List<Card> _tableCards;
        private List<Card> _attackingCards;
        private List<Card> _defendingCards;
        private int _currentAttacker;
        private int _currentDefender;
        private bool _isGameOver;

        public List<Card> GetPlayer1Hand() => _player1.Hand.ToList();
        public List<Card> GetPlayer2Hand() => _player2.Hand.ToList();
        public int GetDeckCount() => _deck.Count;

        public Player CurrentAttacker => _currentAttacker == 0 ? _player1 : _player2;
        public Player CurrentDefender => _currentDefender == 0 ? _player1 : _player2;
        public List<Card> TableCards => _tableCards;
        public bool IsGameOver => _isGameOver;
        public Player Winner { get; private set; }
        public Card TrumpCard => _deck.TrumpCard;

        public List<Card> GetAttackingCards() => _attackingCards.ToList();
        public List<Card> GetDefendingCards() => _defendingCards.ToList();

        public Game(string player1Name, string player2Name)
        {
            _deck = new Deck();
            _player1 = new Player(player1Name);
            _player2 = new Player(player2Name);
            _tableCards = new List<Card>();
            _attackingCards = new List<Card>();
            _defendingCards = new List<Card>();
            _currentAttacker = 0;
            _currentDefender = 1;
            _isGameOver = false;
            Winner = null;
        }

        public void StartGame()
        {
            _deck.Shuffle();
            _deck.RevealTrump();

            _player1.TakeCards(_deck.DealCards(6));
            _player2.TakeCards(_deck.DealCards(6));
        }

        public bool Attack(Card card)
        {
            if (_isGameOver) return false;
            if (!CurrentAttacker.Hand.Contains(card)) return false;

            if (_attackingCards.Count > 0)
            {
                if (CurrentDefender.CardCount == 0)
                {
                    return false;
                }

                int maxCardsCanDefend = CurrentDefender.CardCount + _defendingCards.Count;
                if (_attackingCards.Count >= maxCardsCanDefend)
                {
                    return false;
                }

                var existingRanks = _attackingCards.Select(c => c.Rank)
                    .Union(_defendingCards.Select(c => c.Rank))
                    .ToList();

                if (!existingRanks.Contains(card.Rank))
                    return false;
            }

            CurrentAttacker.RemoveCard(card);
            _attackingCards.Add(card);
            _tableCards.Add(card);

            return true;
        }
        public bool Defend(Card attackingCard, Card defendingCard)
        {
            if (_isGameOver) return false;

            int attackIndex = _attackingCards.IndexOf(attackingCard);
            if (attackIndex == -1) return false;
            if (_defendingCards.Count > attackIndex) return false;
            if (!CurrentDefender.Hand.Contains(defendingCard)) return false;

            if (!defendingCard.CanBeat(attackingCard, _deck.TrumpCard))
                return false;

            CurrentDefender.RemoveCard(defendingCard);
            _defendingCards.Add(defendingCard);
            _tableCards.Add(defendingCard);

            return true;
        }

        public void TakeCards()
        {
            if (_isGameOver) return;

            CurrentDefender.TakeCards(_tableCards);
            _tableCards.Clear();
            _attackingCards.Clear();
            _defendingCards.Clear();

            DrawCardsToSix();

            CheckGameOver();
        }

        public void EndTurn()
        {
            if (_isGameOver) return;

            if (_attackingCards.Count > _defendingCards.Count)
            {
                TakeCards();
            }

            else if (_attackingCards.Count == _defendingCards.Count && _attackingCards.Count > 0)
            {
                EndRound(true);
            }
        }

        public bool IsRoundComplete()
        {
            return _attackingCards.Count > 0 && _attackingCards.Count == _defendingCards.Count;
        }

        private void EndRound(bool defenseSuccess)
        {
            if (defenseSuccess)
            {
                _tableCards.Clear();
                _attackingCards.Clear();
                _defendingCards.Clear();

                SwapRoles();
            }

            DrawCardsToSix();

            CheckGameOver();
        }

        private void SwapRoles()
        {
            int temp = _currentAttacker;
            _currentAttacker = _currentDefender;
            _currentDefender = temp;
        }

        private void DrawCardsToSix()
        {
            while (CurrentAttacker.CardCount < 6 && _deck.Count > 0)
            {
                CurrentAttacker.TakeCards(_deck.DealCards(1));
            }

            while (CurrentDefender.CardCount < 6 && _deck.Count > 0)
            {
                CurrentDefender.TakeCards(_deck.DealCards(1));
            }

            CheckGameOver();
        }

        private void CheckGameOver()
        {
            int player1Cards = _player1.CardCount;
            int player2Cards = _player2.CardCount;

            System.Diagnostics.Debug.WriteLine($"=== CheckGameOver ===");
            System.Diagnostics.Debug.WriteLine($"Игрок 1 карт: {player1Cards}");
            System.Diagnostics.Debug.WriteLine($"Игрок 2 карт: {player2Cards}");

            if (player1Cards == 0)
            {
                _isGameOver = true;
                Winner = _player1;
                System.Diagnostics.Debug.WriteLine($"ПОБЕДИТЕЛЬ: Игрок 1");
            }
            else if (player2Cards == 0)
            {
                _isGameOver = true;
                Winner = _player2;
                System.Diagnostics.Debug.WriteLine($"ПОБЕДИТЕЛЬ: Игрок 2");
            }

            if (_isGameOver)
            {
                System.Diagnostics.Debug.WriteLine($"ИГРА ОКОНЧЕНА! Победитель: {Winner.Name}");
            }
        }

        public string GetGameState()
        {
            return $"Козырь: {_deck.TrumpCard}\n" +
                   $"Атакует: {CurrentAttacker.Name}\n" +
                   $"Защищается: {CurrentDefender.Name}\n" +
                   $"{_player1.Name}: {_player1.CardCount} карт\n" +
                   $"{_player2.Name}: {_player2.CardCount} карт\n" +
                   $"Карт на столе: {_attackingCards.Count}/{_defendingCards.Count}\n" +
                   $"Карт в колоде: {_deck.Count}";
        }

        public List<Rank> GetAvailableRanksToAdd()
        {
            if (_attackingCards.Count == 0)
                return new List<Rank>();

            return _attackingCards.Select(c => c.Rank)
                .Union(_defendingCards.Select(c => c.Rank))
                .Distinct()
                .ToList();
        }

        public string GetWinnerMessage()
        {
            if (!_isGameOver) return "Игра продолжается...";

            string winnerName = Winner?.Name ?? "Ничья";
            int loserCards = Winner == _player1 ? _player2.CardCount : _player1.CardCount;

            if (loserCards == 0)
                return $"🏆 ПОБЕДИТЕЛЬ: {winnerName}! 🏆\nПоздравляем!";
            else
                return $"🏆 ПОБЕДИТЕЛЬ: {winnerName}! 🏆\nУ проигравшего осталось {loserCards} карт";
        }

        public bool CanAddMoreCards()
        {
            if (_attackingCards.Count == 0) return true;

            if (CurrentDefender.CardCount == 0) return false;

            int maxCardsCanDefend = CurrentDefender.CardCount + _defendingCards.Count;

            return _attackingCards.Count < maxCardsCanDefend;
        }
    }
}
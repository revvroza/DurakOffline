using DurakOffline;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    public class PlayerTests
    {
        [Test]
        public void Constructor_CreatesEmptyHand()
        {
            var player = new Player("Тест");

            Assert.That(player.Name, Is.EqualTo("Тест"));
            Assert.That(player.IsAI, Is.False);
            Assert.That(player.CardCount, Is.EqualTo(0));
        }

        [Test]
        public void Constructor_WithAI_SetsAIFlag()
        {
            var player = new Player("Компьютер", true);

            Assert.That(player.IsAI, Is.True);
        }

        [Test]
        public void TakeCards_AddsCardsToHand()
        {
            var player = new Player("Тест");
            var cards = new List<Card>
            {
                new Card(Suit.Hearts, Rank.Six),
                new Card(Suit.Diamonds, Rank.Seven)
            };

            player.TakeCards(cards);

            Assert.That(player.CardCount, Is.EqualTo(2));
        }

        [Test]
        public void DiscardAllCards_ClearsHand()
        {
            var player = new Player("Тест");
            var cards = new List<Card> { new Card(Suit.Hearts, Rank.Six) };
            player.TakeCards(cards);

            player.DiscardAllCards();

            Assert.That(player.CardCount, Is.EqualTo(0));
        }

        [Test]
        public void RemoveCard_RemovesSpecificCard()
        {
            var player = new Player("Тест");
            var card = new Card(Suit.Hearts, Rank.Six);
            player.TakeCards(new List<Card> { card });

            bool removed = player.RemoveCard(card);

            Assert.That(removed, Is.True);
            Assert.That(player.CardCount, Is.EqualTo(0));
        }

        [Test]
        public void GetCardsThatCanBeat_ReturnsCorrectCards()
        {
            var player = new Player("Тест");
            var attackCard = new Card(Suit.Hearts, Rank.Six);
            var trumpCard = new Card(Suit.Diamonds, Rank.Six);

            var winningCard = new Card(Suit.Hearts, Rank.Seven);
            var losingCard = new Card(Suit.Hearts, Rank.Six);

            player.TakeCards(new List<Card> { winningCard, losingCard });

            var canBeat = player.GetCardsThatCanBeat(attackCard, trumpCard);

            Assert.That(canBeat.Count, Is.EqualTo(1));
            Assert.That(canBeat[0], Is.EqualTo(winningCard));
        }

        [Test]
        public void HasCards_WhenHandEmpty_ReturnsFalse()
        {
            var player = new Player("Тест");

            Assert.That(player.HasCards(), Is.False);
        }

        [Test]
        public void HasCards_WhenHandNotEmpty_ReturnsTrue()
        {
            var player = new Player("Тест");
            player.TakeCards(new List<Card> { new Card(Suit.Hearts, Rank.Six) });

            Assert.That(player.HasCards(), Is.True);
        }

        [Test]
        public void GetLowestCard_ReturnsSmallestRank()
        {
            var player = new Player("Тест");
            player.TakeCards(new List<Card>
            {
                new Card(Suit.Hearts, Rank.King),
                new Card(Suit.Diamonds, Rank.Six),
                new Card(Suit.Clubs, Rank.Ten)
            });

            var lowest = player.GetLowestCard();

            Assert.That(lowest.Rank, Is.EqualTo(Rank.Six));
        }

        [Test]
        public void GetLowestWinningCard_ReturnsSmallestWinningCard()
        {
            var player = new Player("Тест");
            var attackCard = new Card(Suit.Hearts, Rank.Six);
            var trumpCard = new Card(Suit.Diamonds, Rank.Six);

            player.TakeCards(new List<Card>
            {
                new Card(Suit.Hearts, Rank.Seven),   // Подходит, бьет
                new Card(Suit.Hearts, Rank.King),    // Подходит, бьет
                new Card(Suit.Spades, Rank.Ace)      // Не подходит
            });

            var winning = player.GetLowestWinningCard(attackCard, trumpCard);

            Assert.That(winning.Rank, Is.EqualTo(Rank.Seven));
        }
    }
}

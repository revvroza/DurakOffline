using DurakOffline;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTests
{
    [TestFixture]
    public class DeckTests
    {
        [Test]
        public void Constructor_Creates36Cards()
        {
            var deck = new Deck();
            Assert.That(deck.Count, Is.EqualTo(36));
        }

        [Test]
        public void Shuffle_ChangesCardOrder()
        {
            var deck1 = new Deck();
            var deck2 = new Deck();

            deck1.Shuffle();
            deck2.Shuffle();

            var cards1 = deck1.GetCards().Select(c => c.ToString()).ToList();
            var cards2 = deck2.GetCards().Select(c => c.ToString()).ToList();

            Assert.That(cards1, Is.Not.EqualTo(cards2));
        }

        [Test]
        public void RevealTrump_SetsTrumpCard()
        {
            var deck = new Deck();
            deck.Shuffle();
            deck.RevealTrump();

            Assert.That(deck.TrumpCard, Is.Not.Null);
            Assert.That(deck.TrumpCard.IsTrump, Is.True);
        }

        [Test]
        public void RevealTrump_WhenDeckEmpty_ThrowsException()
        {
            var deck = new Deck();
            for (int i = 0; i < 36; i++)
            {
                deck.DealCards(1);
            }

            Assert.Throws<InvalidOperationException>(() => deck.RevealTrump());
        }

        [Test]
        public void DealCards_RemovesCorrectNumberOfCards()
        {
            var deck = new Deck();
            int initialCount = deck.Count;

            var cards = deck.DealCards(6);

            Assert.That(cards.Count, Is.EqualTo(6));
            Assert.That(deck.Count, Is.EqualTo(initialCount - 6));
        }

        [Test]
        public void DealCards_WhenNotEnoughCards_ThrowsException()
        {
            var deck = new Deck();

            Assert.Throws<InvalidOperationException>(() => deck.DealCards(40));
        }

        [Test]
        public void AddCardsToBottom_AddsCardsToDeck()
        {
            var deck = new Deck();
            var cards = deck.DealCards(6);
            int countAfterDeal = deck.Count;

            deck.AddCardsToBottom(cards);

            Assert.That(deck.Count, Is.EqualTo(countAfterDeal + 6));
        }
    }
}

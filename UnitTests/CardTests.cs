using DurakOffline;

namespace UnitTests
{
    public class UnitTests
    {
        [TestFixture]
        public class CardTests
        {
            [Test]
            public void ToString_ReturnsCorrectFormat()
            {
                var card = new Card(Suit.Hearts, Rank.Six);
                Assert.That(card.ToString(), Is.EqualTo("6♥"));
            }

            [Test]
            public void CanBeat_SameSuitHigherRank_ReturnsTrue()
            {
                var attack = new Card(Suit.Hearts, Rank.Six);
                var defend = new Card(Suit.Hearts, Rank.Seven);
                var trump = new Card(Suit.Diamonds, Rank.Six);

                bool result = defend.CanBeat(attack, trump);

                Assert.That(result, Is.True);
            }

            [Test]
            public void CanBeat_SameSuitLowerRank_ReturnsFalse()
            {
                var attack = new Card(Suit.Hearts, Rank.Seven);
                var defend = new Card(Suit.Hearts, Rank.Six);
                var trump = new Card(Suit.Diamonds, Rank.Six);

                bool result = defend.CanBeat(attack, trump);

                Assert.That(result, Is.False);
            }

            [Test]
            public void CanBeat_AttackNonTrump_DefendWithTrump_ReturnsTrue()
            {
                var attack = new Card(Suit.Hearts, Rank.Ace);
                var defend = new Card(Suit.Diamonds, Rank.Six);
                var trump = new Card(Suit.Diamonds, Rank.Six);

                bool result = defend.CanBeat(attack, trump);

                Assert.That(result, Is.True);
            }

            [Test]
            public void CanBeat_AttackTrump_DefendLowerTrump_ReturnsFalse()
            {
                var attack = new Card(Suit.Diamonds, Rank.King);
                var defend = new Card(Suit.Diamonds, Rank.Six);
                var trump = new Card(Suit.Diamonds, Rank.Six);

                attack.IsTrump = true;
                defend.IsTrump = true;

                bool result = defend.CanBeat(attack, trump);

                Assert.That(result, Is.False);
            }

            [Test]
            public void CanBeat_AttackTrump_DefendHigherTrump_ReturnsTrue()
            {
                var attack = new Card(Suit.Diamonds, Rank.Six);
                var defend = new Card(Suit.Diamonds, Rank.King);
                var trump = new Card(Suit.Diamonds, Rank.Six);

                attack.IsTrump = true;
                defend.IsTrump = true;

                bool result = defend.CanBeat(attack, trump);

                Assert.That(result, Is.True);
            }
        }
    }
}

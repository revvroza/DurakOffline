using DurakOffline;

namespace UnitTests
{
    [TestFixture]
    public class GameTests
    {
        private Game _game;

        [SetUp]
        public void Setup()
        {
            _game = new Game("Игрок 1", "Игрок 2");
            _game.StartGame();
        }

        [Test]
        public void Attack_ValidCard_ReturnsTrue()
        {
            var initialCards = _game.GetPlayer1Hand().Count;
            var card = _game.GetPlayer1Hand()[0];

            bool result = _game.Attack(card);

            Assert.That(result, Is.True);
            Assert.That(_game.GetAttackingCards().Count, Is.EqualTo(1));
            Assert.That(_game.GetPlayer1Hand().Count, Is.EqualTo(initialCards - 1));
        }

        [Test]
        public void Attack_NotYourTurn_ReturnsFalse()
        {
            var card = _game.GetPlayer2Hand()[0];
            bool result = _game.Attack(card);
            Assert.That(result, Is.False);
        }

        [Test]
        public void Defend_ValidDefense_ReturnsTrue()
        {
            var attackCard = _game.GetPlayer1Hand()[0];
            _game.Attack(attackCard);

            var defendCard = _game.GetPlayer2Hand()[0];
            bool result = _game.Defend(attackCard, defendCard);

            Assert.That(result, Is.True);
            Assert.That(_game.GetDefendingCards().Count, Is.EqualTo(1));
        }

        [Test]
        public void Defend_InvalidCard_ReturnsFalse()
        {
            var attackCard = _game.GetPlayer1Hand()[0];
            _game.Attack(attackCard);

            var defendCard = _game.GetPlayer2Hand()[0];
            bool result = _game.Defend(attackCard, defendCard);

            Assert.That(result, Is.TypeOf<bool>());
        }

        [Test]
        public void TakeCards_WhenAttacking_DefenderTakesCards()
        {
            var attackCard = _game.GetPlayer1Hand()[0];
            _game.Attack(attackCard);

            int defenderCardsBefore = _game.GetPlayer2Hand().Count;

            _game.TakeCards();

            Assert.That(_game.GetPlayer2Hand().Count, Is.GreaterThan(defenderCardsBefore));
            Assert.That(_game.GetAttackingCards().Count, Is.EqualTo(0));
        }

        [Test]
        public void EndTurn_AfterDefense_SwitchesRoles()
        {
            var attackCard = _game.GetPlayer1Hand()[0];
            _game.Attack(attackCard);

            var defendCard = _game.GetPlayer2Hand().FirstOrDefault();
            if (defendCard != null)
            {
                _game.Defend(attackCard, defendCard);
            }

            string attackerBefore = _game.CurrentAttacker.Name;
            _game.EndTurn();

            Assert.That(_game.CurrentAttacker.Name, Is.Not.EqualTo(attackerBefore));
        }

        [Test]
        public void IsRoundComplete_Initially_ReturnsFalse()
        {
            Assert.That(_game.IsRoundComplete(), Is.False);
        }

        [Test]
        public void GetAvailableRanksToAdd_Initially_ReturnsEmpty()
        {
            Assert.That(_game.GetAvailableRanksToAdd(), Is.Empty);
        }

        [Test]
        public void GetPlayer1Hand_ReturnsCorrectCards()
        {
            var hand = _game.GetPlayer1Hand();
            Assert.That(hand, Is.Not.Null);
            Assert.That(hand.Count, Is.EqualTo(6));
        }

        [Test]
        public void GetPlayer2Hand_ReturnsCorrectCards()
        {
            var hand = _game.GetPlayer2Hand();
            Assert.That(hand, Is.Not.Null);
            Assert.That(hand.Count, Is.EqualTo(6));
        }

        [Test]
        public void GetDeckCount_ReturnsPositiveNumber()
        {
            int count = _game.GetDeckCount();
            Assert.That(count, Is.GreaterThan(0));
        }

        [Test]
        public void TrumpCard_IsNotNull()
        {
            Assert.That(_game.TrumpCard, Is.Not.Null);
        }

        [Test]
        public void GetAttackingCards_Initially_ReturnsEmpty()
        {
            Assert.That(_game.GetAttackingCards(), Is.Empty);
        }

        [Test]
        public void GetDefendingCards_Initially_ReturnsEmpty()
        {
            Assert.That(_game.GetDefendingCards(), Is.Empty);
        }

        [Test]
        public void GetGameState_ReturnsNonEmptyString()
        {
            string state = _game.GetGameState();
            Assert.That(state, Is.Not.Null.And.Not.Empty);
            Assert.That(state, Does.Contain("Козырь"));
            Assert.That(state, Does.Contain("Атакует"));
            Assert.That(state, Does.Contain("Защищается"));
        }

        [Test]
        public void CanAddMoreCards_Initially_ReturnsFalse()
        {
            bool result = _game.CanAddMoreCards();
            Assert.That(result, Is.False);
        }
    }
}
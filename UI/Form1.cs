using DurakOffline;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        private Game _game;
        private bool _isPlayer1HandVisible;
        private bool _isPlayer2HandVisible;

        //Новое.Хранит карты для множественной атаки
        private List<Card> _selectedCards = new List<Card>();

        public Form1()
        {
            InitializeComponent();
            this.Text = "Дурак - Hot Seat";

            panelPlayerHand.Visible = false;
            panelTable.BackColor = Color.DarkGreen;

            _isPlayer1HandVisible = false;
            _isPlayer2HandVisible = false;

            btnPlayer1Open.Enabled = false;
            btnPlayer2Open.Enabled = false;
            btnTakeCards.Enabled = false;
            btnEndTurn.Enabled = false;
            btnMultiAttack.Enabled = false;

            btnNewGame.Click += BtnStartGame_Click;
            btnPlayer1Open.Click += (s, e) => TogglePlayerCards(1);
            btnPlayer2Open.Click += (s, e) => TogglePlayerCards(2);
            btnTakeCards.Click += BtnTakeCards_Click;
            btnEndTurn.Click += BtnEndTurn_Click;
            btnMultiAttack.Click += BtnMultiAttack_Click;
        }

        private void BtnStartGame_Click(object sender, EventArgs e)
        {
            _game = new Game("Игрок 1", "Игрок 2");
            _game.StartGame();
            UpdateUI();
            ClearTableDisplay();
            ShowMessage("Игра начата! Ход игрока 1");

            lblMessage.ForeColor = SystemColors.ControlText;
            lblMessage.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular);

            _isPlayer1HandVisible = false;
            _isPlayer2HandVisible = false;
            panelPlayerHand.Visible = false;
            panelPlayerHand.Controls.Clear();
            btnPlayer1Open.Text = "Игрок1: Открыть";
            btnPlayer1Open.BackColor = Color.Green;
            btnPlayer2Open.Text = "Игрок2: Открыть";
            btnPlayer2Open.BackColor = Color.Green;

            btnPlayer1Open.Enabled = true;
            btnPlayer2Open.Enabled = true;
            btnTakeCards.Enabled = true;
            btnEndTurn.Enabled = true;
        }

        private void TogglePlayerCards(int player)
        {
            if (_game == null) { ShowMessage("Сначала начните игру!"); return; }

            bool isDefending = _game.GetAttackingCards().Count > 0 &&
                               _game.GetAttackingCards().Count > _game.GetDefendingCards().Count();
            int expectedPlayer = isDefending ? (_game.CurrentDefender.Name == "Игрок 1" ? 0 : 1) : (_game.CurrentAttacker.Name == "Игрок 1" ? 0 : 1);

            if ((player == 1 && expectedPlayer != 0) || (player == 2 && expectedPlayer != 1))
            {
                ShowMessage($"Сейчас не ваш ход! Должен ходить игрок {(expectedPlayer == 0 ? "1" : "2")}");
                return;
            }

            if (player == 1)
            {
                if (_isPlayer1HandVisible)
                {
                    panelPlayerHand.Visible = false;
                    panelPlayerHand.Controls.Clear();
                    _isPlayer1HandVisible = false;
                    btnPlayer1Open.Text = "Игрок1: Открыть";
                    btnPlayer1Open.BackColor = Color.Green;
                    ShowMessage("Колода закрыта. Передайте ход другому игроку");
                }
                else
                {
                    var hand = _game.GetPlayer1Hand();
                    DisplayPlayerCards(hand, 1);
                    _isPlayer1HandVisible = true;
                    btnPlayer1Open.Text = "Игрок1: Закрыть";
                    btnPlayer1Open.BackColor = Color.Orange;
                }
            }
            else if (player == 2)
            {
                if (_isPlayer2HandVisible)
                {
                    panelPlayerHand.Visible = false;
                    panelPlayerHand.Controls.Clear();
                    _isPlayer2HandVisible = false;
                    btnPlayer2Open.Text = "Игрок2: Открыть";
                    btnPlayer2Open.BackColor = Color.Green;
                    ShowMessage("Колода закрыта. Передайте ход другому игроку");
                }
                else
                {
                    var hand = _game.GetPlayer2Hand();
                    DisplayPlayerCards(hand, 2);
                    _isPlayer2HandVisible = true;
                    btnPlayer2Open.Text = "Игрок2: Закрыть";
                    btnPlayer2Open.BackColor = Color.Orange;
                }
            }
        }

        //Переработано.Добавлена множественная атака
        private void DisplayPlayerCards(List<Card> hand, int player)
        {
            var availableRanks = _game.GetAvailableRanksToAdd();
            bool isAddingPhase = _game.GetAttackingCards().Count > 0 &&
                                 _game.GetAttackingCards().Count == _game.GetDefendingCards().Count();

            panelPlayerHand.Controls.Clear();
            _selectedCards.Clear();
            btnMultiAttack.Enabled = false;

            bool canMultiSelect = _game.GetAttackingCards().Count == 0;

            foreach (var card in hand)
            {
                bool canPlay = true;
                if (isAddingPhase && _game.GetAttackingCards().Count > 0)
                {
                    canPlay = availableRanks.Contains(card.Rank);
                }

                var cardControl = new CardControl(card, canPlay);

                if (canPlay)
                {
                    if (canMultiSelect)
                    {
                        cardControl.CardClicked += (s, c) => ToggleCardSelection(c);
                        cardControl.BackColor = Color.LightGreen;
                    }
                    else
                    {
                        cardControl.CardClicked += (s, c) => OnCardSelected(c);
                        cardControl.BackColor = Color.LightGreen;
                    }
                }
                else
                {
                    cardControl.BackColor = Color.LightGray;
                }

                panelPlayerHand.Controls.Add(cardControl);
            }

            panelPlayerHand.Visible = true;

            if (canMultiSelect && hand.Count > 0)
            {
                ShowMessage("Выберите карты одного достоинства, затем нажмите 'Атака'");
            }

            if (isAddingPhase && _game.GetAttackingCards().Count > 0)
            {
                string ranksText = string.Join(", ", availableRanks.Select(r => r.ToString()));
                ShowMessage($"Можно подкинуть карты достоинством: {ranksText}");
            }
        }

        private void HidePlayerCards()
        {
            panelPlayerHand.Visible = false;
            panelPlayerHand.Controls.Clear();
            _isPlayer1HandVisible = false;
            _isPlayer2HandVisible = false;
            btnPlayer1Open.Text = "Игрок1: Открыть";
            btnPlayer1Open.BackColor = Color.Green;
            btnPlayer2Open.Text = "Игрок2: Открыть";
            btnPlayer2Open.BackColor = Color.Green;
            btnMultiAttack.Enabled = false;
            _selectedCards.Clear();
            ShowMessage("Передайте ход другому игроку. Нажмите 'Открыть' когда будет ваш ход");
        }

        private void OnCardSelected(Card card)
        {
            if (_game == null) return;

            bool isDefending = _game.GetAttackingCards().Count > 0 &&
                               _game.GetAttackingCards().Count > _game.GetDefendingCards().Count();

            if (isDefending)
            {
                var attackCard = _game.GetAttackingCards().Skip(_game.GetDefendingCards().Count).FirstOrDefault();
                if (attackCard != null && _game.Defend(attackCard, card))
                {
                    UpdateUI();
                    UpdateTableDisplay();
                    ShowMessage($"Защита картой {card} успешна!");
                    HidePlayerCards();  
                }
                else
                {
                    ShowMessage($"Нельзя защититься картой {card}!");
                }
            }
            else if (_game.GetAttackingCards().Count > 0 && _game.GetAttackingCards().Count == _game.GetDefendingCards().Count)
            {
                if (_game.Attack(card))
                {
                    UpdateUI();
                    UpdateTableDisplay();
                    ShowMessage($"Подкинута карта {card}");
                    HidePlayerCards();  
                }
                else
                {
                    ShowMessage($"Нельзя подкинуть карту {card}!");
                }
            }
            else
            {
                if (_game.Attack(card))
                {
                    UpdateUI();
                    UpdateTableDisplay();
                    ShowMessage($"Атака картой {card}");
                    HidePlayerCards(); 
                }
                else
                {
                    ShowMessage($"Нельзя атаковать картой {card}!");
                }
            }
        }

        private void BtnTakeCards_Click(object sender, EventArgs e)
        {
            if (_game == null) return;
            _game.TakeCards();
            UpdateUI();
            ClearTableDisplay();
            HidePlayerCards();
            ShowMessage("Карты взяты! Ход остается у атакующего");
        }

        private void BtnEndTurn_Click(object sender, EventArgs e)
        {
            if (_game == null) return;

            if (_game.GetAttackingCards().Count != _game.GetDefendingCards().Count || _game.GetAttackingCards().Count == 0)
            {
                ShowMessage("Нельзя завершить ход! Сначала отбейте все атаки или возьмите карты");
                return;
            }

            _game.EndTurn();
            UpdateUI();
            ClearTableDisplay();
            HidePlayerCards();
            ShowMessage("Ход завершен! Карты ушли в отбой");
        }

        private Button CreateCardButton(Card card, bool clickable)
        {
            return new Button { Text = card.ToString(), Size = new Size(60, 70) };
        }

        private void UpdateTableDisplay()
        {
            if (panelTable == null) return;
            panelTable.Controls.Clear();

            var attackingCards = _game.GetAttackingCards();
            var defendingCards = _game.GetDefendingCards();

            int startX = 50;
            int startY = 20;
            int cardWidth = 80;
            int cardHeight = 110;
            int spacing = 15;

            for (int i = 0; i < attackingCards.Count; i++)
            {
                int x = startX + i * (cardWidth + spacing);

                var attackCardControl = new CardControl(attackingCards[i], false);
                attackCardControl.Location = new Point(x, startY + cardHeight - 35);
                panelTable.Controls.Add(attackCardControl);

                if (i < defendingCards.Count)
                {
                    var defendCardControl = new CardControl(defendingCards[i], false);
                    defendCardControl.Location = new Point(x, startY);
                    panelTable.Controls.Add(defendCardControl);
                    defendCardControl.BringToFront();
                }
                else
                {
                    var emptySpot = new Panel
                    {
                        Size = new Size(cardWidth, cardHeight),
                        Location = new Point(x, startY),
                        BackColor = Color.Transparent,
                        BorderStyle = BorderStyle.None
                    };
                    emptySpot.Paint += (s, ev) =>
                    {
                        using (Pen dashPen = new Pen(Color.Gray, 2) { DashStyle = DashStyle.Dash })
                        {
                            ev.Graphics.DrawRectangle(dashPen, 2, 2, cardWidth - 4, cardHeight - 4);
                        }

                        using (Font qFont = new Font("Arial", 24, FontStyle.Bold))
                        using (Brush brush = new SolidBrush(Color.Gray))
                        {
                            SizeF textSize = ev.Graphics.MeasureString("?", qFont);
                            float xText = (cardWidth - textSize.Width) / 2;
                            float yText = (cardHeight - textSize.Height) / 2;
                            ev.Graphics.DrawString("?", qFont, brush, xText, yText);
                        }
                    };
                    panelTable.Controls.Add(emptySpot);
                    emptySpot.BringToFront();
                }
            }
        }

        private void ClearTableDisplay()
        {
            if (panelTable != null) panelTable.Controls.Clear();
        }

        private void UpdateUI()
        {
            if (_game == null) return;

            lblTrump.Text = $"Козырь: {_game.TrumpCard}";

            if (_game.IsGameOver)
            {
                var winner = _game.Winner;
                string winnerName = winner?.Name ?? "Ничья";

                MessageBox.Show($"Победил {winnerName}!\nНажмите 'Новая игра' чтобы продолжить",
                    "Игра окончена",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                btnPlayer1Open.Enabled = false;
                btnPlayer2Open.Enabled = false;
                btnTakeCards.Enabled = false;
                btnEndTurn.Enabled = false;

                panelPlayerHand.Visible = false;
                panelPlayerHand.Controls.Clear();

                btnPlayer1Open.Text = "Игрок1: Открыть";
                btnPlayer2Open.Text = "Игрок2: Открыть";
                btnPlayer1Open.BackColor = Color.Green;
                btnPlayer2Open.BackColor = Color.Green;

                return;
            }

            lblMessage.ForeColor = SystemColors.ControlText;
            lblMessage.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular);

            int attackingCount = _game.GetAttackingCards().Count;
            int defendingCount = _game.GetDefendingCards().Count;

            if (attackingCount == 0)
            {
                lblTurn.Text = $"Ход: {_game.CurrentAttacker.Name} - Атакуйте!";
                btnTakeCards.Enabled = false;
                btnEndTurn.Enabled = false;
            }
            else if (attackingCount > defendingCount)
            {
                lblTurn.Text = $"Ход: {_game.CurrentDefender.Name} - Защищайтесь!";
                btnTakeCards.Enabled = true;
                btnEndTurn.Enabled = false;
            }
            else if (attackingCount == defendingCount && attackingCount > 0)
            {
                lblTurn.Text = $"Ход: {_game.CurrentAttacker.Name} - Можете подкинуть или закончить ход (Бита)";
                btnTakeCards.Enabled = false;
                btnEndTurn.Enabled = true;
            }

            this.Text = $"Дурак | Игрок 1: {_game.GetPlayer1Hand().Count} | Игрок 2: {_game.GetPlayer2Hand().Count} | Колода: {_game.GetDeckCount()}";
        }

        private void ShowMessage(string msg)
        {
            lblMessage.Text = msg;
        }

        private void panelPlayerHand_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "Вы уверены, что хотите выйти из игры?",
            "Выход",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); 
            }
        }

        //Новое. Выбор нескольких карт одного достоинства для атаки
        private void ToggleCardSelection(Card card)
        {
            if (_selectedCards.Contains(card))
            {
                _selectedCards.Remove(card);
                ShowMessage($"Карта {card} отменена. Выбрано: {_selectedCards.Count}");
            }
            else
            {
                if (_selectedCards.Count > 0 && _selectedCards[0].Rank != card.Rank)
                {
                    ShowMessage("Можно выбирать только карты одного достоинства!");
                    return;
                }

                _selectedCards.Add(card);
                ShowMessage($"Карта {card} выбрана. Выбрано: {_selectedCards.Count}");
            }

            btnMultiAttack.Enabled = _selectedCards.Count > 0;
        }

        //Новая кнопка для атаки        
        private void BtnMultiAttack_Click(object sender, EventArgs e)
        {
            if (_game == null || _selectedCards.Count == 0) return;

            bool success = true;
            foreach (var card in _selectedCards)
            {
                if (!_game.Attack(card))
                {
                    success = false;
                    break;
                }
            }

            if (success)
            {
                UpdateUI();
                UpdateTableDisplay();
                ShowMessage($"Атака {_selectedCards.Count} картами!");

                _selectedCards.Clear();
                btnMultiAttack.Enabled = false;
                _isPlayer1HandVisible = false;
                _isPlayer2HandVisible = false;
                panelPlayerHand.Visible = false;
                btnPlayer1Open.Text = "Игрок1: Открыть";
                btnPlayer1Open.BackColor = Color.Green;
                btnPlayer2Open.Text = "Игрок2: Открыть";
                btnPlayer2Open.BackColor = Color.Green;
            }
            else
            {
                ShowMessage("Не удалось атаковать выбранными картами!");
                _selectedCards.Clear();
                btnMultiAttack.Enabled = false;
            }
        }
    }
}
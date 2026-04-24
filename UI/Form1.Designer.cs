namespace UI
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            btnNewGame = new Button();
            btnPlayer1Open = new Button();
            btnPlayer2Open = new Button();
            btnTakeCards = new Button();
            btnEndTurn = new Button();
            lblTrump = new Label();
            lblTurn = new Label();
            lblMessage = new Label();
            panelTable = new Panel();
            panelPlayerHand = new FlowLayoutPanel();
            btnExit = new Button();
            SuspendLayout();

            // btnNewGame

            btnNewGame.BackColor = Color.Green;
            btnNewGame.Font = new Font("Times New Roman", 19.8000011F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnNewGame.Location = new Point(467, 19);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(455, 72);
            btnNewGame.TabIndex = 0;
            btnNewGame.Text = "Новая игра";
            btnNewGame.UseVisualStyleBackColor = false;

            // btnPlayer1Open

            btnPlayer1Open.BackColor = Color.Green;
            btnPlayer1Open.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnPlayer1Open.Location = new Point(35, 611);
            btnPlayer1Open.Name = "btnPlayer1Open";
            btnPlayer1Open.Size = new Size(225, 57);
            btnPlayer1Open.TabIndex = 1;
            btnPlayer1Open.Text = "Игрок1: Открыть";
            btnPlayer1Open.UseVisualStyleBackColor = false;
 
            // btnPlayer2Open
  
            btnPlayer2Open.BackColor = Color.Green;
            btnPlayer2Open.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnPlayer2Open.Location = new Point(1071, 611);
            btnPlayer2Open.Name = "btnPlayer2Open";
            btnPlayer2Open.Size = new Size(225, 57);
            btnPlayer2Open.TabIndex = 2;
            btnPlayer2Open.Text = "Игрок2: Открыть";
            btnPlayer2Open.UseVisualStyleBackColor = false;
          
            // btnTakeCards
           
            btnTakeCards.BackColor = Color.Green;
            btnTakeCards.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnTakeCards.Location = new Point(397, 611);
            btnTakeCards.Name = "btnTakeCards";
            btnTakeCards.Size = new Size(225, 57);
            btnTakeCards.TabIndex = 3;
            btnTakeCards.Text = "Взять карты";
            btnTakeCards.UseVisualStyleBackColor = false;
           
            // btnEndTurn
          
            btnEndTurn.BackColor = Color.Green;
            btnEndTurn.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnEndTurn.Location = new Point(732, 611);
            btnEndTurn.Name = "btnEndTurn";
            btnEndTurn.Size = new Size(225, 57);
            btnEndTurn.TabIndex = 4;
            btnEndTurn.Text = "Бита";
            btnEndTurn.UseVisualStyleBackColor = false;
           
            // lblTrump
            
            lblTrump.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTrump.Location = new Point(36, 45);
            lblTrump.Name = "lblTrump";
            lblTrump.Size = new Size(338, 46);
            lblTrump.TabIndex = 5;
            lblTrump.Text = "Козырь:";
            
            // lblTurn
            
            lblTurn.Font = new Font("Times New Roman", 18F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblTurn.Location = new Point(36, 111);
            lblTurn.Name = "lblTurn";
            lblTurn.Size = new Size(441, 87);
            lblTurn.TabIndex = 6;
            lblTurn.Text = "Ход:";
            
            // lblMessage
            
            lblMessage.Font = new Font("Times New Roman", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 204);
            lblMessage.Location = new Point(36, 683);
            lblMessage.Name = "lblMessage";
            lblMessage.Size = new Size(1261, 102);
            lblMessage.TabIndex = 7;
            lblMessage.TextAlign = ContentAlignment.MiddleCenter;
           
            // panelTable
           
            panelTable.Location = new Point(244, 174);
            panelTable.Name = "panelTable";
            panelTable.Size = new Size(849, 261);
            panelTable.TabIndex = 8;
            
            // panelPlayerHand
            
            panelPlayerHand.Location = new Point(36, 452);
            panelPlayerHand.Name = "panelPlayerHand";
            panelPlayerHand.Size = new Size(1261, 145);
            panelPlayerHand.TabIndex = 9;
            
            // btnExit
            
            btnExit.BackColor = Color.Red;
            btnExit.Font = new Font("Times New Roman", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 204);
            btnExit.Location = new Point(1235, 19);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(105, 57);
            btnExit.TabIndex = 10;
            btnExit.Text = "Выход";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            
            // Form1
            
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.SeaGreen;
            ClientSize = new Size(1352, 801);
            Controls.Add(btnExit);
            Controls.Add(panelPlayerHand);
            Controls.Add(panelTable);
            Controls.Add(lblMessage);
            Controls.Add(lblTurn);
            Controls.Add(lblTrump);
            Controls.Add(btnEndTurn);
            Controls.Add(btnTakeCards);
            Controls.Add(btnPlayer2Open);
            Controls.Add(btnPlayer1Open);
            Controls.Add(btnNewGame);
            Name = "Form1";
            Text = "Дурак - Hot Seat";
            ResumeLayout(false);
        }

        private Button btnNewGame;
        private Button btnPlayer1Open;
        private Button btnPlayer2Open;
        private Button btnTakeCards;
        private Button btnEndTurn;
        private Label lblTrump;
        private Label lblTurn;
        private Label lblMessage;
        private Panel panelTable;
        private FlowLayoutPanel panelPlayerHand;
        private Button btnExit;
    }
}
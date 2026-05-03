using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using DurakOffline;

namespace UI
{
    public class CardControl : Panel
    {
        private Card _card;
        private bool _isClickable;
        private Font _rankFont = new Font("Arial", 14, FontStyle.Bold);
        private Font _smallFont = new Font("Arial", 10, FontStyle.Bold);

        public event EventHandler<Card> CardClicked;

        public CardControl(Card card, bool isClickable)
        {
            _card = card;
            _isClickable = isClickable;
            this.Size = new Size(80, 110);
            this.BackColor = Color.White;
            this.BorderStyle = BorderStyle.None; 
            this.Cursor = isClickable ? Cursors.Hand : Cursors.Default;

            this.Padding = new Padding(2);

            if (isClickable)
            {
                this.Click += OnClick;
            }

            this.Paint += OnPaint;
        }

        private void OnClick(object sender, EventArgs e)
        {
            CardClicked?.Invoke(this, _card);
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int cardWidth = Width - 2;
            int cardHeight = Height - 2;

            Rectangle shadowRect = new Rectangle(2, 2, cardWidth, cardHeight);
            using (GraphicsPath shadowPath = GetRoundedRectangle(shadowRect, 10))
            {
                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                {
                    g.FillPath(shadowBrush, shadowPath);
                }
            }

            Rectangle cardRect = new Rectangle(0, 0, cardWidth, cardHeight);
            using (GraphicsPath cardPath = GetRoundedRectangle(cardRect, 10))
            {
                using (SolidBrush bgBrush = new SolidBrush(_isClickable ? Color.LightGreen : Color.White))
                {
                    g.FillPath(bgBrush, cardPath);
                }

                using (Pen outlinePen = new Pen(Color.White, 2))
                {
                    g.DrawPath(outlinePen, cardPath);
                }

                using (Pen borderPen = new Pen(Color.Black, 1.5f))
                {
                    g.DrawPath(borderPen, cardPath);
                }
            }

            Color suitColor = (_card.Suit == Suit.Hearts || _card.Suit == Suit.Diamonds) ? Color.Red : Color.Black;

            string suitSymbol = _card.Suit switch
            {
                Suit.Hearts => "♥",
                Suit.Diamonds => "♦",
                Suit.Clubs => "♣",
                Suit.Spades => "♠",
                _ => ""
            };

            string rankStr = _card.Rank switch
            {
                Rank.Six => "6",
                Rank.Seven => "7",
                Rank.Eight => "8",
                Rank.Nine => "9",
                Rank.Ten => "10",
                Rank.Jack => "В",
                Rank.Queen => "Д",
                Rank.King => "К",
                Rank.Ace => "Т",
                _ => ""
            };

            using (Brush brush = new SolidBrush(suitColor))
            {
                g.DrawString(rankStr, _rankFont, brush, 5, 5);
                g.DrawString(suitSymbol, _smallFont, brush, 5, 28);
            }

            using (Font bigFont = new Font("Arial", 36, FontStyle.Bold))
            using (Brush brush = new SolidBrush(suitColor))
            {
                SizeF textSize = g.MeasureString(suitSymbol, bigFont);
                float x = (Width - textSize.Width) / 2;
                float y = (Height - textSize.Height) / 2;
                g.DrawString(suitSymbol, bigFont, brush, x, y);
            }

            using (Brush brush = new SolidBrush(suitColor))
            {
                g.TranslateTransform(Width - 5, Height - 5);
                g.RotateTransform(180);
                g.DrawString(rankStr, _rankFont, brush, 0, 0);
                g.DrawString(suitSymbol, _smallFont, brush, 0, 23);
                g.ResetTransform();
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
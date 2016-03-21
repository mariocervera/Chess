using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace Chess
{
    /*
    * This class implements a Form that is shown when a Pawn reaches its last posible square.
    * The Form allows the player to transform the pawn into another piece.
    */
    public class FormPromotion : System.Windows.Forms.Form
    {
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxFigures;
        private System.Windows.Forms.Button buttonAccept;

        private System.ComponentModel.Container components = null;

        private Pawn pawn; // Promoting pawn
        private bool promotionRealized = false;

        public FormPromotion(Pawn p)
        {
            InitializeComponent();

            this.pawn = p;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms
        
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxFigures = new System.Windows.Forms.ComboBox();
            this.buttonAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select a figure";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBoxFigures
            // 
            this.comboBoxFigures.Items.AddRange(new object[] {
                                                                 "Bishop",
                                                                 "Knight",
                                                                 "Queen",
                                                                 "Rook"});
            this.comboBoxFigures.Location = new System.Drawing.Point(48, 56);
            this.comboBoxFigures.Name = "comboBoxFigures";
            this.comboBoxFigures.Size = new System.Drawing.Size(121, 21);
            this.comboBoxFigures.TabIndex = 1;
            // 
            // buttonAccept
            // 
            this.buttonAccept.Location = new System.Drawing.Point(192, 56);
            this.buttonAccept.Name = "buttonAccept";
            this.buttonAccept.TabIndex = 2;
            this.buttonAccept.Text = "Ok";
            this.buttonAccept.Click += new System.EventHandler(this.buttonAccept_Click);
            // 
            // FormPromotion
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(296, 117);
            this.Controls.Add(this.buttonAccept);
            this.Controls.Add(this.comboBoxFigures);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(304, 144);
            this.MinimumSize = new System.Drawing.Size(304, 144);
            this.Name = "FormPromotion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Promotion";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FormPromotion_Closing);
            this.ResumeLayout(false);

        }
        #endregion


        private void buttonAccept_Click(object sender, System.EventArgs e)
        {
            //Button "Accept" clicked, carry out the promotion

            if (comboBoxFigures.SelectedItem == null)
            {
                MessageBox.Show(this, "You must select a figure", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {

                promotionRealized = true;

                //Delete the pawn

                Board.getInstance().killPiece(pawn.getPosition(), true);

                //Create the new figure

                string fig = comboBoxFigures.SelectedItem.ToString();
                Position pos = pawn.getPosition();
                Color color = pawn.getColor();
                Piece piece = null;

                if (fig == "Bishop")
                {
                    piece = new Bishop(pos, color);
                }
                else if (fig == "Knight")
                {
                    piece = new Knight(pos, color);
                }
                else if (fig == "Queen")
                {
                    piece = new Queen(pos, color);
                }
                else if (fig == "Rook")
                {
                    piece = new Rook(pos, color);
                }

                if (color == Color.White)
                {
                    Board.getInstance().getWhitePieces().insert(piece);
                }
                else {
                    Board.getInstance().getBlackPieces().insert(piece);
                }

                Board.getInstance().deletePiece(pawn);
                Board.getInstance().drawPiece(piece);

                this.Close();
            }
        }

        private void FormPromotion_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!promotionRealized)
            {
                e.Cancel = true;
            }
        }
    }
}

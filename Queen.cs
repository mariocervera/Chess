using System;
using System.Collections;
using System.Drawing;

namespace Chess {

	public class Queen: Piece {

		public Queen(Position p, Color c) : base(p,c) {

            if (base.getColor() == Color.Black) {

                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackQueen.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhiteQueen.gif"));
            }
		}

        public override void updateAllowedMovements() {

            this.allowedMovements.clear();

            calculateLine(0, 1);
            calculateLine(1, 0);
            calculateLine(0, -1);
            calculateLine(-1, 0);

            calculateLine(1, 1);
            calculateLine(-1, -1);
            calculateLine(1, -1);
            calculateLine(-1, 1);
        }

        public override string ToString() {

            if (this.getColor() == Color.White) {

                return "White Queen";
            }

            return "Black Queen";
        }
    }
}

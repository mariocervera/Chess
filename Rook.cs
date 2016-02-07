using System;
using System.Collections;
using System.Drawing;

namespace Chess {

	public class Rook: Piece {

		public Rook(Position p, Color c) : base(p,c) {

            if (base.getColor() == Color.Black) {

                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackRook.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhiteRook.gif"));
            }
		}

		public override void updateAllowedMovements() {

			this.allowedMovements.clear();

            calculateLine(0, 1);
            calculateLine(1, 0);
            calculateLine(0, -1);
            calculateLine(-1, 0);
        }		

		public override string ToString() {

            if (this.getColor() == Color.White) {

                return "White Rook";
            }

			return "Black Rook";
		}
		
	}
}

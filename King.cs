using System;
using System.Collections;
using System.Drawing;

namespace Chess {
	
	public class King: Piece { 

		public King(Position p, Color c) : base(p,c) {

            if (base.getColor() == Color.Black) {

                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackKing.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhiteKing.gif"));
            }
		}

        public override void updateAllowedMovements() {

            this.allowedMovements.clear();

            fillSquare(1, 1);
            fillSquare(0, 1);
            fillSquare(1, -1);
            fillSquare(1, 0);
            fillSquare(-1, 1);
            fillSquare(0, -1);
            fillSquare(-1, -1);
            fillSquare(-1, 0);
        }

        public override string ToString() {

            if (this.getColor() == Color.White) {

                return "White King";
            }

            return "Black King";
        }
    }
}

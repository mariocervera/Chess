using System;
using System.Collections;
using System.Drawing;

namespace Chess {
	
	public class Knight: Piece {

		public Knight(Position p, Color c) : base(p,c) {

            if (base.getColor() == Color.Black) {

                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackKnight.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhiteKnight.gif"));
            }
		}

        public override void updateAllowedMovements() {

            this.allowedMovements.clear();

            fillSquare(1, 2);
            fillSquare(2, 1);
            fillSquare(1, -2);
            fillSquare(2, -1);
            fillSquare(-1, 2);
            fillSquare(-2, 1);
            fillSquare(-1, -2);
            fillSquare(-2, -1);
        }

        public override string ToString() {

            if (this.getColor() == Color.White) {

                return "White Knight";
            }

            return "Black Knight";
        }
    }
}
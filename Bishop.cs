using System;
using System.Collections;
using System.Drawing;

namespace Chess {
	
	public class Bishop: Piece {
		
		public Bishop(Position p, Color c) : base(p,c) {

            if (base.getColor() == Color.Black) {

                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackBishop.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhiteBishop.gif"));
            }
		}

        public override void updateAllowedMovements() {

            this.allowedMovements.clear();
            
            calculateLine(1, 1);
            calculateLine(-1, -1);
            calculateLine(1, -1);
            calculateLine(-1, 1);
        }

        public override string ToString() {

            if (this.getColor() == Color.White) {

                return "White Bishop";
            }

            return "Black Bishop";
        }
    }
}

using System;
using System.Collections;
using System.Drawing;

namespace Chess
{
    /*
    * The King class. Inherits attributes and methods that are common for all pieces (from Piece.cs).
    */
    public class King : Piece
    {
        public King(Position p, Color c) : base(p, c)
        {
            if (base.getColor() == Color.Black)
            {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackKing.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhiteKing.gif"));
            }
        }

        /*
         * This method updates the set of allowed movements. Implememts an abstract method from Piece.cs
         */
        public override void updateAllowedMovements()
        {
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

        public override string ToString()
        {
            if (this.getColor() == Color.White)
            {
                return "White King";
            }

            return "Black King";
        }
    }
}

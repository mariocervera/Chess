using System;
using System.Collections;
using System.Drawing;

namespace Chess
{
    /*
    * The Knight class. Inherits attributes and methods that are common for all pieces (from Piece.cs).
    */
    public class Knight : Piece
    {
        public Knight(Position p, Color c) : base(p, c)
        {
            if (base.getColor() == Color.Black)
            {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackKnight.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhiteKnight.gif"));
            }
        }

        /*
         * This method updates the set of allowed movements. Implememts an abstract method from Piece.cs
         */
        public override void updateAllowedMovements()
        {
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

        public override string ToString()
        {
            if (this.getColor() == Color.White)
            {
                return "White Knight";
            }

            return "Black Knight";
        }
    }
}
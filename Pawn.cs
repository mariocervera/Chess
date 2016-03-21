using System;
using System.Collections;
using System.Drawing;

namespace Chess
{
    /*
    * The Pawn class. Inherits attributes and methods that are common for all pieces (from Piece.cs).
    */
    public class Pawn : Piece
    {
        public Pawn(Position p, Color c) : base(p, c)
        {
            if (base.getColor() == Color.Black)
            {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//BlackPawn.gif"));
            }
            else {
                base.setImage(new Bitmap(Board.getInstance().getPath() + "//WhitePawn.gif"));
            }
        }

        /*
         * This method updates the set of allowed movements. Implememts an abstract method from Piece.cs
         */
        public override void updateAllowedMovements()
        {
            this.allowedMovements.clear();

            int row = position.getRow();
            int column = position.getColumn();
            int i, j;
            Position currentPos;

            if (this.color == Color.Black) //Black pawn
            { 
                //Down

                i = row + 1;
                j = column;

                if (i <= 7)
                {
                    currentPos = new Position(i, j);

                    if (Board.getInstance().consultPiece(currentPos, true) == null
                            && Board.getInstance().consultPiece(currentPos, false) == null)
                    {

                        this.allowedMovements.insert(currentPos);
                    }
                }

                //2 positions down for first movement

                if (row == 1 && Board.getInstance().consultPiece(new Position(2, column), true) == null
                        && Board.getInstance().consultPiece(new Position(2, column), false) == null
                        && Board.getInstance().consultPiece(new Position(3, column), true) == null)
                {
                    checkSquare(new Position(row + 2, column));
                }

                //Down-right diagonal, only to atack

                i = row + 1;
                j = column + 1;

                if (i < Board.NUM_ROWS && j < Board.NUM_ROWS)
                {
                    currentPos = new Position(i, j);

                    if (Board.getInstance().consultPiece(currentPos, true) != null) //Opponent's piece
                    { 
                        this.allowedMovements.insert(currentPos);
                    }
                }

                //Down-left diagonal, only to atack

                i = row + 1;
                j = column - 1;

                if (i < Board.NUM_ROWS && j >= 0)
                {
                    currentPos = new Position(i, j);

                    if (Board.getInstance().consultPiece(currentPos, true) != null) //Opponent's piece
                    { 
                        this.allowedMovements.insert(currentPos);
                    }
                }

            }

            else { // White pawn

                //Up

                i = row - 1;
                j = column;

                if (i >= 0)
                {
                    currentPos = new Position(i, j);
                    if (Board.getInstance().consultPiece(currentPos, true) == null
                        && Board.getInstance().consultPiece(currentPos, false) == null)
                    {
                        this.allowedMovements.insert(currentPos);
                    }

                }

                //Two positions up for first movement

                if (row == 6 && Board.getInstance().consultPiece(new Position(5, column), true) == null
                    && Board.getInstance().consultPiece(new Position(5, column), false) == null
                    && Board.getInstance().consultPiece(new Position(4, column), false) == null)
                {
                    checkSquare(new Position(row - 2, column));
                }

                //Up-right diagonal, only to attack

                i = row - 1;
                j = column + 1;

                if (i >= 0 && j < Board.NUM_ROWS)
                {
                    currentPos = new Position(i, j);

                    if (Board.getInstance().consultPiece(currentPos, false) != null) //Opponent's piece
                    { 

                        this.allowedMovements.insert(currentPos);
                    }
                }

                //Up-left diagonal, only to attack

                i = row - 1;
                j = column - 1;

                if (i >= 0 && j >= 0)
                {
                    currentPos = new Position(i, j);

                    if (Board.getInstance().consultPiece(currentPos, false) != null) // Opponent's piece
                    { 
                        this.allowedMovements.insert(currentPos);
                    }
                }

            }
        }

        public override string ToString()
        {
            if (this.getColor() == Color.White)
            {
                return "White Pawn";
            }

            return "Black Pawn";
        }
    }
}

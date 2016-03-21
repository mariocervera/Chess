using System;
using System.Collections;
using System.Drawing;

namespace Chess
{
    /*
    * This class represents the pieces of the chess board.
    *
    * Subclasses: King, Queen, Rook, Knight, Bishop, and Pawn.
    */
    public abstract class Piece
    {
        protected Position position;
        protected Color color;
        protected Image image;
        protected List allowedMovements;

        public Piece(Position p, Color c)
        {
            position = p;
            color = c;
            allowedMovements = new List();
        }

        //Getters and setters

        public Position getPosition()
        {
            return position;
        }

        public Color getColor()
        {
            return color;
        }

        public Image getImage()
        {
            return image;
        }

        public List getAllowedMovements()
        {
            return allowedMovements;
        }

        public void setPosition(Position p)
        {
            position = p;
        }

        public void setImage(Image i)
        {
            image = i;
        }

        /*
         * This method updates the set of allowed movements. Each type of piece calculates these movements
         * in different ways. Therefore, this method must be abstract.
         */
        public abstract void updateAllowedMovements();

        /*
         * Checks whether a given position is valid as a next move.
         */
        public bool isvalidMove(Position newPosition)
        {
            Iterator iterator = allowedMovements.getIterator();

            while (iterator.MoveNext())
            {
                Position p = iterator.Current as Position;
                if (p.Equals(newPosition))
                {
                    return true;
                }
            }

            return false;
        }

        /*
         * Checks the given position to see whether it can be inserted in the list of allowed movements
         * (depending on whether this position is already occupied by a white or black piece)
         */
        protected bool checkSquare(Position currentPos)
        {
            Piece whitePiece = Board.getInstance().consultPiece(currentPos, true);
            Piece blackPiece = Board.getInstance().consultPiece(currentPos, false);

            if (this.getColor() == Color.White)
            {

                if (whitePiece != null)
                {

                    return false;
                }
                else if (blackPiece != null)
                {

                    this.allowedMovements.insert(currentPos);
                    return false;
                }
            }
            if (this.getColor() == Color.Black)
            {

                if (blackPiece != null)
                {
                    return false;
                }
                else if (whitePiece != null)
                {

                    this.allowedMovements.insert(currentPos);
                    return false;
                }
            }

            this.allowedMovements.insert(currentPos);
            return true;
        }

        /*
         * This method fills the allowed movements for an entire row, column and/or diagonal.
         * Useful for Rook, Bishop, and Queen.
         */
        protected void calculateLine(int dirX, int dirY)
        {
            int row = this.getPosition().getRow();
            int column = this.getPosition().getColumn();

            for (int i = row + dirX, j = column + dirY;
                    i >= 0 && j >= 0 && i < Board.NUM_ROWS && j < Board.NUM_ROWS;
                    i += dirX, j += dirY)
            {

                if (!checkSquare(new Position(i, j)))
                {
                    break;
                }
            }
        }

        /*
         * Used to add a single square to the list of allowed movements.
         * Useful for Knight and King.
         */
        protected void fillSquare(int x, int y)
        {
            int row = this.getPosition().getRow() + x;
            int column = this.getPosition().getColumn() + y;

            if (row >= 0 && row < Board.NUM_ROWS && column >= 0 && column < Board.NUM_ROWS)
            {
                checkSquare(new Position(row, column));
            }
        }

    }
}

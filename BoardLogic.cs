using System;

namespace Chess
{
    /*
    * A class for generic methods that implement some rules of the game.
    */
    public class BoardLogic
    {
        public BoardLogic()
        {

        }

        /*
        * This method checks whether the king can be killed by the opponent
        */
        public static bool isCheck(bool white)
        {
            // Get the king's position

            Position posKing = null;
            Iterator it;

            if (white)
            {
                it = Board.getInstance().getWhitePieces().getIterator();
            }
            else {
                it = Board.getInstance().getBlackPieces().getIterator();
            }

            while (it.MoveNext() && posKing == null)
            {

                if ((it.Current as Piece) is King)
                {
                    posKing = (it.Current as Piece).getPosition();
                }
            }

            //Check "check" :)

            if (white)
            {
                it = Board.getInstance().getBlackPieces().getIterator();
            }
            else {
                it = Board.getInstance().getWhitePieces().getIterator();
            }

            while (it.MoveNext())
            {
                Piece piece = it.Current as Piece;
                Iterator itPositions = piece.getAllowedMovements().getIterator();

                while (itPositions.MoveNext())
                {
                    Position pos = itPositions.Current as Position;
                    if (pos.Equals(posKing))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /*
        * This method updates the allowed movements of the pieces that are still part of the game
        */
        public static void updateListAllowedMovements()
        {
            Iterator it = Board.getInstance().getWhitePieces().getIterator();

            while (it.MoveNext())
            {
                (it.Current as Piece).updateAllowedMovements();
            }

            it = Board.getInstance().getBlackPieces().getIterator();

            while (it.MoveNext())
            {
                (it.Current as Piece).updateAllowedMovements();
            }
        }
    }
}

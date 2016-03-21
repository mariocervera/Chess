using System;

namespace Chess
{
    /*
     * Memento design pattern
     */
    public class MementoBoard
    {
        /*
        * A specific state of a chess game is represented by the specific states of the pieces.
        * For this reason, any memento object must store two lists: one for the white pieces and another
        * one for the black pieces. This way, previous states of the game can be restored by means of
        * the "undo" action.
        */
        private List whitePieces;
        private List blackPieces;

        public MementoBoard(List wp, List bp)
        {
            this.whitePieces = wp;
            this.blackPieces = bp;
        }

        //Getters and setters

        public List getWhitePieces()
        {
            return whitePieces;
        }

        public List getBlackPieces()
        {
            return blackPieces;
        }

        public void setWhitePieces(List wp)
        {
            whitePieces = wp;
        }

        public void setBlackPieces(List bp)
        {
            blackPieces = bp;
        }
    }
}

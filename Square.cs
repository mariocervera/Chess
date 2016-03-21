using System;
using System.Windows.Forms;

namespace Chess
{
    /*
    * This class encapsulates the squares of the Chess board.
    * The Chess board is implemented as a matrix of buttons.
    */
    public class Square : Button
    {
        Position position;

        public Square(Position p)
        {
            position = p;
        }

        public Position getPosition()
        {
            return position;
        }
    }
}

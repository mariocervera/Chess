using System;
using System.Windows.Forms;

namespace Chess { 

	public class Square: Button {

		Position position;

		public Square(Position p) {

			position = p;
		}

		public Position getPosition() {

            return position;
        }
	}
}

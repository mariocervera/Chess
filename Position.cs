using System;

namespace Chess {

	public class Position {

		private int row;
		private int column;

		public Position(int r, int c) {

			row = r;
			column = c;
        }

        //Getters and setters

        public int getRow() {
            return row;
        }

		public int getColumn(){
            return column;
        }

		public void setRow(int r){
            row = r;
        }

		public void setColumn(int c) {
            column = c;
        }

        /*
         * Checks whether two positions are the same
         */
        public override bool Equals(object obj) {

            if(obj is Position) {
                Position p = obj as Position;

                return (p.getRow() == this.getRow()
                    && p.getColumn() == this.getColumn());
            }

            return base.Equals(obj);
        }

        public override string ToString() {

			return (this.getRow() + ":" + this.getColumn());
		}
	}
}

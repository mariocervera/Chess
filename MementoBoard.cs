using System;

namespace Chess {
	
    /*
     * Memento design pattern
     */
	public class MementoBoard { 

		private List whitePieces;
		private List blackPieces;

		public MementoBoard(List wp, List bp) { 

			this.whitePieces = wp;
			this.blackPieces = bp;
		}

		//Getters and setters

		public List getWhitePieces() {

            return whitePieces;
        }

		public List getBlackPieces() {

            return blackPieces;
        }

		public void setWhitePieces(List wp) {

            whitePieces = wp;
        }

        public void setBlackPieces(List bp) {

            blackPieces = bp;
        }

    }
}

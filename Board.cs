using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Chess {

	//This class is a singleton

	public class Board: Panel {
        		
		public const int NUM_ROWS = 8;

		private Square [,] matrix; //The board is a matriz of buttons
		private List whitePieces;
		private List blackPieces;
		private string path="Images/Classic";
		private MainForm mainForm;

		//Singleton

		private static Board board;

		private Board() {

			matrix = new Square[NUM_ROWS, NUM_ROWS];
            whitePieces = new List();
            blackPieces = new List();
			
			this.Size=new Size(480,480);
			this.Location=new Point(30,30);

            // Create the squares

            int h;
			int v=0;

			for(int i=0; i< NUM_ROWS; i++) { 
			
				h=0;

                for (int j = 0; j < NUM_ROWS; j++) { 
				
					matrix[i,j] = new Square(new Position(i,j));

                    if ((i + j) % 2 == 0) { 
                    
                        matrix[i, j].BackgroundImage = Image.FromFile(path + "//WhiteWood.gif");
                    }
                    else {
                        matrix[i, j].BackgroundImage = Image.FromFile(path + "//BlackWood.gif");
                    }

					matrix[i,j].Name = "square" + i + "" + j; //square00, square01, etc.
					matrix[i,j].Location = new Point(h,v);
					matrix[i,j].Click += new System.EventHandler(this.square_Click);
					h+=60;
					matrix[i,j].Size=new Size(60,60);
					this.Controls.Add(matrix[i,j]);
				}

				v+=60;
			}


		}
		
		/*
         * This method returns the only instance of this class
         */
		public static Board getInstance() {

            if (board == null) {
            
                board = new Board();
            }

			return board;
		}

        //Getters and setters

		public List getWhitePieces() {

            return whitePieces;
        }

        public List getBlackPieces() {

            return blackPieces;
        }

        public void setMainForm(MainForm mf) {

            this.mainForm = mf;
        }

		public string getPath() {

            return path;
        }

		public void setPath(string path) {

            this.path = path;
        }
		
		 #region Memento

		/*
         * This method restores the previous state of the game
         */
		public void restoreFromMementos(MementoBoard mb, MementoImages mi) {

			whitePieces = mb.getWhitePieces();
			blackPieces = mb.getBlackPieces();
            mainForm.setWhiteImages(mi.getWhiteImages());
            mainForm.setBlackImages(mi.getBlackImages());
			redrawBoard();
            mainForm.paintDeletedWhitePieces();
            mainForm.paintDeletedBlackPieces();
            mainForm.shiftTurn();
            mainForm.removeFromLog();
		}


		public void createMementos() {

			Caretaker.mementoBoard = new MementoBoard(whitePieces.clone(), blackPieces.clone());
			ImageList whiteImages = new ImageList();
			ImageList blackImages = new ImageList();

            foreach (Image i in mainForm.getWhiteImages().Images) {

                whiteImages.Images.Add(i);
            }

            foreach (Image i in mainForm.getBlackImages().Images) {

                blackImages.Images.Add(i);
            }

            Caretaker.mementoImages=new MementoImages(whiteImages, blackImages);
		}

		#endregion

		public void initializePieces() {

            clear();			

            // White pieces

			whitePieces.insert(new Rook(new Position(7,0), Color.White));
            whitePieces.insert(new Knight(new Position(7,1), Color.White));
            whitePieces.insert(new Bishop(new Position(7,2), Color.White));
            whitePieces.insert(new Queen(new Position(7,3), Color.White));
            whitePieces.insert(new King(new Position(7,4), Color.White));
            whitePieces.insert(new Bishop(new Position(7,5), Color.White));
            whitePieces.insert(new Knight(new Position(7,6), Color.White));
            whitePieces.insert(new Rook(new Position(7,7), Color.White));

            for (int i = 0; i < NUM_ROWS; i++) { //White pawns

                whitePieces.insert(new Pawn(new Position(6,i),Color.White));
			}

			// Black pieces

			blackPieces.insert(new Rook(new Position(0,0), Color.Black));
            blackPieces.insert(new Knight(new Position(0,1), Color.Black));
            blackPieces.insert(new Bishop(new Position(0,2), Color.Black));
            blackPieces.insert(new Queen(new Position(0,3), Color.Black));
            blackPieces.insert(new King(new Position(0,4), Color.Black));
            blackPieces.insert(new Bishop(new Position(0,5), Color.Black));
            blackPieces.insert(new Knight(new Position(0,6), Color.Black));
            blackPieces.insert(new Rook(new Position(0,7), Color.Black));

			for(int i = 0; i < NUM_ROWS; i++) { // Black pawns

                blackPieces.insert(new Pawn(new Position(1,i), Color.Black));
			}

            redrawBoard();
		}

		public void redrawBoard() {

			for(int i = 0; i < NUM_ROWS; i++) {

				for(int j=0; j< NUM_ROWS; j++) {

                    if ((i + j) % 2 == 0) {

                        matrix[i, j].BackgroundImage = Image.FromFile(path + "//WhiteWood.gif");
                    }
                    else {
                        matrix[i, j].BackgroundImage = Image.FromFile(path + "//BlackWood.gif");
                    }
					matrix[i,j].Image = null;
				}
			}

			Iterator it = whitePieces.getIterator();
			while(it.MoveNext()) {
                drawPiece(it.Current as Piece);
            }

			it = blackPieces.getIterator();
			while(it.MoveNext()) {
                drawPiece(it.Current as Piece);
            }
		}

		/*
         * Realizes a movement. If a piece is deleted, it is returned by the method
         */
		private Piece realizeMovement(Position pos) {

			Piece piece = killPiece(pos, false);

			//Redraw the board and update the piece position

			deletePieceSquare(MainForm.selectedPiece);
            Board.getInstance().deletePiece(MainForm.selectedPiece);
			MainForm.selectedPiece.setPosition(pos);
            Board.getInstance().drawPiece(MainForm.selectedPiece);
			deleteAllowedMovements(MainForm.selectedPiece.getAllowedMovements());

			return piece;
		}

        /*
         * Clears the lists of pieces. It is used when the game is over.
         */
		public void clear() {

			whitePieces.clear();
			blackPieces.clear();
		}

		/*
         * Draws a piece in the board
         */
		public void drawPiece(Piece p) {
               
			Position pos = p.getPosition();
			matrix[pos.getRow(), pos.getColumn()].Image = p.getImage();
		}

        /*
         * Removes a piece from the board
         */
        public void deletePiece(Piece p) {
               
			Position pos = p.getPosition();
			matrix[pos.getRow(), pos.getColumn()].Image=null;
		}

        /*
         * Checks whether there is a piece in a given position
         */
        public Piece consultPiece(Position p, bool white) {

			Iterator it;

            if (white) {
                it = whitePieces.getIterator();
            }
            else {
                it = blackPieces.getIterator();
            }

			while(it.MoveNext()) { 

				Piece piece = (it.Current as Piece);
                if (piece.getPosition().Equals(p)) {
                    return piece;
                }
			}

			return null;
		}

        /*
         * Deletes a piece from a given position. In case of "pawn promotion", do not put into deleted pieces set.
         */
        public Piece killPiece(Position pos, bool promotion) { 

			bool found = false;
			Piece piece = null;

			Iterator it = whitePieces.getIterator();

            while (it.MoveNext()) {

                Piece p = (it.Current as Piece);

                if (p.getPosition().Equals(pos)) {

					piece = p;

                    if (!promotion) {
                        mainForm.getWhiteImages().Images.Add(p.getImage());
                    }

					whitePieces.remove(p);
					found = true;
					break;
				}
			}

			if(!found) { 
			
				it = blackPieces.getIterator();

                while (it.MoveNext()) {

					Piece p = (it.Current as Piece);

                    if (p.getPosition().Equals(pos)) {

						piece = p;
                        if (!promotion) {
                            mainForm.getBlackImages().Images.Add(p.getImage());
                        }
						blackPieces.remove(p);
						break;
					}
				}
			}

			return piece;
		}
		

		//Methods for painting squares

		private void paintAllowedMovements(List positions) { 
		
			int row, column;

			Iterator it = positions.getIterator();

            while (it.MoveNext()) {

                row = (it.Current as Position).getRow();
                column = (it.Current as Position).getColumn();
                matrix[row, column].BackgroundImage = null;
                matrix[row, column].BackColor = Color.LightGreen;
            }	
		}

		public void deleteAllowedMovements(List positions) {

			int row, column;

			Iterator it = positions.getIterator();

			while(it.MoveNext()) {

				row = (it.Current as Position).getRow();
				column = (it.Current as Position).getColumn();
				matrix[row,column].BackColor=Color.Empty;
                if ((row + column) % 2 == 0) {
                    matrix[row, column].BackgroundImage = Image.FromFile(Board.getInstance().getPath() + "//WhiteWood.gif");
                }
                else {
                    matrix[row, column].BackgroundImage = Image.FromFile(Board.getInstance().getPath() + "//BlackWood.gif");
                }
			}
		}

		private void paintPiece(Piece p) {

			int row = p.getPosition().getRow();
			int column = p.getPosition().getColumn();
			matrix[row,column].BackgroundImage = null;
			matrix[row,column].BackColor=Color.LightCoral;
		}
		
		public void deletePieceSquare(Piece p) {

            int row = p.getPosition().getRow();
            int column = p.getPosition().getColumn();
            matrix[row,column].BackColor=Color.Empty;
            if ((row + column) % 2 == 0) {
                matrix[row, column].BackgroundImage = Image.FromFile(Board.getInstance().getPath() + "//WhiteWood.gif");
            }
            else {
                matrix[row, column].BackgroundImage = Image.FromFile(Board.getInstance().getPath() + "//BlackWood.gif");
            }
		}

		private void square_Click(object sender, System.EventArgs e) {

			Square sq = (Square) sender;

			BoardLogic.updateListAllowedMovements();
			
			bool whiteTurn = (MainForm.turn == "White");

			listenerSquare(sq, whiteTurn);

		}

		private void listenerSquare(Square sq, bool whiteTurn) {

			List pWhite = null;
			List pBlack = null;
			ImageList iWhite = new ImageList();
			ImageList iBlack = new ImageList();

			Piece piece = null;

            if (whiteTurn) {
                piece = Board.getInstance().consultPiece(sq.getPosition(), true);
            }
            else {
                piece = Board.getInstance().consultPiece(sq.getPosition(), false);
            }

			if(piece != null) { 

				if(MainForm.selectedPiece != null) {

					deletePieceSquare(MainForm.selectedPiece);
					deleteAllowedMovements(MainForm.selectedPiece.getAllowedMovements());
				}

				MainForm.selectedPiece = piece;
				paintPiece(MainForm.selectedPiece);
                paintAllowedMovements(MainForm.selectedPiece.getAllowedMovements());
			}
			else { //Empty square or occupied by an opponent's piece
			
				Color turnColor;
                if (whiteTurn) {
                    turnColor = Color.White;
                }
                else {
                    turnColor = Color.Black;
                }

				if(MainForm.selectedPiece != null && MainForm.selectedPiece.getColor() == turnColor) { // A piece of the current player is selected
				
					Position posIni = MainForm.selectedPiece.getPosition();
					Position posFin = sq.getPosition();

					Piece deletedPiece = null; //Deleted piece. It is returned in case an undo action is triggered.

					if(MainForm.selectedPiece.isvalidMove(posFin)) { //If the movement is valid ...
					
						if(Caretaker.mementoBoard != null) {

							pWhite = Caretaker.mementoBoard.getWhitePieces().clone();
							pBlack = Caretaker.mementoBoard.getBlackPieces().clone();
						}
						if(Caretaker.mementoImages != null) {
                            foreach (Image i in Caretaker.mementoImages.getWhiteImages().Images) {
                                iWhite.Images.Add(i);
                            }
                            foreach (Image i in Caretaker.mementoImages.getBlackImages().Images) {
                                iBlack.Images.Add(i);
                            }
						}
						createMementos();
                        deletedPiece = realizeMovement(posFin);
						BoardLogic.updateListAllowedMovements();

						bool kingCheck = false;

                        if (whiteTurn) {
                            kingCheck = BoardLogic.isCheck(true);
                        }
                        else {
                            kingCheck = BoardLogic.isCheck(false);
                        }

						if(kingCheck) {

							//Undo the movement

							realizeMovement(posIni);

							Caretaker.mementoBoard.setWhitePieces(pWhite);
							Caretaker.mementoBoard.setBlackPieces(pBlack);
                            Caretaker.mementoImages.setWhiteImages(iWhite);
							Caretaker.mementoImages.setBlackImages(iBlack);

							if(deletedPiece != null) {

                                //Return the deleted piece

                                if (whiteTurn) {
                                    blackPieces.insert(deletedPiece);
                                }
                                else {
                                    whitePieces.insert(deletedPiece);
                                }

                                //Delete inserted image

                                if (whiteTurn) {
                                
                                    mainForm.deleteLastImage(mainForm.getBlackImages());
                                }
                                else {
                                    mainForm.deleteLastImage(mainForm.getWhiteImages());
                                }

								drawPiece(deletedPiece);
							}
							MessageBox.Show(this,"Your king would be in check if you carried out this movement", "Message", MessageBoxButtons.OK,MessageBoxIcon.Information);
						}
						else { //The king is not in check
						
							int lastRow = -1;

                            if (whiteTurn) {
                                lastRow = 0;
                            }
                            else {
                                lastRow = 7;
                            }

							if(MainForm.selectedPiece is Pawn && posFin.getRow() == lastRow) {

                                //Promotion

                                FormPromotion f = new FormPromotion(MainForm.selectedPiece as Pawn);
								f.ShowDialog();
							}

                            BoardLogic.updateListAllowedMovements();

							bool doCheck;

                            if (whiteTurn) {
                                doCheck = BoardLogic.isCheck(false);
                            }
                            else {
                                doCheck = BoardLogic.isCheck(true);
                            }

                            if (doCheck) {

                                MessageBox.Show(this, "King is in check!", "Check", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            if (deletedPiece != null) {

                                if (whiteTurn) {
                                    mainForm.paintDeletedBlackPieces();
                                }
                                else {
                                    mainForm.paintDeletedWhitePieces();
                                }
							}

                            mainForm.showInLog(MainForm.selectedPiece.ToString() + " --> " + "From " + posIni.ToString() + " to " + posFin.ToString());
                            mainForm.enableUndo();
                            mainForm.shiftTurn();
						}
					}
				}
			}
		}

	}
}

using System;
using System.Collections;
using System.Drawing;

namespace Chess
{
    /*
    * Iterator design pattern
    */
    public class List
    {
        private ArrayList list;

        public List()
        {
            list = new ArrayList();
        }

        /*
         * This constructor is used in the clone method
         */
        public List(ArrayList l)
        {
            list = new ArrayList();

            foreach (Piece p in l)
            {
                Position pos = new Position(p.getPosition().getRow(), p.getPosition().getColumn());
                Color color = p.getColor();
                Image image = p.getImage();
                Piece piece = null;

                if (p is Bishop)
                {
                    piece = new Bishop(pos, color);
                }
                else if (p is Knight)
                {
                    piece = new Knight(pos, color);
                }
                else if (p is Pawn)
                {
                    piece = new Pawn(pos, color);
                }
                else if (p is Queen)
                {
                    piece = new Queen(pos, color);
                }
                else if (p is Rook)
                {
                    piece = new Rook(pos, color);
                }
                else if (p is King)
                {
                    piece = new King(pos, color);
                }

                list.Add(piece);
            }

        }

        public Iterator getIterator()
        {
            return new Iterator(this);
        }

        public int count()
        {
            return list.Count;
        }

        public List clone()
        {
            return new List(this.list);
        }

        public void clear()
        {
            list.Clear();
        }

        public void insert(object obj)
        {
            list.Add(obj);
        }

        public void remove(object obj)
        {
            try
            {
                list.Remove(obj);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object getElement(int index)
        {
            try
            {
                object obj = list[index];

                return obj;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
            }
        }
    }
}

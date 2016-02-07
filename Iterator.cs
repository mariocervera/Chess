using System;
using System.Collections;

namespace Chess {


    /*
     * Iterator design pattern
     */
	public class Iterator: IEnumerator {

		private List list;
		private int index;

		public object Current {

			get {

                if (index == -1) {
                    return null;
                }

				return list.getElement(index);
			}
		}
		
		public Iterator(List l) {
			list = l;
			index = -1;
		}

		public bool MoveNext() {

			index++;

            return index < list.count();
		}

		public void Reset() {

            index = -1;
		}
	}
}

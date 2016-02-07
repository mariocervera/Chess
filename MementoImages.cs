using System;
using System.Windows.Forms;

namespace Chess {

    /*
     * Memento design pattern
     */
    public class MementoImages {

		private ImageList whiteImages;
		private ImageList blackImages;

		public MementoImages(ImageList wi, ImageList bi) { 

			this.whiteImages = wi;
			this.blackImages = bi;
		}

        //Getters and setters

        public ImageList getWhiteImages() {

            return whiteImages;
        }

        public ImageList getBlackImages() {

            return blackImages;
        }

        public void setWhiteImages(ImageList wi) {

            whiteImages = wi;
        }

        public void setBlackImages(ImageList bi) {

            blackImages = bi;
        }

    }
}

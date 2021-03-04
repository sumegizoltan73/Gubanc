using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gubanc.include
{
    public class Card
    {
        private bool isConnected = false;
        private int orientation = 0;
        private int cardSpace = 0;
        private string[] sideMask;
        private string[] sideMaskBackup;
        private string name = String.Empty;

        public Card(string name, string top, string right, string bottom, string left)
        {
            this.name = name;
            sideMaskBackup = new string[4];
            sideMaskBackup[0] = top;
            sideMaskBackup[1] = right;
            sideMaskBackup[2] = bottom;
            sideMaskBackup[3] = left;
            
            InitCard();
        }

        public string Name
        {
            get { return String.Format("Card{0}", name); }
        }

        public int Orientation
        {
            get { return orientation; }
        }

        public string Top
        {
            get 
            {
                return (orientation == 1 || orientation == 2) ? GetReverese(sideMask[0]) : sideMask[0]; 
            }
        }

        public string Right
        {
            get
            {
                return (orientation == 2 || orientation == 3) ? GetReverese(sideMask[1]) : sideMask[1];
            }
        }

        public string Bottom
        {
            get 
            {
                return (orientation == 1 || orientation == 2) ? GetReverese(sideMask[2]) : sideMask[2]; 
            }
        }

        public string Left
        {
            get
            {
                return (orientation == 2 || orientation == 3) ? GetReverese(sideMask[3]) : sideMask[3];
            }
        }

        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value; }
        }

        public bool IsRotateEnabled
        {
            get { return (orientation < 4); }
        }

        public int CardSpace
        {
            get { return cardSpace; }
            set { cardSpace = value; }
        }

        private string GetReverese(string str)
        {
            char[] chars = str.ToCharArray();
            return String.Format("{0}{1}", chars[1], chars[0]);
        }

        private void InitCard()
        {
            orientation = 0;
            sideMask = sideMaskBackup.ToArray();
        }

        public void Reset()
        {
            InitCard();
        }

        public void Rotate() {
            if (IsRotateEnabled)
            {
                orientation++;

                sideMask = sideMask.Skip(3).Concat(sideMask.Take(3)).ToArray();
            }
        }
    }
}

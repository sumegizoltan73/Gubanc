using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Gubanc.include
{
    public class CardCollections
    {
        #region declaration
        public List<Card> CardCollection = null;
        public List<Card> Solution = null;

        private static CardCollections instance = null;
        private static object syncLock = new object();
        #endregion

        #region ctor
        public CardCollections()
        {
            CardCollection = new List<Card>();
            Solution = new List<Card>();
        }
        #endregion

        #region methods
        public static CardCollections GetCardCollections()
        {
            if (instance == null)
            {
#if TEST
#else
                Monitor.Enter(syncLock);
#endif
                if (instance == null)
                {
                    instance = new CardCollections();
                }
#if TEST
#else
                Monitor.Exit(syncLock);
#endif
            }

            return instance;
        }
        #endregion
    }
}

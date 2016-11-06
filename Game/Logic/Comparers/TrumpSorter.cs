using Common;
using Game.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Logic.Comparers
{
    public class TrumpSorter : IComparer<DeckValue>
    {
        public int Compare(DeckValue first, DeckValue second)
        {
            if (first != null && second != null)
            {
                // We can compare both properties.
                return first.value.CompareTo(second.value);
            }

            if (first == null && second == null)
            {
                // We can't compare any properties, so they are essentially equal.
                return 0;
            }

            if (first != null)
            {
                // Only the first instance is not null, so prefer that.
                return -1;
            }

            // Only the second instance is not null, so prefer that.
            return 1;
        }
    }
}

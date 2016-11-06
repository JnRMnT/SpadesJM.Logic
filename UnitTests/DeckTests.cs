using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common.Structure;
using System.Collections.Generic;
using Common;

namespace UnitTests
{
    [TestClass]
    public class DeckTests
    {
        [TestMethod]
        public void Shuffle()
        {
            Deck deck = new Deck();
            List<Card>[] dealedCards = deck.Deal();
        }
    }
}

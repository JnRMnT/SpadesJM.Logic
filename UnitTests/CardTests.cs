using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Common;
using Common.Enums;
using Common.Helpers;
using Game.Logic;

namespace UnitTests
{
    [TestClass]
    public class CardTests
    {
        [TestMethod]
        public void TrumpTest()
        {
            Card trumpCard = new Card(CardValue.TWO, CardType.Club);
            Card normalCard = new Card(CardValue.ACE, CardType.Diamond);

            int trumpCardValue = trumpCard.CalculateRealValue(CardType.Club);
            int normalCardValue = normalCard.CalculateRealValue(CardType.Club);

            Assert.IsTrue(trumpCardValue > normalCardValue);
        }

        [TestMethod]
        public void ValueTest()
        {
            Card two = new Card(CardValue.TWO, CardType.Club);
            Card ace = new Card(CardValue.ACE, CardType.Diamond);
            
            Assert.IsTrue(ace.GetNumericValue() > two.GetNumericValue());
        }
    }
}

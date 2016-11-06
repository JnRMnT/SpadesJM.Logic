using Game.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace Game.Logic
{
    public class TargetScoreCondition : GameEndingCondition
    {
        public TargetScoreCondition(object[] conditionElements)
            : base(conditionElements)
        {
        }

        public override bool isConditionMet()
        {
            int goal;
            int.TryParse(conditionElements[0].ToString(), out goal);
            if (ScoreBoard.GetLeadingScore() >= goal)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}

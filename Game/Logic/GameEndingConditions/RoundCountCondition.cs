using Game.Actors;
using Game.Structure;
using System.Collections.Generic;
using System.Text;

namespace Game.Logic
{
    public class RoundCountCondition : GameEndingCondition
    {
        public RoundCountCondition(object[] conditionElements)
            : base(conditionElements)
        {
        }

        public override bool isConditionMet()
        {
            int goal;
            int.TryParse(conditionElements[1].ToString(), out goal);
            if (((GameTable)conditionElements[0]).GetGameInstance().GetRoundCount() >= goal)
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

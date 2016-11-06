using System.Collections.Generic;
using System.Text;

namespace Game.Logic
{
    public class GameEndingCondition
    {
        protected object[] conditionElements;

        public virtual bool isConditionMet()
        {
            return false;
        }

        public GameEndingCondition(object[] conditionElements)
        {
            this.conditionElements = conditionElements;
        }
    }
}

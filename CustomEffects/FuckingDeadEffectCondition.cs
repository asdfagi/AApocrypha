using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Assets
{
    public class FuckingDeadEffectCondition : EffectConditionSO
    {
        //public bool trueIfHasItem = true;

        public override bool MeetCondition(IUnit caster, EffectInfo[] effects, int currentIndex)
        {
            return caster.CurrentHealth <= 0;
        }
    }
}

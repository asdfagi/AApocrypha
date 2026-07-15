using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class TargetHasCasterHealthColorEffectorCondition : EffectorConditionSO
    {
        public bool _passIfTrue = true;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException context)
            {
                return (context.casterUnit.HealthColor == context.damagedUnit.HealthColor) == _passIfTrue;
            }
            return false;
        }
    }
}

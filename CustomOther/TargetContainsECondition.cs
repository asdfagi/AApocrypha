using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class TargetContainsECondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException context)
            {
                Debug.Log(context.damagedUnit.Name);
                return GadsbyChecker(context.damagedUnit.Name);
            }
            return false;
        }
        public static bool GadsbyChecker(string input)
        {
            foreach (char c in input)
            {
                if (c == 'e' || c == 'E')
                {
                    return true;
                }
            }
            return false;
        }
    }
}

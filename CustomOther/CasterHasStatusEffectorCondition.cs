using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class CasterHasStatusEffectorCondition : EffectorConditionSO
    {
        public bool _passIfTrue = true;
        public string _statusID = "";

        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (effector is IUnit caster) 
            {
                if (_statusID == "")
                {
                    return _passIfTrue == (caster.StatusEffectCount > 0);
                }
                else if (caster.ContainsStatusEffect(_statusID))
                {
                    return _passIfTrue;
                }
                else
                {
                    return false;
                }
            }
            else { return false; }
        }
    }
}

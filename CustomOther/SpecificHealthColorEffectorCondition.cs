using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class SpecificHealthColorEffectorCondition : EffectorConditionSO
    {
        public ManaColorSO _color;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            ManaColorSO healthColor = effector.HealthColor;
            return healthColor == _color;
        }
    }
}

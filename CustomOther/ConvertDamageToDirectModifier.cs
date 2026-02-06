using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class ConvertDamageToDirectModifier(List<EffectInfo> effects, IUnit caster) : IntValueModifier(60)
    {
        public override int Modify(int value)
        {
            if (value <= 0) { return value; }
            CombatManager.Instance.ProcessImmediateAction(new ImmediateEffectAction([.. effects], caster, value));
            return 0;
        }
    }
}

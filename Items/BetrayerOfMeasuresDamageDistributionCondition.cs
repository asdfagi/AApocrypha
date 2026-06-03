using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Items
{
    public class BetrayerOfMeasuresDamageDistributionCondition : EffectorConditionSO
    {
        public BaseCombatTargettingSO _targeting;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is IntegerReference reference)
            {
                if (reference.value <= 0) { return false; }

                DamageOfTypeEffect indirectOw = ScriptableObject.CreateInstance<DamageOfTypeEffect>();
                indirectOw._indirect = true;
                indirectOw._DamageTypeID = CombatType_GameIDs.Dmg_Linked.ToString();
                
                CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(indirectOw, reference.value, _targeting)], effector as IUnit));
            }
            return false;
        }
    }
}

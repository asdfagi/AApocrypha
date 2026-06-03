using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Items
{
    public class StarvedBannerHealEffectCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is HealingDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.healingUnit is IUnit healed)
                {
                    AddRandomPassiveEffect shapelingAny = ScriptableObject.CreateInstance<AddRandomPassiveEffect>();
                    shapelingAny._passivesToAdd = VaughanCharacter.allPassives.ToArray();
                    shapelingAny._popup = true;
                    shapelingAny._fixedCap = 10;

                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(shapelingAny, 1, Targeting.Slot_SelfSlot)], healed, 0));
                }
            }
            return false;
        }
    }
}

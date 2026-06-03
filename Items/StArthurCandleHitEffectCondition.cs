using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Items
{
    public class StArthurCandleHitEffectCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.damagedUnit is IUnit damaged)
                {
                    if (!damaged.IsUnitCharacter || damaged.SlotID == reference.casterUnit.SlotID) { return false; }
                    CombatManager.Instance.AddSubAction(new EffectAction([Effects.GenerateEffect(ScriptableObject.CreateInstance<HealEffect>(), Mathf.Max(0, Mathf.FloorToInt((float)reference.amount / 2)), Targeting.Slot_SelfSlot)], reference.casterUnit, 0));
                }
            }
            return false;
        }
    }
}

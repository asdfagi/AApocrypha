using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.Encounters.Orph.H;

namespace A_Apocrypha.Items
{
    public class ElementOfDawnHitEffectCondition : EffectorConditionSO
    {
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageDealtValueChangeException reference)
            {
                if (reference.amount <= 0) { return false; }
                if (reference.damagedUnit is IUnit damaged)
                {
                    damaged.ApplyStatusEffect(StatusField.GetCustomStatusEffect("Smouldering_ID"), Mathf.Max(1, Mathf.CeilToInt((float) reference.amount / 3)));
                }
            }
            return false;
        }
    }
}

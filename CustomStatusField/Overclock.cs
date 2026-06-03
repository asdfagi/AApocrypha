using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class Overclock : StatusEffect_SO
    {
        public int _OverclockMultiplier = 2;
        public override bool IsPositive => true;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnAbilityUsed.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnWillApplyDamage.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnAbilityUsed.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            DamageDealtValueChangeException ex = args as DamageDealtValueChangeException;
            IStatusEffector effector = sender as IStatusEffector;
            ex.AddModifier(new MultiplyIntValueModifier(true, _OverclockMultiplier));
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder, sender as IStatusEffector);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class Irradiated : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            holder.m_ObjectData = caller;

            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnTurnFinished.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is IUnit u)
            {
                int randomDamage = UnityEngine.Random.Range(1, holder.m_ContentMain + 1);
                if (u.MaximumHealth - 2 <= 0)
                {
                    u.MaximizeHealth(1);
                }
                else
                {
                    u.MaximizeHealth(u.MaximumHealth - 2);
                }
            }
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder, sender as IStatusEffector);
        }
        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            if (!CanReduceDuration)
            {
                return;
            }

            int contentMain = holder.m_ContentMain;
            holder.m_ContentMain -= 1;
            if (!TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain)
            {
                effector.StatusEffectValuesChanged(_StatusID, holder.m_ContentMain - contentMain, true);
            }
        }
    }
}

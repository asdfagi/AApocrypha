using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class CeleritySE_SO : StatusEffect_SO
    {
        public override bool IsPositive => true;
        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            if (caller.IsStatusEffectorCharacter) CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.AttacksPerTurn.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            if (caller.IsStatusEffectorCharacter) CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnAbilityUsed.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.AttacksPerTurn.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is CharacterCombat chara)
            {
                chara.RefreshAbilityUse();
                ReduceDuration(holder, sender as IStatusEffector);
            }
        }
        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            if (args is IntegerReference integerReference)
                integerReference.value += 1;
            ReduceDuration(holder, sender as IStatusEffector);
        }
        public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            if (CanReduceDuration || effector.IsStatusEffectorCharacter)
            {
                int contentMain = holder.m_ContentMain;
                holder.m_ContentMain = Mathf.Max(0, contentMain - 1);
                if (!TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain)
                {
                    effector.StatusEffectValuesChanged(_StatusID, holder.m_ContentMain - contentMain, doesPopUp: true);
                }
            }
        }
    }
}

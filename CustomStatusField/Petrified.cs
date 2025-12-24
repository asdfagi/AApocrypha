using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class Petrified : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            holder.m_ObjectData = caller;
            //CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnStatusEffectApplied.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnStart.ToString(), caller);
            if((caller as IUnit).ContainsPassiveAbility(Passives.Inanimate.m_PassiveID)){
                (caller as IUnit).SimpleSetStoredValue("Petrified_Immune_SV", 1);
                RemoveStatusEffectEffect nopetr = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                nopetr._status = StatusField.GetCustomStatusEffect("Petrified_ID");
                EffectInfo[] veldamn =
                [
                        Effects.GenerateEffect(nopetr, 1, Slots.Self),
                    ];
                CombatManager.Instance.AddSubAction(new EffectAction(veldamn, (caller as IUnit)));
            }
            else
            {
                CheckPassiveAbilityEffect damn = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
                damn.m_PassiveID = Passives.Inanimate.m_PassiveID;
                RemoveStatusEffectEffect nopetr = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                nopetr._status = StatusField.GetCustomStatusEffect("Petrified_ID");
                AddPassiveEffect addinanim = ScriptableObject.CreateInstance<AddPassiveEffect>();
                addinanim._passiveToAdd = Passives.Inanimate;
                EffectInfo[] veldamn =
                [
                    Effects.GenerateEffect(damn, 1, Slots.Self),
                        Effects.GenerateEffect(nopetr, 1, Slots.Self, BasicEffects.DidThat(true,1)),
                        Effects.GenerateEffect(addinanim, 1, Slots.Self, BasicEffects.DidThat(false,2)),
                    ];
                CombatManager.Instance.AddSubAction(new EffectAction(veldamn, (caller as IUnit)));
            }
            

        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            if ((caller as IUnit).SimpleGetStoredValue("Petrified_Immune_SV") != 1) {
                //CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnStatusEffectApplied.ToString(), caller);
                CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnStart.ToString(), caller);
                CheckPassiveAbilityEffect damn = ScriptableObject.CreateInstance<CheckPassiveAbilityEffect>();
                damn.m_PassiveID = Passives.Inanimate.m_PassiveID;
                RemoveStatusEffectEffect nopetr = ScriptableObject.CreateInstance<RemoveStatusEffectEffect>();
                nopetr._status = StatusField.GetCustomStatusEffect("Petrified_ID");
                RemovePassiveEffect addinanim = ScriptableObject.CreateInstance<RemovePassiveEffect>();
                addinanim.m_PassiveID = Passives.Inanimate.m_PassiveID;
                EffectInfo[] veldamn =
                [
                    Effects.GenerateEffect(addinanim, 1, Slots.Self),
                    ];
                CombatManager.Instance.AddSubAction(new EffectAction(veldamn, (caller as IUnit)));
            }
            
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
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
                effector.StatusEffectValuesChanged(_StatusID, holder.m_ContentMain - 1, true);
            }
            else
            {
                (effector as IUnit).TryRemovePassiveAbility(Passives.Inanimate.m_PassiveID);
            }
        }
    }
}
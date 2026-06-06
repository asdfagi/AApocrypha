using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UI.CanvasScaler;

namespace A_Apocrypha.CustomStatusField
{
    public class Smouldering : StatusEffect_SO
    {
        public override bool IsPositive => false;

        public override void OnTriggerAttached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            holder.m_ObjectData = caller;

            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnRoundFinished.ToString(), caller);
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnStatusEffectContentAdded.ToString(), caller);
        }

        public override void OnTriggerDettached(StatusEffect_Holder holder, IStatusEffector caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnRoundFinished.ToString(), caller);
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnStatusEffectContentAdded.ToString(), caller);
        }

        public override void OnEventCall_01(StatusEffect_Holder holder, object sender, object args)
        {
            if (sender is IUnit u)
            {
                if (u.ContainsPassiveAbility("AA_FireSafety_PA") && holder.m_ContentMain >= 3)
                {
                    ReduceDuration(holder, sender as IStatusEffector);
                }
                bool isLogos = false;
                foreach (string type in u.UnitTypes)
                {
                    if (type == "Logos")
                    {
                        isLogos = true;
                        break;
                    }
                }
                bool oilCheck = false;
                if (u.ContainsStatusEffect(StatusField.OilSlicked.StatusID))
                {
                    oilCheck = true;
                    u.TryRemoveStatusEffect(StatusField.OilSlicked.StatusID);
                }
                u.Damage((oilCheck ? holder.m_ContentMain * 2 : holder.m_ContentMain), null, DeathType_GameIDs.Obliteration.ToString(), 0, false, false, true, (isLogos ? CombatType_GameIDs.Dmg_Linked.ToString() : CombatType_GameIDs.Dmg_Fire.ToString()));
            }
        }

        public override void OnEventCall_02(StatusEffect_Holder holder, object sender, object args)
        {
            Debug.Log("Smouldering | ding! increase trigger");
            if (sender is IUnit u)
            {
                if (holder.m_ContentMain > 7)
                {
                    bool isLogos = false;
                    foreach (string type in u.UnitTypes)
                    {
                        if (type == "Logos")
                        {
                            isLogos = true;
                            break;
                        }
                    }
                    int excess = holder.m_ContentMain - 7;
                    ReduceDurationSpecial(holder, sender as IStatusEffector);
                    if (u.ContainsStatusEffect(StatusField.OilSlicked.StatusID))
                    {
                        excess *= 2;
                        u.TryRemoveStatusEffect(StatusField.OilSlicked.StatusID);
                    }
                    u.Damage(excess, null, DeathType_GameIDs.Obliteration.ToString(), 0, false, false, true, (isLogos ? CombatType_GameIDs.Dmg_Linked.ToString() : CombatType_GameIDs.Dmg_Fire.ToString()));
                }
            }
        }
        /*public override void ReduceDuration(StatusEffect_Holder holder, IStatusEffector effector)
        {
            if (CanReduceDuration)
            {
                int contentMain = holder.m_ContentMain;
                holder.m_ContentMain = Mathf.Max(0, contentMain - 1);
                if (!TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain)
                {
                    effector.StatusEffectValuesChanged(_StatusID, holder.m_ContentMain - contentMain, doesPopUp: true);
                }
            }
        }*/

        public void ReduceDurationSpecial(StatusEffect_Holder holder, IStatusEffector effector)
        {
            int contentMain = holder.m_ContentMain;
            holder.m_ContentMain -= (holder.m_ContentMain - 7);
            if (!TryRemoveStatusEffect(holder, effector) && contentMain != holder.m_ContentMain)
            {
                effector.StatusEffectValuesChanged(_StatusID, holder.m_ContentMain - contentMain, true);
            }
        }
    }
}

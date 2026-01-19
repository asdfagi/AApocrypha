using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomStatusField
{
    public class Hoarfrost : FieldEffect_SO
    {
        public int _MinDamage = 2;

        public int _MaxDamage = 4;

        public int _StatusMultiplier = 2;

        public StatusEffect_SO _MultStatus = StatusField.Ruptured;

        public string _DeathTypeID = "Frost";

        public string _dmgTypeID = "AA_Frost_Damage";

        public override void OnSlotEffectorTriggerAttached(FieldEffect_Holder holder)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), holder.Effector);
        }

        public override void OnSlotEffectorTriggerDettached(FieldEffect_Holder holder)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_01, TriggerCalls.OnTurnFinished.ToString(), holder.Effector);
        }

        public override void OnTriggerAttached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.AddObserver(holder.OnEventTriggered_02, TriggerCalls.OnAbilityUsed.ToString(), caller);
        }

        public override void OnTriggerDettached(FieldEffect_Holder holder, IUnit caller)
        {
            CombatManager.Instance.RemoveObserver(holder.OnEventTriggered_02, TriggerCalls.OnAbilityUsed.ToString(), caller);
        }

        public override void OnSubActionTrigger(FieldEffect_Holder holder, object sender, object args, bool stateCheck)
        {
            //Debug.Log("Hoarfrost | subaction started");
            int num = UnityEngine.Random.Range(_MinDamage, _MaxDamage + 1);
            //Debug.Log($"Hoarfrost | getting unit from sender {sender}");
            IUnit unit = sender as IUnit;
            //Debug.Log($"Hoarfrost | sender acquired ({unit.Name})");
            if (unit.ContainsStatusEffect(_MultStatus.StatusID) && !(sender as IUnit).ContainsPassiveAbility("DriedOut"))
            {
                //Debug.Log("Hoarfrost | ruptured detected, multiplying and removing");
                num *= _StatusMultiplier;
                unit.TryRemoveStatusEffect(_MultStatus.StatusID);
            }

            unit.Damage(num, null, _DeathTypeID, 0, addHealthMana: true, directDamage: true, ignoresShield: false, _dmgTypeID);
        }
        public override void OnEventCall_01(FieldEffect_Holder holder, object sender, object args)
        {
            ReduceDuration(holder);
        }

        public override void OnEventCall_02(FieldEffect_Holder holder, object sender, object args)
        {
            //Debug.Log($"Hoarfrost | trigger called with sender {sender} ({sender.GetType().ToString()})");
            if (!(sender as IUnit).ContainsPassiveAbility("Antifreeze"))
            {
                //Debug.Log("Hoarfrost | antifreeze not detected, proceeding...");
                CombatManager.Instance.AddSubAction(new PerformSlotStatusEffectAction(holder, sender, args, stateCheck: false));
            }
        }
    }
}

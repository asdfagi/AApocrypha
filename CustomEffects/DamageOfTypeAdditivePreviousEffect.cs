using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class DamageOfTypeAdditivePreviousEffect : EffectSO
    {
        [DeathTypeEnumRef]
        public string _DeathTypeID = "Basic";

        public bool _usePreviousExitValue;

        public bool _ignoreShield;

        public bool _indirect;

        public bool _returnKillAsSuccess;

        public string _DamageTypeID = CombatType_GameIDs.Dmg_Normal.ToString();

        public int _bonusAmount = 1;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (_usePreviousExitValue)
            {
                entryVariable += base.PreviousExitValue * _bonusAmount;
            }

            exitAmount = 0;
            bool flag = false;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    int targetSlotOffset = (areTargetSlots ? (targetSlotInfo.SlotID - targetSlotInfo.Unit.SlotID) : (-1));
                    int amount = entryVariable;
                    DamageInfo damageInfo;
                    if (_indirect)
                    {
                        damageInfo = targetSlotInfo.Unit.Damage(amount, null, _DeathTypeID, targetSlotOffset, addHealthMana: false, directDamage: false, ignoresShield: true, _DamageTypeID);
                    }
                    else
                    {
                        amount = caster.WillApplyDamage(amount, targetSlotInfo.Unit);
                        damageInfo = targetSlotInfo.Unit.Damage(amount, caster, _DeathTypeID, targetSlotOffset, addHealthMana: true, directDamage: true, _ignoreShield, _DamageTypeID);
                    }

                    flag |= damageInfo.beenKilled;
                    exitAmount += damageInfo.damageAmount;
                }
            }

            if (!_indirect && exitAmount > 0)
            {
                caster.DidApplyDamage(exitAmount);
            }

            if (!_returnKillAsSuccess)
            {
                return exitAmount > 0;
            }

            return flag;
        }
    }
}

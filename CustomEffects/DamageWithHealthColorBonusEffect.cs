using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class DamageWithHealthColorBonusEffect : EffectSO
    {
        [DeathTypeEnumRef]
        public string _DeathTypeID = "Basic";

        public bool _usePreviousExitValue;

        public bool _ignoreShield;

        public bool _indirect;

        public bool _returnKillAsSuccess;

        public int _bonusAmount = 1;

        public ManaColorSO _color;

        public bool _contains = false;

        public bool _pureBlocked = true;

        public bool _entryAsBaseDamage = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (_usePreviousExitValue)
            {
                if (_entryAsBaseDamage) { entryVariable += base.PreviousExitValue; }
                else { entryVariable *= base.PreviousExitValue; }
            }

            exitAmount = 0;
            bool flag = false;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    int targetSlotOffset = (areTargetSlots ? (targetSlotInfo.SlotID - targetSlotInfo.Unit.SlotID) : (-1));
                    int amount = entryVariable;
                    int bonus = 0;
                    if (_contains == true && targetSlotInfo.Unit.HealthColor.SharesPigmentColor(_color))
                    {
                        bonus += _bonusAmount;
                    }
                    if (_contains == false && targetSlotInfo.Unit.HealthColor.pigmentID == _color.pigmentID)
                    {
                        bonus += _bonusAmount;
                    }
                    if (!targetSlotInfo.Unit.ContainsPassiveAbility(Passives.Pure.m_PassiveID) || !_pureBlocked) { amount += bonus; }
                    DamageInfo damageInfo;
                    if (_indirect)
                    {
                        damageInfo = targetSlotInfo.Unit.Damage(amount, null, _DeathTypeID, targetSlotOffset, addHealthMana: false, directDamage: false, ignoresShield: true);
                    }
                    else
                    {
                        amount = caster.WillApplyDamage(amount, targetSlotInfo.Unit);
                        damageInfo = targetSlotInfo.Unit.Damage(amount, caster, _DeathTypeID, targetSlotOffset, addHealthMana: true, directDamage: true, _ignoreShield);
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

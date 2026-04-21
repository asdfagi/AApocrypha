using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomEffects
{
    public class DamageWithPigmentRepetitionsEffect : EffectSO
    {
        [DeathTypeEnumRef]
        public string _DeathTypeID = "Basic";

        public bool _usePreviousExitValue;

        public bool _ignoreShield;

        public bool _indirect;

        public bool _returnKillAsSuccess;

        public ManaColorSO _color;

        public bool _contains = false;

        public int _threshold = 1;

        public bool _useCasterHealthColor = false;

        public AttackVisualsSO _visuals = Visuals.Crush;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (_usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }

            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                Debug.Log(manaSlot.IsEmpty ? "empty!" : manaSlot.ManaColor.name);
            }

            exitAmount = 0;
            bool flag = false;
            if (_useCasterHealthColor) { _color = caster.HealthColor; }
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    int targetSlotOffset = (areTargetSlots ? (targetSlotInfo.SlotID - targetSlotInfo.Unit.SlotID) : (-1));
                    bool isAlly = true;
                    if (caster.IsUnitCharacter && targetSlotInfo.Unit.IsUnitCharacter) { isAlly = false; }
                    if (!caster.IsUnitCharacter && !targetSlotInfo.Unit.IsUnitCharacter) { isAlly = false; }
                    int amount = entryVariable;
                    int bonus = 0;
                    int repetitions = 1;
                    foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
                    {
                        if (manaSlot.ManaColor != null)
                        {
                            if (_contains == false && manaSlot.ManaColor == _color)
                            {
                                bonus += 1;
                            }
                            if (_contains == true && manaSlot.ManaColor.ContainsPigment([_color.pigmentID]))
                            {
                                bonus += 1;
                            }
                            if (bonus >= _threshold) {
                                repetitions += 1;
                                bonus = 0;
                                Debug.Log("repetition up! new total " + repetitions);
                            }
                        }
                    }
                    DamageInfo damageInfo;
                    int i = 0;
                    Debug.Log("repetitions: " + repetitions);
                    while (i < repetitions)
                    {
                        if (_indirect)
                        {
                            if (i != 0) { CombatManager.Instance.AddUIAction(new PlayAbilityAnimationAction(_visuals, Targeting.GenerateSlotTarget([targetSlotOffset], !isAlly), caster)); }
                            damageInfo = targetSlotInfo.Unit.Damage(amount, null, _DeathTypeID, targetSlotOffset, addHealthMana: false, directDamage: false, ignoresShield: true);
                        }
                        else
                        {
                            if (i != 0) { CombatManager.Instance.AddUIAction(new PlayAbilityAnimationAction(_visuals, Targeting.GenerateSlotTarget([targetSlotOffset], !isAlly), caster)); }
                            amount = caster.WillApplyDamage(amount, targetSlotInfo.Unit);
                            damageInfo = targetSlotInfo.Unit.Damage(amount, caster, _DeathTypeID, targetSlotOffset, addHealthMana: true, directDamage: true, _ignoreShield);
                        }

                        flag |= damageInfo.beenKilled;
                        exitAmount += damageInfo.damageAmount;
                        //if (damageInfo.beenKilled) { break; }
                        i++;
                    }
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

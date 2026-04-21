using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ApplyOrRemoveCostOfColorEffect : EffectSO
    {
        public ManaColorSO _mana;
        public bool _removeCost = false;
        public bool _skipSlapLikes = true;
        public ManaColorSO[] ExpandedArray(int length, ManaColorSO[] OrigCost)
        {
            List<ManaColorSO> list = [];
            list.Add(_mana);
            for (int i = 0; i < length; i++)
            {
                list.Add(OrigCost[i]);
            }
            return [.. list];
        }

        public ManaColorSO[] ReducedArray(int length, ManaColorSO[] OrigCost)
        {
            List<ManaColorSO> list = [];
            bool sorted = false;
            for (int i = 0; i < length; i++)
            {
                if (OrigCost[i] == _mana && sorted == false)
                {
                    sorted = true;
                }
                else { list.Add(OrigCost[i]); }
            }
            return [.. list];
        }

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_mana == null) { return false; }
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit && targetSlotInfo.Unit is CharacterCombat cc)
                {
                    foreach (var ab in cc.CombatAbilities)
                    {
                        if (ab.ability == cc.Character.basicCharAbility.ability && _skipSlapLikes) { continue; }
                        int num = ab.cost.Length;
                        ab.cost = (_removeCost ? ReducedArray(num, ab.cost) : ExpandedArray(num, ab.cost));
                        exitAmount += num;
                    }
                    foreach (CharacterCombatUIInfo characterCombatUIInfo in stats.combatUI._charactersInCombat.Values)
                    {
                        bool flag3 = characterCombatUIInfo.SlotID == targetSlotInfo.Unit.SlotID;
                        if (flag3)
                        {
                            characterCombatUIInfo.UpdateAttacks([.. (targetSlotInfo.Unit as CharacterCombat).CombatAbilities]);
                            break;
                        }
                    }
                }
            }
            return exitAmount > 0;
        }

    }
}

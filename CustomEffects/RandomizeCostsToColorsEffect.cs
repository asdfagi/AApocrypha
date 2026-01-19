using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class RandomizeCostsToColorsEffect : EffectSO
    {
        // from the Hell Island Fell github (Custom Effects/RandomizeCostsToColorsEffect.cs)
        public ManaColorSO[] _mana = [];
        public ManaColorSO[] RandomArray(int length, ManaColorSO[] OrigCost)
        {
            List<ManaColorSO> list = [];
            for (int i = 0; i < length; i++)
            {
                list.Add(RandomPig());
            }
            return [.. list];
        }

        public ManaColorSO RandomPig()
        {
            ManaColorSO[] array = _mana;
            return array[UnityEngine.Random.Range(0, array.Length)];
        }

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit && targetSlotInfo.Unit is CharacterCombat cc)
                {
                    foreach (var ab in cc.CombatAbilities)
                    {
                        int num = ab.cost.Length;
                        ab.cost = RandomArray(num, ab.cost);
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

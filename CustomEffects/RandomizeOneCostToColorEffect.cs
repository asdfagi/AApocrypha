using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class RandomizeOneCostToColorEffect : EffectSO
    {
        public ManaColorSO _mana;
        public ManaColorSO[] RandomTransform(int length, ManaColorSO[] OrigCost)
        {
            List<ManaColorSO> list = [];
            bool tracker = false;
            int costIndex = -1;
            while (!tracker)
            {
                int randomIndex = UnityEngine.Random.Range(0, length);
                if (OrigCost[randomIndex] != _mana)
                {
                    tracker = true;
                    costIndex = randomIndex;
                }
            }
            for (int i = 0; i < length; i++)
            {
                list.Add((i == costIndex ? _mana : OrigCost[i]));
            }
            return [.. list];
        }

        public bool CheckValidTarget(ManaColorSO[] OrigCost)
        {
            ManaColorSO toCheck = _mana;
            foreach (ManaColorSO cost in OrigCost)
            {
                if (cost != toCheck) { return true; }
            }
            return false;
        }

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_mana == null) { return false; }
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit && targetSlotInfo.Unit is CharacterCombat cc)
                {
                    List<CombatAbility> validAbilities = [];
                    foreach (var ab in cc.CombatAbilities)
                    {
                        if (CheckValidTarget(ab.cost))
                        {
                            validAbilities.Add(ab);
                        }
                    }
                    while (validAbilities.Count > 1)
                    {
                        int randomIndex = UnityEngine.Random.Range(0, validAbilities.Count);
                        validAbilities.Remove(validAbilities[randomIndex]);
                    }
                    if (validAbilities.Count <= 0) { return false; }
                    int num = validAbilities[0].cost.Length;
                    foreach (var ab in cc.CombatAbilities)
                    {
                        if (ab.ability == validAbilities[0].ability)
                        {
                            ab.cost = RandomTransform(num, ab.cost);
                            exitAmount += num;
                            break;
                        }
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

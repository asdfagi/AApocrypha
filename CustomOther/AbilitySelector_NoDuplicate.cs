using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    // based off of the Salt Enemies github (CustomEffects/Bosses/CrowChildEffects.cs)
    public class AbilitySelector_NoDuplicate : BaseAbilitySelectorSO
    {
        public Dictionary<int, string> MostRecent;
        public override bool UsesRarity => true;
        public override int GetNextAbilitySlotUsage(List<CombatAbility> abilities, IUnit unit)
        {
            if (MostRecent == null) {
                MostRecent = new Dictionary<int, string>();
            }

            int num = 0;
            int num2 = 0;
            List<int> list = new List<int>();
            List<int> list2 = new List<int>();
            for (int i = 0; i < abilities.Count; i++)
            {
                if (ShouldBeIgnored(abilities[i], unit))
                {
                    num2 += abilities[i].rarity.rarityValue;
                    list2.Add(i);
                }
                else
                {
                    num += abilities[i].rarity.rarityValue;
                    list.Add(i);
                }
            }

            int num3 = UnityEngine.Random.Range(0, num);
            num = 0;
            foreach (int item in list)
            {
                num += abilities[item].rarity.rarityValue;
                if (num3 < num)
                {
                    MostRecent[unit.ID] = abilities[item].ability.name;
                    return item;
                }
            }

            num3 = UnityEngine.Random.Range(0, num2);
            num2 = 0;
            foreach (int item2 in list2)
            {
                num2 += abilities[item2].rarity.rarityValue;
                if (num3 < num2)
                {
                    MostRecent[unit.ID] = abilities[item2].ability.name;
                    return item2;
                }
            }

            return -1;
        }
        public bool ShouldBeIgnored(CombatAbility ability, IUnit unit)
        {
            if (MostRecent.TryGetValue(unit.ID, out string value))
            {
                return ability.ability.name == value;
            }

            IntegerReference intReference = new IntegerReference(entryValue: 0);
            CombatManager.Instance.ProcessImmediateAction(new CheckAliveEnemiesIAction(intReference));
            return intReference.value >= 5;
        }
    }
}

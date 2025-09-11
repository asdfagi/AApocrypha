using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class AbilitySelector_SculptorBird : BaseAbilitySelectorSO
    {
        public string _clearAbility = "AApocrypha_ExciseFlaws_A";
        public override bool UsesRarity => true;
        public override int GetNextAbilitySlotUsage(List<CombatAbility> abilities, IUnit unit)
        {
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
                    return item2;
                }
            }

            return -1;
        }
        public bool ShouldBeIgnored(CombatAbility ability, IUnit unit)
        {
            if (ability.ability.name != _clearAbility)
            {
                return false;
            }

            BooleanReference booleanReference = new BooleanReference(entryValue: false);
            CombatManager.Instance.ProcessImmediateAction(new CheckUnitHasAnyOfStatusEffectsIAction(booleanReference, [StatusField.Scars._StatusID, StatusField.Ruptured._StatusID, StatusField.OilSlicked._StatusID, StatusField.Frail._StatusID], unit));
            return !booleanReference.value;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class AbilitySelector_Assessor : BaseAbilitySelectorSO
    {
        public string _clearAbility = "AApocrypha_FactoryReset_A";

        public string _openerAbility = "AApocrypha_AutomatedAmputation_A";
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
            IntegerReference roundIntReference = new IntegerReference(-1);
            CombatManager.Instance.ProcessImmediateAction(new CheckPlayerTurnCountIAction(roundIntReference));
            if (roundIntReference.value < 1)
            {
                if (ability.ability.name == _openerAbility) { return false; }
                else { return true; }
            }

            if (ability.ability.name != _clearAbility)
            {
                return false;
            }

            IntegerReference intReference = new IntegerReference(entryValue: 0);
            CombatManager.Instance.ProcessImmediateAction(new CheckAliveEnemiesIAction(intReference));
            return intReference.value <= 1;
        }
    }
}

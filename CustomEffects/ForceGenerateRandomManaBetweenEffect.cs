using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.CustomEffects
{
    public class ForceGenerateRandomManaBetweenEffect : EffectSO
    {
        public bool usePreviousExitValueAsMultiplier;

        public ManaColorSO[] possibleMana;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (possibleMana.Length < 0)
            {
                exitAmount = 0;
                return false;
            }

            if (usePreviousExitValueAsMultiplier)
            {
                entryVariable *= base.PreviousExitValue;
            }

            exitAmount = entryVariable;
            int num = -1;
            int num2 = 1;
            for (int i = 0; i < entryVariable; i++)
            {
                int num3 = UnityEngine.Random.Range(0, possibleMana.Length);
                if (num == -1)
                {
                    num = num3;
                    continue;
                }

                if (num == num3)
                {
                    num2++;
                    continue;
                }

                CombatManager.Instance.ProcessImmediateAction(new ForceAddManaToManaBarAction(possibleMana[num], num2, caster.IsUnitCharacter, caster.ID));
                num = num3;
                num2 = 1;
            }

            if (num >= 0)
            {
                CombatManager.Instance.ProcessImmediateAction(new ForceAddManaToManaBarAction(possibleMana[num], num2, caster.IsUnitCharacter, caster.ID));
            }

            return true;
        }
    }
}

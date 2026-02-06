using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class SimulacrumWipeCopyEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (caster is EnemyCombat casterEN)
            {
                CombatAbility comeagain = casterEN.Abilities[0];
                List<CombatAbility> newAbilities = new List<CombatAbility>();
                newAbilities.Add(comeagain);
                casterEN.Abilities = newAbilities;
                List<BasePassiveAbilitySO> newPassives = new List<BasePassiveAbilitySO>();
                newPassives.AddRange(casterEN.Enemy.passiveAbilities);
                casterEN.TryRemoveAllPassiveAbilities();
                foreach (BasePassiveAbilitySO newPassive in newPassives)
                {
                    casterEN.AddPassiveAbility(newPassive);
                }
                exitAmount = 1;
            }

            return exitAmount > 0;
        }
    }
}

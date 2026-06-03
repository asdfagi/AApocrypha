using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CheckTargetHasEmptyNameEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is CharacterCombat ch)
                    {
                        if (ch._currentName == null || ch._currentName == "")
                        {
                            exitAmount++;
                            break;
                        }
                    }
                    else if (target.Unit is EnemyCombat en)
                    {
                        if (en._currentName == null || en._currentName == "")
                        {
                            exitAmount++;
                            break;
                        }
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}

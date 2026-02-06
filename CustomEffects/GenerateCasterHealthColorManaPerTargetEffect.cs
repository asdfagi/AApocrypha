using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.GraphicsBuffer;

namespace A_Apocrypha.CustomEffects
{
    public class GenerateCasterHealthColorManaPerTargetEffect : EffectSO
    {

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit) 
                {
                    exitAmount += entryVariable;
                    CombatManager.Instance.ProcessImmediateAction(new AddManaToManaBarAction(caster.HealthColor, entryVariable, target.Unit.IsUnitCharacter, target.Unit.ID));
                }
            }
            return exitAmount > 0;
        }
    }
}

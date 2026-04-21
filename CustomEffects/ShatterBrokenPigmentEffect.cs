using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ShatterBrokenPigmentEffect : EffectSO
    {
        // credits to WolfaCola
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = stats.MainManaBar.ConsumeAllManaColor(LoadedDBsHandler.PigmentDB.GetPigment("Broken"), null, "event:/AASFX/BrokenPigmentShatter");
            return exitAmount > 0;
        }
    }
}

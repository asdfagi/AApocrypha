using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetSplitOrReplaceHealthEffect : EffectSO
    {
        public ManaColorSO _color;
        public List<ManaColorSO> _colorBlacklist;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_color == null || _colorBlacklist == null) { return false; }
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    IUnit targetUnit = target.Unit;
                    if (targetUnit.HealthColor.ContainsPigment([_color.pigmentID]))
                    {
                        continue;
                    }
                    if (_colorBlacklist.Contains(targetUnit.HealthColor))
                    {
                        targetUnit.ChangeHealthColor(_color);
                        exitAmount++;
                    }
                    else
                    {
                        List<ManaColorSO> newColors = [];
                        List<ManaColorSO> validInputColors = [];
                        //Debug.Log($"testing unit {targetUnit.Name} with health color {targetColor.pigmentID}");
                        foreach (ManaColorSO loadedPigment in LoadedDBsHandler.PigmentDB._PigmentPool.Values)
                        {
                            //Debug.Log($"testing pigment {loadedPigment.pigmentID}...");
                            if (targetUnit.HealthColor.ContainsPigment([loadedPigment.pigmentID]))
                            {
                                //Debug.Log($"match found - adding {loadedPigment.pigmentID}");
                                newColors.Add(loadedPigment);
                            }
                        }
                        if (newColors.Count == 0)
                        {
                            Debug.LogWarning($"Health Splitter | no valid pigments registered in unit {targetUnit.Name} - skipping...");
                            continue;
                        }
                        newColors.Add(_color);
                        targetUnit.ChangeHealthColor(Pigments.SplitPigment(newColors.ToArray()));
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}

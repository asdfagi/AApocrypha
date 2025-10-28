using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.GraphicsBuffer;

namespace A_Apocrypha.CustomEffects
{
    public class RainbowRefractionEffect : EffectSO
    {
        public ManaColorSO[] manas;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit) 
                {
                    IUnit targetUnit = target.Unit;
                    if (targetUnit.HealthColor != Pigments.Grey && targetUnit.HealthColor != LoadedDBsHandler.PigmentDB.GetPigment("Rainbow"))
                    {
                        exitAmount += 1;
                        ManaColorSO targetColor = targetUnit.HealthColor;
                        List<ManaColorSO> newColors = [];
                        List<ManaColorSO> validInputColors = [];
                        //Debug.Log($"testing unit {targetUnit.Name} with health color {targetColor.pigmentID}");
                        foreach (ManaColorSO loadedPigment in LoadedDBsHandler.PigmentDB._PigmentPool.Values)
                        {
                            //Debug.Log($"testing pigment {loadedPigment.pigmentID}...");
                            if (targetColor.ContainsPigment([loadedPigment.pigmentID]))
                            {
                                //Debug.Log($"match found - adding {loadedPigment.pigmentID}");
                                newColors.Add( loadedPigment );
                            }
                        }
                        if (newColors.Count == 0) {
                            Debug.LogWarning($"no valid pigments registered in unit {targetUnit.Name} - skipping...");
                            continue;
                        };
                        foreach (ManaColorSO mana in manas)
                        {
                            if (!newColors.Contains(mana))
                            {
                                validInputColors.Add( mana );
                            }
                        }
                        //Debug.Log($"unit {targetUnit.Name} checked - matching colors: {newColors.Count}");
                        if (validInputColors.Count <= 0 || newColors.Count >= entryVariable)
                        {
                            targetUnit.ChangeHealthColor(LoadedDBsHandler.PigmentDB.GetPigment("Rainbow"));
                            //Debug.Log($"unit {targetUnit.Name} meets criteria - rainbowed");
                        } else
                        {
                            int randomIndex = UnityEngine.Random.Range(0, validInputColors.Count);
                            //Debug.Log($"adding pigment {validInputColors[randomIndex].pigmentID} to unit {targetUnit.Name}...");
                            newColors.Add(validInputColors[randomIndex]);
                            targetUnit.ChangeHealthColor(Pigments.SplitPigment(newColors.ToArray()));
                        }
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetExtractHealthColorEffect : EffectSO
    {
        public ManaColorSO _color;
        public ManaColorSO[] _fallbackColors;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<ManaColorSO> pigmentFilter = new List<ManaColorSO>();
            PigmentFilterFiller(pigmentFilter, _color);

            if (_color == null || _fallbackColors == null) { return false; }
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    IUnit targetUnit = target.Unit;
                    if (targetUnit.HealthColor == _color)
                    {
                        if (_fallbackColors.Length == 1) { targetUnit.ChangeHealthColor(_fallbackColors[0]); }
                        else
                        {
                            int randomIndex = UnityEngine.Random.Range(0, _fallbackColors.Length);
                            targetUnit.ChangeHealthColor(_fallbackColors[randomIndex]);
                        }
                    }
                    if (targetUnit.HealthColor.SharesPigmentColor(_color))
                    {
                        List<ManaColorSO> newColors = [];
                        List<ManaColorSO> validInputColors = [];
                        //Debug.Log($"testing unit {targetUnit.Name} with health color {targetColor.pigmentID}");
                        foreach (ManaColorSO pigmentFilterEntry in pigmentFilter)
                        {
                            if (targetUnit.HealthColor.SharesPigmentColor(pigmentFilterEntry))
                            {
                                newColors.Add(pigmentFilterEntry);
                            }
                        }
                        if (newColors.Count == 0)
                        {
                            Debug.LogWarning($"Health Splitter | no valid pigments registered in unit {targetUnit.Name} - skipping...");
                            continue;
                        }
                        //Debug.Log($"Health Splitter | final output length: {newColors.Count}");
                        foreach (ManaColorSO newColor in newColors)
                        {
                            //Debug.Log($"Health Splitter | final output contains {newColor.name}");
                        }
                        if (newColors.Count == 1) { if (targetUnit.ChangeHealthColor(newColors[0])) { exitAmount++; } }
                        else { if (targetUnit.ChangeHealthColor(Pigments.SplitPigment(newColors.ToArray()))) { exitAmount++; } }
                    }
                }
            }
            return exitAmount > 0;
        }
        static void PigmentFilterFiller(List<ManaColorSO> list, ManaColorSO forbiddenColor)
        {
            ManaColorSO[] vanillaPigments = [Pigments.Red, Pigments.Blue, Pigments.Yellow, Pigments.Purple, Pigments.Green, Pigments.Grey];
            foreach (ManaColorSO color in vanillaPigments)
            {
                if (color != forbiddenColor)
                {
                    Debug.Log($"Pigment Filter | added pigment {color.name}");
                    list.Add(color);
                }
            }
            
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                ManaColorSO[] itaPigments = [
                    LoadedDBsHandler.PigmentDB.GetPigment("Iridescent"),
                    LoadedDBsHandler.PigmentDB.GetPigment("Clusterfuck"),
                    LoadedDBsHandler.PigmentDB.GetPigment("EntropicBase"),
                ];
                foreach (ManaColorSO color in itaPigments)
                {
                    if (color != forbiddenColor)
                    {
                        Debug.Log($"Pigment Filter | added pigment {color.name}"); 
                        list.Add(color); 
                    }
                }
            }
        }
    }
}

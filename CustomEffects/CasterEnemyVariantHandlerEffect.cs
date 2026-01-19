using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    internal class CasterEnemyVariantHandlerEffect : EffectSO
    {
        public int _variantNumber = 1;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_variantNumber <= 0)
            {
                Debug.LogWarning($"Variant Handler | _variantNumber {_variantNumber} is less than 1!");
                return false;
            }
            int variantIndex = UnityEngine.Random.Range(0, _variantNumber) + 1;
            Debug.Log($"Variant Handler | variantIndex of {variantIndex}");
            CombatManager.Instance.AddUIAction(new SetUnitAnimationParameterUIAction(caster.ID, caster.IsUnitCharacter, "Variant", variantIndex));
            exitAmount = variantIndex + 1;
            return exitAmount > 0;
        }
    }
}

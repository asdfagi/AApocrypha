using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    internal class ModifyCasterAnimationParameterEffect : EffectSO
    {
        public string _parameterName = "";

        public int _parameterValue = 0;

        public bool _UsePrevious = false;

        public bool _subtract = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            CombatManager.Instance.AddUIAction(new SetUnitAnimationParameterUIAction(caster.ID, caster.IsUnitCharacter, _parameterName, _UsePrevious ? base.PreviousExitValue : _parameterValue));
            return true;
        }
    }
}

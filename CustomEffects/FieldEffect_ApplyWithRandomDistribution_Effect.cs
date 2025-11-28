using System;
using System.Collections.Generic;
using System.Text;
using Random = UnityEngine.Random;

namespace A_Apocrypha.CustomEffects
{
    public class FieldEffect_ApplyWithRandomDistribution_Effect : EffectSO
    {
        // thanks MillieAmp
        public FieldEffect_SO field;
        public bool usePrevious;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (usePrevious)
            {
                entryVariable *= base.PreviousExitValue;
            }
            Dictionary<IUnit, int> applyTo = [];
            foreach (KeyValuePair<IUnit, int> pairshit in applyTo)
            {
                applyTo.Remove(pairshit.Key);
            }
            exitAmount = 0;
            List<IUnit> list = new List<IUnit>();
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    if (!list.Contains(targetSlotInfo.Unit))
                    {
                        list.Add(targetSlotInfo.Unit);
                    }

                }
            }

            if (list.Count <= 0)
            {
                return false;
            }

            //IUnit[] applyTo;
            foreach (IUnit unit2 in list)
            {
                if (!applyTo.ContainsKey(unit2))
                {
                    applyTo.Add(unit2, 0);
                }
            }
            for (int j = 0; j < entryVariable; j++)
            {
                int index = UnityEngine.Random.Range(0, list.Count);
                IUnit unit = list[index];
                if (!applyTo.ContainsKey(unit))
                {
                    applyTo.Add(unit, 1);
                }
                else
                {
                    applyTo[unit] += 1;
                }
            }
            foreach (KeyValuePair<IUnit, int> applypair in applyTo)
            {
                if (applypair.Value != 0)
                {
                    FieldEffect_Apply_Effect doField = ScriptableObject.CreateInstance<FieldEffect_Apply_Effect>();
                    doField._Field = field;
                    EffectInfo[] fuck =
                    [
                        Effects.GenerateEffect(doField, applypair.Value, Targeting.Slot_SelfAll),
                    ];
                    CombatManager.Instance.AddSubAction(new EffectAction(fuck, (applypair.Key as IUnit)));
                    //CombatManager.Instance.AddUIAction(new CharacterStatusEffectAppliedUIAction(applypair.Key.ID, field._EffectInfo, field.name, applypair.Value, true));
                    exitAmount += applypair.Value;
                }
            }
            return exitAmount > 0;
        }
    }
}
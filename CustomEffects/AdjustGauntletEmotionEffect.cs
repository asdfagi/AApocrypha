using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace A_Apocrypha.CustomEffects
{
    public class AdjustGauntletEmotionEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            OverworldCombatSharedDataSO current = CombatManager.Instance._informationHolder.CombatData;
            Debug.Log(CombatManager.Instance._combatEnvHandler.gameObject.transform.Find("GauntletTerminal").name);

            Animator animator = CombatManager.Instance._combatEnvHandler.gameObject.transform.Find("GauntletTerminal").GetComponent<Animator>();
            AnimatorControllerParameter[] parameters = animator.parameters;

            foreach (AnimatorControllerParameter animatorControllerParameter in parameters)
            {
                if (animatorControllerParameter.name == "Face")
                {
                    if (animatorControllerParameter.type == AnimatorControllerParameterType.Int)
                    {
                        animator.SetInteger("Face", entryVariable);
                    }
                }
            }

            return true;
        }
    }
}

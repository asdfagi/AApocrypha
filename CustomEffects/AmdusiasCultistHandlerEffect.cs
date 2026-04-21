using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class AmdusiasCultistHandlerEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            OverworldCombatSharedDataSO current = CombatManager.Instance._informationHolder.CombatData;
            Debug.Log(CombatManager.Instance._combatEnvHandler.gameObject.transform.Find("Crowd").name);

            Animator animator = CombatManager.Instance._combatEnvHandler.gameObject.transform.Find("Crowd").GetComponent<Animator>();
            AnimatorControllerParameter[] parameters = animator.parameters;

            bool crowdTracker = true;

            foreach (AnimatorControllerParameter animatorControllerParameter in parameters)
            {
                if (animatorControllerParameter.name == "Cultists")
                {
                    if (animatorControllerParameter.type == AnimatorControllerParameterType.Int)
                    {
                        int currentValue = animator.GetInteger("Cultists");
                        if (currentValue > 0) { animator.SetInteger("Cultists", currentValue - 1); }
                        else { crowdTracker = false; }
                    }
                }
            }

            return crowdTracker;
        }
    }
}

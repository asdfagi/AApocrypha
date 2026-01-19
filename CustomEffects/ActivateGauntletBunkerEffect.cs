using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ActivateGauntletBunkerEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            OverworldCombatSharedDataSO current = CombatManager.Instance._informationHolder.CombatData;
            CombatManager.Instance._combatEnvHandler.gameObject.SetActive(false);
            CombatManager.Instance.GenerateCombatEnvironment("GauntletCalmEnv", "");
            CombatManager.Instance._combatEnvHandler.SetUpNotifications();
            CombatManager.Instance._combatEnvHandler.InitializeExtraData(CombatManager.Instance._informationHolder.Game);

            return true;
        }
    }
}

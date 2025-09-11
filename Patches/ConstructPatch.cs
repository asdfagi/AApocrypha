using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public static class ConstructPatch
    {
        [HarmonyPatch(typeof(EnemyCombat), nameof(EnemyCombat.GetAvailableAbilities))]
        [HarmonyPrefix]
        public static void GetAvailableAbilitiesRarityPatch(EnemyCombat __instance)
        {
            foreach (CombatAbility ability in __instance.Abilities)
            {
                if (ability == null || ability.rarity != null)
                    continue;

                ability.rarity = Rarity.Impossible;
            }
        }
    }
}

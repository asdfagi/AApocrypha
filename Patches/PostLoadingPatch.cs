using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Items;
using HarmonyLib;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public class PostLoadingPatch
    {
        static bool _called = false;

        [HarmonyPatch(typeof(MainMenuController), nameof(MainMenuController.Start))]
        [HarmonyPrefix]
        public static bool PostLoading(MainMenuController __instance)
        {
            if (!_called)
            {
                // Deathmatch Unlock Handler
                if (LoadedAssetsHandler.LoadedEnemyBundles.ContainsKey("Deathmatch_BOSS"))
                {
                    Debug.Log("AApocrypha Post-Loading | Deathmatch Unlocks");
                    BloodiedFlint.Add();
                    HoneyStainedShackles.Add();

                    AgeOfDestruction.Add();

                    Chthonosophy.Add();

                }
                _called = true;
            }
            return true;
        }
    }
}

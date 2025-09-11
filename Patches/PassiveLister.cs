using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace A_Apocrypha.Patches
{
    //[HarmonyPatch]
    public static class PassiveLister
    {
        //[HarmonyPatch(typeof(RunDataSO), "InitializeRun")]
        //[HarmonyPostfix]
        public static void PrintLoadedPassives()
        {
            Debug.Log("Listing Passives...");

            foreach (BasePassiveAbilitySO item in LoadedAssetsHandler.LoadedPassives.Values)
            {
                Debug.Log("Passive [" + item._passiveName + "] with m_PassiveID [" + item.m_PassiveID + "]");
            }
        }
    }
}

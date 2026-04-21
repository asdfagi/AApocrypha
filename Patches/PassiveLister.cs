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
        public static void PrintLoadedRooms()
        {
            Debug.Log("Listing Room IDs in shore H");

            ZoneBGDataBaseSO shorehard = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using A_Apocrypha.Events;
using HarmonyLib;
using Tools;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public class BarHandlerPatch
    {
        [HarmonyPatch(typeof(RunDataSO), nameof(RunDataSO.InitializeRun))]
        [HarmonyPostfix]
        public static void BarHandlerInit(RunDataSO __instance, IGameCheckData gameData, InitialCharacter[] initialCharacters)
        {
            // BAR SEAT HANDLER
            //Debug.Log("Bar Handler | patch is working!");
            int barSeatShuffler1 = UnityEngine.Random.Range(0, BarHandler._seats.Length);
            LoadedDBsHandler.InfoHolder.Game.SetIntData("AA_BarSeatShuffler1", barSeatShuffler1);
            //Debug.Log("Bar Handler | chosen shuffler value for seat 1: " + LoadedDBsHandler.InfoHolder.Game.GetIntData("AA_BarSeatShuffler1"));
            LoadedDBsHandler.InfoHolder.Game.SetIntData("AA_BarSeatLoaded", 0);

            // MAIN CHARACTER DETECTOR
            foreach (InitialCharacter initCH in initialCharacters)
            {
                //Debug.Log("Bar Handler | initial character " + initCH.character.name);
                if (initCH.isMainCharacter)
                {
                    //Debug.Log("Bar Handler | " + initCH.character._characterName + " is the main character!");
                    __instance.inGameData.SetStringData("AA_MainCharacter", initCH.character.name);
                    //Debug.Log("Bar Handler | value saved to AA_MainCharacter: " + __instance.inGameData.GetStringData("AA_MainCharacter"));
                    break;
                }
            }

            // MEASURER ROBOT LOOT DETECTOR
            // bar seat shuffler 1 - corresponds to second bar option, the Measurer of the Institute
            LoadedDBsHandler.InfoHolder.Game.SetBoolData("AA_InstituteRobotLootCheck", barSeatShuffler1 == 1);
            if (barSeatShuffler1 == 1) { Debug.Log("Bar Handler | Robot Loot Drops Enabled"); }

            // (OBSOLETE) WHITLOCK STASH ADDER
            /*if (__instance.inGameData.GetStringData("AA_MainCharacter") == "Whitlock_CH")
            {
                foreach (RunZoneData data in __instance.zoneData)
                {
                    Debug.Log(data._zoneDBName);
                    if (data._zoneDBName == "ZoneDB_Hard_01")
                    {
                        //ZoneBGDataBaseSO zoneBG = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
                        //zoneBG._QuestPool.Add("Whitlock_Stash");
                    }
                }
            }*/
        }
    }
}

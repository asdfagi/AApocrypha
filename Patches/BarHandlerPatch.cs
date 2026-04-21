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
            //Debug.Log("Bar Handler | patch is working!");
            LoadedDBsHandler.InfoHolder.Game.SetIntData("AA_BarSeatShuffler1", UnityEngine.Random.Range(0, BarHandler._seats.Length));
            //Debug.Log("Bar Handler | chosen shuffler value for seat 1: " + LoadedDBsHandler.InfoHolder.Game.GetIntData("AA_BarSeatShuffler1"));
            LoadedDBsHandler.InfoHolder.Game.SetIntData("AA_BarSeatLoaded", 0);
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
        }
    }
}

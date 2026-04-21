using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Events;
using HarmonyLib;

namespace A_Apocrypha.Patches
{
    [HarmonyPatch]
    public class BarLoadHandlerPatch
    {
        [HarmonyPatch(typeof(BarRoomHandler), nameof(BarRoomHandler.PopulateRoom))]
        [HarmonyPostfix]
        public static void TellMeDamnYou(IGameCheckData gameData, IMinimalRunInfoData runData, IMinimalZoneInfoData zoneData, int dataID)
        {
            //Debug.Log("bar seats: " + zoneData.GetBarData(dataID).barSeats.Values.Count);
            foreach (var thingy in zoneData.GetBarData(dataID).barSeats.Values)
            {
                //Debug.Log($"bar seat - state {thingy.seatState} - extra state {thingy.seatExtraState} - selectionID {thingy.seatSelectionID}");
            }
        }

        [HarmonyPatch(typeof(BarRoomHandler), nameof(BarRoomHandler.PopulateRoom))]
        [HarmonyPrefix]
        public static void DupeBlocker(IGameCheckData gameData)
        {
            if (LoadedDBsHandler.InfoHolder.Game.GetIntData("AA_BarSeatLoaded") == 1) { return; }
            int shuffler1 = gameData.GetIntData("AA_BarSeatShuffler1");
            //Debug.Log("bar dupe blocker: AA_BarSeatShuffler1 value is " + shuffler1);
            foreach (string entity in LoadedDBsHandler.InfoHolder.Run.entitiesInRun)
            {
                //Debug.Log("entity in run: " + entity);
                if (shuffler1 >= BarHandler._seats.Length) { continue; }
                if (BarHandler._seats[shuffler1].m_EntityID == entity)
                {
                    //Debug.Log("bar seating: " + entity + " already loaded! emptying bar seat 1");
                    LoadedDBsHandler.InfoHolder.Game.SetIntData("AA_BarSeatShuffler1", -1);
                }
            }

            LoadedDBsHandler.InfoHolder.Game.SetIntData("AA_BarSeatLoaded", 1);
        }
    }
}

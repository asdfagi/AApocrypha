using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class WhitlockFreeEvent
    {
        public static void Add()
        {
            string text = "Whitlock_Dialogue";
            string text2 = "Whitlock_FreeFool";
            string text3 = "Whitlock_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/WhitlockFree.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/WhitlockFreeScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Whitlock.TryHire");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("WhitlockOverworld", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Whitlock_CH",
            };
            freeFoolEncounterSO._freeFool = "Whitlock_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO2 = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO2._FreeFoolsPool.Add(text2);
        }
    }
}

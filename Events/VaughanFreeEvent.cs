using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class VaughanFreeEvent
    {
        public static void Add()
        {
            string text = "Vaughan_Dialogue";
            string text2 = "Vaughan_FreeFool";
            string text3 = "Vaughan_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/VaughanFree.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/VaughanFreeScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Vaughan.TryHire");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("VaughanOverworld", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Vaughan_CH",
            };
            freeFoolEncounterSO._freeFool = "Vaughan_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO2 = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_02") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO2._FreeFoolsPool.Add(text2);
            Debug.Log("Free Fool Events | Orpheum | Vaughan");
        }
    }
}

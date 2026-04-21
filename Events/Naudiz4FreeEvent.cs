using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class Naudiz4FreeEvent
    {
        public static void Add()
        {
            string text = "Naudiz4_Dialogue";
            string text2 = "Naudiz4_FreeFool";
            string text3 = "Naudiz4_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/Naudiz4Free.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/Naudiz4FreeScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Naudiz4.TryHire");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("Naudiz4Overworld", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Naudiz4_CH",
            };
            freeFoolEncounterSO._freeFool = "Naudiz4_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO2 = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO2._FreeFoolsPool.Add(text2);
            Debug.Log("Free Fool Events | Far Shore | Naudiz 4");
        }
    }
}

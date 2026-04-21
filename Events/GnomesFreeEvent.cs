using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class GnomesFreeEvent
    {
        public static void Add()
        {
            string text = "Gnomes_Dialogue";
            string text2 = "Gnomes_FreeFool";
            string text3 = "Gnomes_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/GnomesFree.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/GnomesFreeScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Gnomes.TryHire");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("GnomesTimeline", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Gnome_CH"
            };
            freeFoolEncounterSO._freeFool = "Gnome_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO = LoadedAssetsHandler.GetZoneDB("TheAbyss") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO._FreeFoolsPool.Add(text2);
            Debug.Log("Free Fool Events | Abyss | Gnomes");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class GauntletEvents
    {
        public static void Add()
        {
            SpeakerBundle speakerBundleGauntlet = new SpeakerBundle();
            speakerBundleGauntlet.bundleTextColor = new Color(0.2f, 0.4f, 1f);
            speakerBundleGauntlet.dialogueSound = "event:/AASFX/DX/gauntlet-terminal-dx";
            speakerBundleGauntlet.portrait = ResourceLoader.LoadSprite("GauntletTerminalTalk", new Vector2(0.5f, 0f), 32);
            Dialogues.CreateAndAddCustom_SpeakerData("Gauntlet", speakerBundleGauntlet, true, false, new SpeakerEmote[0]);

            string text = "Gauntlet_Dialogue_Shore";
            string text2 = "Gauntlet_Shore";
            string text3 = "Gauntlet_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/GauntletShore.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/GauntletScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Gauntlet.Interact");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("GauntletIcon", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            ConditionEncounterSO gauntletShore = ScriptableObject.CreateInstance<ConditionEncounterSO>();
            gauntletShore.encounterEntityIDs =
            [
                "Gauntlet_NPC",
            ];
            gauntletShore.m_QuestName = "Gauntlet";
            gauntletShore.m_QuestsCompletedNeeded = [];
            gauntletShore.encounterRoom = text2;
            gauntletShore._dialogue = text;
            gauntletShore.signID = text3;
            ModdedNPCs.AddCustom_ConditionEncounter(text2, gauntletShore);
            ZoneBGDataBaseSO zBGDB = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zBGDB._QuestPool.Add(text2);
            /*FreeFoolEncounterSO freeFoolEncounterSO = ScriptableObject.CreateInstance<FreeFoolEncounterSO>();
            freeFoolEncounterSO.encounterEntityIDs = new string[]
            {
                "Kneynsberg_CH",
            };
            freeFoolEncounterSO._freeFool = "Kneynsberg_CH";
            freeFoolEncounterSO.signID = text3;
            freeFoolEncounterSO._dialogue = text;
            freeFoolEncounterSO.encounterRoom = text2;
            ModdedNPCs.AddCustom_FreeFoolEncounter(text2, freeFoolEncounterSO);
            ZoneBGDataBaseSO zoneBGDataBaseSO2 = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zoneBGDataBaseSO2._FreeFoolsPool.Add(text2);*/
        }
    }
}

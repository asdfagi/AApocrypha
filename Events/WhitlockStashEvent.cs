using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.Patches;

namespace A_Apocrypha.Events
{
    public class WhitlockStashEvent
    {
        public static void Add()
        {
            string text = "Whitlock_Stash_Dialogue";
            string text2 = "Whitlock_Stash";
            string text3 = "Whitlock_Stash_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/WhitlockStash.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/WhitlockStashScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Whitlock.Stash");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("WhitlockOverworld", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            ConditionEncounterSO stashEncounter = ScriptableObject.CreateInstance<ConditionEncounterSO>();
            stashEncounter.encounterEntityIDs = ["WhitlockStash"];
            stashEncounter.signID = text3;
            stashEncounter._dialogue = text;
            stashEncounter.encounterRoom = text2;
            stashEncounter.m_QuestsCompletedNeeded = [];
            ModdedNPCs.AddCustom_ConditionEncounter(text2, stashEncounter);
            //ZoneBGDataBaseSO zoneBG = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            //zoneBG._QuestPool.Add(text2);
            MainCharacterBonusCardPatch.AddWhitlockBonusEncounter(stashEncounter, "ZoneDB_Hard_01");
        }
    }
}

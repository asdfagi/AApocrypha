using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class KneynsbergParabolaShoreEvent
    {
        public static void Add()
        {
            string text = "Kneynsberg_Mirror_Dialogue";
            string text2 = "Kneynsberg_Mirror_Shore";
            string text3 = "Kneynsberg_Mirror_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/KneynsbergMirrorShore.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/KneynsbergMirrorScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Kneynsberg.FirstMeeting");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("GlassworkCoinIcon", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            ConditionEncounterSO kneynsbergMirrorShore = ScriptableObject.CreateInstance<ConditionEncounterSO>();
            kneynsbergMirrorShore.encounterEntityIDs =
            [
                "Kneynsberg_NPC",
            ];
            kneynsbergMirrorShore.m_QuestName = "KneynsbergMirrorInit";
            kneynsbergMirrorShore.m_QuestsCompletedNeeded = [];
            kneynsbergMirrorShore.encounterRoom = text2;
            kneynsbergMirrorShore._dialogue = text;
            kneynsbergMirrorShore.signID = text3;
            ModdedNPCs.AddCustom_ConditionEncounter(text2, kneynsbergMirrorShore);
            ZoneBGDataBaseSO zBGDB = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zBGDB._QuestPool.Add(text2);
        }
    }
}

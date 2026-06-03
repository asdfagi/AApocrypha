using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Events
{
    public class DarkWaterShoreEvent
    {
        public static void Add()
        {
            string text = "MrEaten_Dialogue";
            string text2 = "Well_Shore";
            string text3 = "Well_Sign";
            OverworldRooms.Prepare_NPC_RoomPrefab("Assets/Apocrypha_Rooms/Eaten/WellShore.prefab", text2, AApocrypha.assetBundle);
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/Eaten/MrEatenScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.MrEaten.FirstMeeting");
            Portals.AddPortalSign(text3, ResourceLoader.LoadSprite("EatenCoinIcon", new Vector2(0.5f, 0f), 32), Portals.NPCIDColor);
            BasicEncounterSO wellShore = ScriptableObject.CreateInstance<BasicEncounterSO>(); //switch to ConditionEncounterSO for quest stuff
            //ConditionEncounterSO wellShore = ScriptableObject.CreateInstance<ConditionEncounterSO>();
            wellShore.encounterEntityIDs =
            [
                "Mr_Eaten",
            ];
            //wellShore.m_QuestName = "EatenInit"; //only for conditionencounter
            //wellShore.m_QuestsCompletedNeeded = []; //only for conditionencounter
            wellShore.encounterRoom = text2;
            wellShore._dialogue = text;
            wellShore.signID = text3;
            ModdedNPCs.AddCustom_BasicEncounter(text2, wellShore); //switch to AddCustom_ConditionEncounter for quest stuff
            //ModdedNPCs.AddCustom_ConditionEncounter(text2, wellShore);
            ZoneBGDataBaseSO zBGDB = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            zBGDB._FlavourPool.Add(text2);

            SpeakerBundle speakerBundleEaten = new SpeakerBundle();
            speakerBundleEaten.bundleTextColor = new Color32(34, 107, 113, 255);
            speakerBundleEaten.dialogueSound = "event:/AASFX/Nothing_SFX";//LoadedAssetsHandler.GetCharacter("Kneynsberg_CH").dxSound;
            speakerBundleEaten.portrait = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleEaten1 = new SpeakerBundle();
            speakerBundleEaten1.bundleTextColor = new Color32(34, 86, 96, 255);
            speakerBundleEaten1.dialogueSound = speakerBundleEaten.dialogueSound;
            speakerBundleEaten1.portrait = speakerBundleEaten.portrait;

            SpeakerBundle speakerBundleEaten2 = new SpeakerBundle();
            speakerBundleEaten2.bundleTextColor = new Color32(55, 80, 99, 255);
            speakerBundleEaten2.dialogueSound = speakerBundleEaten.dialogueSound;
            speakerBundleEaten2.portrait = speakerBundleEaten.portrait;

            SpeakerBundle speakerBundleEaten3 = new SpeakerBundle();
            speakerBundleEaten3.bundleTextColor = new Color32(52, 55, 106, 255);
            speakerBundleEaten3.dialogueSound = speakerBundleEaten.dialogueSound;
            speakerBundleEaten3.portrait = speakerBundleEaten.portrait;

            SpeakerBundle speakerBundleEaten4 = new SpeakerBundle();
            speakerBundleEaten4.bundleTextColor = new Color32(47, 33, 86, 255);
            speakerBundleEaten4.dialogueSound = speakerBundleEaten.dialogueSound;
            speakerBundleEaten4.portrait = speakerBundleEaten.portrait;

            SpeakerBundle speakerBundleEaten5 = new SpeakerBundle();
            speakerBundleEaten5.bundleTextColor = new Color32(108, 43, 43, 255);
            speakerBundleEaten5.dialogueSound = speakerBundleEaten.dialogueSound;
            speakerBundleEaten5.portrait = speakerBundleEaten.portrait;

            Dialogues.CreateAndAddCustom_SpeakerData("MrEaten", speakerBundleEaten, true, false, new SpeakerEmote[5]
            {
                new SpeakerEmote {
                    emotion = "1",
                    bundle = speakerBundleEaten1,
                },
                new SpeakerEmote {
                    emotion = "2",
                    bundle = speakerBundleEaten2,
                },
                new SpeakerEmote {
                    emotion = "3",
                    bundle = speakerBundleEaten3,
                },
                new SpeakerEmote {
                    emotion = "4",
                    bundle = speakerBundleEaten4,
                },
                new SpeakerEmote {
                    emotion = "5",
                    bundle = speakerBundleEaten5,
                },
            });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using A_Apocrypha.CustomOther;

namespace A_Apocrypha.Events
{
    public class BarHandler
    {
        public static BarSeatData[] _seats = [];
        public static void Add(/*IGameCheckData gameData, PlayerInGameData oldPlayerData*/)
        {
            SpeakerBundle speakerBundleMeasurer = new SpeakerBundle();
            speakerBundleMeasurer.bundleTextColor = new Color32(117, 131, 144, 255);
            speakerBundleMeasurer.dialogueSound = "event:/AASFX/DX/gauntlet-terminal-dx";
            speakerBundleMeasurer.portrait = ResourceLoader.LoadSprite("InstituteMeasurerTalk", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMeasurerYay = new SpeakerBundle();
            speakerBundleMeasurerYay.bundleTextColor = speakerBundleMeasurer.bundleTextColor;
            speakerBundleMeasurerYay.dialogueSound = "event:/AASFX/DX/gauntlet-terminal-dx";//LoadedAssetsHandler.GetCharacter("Naudiz4_CH").dxSound;
            speakerBundleMeasurerYay.portrait = ResourceLoader.LoadSprite("InstituteMeasurerTalkHappy", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMeasurerMeh = new SpeakerBundle();
            speakerBundleMeasurerMeh.bundleTextColor = speakerBundleMeasurer.bundleTextColor;
            speakerBundleMeasurerMeh.dialogueSound = "event:/AASFX/DX/gauntlet-terminal-dx"; //LoadedAssetsHandler.GetCharacter("Naudiz4_CH").dxSound;
            speakerBundleMeasurerMeh.portrait = ResourceLoader.LoadSprite("InstituteMeasurerTalkMeh", new Vector2(0.5f, 0f), 32);

            SpeakerBundle speakerBundleMeasurerStudy = new SpeakerBundle();
            speakerBundleMeasurerStudy.bundleTextColor = speakerBundleMeasurer.bundleTextColor;
            speakerBundleMeasurerStudy.dialogueSound = "event:/AASFX/DX/gauntlet-terminal-dx"; //LoadedAssetsHandler.GetCharacter("Naudiz4_CH").dxSound;
            speakerBundleMeasurerStudy.portrait = ResourceLoader.LoadSprite("InstituteMeasurerTalkStudy", new Vector2(0.5f, 0f), 32);
            Dialogues.CreateAndAddCustom_SpeakerData("IGRMeasurer", speakerBundleMeasurer, true, false, new SpeakerEmote[3]
            {
                new SpeakerEmote {
                    emotion = "Happy",
                    bundle = speakerBundleMeasurerYay,
                },
                new SpeakerEmote {
                    emotion = "Meh",
                    bundle = speakerBundleMeasurerMeh,
                },
                new SpeakerEmote {
                    emotion = "Study",
                    bundle = speakerBundleMeasurerStudy,
                },
            });

            string text = "Whitlock_Bar_Dialogue";
            //string text2 = "WeirdSeat_Bar_Dialogue";
            string text2 = "InstituteMeasurer_Bar_Dialogue";
            ZoneBGDataBaseSO shorehard = LoadedAssetsHandler.GetZoneDB("ZoneDB_Hard_01") as ZoneBGDataBaseSO;
            
            YarnProgram yarnProgram = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/WhitlockBarScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text, yarnProgram);
            Dialogues.CreateAndAddCustom_DialogueSO(text, yarnProgram, text, "AApocrypha.Whitlock.BarStart");

            YarnProgram yarnProgram2 = AApocrypha.assetBundle.LoadAsset<YarnProgram>(string.Format("Assets/Apocrypha_Rooms/IGRMeasurerBarScript.yarn"));
            Dialogues.AddCustom_DialogueProgram(text2, yarnProgram2);
            Dialogues.CreateAndAddCustom_DialogueSO(text2, yarnProgram2, text2, "AApocrypha.Institute.Measurer.BarStart");

            /*CharacterInPartyConditionSO whitlockHere = ScriptableObject.CreateInstance<CharacterInPartyConditionSO>();
            whitlockHere._characterID = "Whitlock_CH";
            whitlockHere._passIfFalse = true;
            whitlockHere.dialogData = oldPlayerData;*/

            BarSeatData whitlockSeatData = new BarSeatData();
            whitlockSeatData.m_Sprite = ResourceLoader.LoadSprite("WhitlockBar", new Vector2(0.5f, 0f), 32);
            whitlockSeatData.m_EntityID = "Whitlock_CH";
            whitlockSeatData.m_Dialogue = text;
            whitlockSeatData.m_Conditions =
            [
                GameInt_GenericCondition.GenerateIntCondition("AA_BarSeatShuffler1", true, 0, 0),
            ];

            BarSeatData measurerSeatData = new BarSeatData();
            measurerSeatData.m_Sprite = ResourceLoader.LoadSprite("InstituteMeasurerBar", new Vector2(0.5f, 0f), 32);
            measurerSeatData.m_EntityID = "MeasurerBar";
            measurerSeatData.m_Dialogue = text2;
            measurerSeatData.m_Conditions =
            [
                GameInt_GenericCondition.GenerateIntCondition("AA_BarSeatShuffler1", true, 0, 1),
            ];

            _seats = [whitlockSeatData, measurerSeatData];
            //int index = UnityEngine.Random.Range(0, _seats.Length);
            foreach (BarSeatData seat in _seats) { OverworldRooms.Add_Bar_SeatOption(shorehard._barRoom.ToString(), seat, 1); }
            //Debug.Log("Bar Handler | loaded " + _seats[index].m_EntityID);
        }
    }
}

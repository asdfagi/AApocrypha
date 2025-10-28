using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CoruscatingJumbleGutsEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("RainbowGuts_Sign", ResourceLoader.LoadSprite("RainbowGutsTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API rainbowGutsMedium = new EnemyEncounter_API(0, "H_Zone02_CoruscatingJumbleGuts_Medium_EnemyBundle", "RainbowGuts_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp-SecondaryColors",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone02_JumbleGuts_Flummoxing_Medium_EnemyBundle")._roarReference.roarEvent,
            };
            rainbowGutsMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
                "JumbleGuts_Hollowing_EN",
                "MusicMan_EN",
            ], null);
            rainbowGutsMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_CoruscatingJumbleGuts_Medium_EnemyBundle", 10, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TestEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Test_Sign", ResourceLoader.LoadSprite("IconCometGaze", new Vector2(0.5f, 0f), 32), Portals.BossIDColor);
            EnemyEncounter_API testMedium = new EnemyEncounter_API((EncounterType)1, "H_Zone01_Test_Medium_EnemyBundle", "Test_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp-SecondaryColors",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone01_JumbleGuts_Clotted_Medium_EnemyBundle")._roarReference.roarEvent,
            };
            testMedium.CreateNewEnemyEncounterData(
            [
                "CoruscatingJumbleGuts_EN",
            ], [2]);
            testMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Test_Medium_EnemyBundle", 9999, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}

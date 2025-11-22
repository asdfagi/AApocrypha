using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ColophonDualisticEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("ColophonDualistic_Sign", ResourceLoader.LoadSprite("RadtickTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API colophonDualisticEasy = new EnemyEncounter_API(0, "H_Zone01_ColophonDualistic_Easy_EnemyBundle", "ColophonDualistic_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor-HurtPeopleFullCircle",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("ComposedEasy")._roarReference.roarEvent,
            };
            colophonDualisticEasy.CreateNewEnemyEncounterData(
            [
                "ColophonDualistic_EN",
                "MudLung_EN",
                "Mung_EN",
            ], null);
            colophonDualisticEasy.CreateNewEnemyEncounterData(
            [
                "ColophonDualistic_EN",
                "MunglingMudLung_EN",
            ], null);
            colophonDualisticEasy.CreateNewEnemyEncounterData(
            [
                "ColophonDualistic_EN",
                "SandSifter_EN",
                "Mung_EN",
            ], null);
            colophonDualisticEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_ColophonDualistic_Easy_EnemyBundle", 3, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy); //default: 3

            EnemyEncounter_API colophonDualisticMedium = new EnemyEncounter_API(0, "H_Zone01_ColophonDualistic_Medium_EnemyBundle", "ColophonDualistic_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor-HurtPeopleFullCircle",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("ComposedEasy")._roarReference.roarEvent,
            };
            colophonDualisticMedium.CreateNewEnemyEncounterData(
            [
                "ColophonDualistic_EN",
                "MudLung_EN",
                "ColophonDefeated_EN",
            ], null);
            colophonDualisticMedium.CreateNewEnemyEncounterData(
            [
                "ColophonDualistic_EN",
                "MunglingMudLung_EN",
                "TearDrinker_EN",
            ], null);
            colophonDualisticMedium.CreateNewEnemyEncounterData(
            [
                "ColophonDualistic_EN",
                "SandSifter_EN",
                "MudLung_EN",
                "ColophonComposed_EN",
            ], null);
            colophonDualisticMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_ColophonDualistic_Medium_EnemyBundle", 5, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium); //default: 5
        }
    }
}

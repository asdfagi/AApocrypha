using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class MaceratorEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Macerator_Sign", ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API maceratorEasy = new EnemyEncounter_API(0, "H_Zone01_Macerator_Easy_EnemyBundle", "Macerator_Sign")
            {
                MusicEvent = "event:/Music/Mx_Mung",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Keko/CHR_ENM_Keko_Roar",
            };
            maceratorEasy.CreateNewEnemyEncounterData(
                [
                    "Macerator_EN",
                ], null);
            maceratorEasy.CreateNewEnemyEncounterData(
                [
                    "Macerator_EN",
                    "Mung_EN",
                ], null);
            maceratorEasy.CreateNewEnemyEncounterData(
                [
                    "Macerator_EN",
                    "Mudlung_EN",
                    "Mung_EN",
                ], null);
            maceratorEasy.CreateNewEnemyEncounterData(
                [
                    "Macerator_EN",
                    "Keko_EN",
                ], null);
            maceratorEasy.CreateNewEnemyEncounterData(
                [
                    "Macerator_EN",
                    "Macerator_EN",
                    "Mung_EN",
                ], null);
            maceratorEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Macerator_Easy_EnemyBundle", 1, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);
        }
    }
}

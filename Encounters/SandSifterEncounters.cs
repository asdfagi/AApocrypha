using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SandSifterEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("SandSifter_Sign", ResourceLoader.LoadSprite("SandSifterTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sandSifterEasy = new EnemyEncounter_API(0, "H_Zone01_SandSifter_Easy_EnemyBundle", "SandSifter_Sign")
            {
                MusicEvent = "event:/AAMusic/SieveOurSouls",
                RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
            sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                ], null);
            sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Mung_EN",
                ], null);
            sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Mudlung_EN",
                    "Mung_EN",
                ], null);
            sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Keko_EN",
                ], null);
            sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Keko_EN",
                    "Keko_EN",
                ], null);
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Draugr_EN",
                ], null);
                sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Mung_EN",
                    "Keklung_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Minana_EN",
                    "Mung_EN",
                ], null);
                sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Mung_EN",
                    "LittleBeak_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                sandSifterEasy.CreateNewEnemyEncounterData(
                [
                    "SandSifter_EN",
                    "Scylla_EN",
                    "Mung_EN",
                ], null);
            }
            sandSifterEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_SandSifter_Easy_EnemyBundle", 8, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);
        }
    }
}

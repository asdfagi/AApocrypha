using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class DuneThresherEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("DuneThresher_Sign", ResourceLoader.LoadSprite("DuneThresherTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API duneThresherHard = new EnemyEncounter_API(0, "H_Zone01_DuneThresher_Hard_EnemyBundle", "DuneThresher_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/duneboi",
                RoarEvent = "event:/AAEnemy/EmplacementRoar",
            };
            duneThresherHard.CreateNewEnemyEncounterData(
                [
                    "DuneThresher_EN",
                    "SandSifter_EN",
                ], null);
            duneThresherHard.CreateNewEnemyEncounterData(
                [
                    "DuneThresher_EN",
                    "SandSifter_EN",
                    "SandSifter_EN",
                ], null);
            duneThresherHard.CreateNewEnemyEncounterData(
                [
                    "DuneThresher_EN",
                    "SandSifter_EN",
                    "Mung_EN",
                ], null);
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                duneThresherHard.CreateNewEnemyEncounterData(
                [
                    "DuneThresher_EN",
                    "SandSifter_EN",
                    "Scylla_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                duneThresherHard.CreateNewEnemyEncounterData(
                [
                    "DuneThresher_EN",
                    "SandSifter_EN",
                    "MycotoxicSpoggle_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                duneThresherHard.CreateNewEnemyEncounterData(
                [
                    "DuneThresher_EN",
                    "SandSifter_EN",
                    "Madman_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                duneThresherHard.CreateNewEnemyEncounterData(
                [
                    "DuneThresher_EN",
                    "SandSifter_EN",
                    "Enno_EN",
                ], null);
            }
            duneThresherHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_DuneThresher_Hard_EnemyBundle", 8, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard); //8
        }
    }
}

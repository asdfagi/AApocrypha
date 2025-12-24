using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class GammamiteEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Gammamite_Sign", ResourceLoader.LoadSprite("RadtickTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API gammamiteHard = new EnemyEncounter_API(0, "H_Zone01_Gammamite_Hard_EnemyBundle", "Gammamite_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/Raytrot",
                RoarEvent = "event:/AAEnemy/RadtickRoar",
            };
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MudLung_EN",
                    "Mung_EN",
                ], null);
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MunglingMudLung_EN",
                ], null);
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "JumbleGuts_Waning_EN",
                ], null);
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "JumbleGuts_Waning_EN",
                    "MudLung_EN",
                ], null);
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "Keko_EN",
                    "Keko_EN",
                ], null);
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "Spoggle_Ruminating_EN",
                ], null);
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "JumbleGuts_Waning_EN",
                    "TearDrinker_EN",
                ], null);
            gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MudLung_EN",
                    "FlaMinGoa_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "ColophonComposed_EN",
                    "TearDrinker_EN",
                ], null);
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "ColophonDualistic_EN",
                    "Spoggle_Spitfire_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MycotoxicSpoggle_EN",
                    "SandSifter_EN",
                ], null);
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "Follower_EN",
                    "JumbleGuts_Clotted_EN",
                ], null);
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "Goomba_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MudLung_EN",
                    "DryBait_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "Draugr_EN",
                ], null);
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MudLung_EN",
                    "Keklung_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies && AApocrypha.CrossMod.GlitchsFreaks)
            {
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "NobodyGrave_EN",
                    "DryBait_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MudLung_EN",
                    "Wall_EN",
                ], null);
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "Waltz_EN",
                    "Waltz_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "MudLung_EN",
                    "Madman_EN",
                ], null);
                gammamiteHard.CreateNewEnemyEncounterData(
                [
                    "Gammamite_EN",
                    "TearDrinker_EN",
                    "Mung_EN",
                    "Madman_EN",
                ], null);
            }
            gammamiteHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Gammamite_Hard_EnemyBundle", 10, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard); //default: 10
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class FungusColumnEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("FungusColumn_Sign", ResourceLoader.LoadSprite("FungusColumnTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API fungusMedium = new EnemyEncounter_API(0, "H_Zone01_FungusColumn_Medium_EnemyBundle", "FungusColumn_Sign")
            {
                MusicEvent = "event:/AAMusic/FallenLondon/WhyWeWearFaces",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "MudLung_EN",
                ], null);
            fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "JumbleGuts_Waning_EN",
                ], null);
            fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "JumbleGuts_Waning_EN",
                    "MudLung_EN",
                ], null);
            fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "Keko_EN",
                    "Keko_EN",
                ], null);
            fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "Spoggle_Ruminating_EN",
                ], null);
            if (AApocrypha.CrossMod.Colophons)
            {
                fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "ColophonDefeated_EN",
                ], null);
                fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "ColophonComposed_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "MycotoxicSpoggle_EN",
                ], null);
                fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "Follower_EN",
                    "MudLung_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "MudLung_EN",
                    "NotAn_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                fungusMedium.CreateNewEnemyEncounterData(
                [
                    "FungusColumn_EN",
                    "MudLung_EN",
                    "RatThing_EN",
                ], null);
            }
            fungusMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_FungusColumn_Medium_EnemyBundle", 16, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium); //default: 16
        }
    }
}

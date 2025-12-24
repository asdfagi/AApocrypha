using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ColophonSaccharineEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("ColophonSaccharine_Sign", ResourceLoader.LoadSprite("ColophonPeppermintTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API colophonSaccharineMedium = new EnemyEncounter_API(0, "H_Zone03_ColophonSaccharine_Medium_EnemyBundle", "ColophonSaccharine_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor/HurtPeopleFullCircle",
                RoarEvent = "event:/AAEnemy/ColophonSaccharineRoar",
            };
            colophonSaccharineMedium.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "InHisImage_EN",
                    "InHerImage_EN",
                    "NextOfKin_EN",
                ], null);
            colophonSaccharineMedium.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "InHisImage_EN",
                    "NextOfKin_EN",
                ], null);
            colophonSaccharineMedium.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "ShiveringHomunculus_EN",
                    "ShiveringHomunculus_EN",
                ], null);
            colophonSaccharineMedium.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "GigglingMinister_EN",
                ], null);
            colophonSaccharineMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_ColophonSaccharine_Medium_EnemyBundle", 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyEncounter_API colophonSaccharineHard = new EnemyEncounter_API(0, "H_Zone02_ColophonSaccharine_Hard_EnemyBundle", "ColophonSaccharine_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor/HurtPeopleFullCircle",
                RoarEvent = "event:/AAEnemy/ColophonSaccharineRoar",
            };
            colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "ColophonComposed_EN",
                ], null);
            colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "ColophonDefeated_EN",
                    "ColophonComposed_EN",
                ], null);
            colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "ColophonDefeated_EN",
                ], null);
            colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "Spoggle_Spitfire_EN",
                    "MusicMan_EN",
                ], null);
            colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "MusicMan_EN",
                    "Scrungie_EN",
                ], null);
            colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "SingingStone_EN",
                    "Scrungie_EN",
                    "Scrungie_EN",
                ], null);
            if (AApocrypha.CrossMod.Mythos)
            {
                colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "ColophonComposed_EN",
                    "Lloigor_EN",
                ], null);
                colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "ColophonDefeated_EN",
                    "StarVampire_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "ColophonComposed_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                ], null);
                colophonSaccharineHard.CreateNewEnemyEncounterData(
                [
                    "ColophonSaccharine_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                ], null);
            }
            colophonSaccharineHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_ColophonSaccharine_Hard_EnemyBundle", 4, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Hard);
        }
    }
}

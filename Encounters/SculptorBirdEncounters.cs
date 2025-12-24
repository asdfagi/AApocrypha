using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SculptorBirdEncounters
    {   
        public static void Add()
        {
            Portals.AddPortalSign("SculptorBird_Sign", ResourceLoader.LoadSprite("SculptorBirdTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sculptorBirdMedium = new EnemyEncounter_API(0, "H_Zone02_SculptorBird_Medium_EnemyBundle", "SculptorBird_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/DepressionShop",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Scrungie/CHR_ENM_Scrungie_Roar",
            };
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "MusicMan_EN",
                ], null);
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "BloatfingerHiddenOrpheum_EN",
                    "MusicMan_EN",
                    "MusicMan_EN",
                ], null);
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "Spoggle_Ruminating_EN",
                    "Jumbleguts_Clotted_EN",
                ], null);
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "DevotedSpoggle_EN",
                    "Jumbleguts_Waning_EN",
                ], null);
            sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "Blemmigan_EN",
                    "Blemmigan_EN",
                ], null);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                    [
                        "SculptorBird_EN",
                        "SculptorBirdSculpture_EN",
                        "Frostbite_EN",
                        "Frostbite_EN",
                    ], null);
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                    [
                        "SculptorBird_EN",
                        "SculptorBirdSculpture_EN",
                        "Frostbite_EN",
                        "Frostbite_Bipedal_EN",
                    ], null);
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                    [
                        "SculptorBird_EN",
                        "BloatfingerHiddenOrpheum_EN",
                        "Frostbite_EN",
                        "Frostbite_EN",
                    ], null);
            }
            ;
            if (AApocrypha.CrossMod.Colophons)
            {
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "ColophonDelighted_EN",
                ], null);
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "ColophonMaladjusted_EN",
                ], null);
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "ColophonDelighted_EN",
                    "Scrungie_EN",
                ], null);
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "ColophonMaladjusted_EN",
                    "Spoggle_Resonant_EN",
                ], null);
                if (AApocrypha.CrossMod.pigmentPeppermint)
                {
                    sculptorBirdMedium.CreateNewEnemyEncounterData(
                    [
                        "SculptorBird_EN",
                        "SculptorBirdSculpture_EN",
                        "ColophonSaccharine_EN",
                        "Scrungie_EN",
                    ], null);
                    sculptorBirdMedium.CreateNewEnemyEncounterData(
                    [
                        "SculptorBird_EN",
                        "SculptorBirdSculpture_EN",
                        "ColophonSaccharine_EN",
                        "Spoggle_Spitfire_EN",
                    ], null);
                }
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    sculptorBirdMedium.CreateNewEnemyEncounterData(
                    [
                        "SculptorBird_EN",
                        "SculptorBirdSculpture_EN",
                        "ColophonDisaffected_EN",
                        "Jumbleguts_Flummoxing_EN",
                    ], null);
                    sculptorBirdMedium.CreateNewEnemyEncounterData(
                    [
                        "SculptorBird_EN",
                        "BloatfingerHiddenOrpheum_EN",
                        "ColophonDisaffected_EN",
                    ], null);
                }
            };
            if (AApocrypha.CrossMod.Mythos)
            {
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "Lloigor_EN",
                ], null);
                sculptorBirdMedium.CreateNewEnemyEncounterData(
                [
                    "SculptorBird_EN",
                    "SculptorBirdSculpture_EN",
                    "StarVampire_EN",
                    "MusicMan_EN",
                ], null);
            }
            sculptorBirdMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_SculptorBird_Medium_EnemyBundle", 18, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

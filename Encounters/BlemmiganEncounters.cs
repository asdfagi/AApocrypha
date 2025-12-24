using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BlemmiganEncounters
    {
        public static void Add()
        {
            // do not combine silver suckles and blemmigans - they are mortal enemies
            Portals.AddPortalSign("Uttershroom_Sign", ResourceLoader.LoadSprite("UttershroomPortal", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API uttershroomMedium = new EnemyEncounter_API(0, "H_Zone02_Uttershroom_Medium_EnemyBundle", "Uttershroom_Sign")
            {
                MusicEvent = "event:/AAMusic/FallenLondon/KhansHeart",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("H_Zone03_Sepulchre_Hard_EnemyBundle")._roarReference.roarEvent,
            };
            uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Blemmigan_EN",
                    "SingingStone_EN",
                ], null);
            uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "MusicMan_EN",
                ], null);
            uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Scrungie_EN",
                ], null);
            uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Scrungie_EN",
                    "SculptorBirdSculpture_EN",
                ], null);
            uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "MusicMan_EN",
                    "SculptorBirdSculpture_EN",
                ], null);
            uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Scrungie_EN",
                    "BloatfingerHiddenOrpheum_EN",
                ], null);
            uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "MusicMan_EN",
                    "BloatfingerHiddenOrpheum_EN",
                ], null);
            if (AApocrypha.CrossMod.EnemyPack)
            {
                uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Neoplasm_EN",
                ], null);
                uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Gizo_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Blemmigan_EN",
                    "Gungrot_EN",
                ], null);
                uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "JumbleGuts_Digital_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Frostbite_EN",
                    "Frostbite_EN",
                ]);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "RedBot_EN",
                    "Blemmigan_EN",
                ]);
                uttershroomMedium.CreateNewEnemyEncounterData(
                [
                    "UttershroomSpore_EN",
                    "Blemmigan_EN",
                    "Something_EN",
                ]);
            }
            uttershroomMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_Uttershroom_Medium_EnemyBundle", 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium); //12
        }
    }
}

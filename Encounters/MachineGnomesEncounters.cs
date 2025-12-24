using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class MachineGnomesEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("MachineGnomes_Sign", ResourceLoader.LoadSprite("GnomesTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API gnomesMedium = new EnemyEncounter_API(0, "H_Zone03_MachineGnomes_Medium_EnemyBundle", "MachineGnomes_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/DoYouHearGnomes",
                RoarEvent = "event:/AAEnemy/GnomesRoar",
            };
            gnomesMedium.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "NextOfKin_EN",
                "NextOfKin_EN",
            ], null);
            gnomesMedium.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "ShiveringHomunculus_EN",
                "InHisImage_EN",
            ], null);
            gnomesMedium.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "InHisImage_EN",
                "InHisImage_EN",
            ], null);
            gnomesMedium.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "InHerImage_EN",
                "InHerImage_EN",
                "NextOfKin_EN",
            ], null);
            gnomesMedium.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "MachineGnomes_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                gnomesMedium.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "Streetlight_EN",
                ], null);
                gnomesMedium.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "ShiveringHomunculus_EN",
                    "YieldSign_EN",
                ], null);
                gnomesMedium.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "InHisImage_EN",
                    "ParkingSign_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                gnomesMedium.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "TortureMeNot_EN",
                    "TortureMeNot_EN",
                ], null);
            }
            gnomesMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_MachineGnomes_Medium_EnemyBundle", 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);

            EnemyEncounter_API gnomesHard = new EnemyEncounter_API(0, "H_Zone03_MachineGnomes_Hard_EnemyBundle", "MachineGnomes_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/DoYouHearGnomes",
                RoarEvent = "event:/AAEnemy/GnomesRoar",
            };
            gnomesHard.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            gnomesHard.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "InHisImage_EN",
                "InHerImage_EN",
                "InHerImage_EN",
            ], null);
            gnomesHard.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "InHisImage_EN",
                "InHisImage_EN",
                "InHerImage_EN",
            ], null);
            gnomesHard.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "GigglingMinister_EN",
                "MachineGnomes_EN",
            ], null);
            gnomesHard.CreateNewEnemyEncounterData(
            [
                "MachineGnomes_EN",
                "SomeoneSister_EN",
                "MachineGnomes_EN",
            ], null);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                gnomesHard.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "Vagabond_EN",
                    "NextOfKin_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                gnomesHard.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "MonumentOfEnmity_EN",
                ], null);
                gnomesHard.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "MachineGnomes_EN",
                    "AloofEnvoy_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                gnomesHard.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "Fleet_EN",
                ], null);
                gnomesHard.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "Streetlight_EN",
                    "Streetlight_EN",
                ], null);
                gnomesHard.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "MachineGnomes_EN",
                    "Eater_Invis_EN",
                ], null);
                gnomesHard.CreateNewEnemyEncounterData(
                [
                    "MachineGnomes_EN",
                    "MachineGnomes_EN",
                    "Monad_EN",
                ], null);
            }
            gnomesHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_MachineGnomes_Hard_EnemyBundle", 4, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

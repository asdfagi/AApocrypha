using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SistersEncounters
    {
        public static void Add()
        {
            bool bright = false;
            double moonVisibility = AApocrypha.MoonData.Visibility;
            if (moonVisibility > 50) { bright = true; }
            string primarySister = bright ? "SomeoneSister_EN" : "NooneSister_EN";
            string secondarySister = bright ? "NooneSister_EN" : "SomeoneSister_EN";
            Portals.AddPortalSign("Sisters_Sign", ResourceLoader.LoadSprite((bright ? "SomeoneSisterOverworld" : "NooneSisterOverworld"), new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sistersMedium = new EnemyEncounter_API(0, "H_Zone03_OnesSisters_Medium_EnemyBundle", "Sisters_Sign")
            {
                MusicEvent = "event:/AAMusic/BelowZion",
                RoarEvent = "event:/AAEnemy/SistersRoar",
            };
            sistersMedium.CreateNewEnemyEncounterData(
            [
                primarySister,
                "NextOfKin_EN",
                "NextOfKin_EN",
            ], null);
            sistersMedium.CreateNewEnemyEncounterData(
            [
                primarySister,
                "InHisImage_EN",
                "NextOfKin_EN",
            ], null);
            sistersMedium.CreateNewEnemyEncounterData(
            [
                primarySister,
                "InHerImage_EN",
                "NextOfKin_EN",
            ], null);
            sistersMedium.CreateNewEnemyEncounterData(
            [
                primarySister,
                primarySister,
                "MachineGnomes_EN",
                "MachineGnomes_EN",
            ], null);
            sistersMedium.CreateNewEnemyEncounterData(
            [
                primarySister,
                secondarySister,
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                sistersMedium.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    secondarySister,
                    "Monad_EN",
                ], null);
                sistersMedium.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    primarySister,
                    "NextOfKin_EN",
                    "HospitalSign_EN",
                ], null);
                sistersMedium.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    primarySister,
                    "NextOfKin_EN",
                    "ParkingSign_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sistersMedium.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    secondarySister,
                    "Damocles_EN",
                ], null);
                sistersMedium.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    primarySister,
                    "BlueFlower_EN",
                    "PawnA_EN",
                ], null);
            }
            sistersMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_OnesSisters_Medium_EnemyBundle", 11, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);

            EnemyEncounter_API sistersHard = new EnemyEncounter_API(0, "H_Zone03_OnesSisters_Hard_EnemyBundle", "Sisters_Sign")
            {
                MusicEvent = "event:/AAMusic/BelowZion",
                RoarEvent = "event:/AAEnemy/SistersRoar",
            };
            sistersHard.CreateNewEnemyEncounterData(
            [
                primarySister,
                "GigglingMinister_EN",
                "GigglingMinister_EN",
            ], null);
            sistersHard.CreateNewEnemyEncounterData(
            [
                primarySister,
                "InHisImage_EN",
                "InHisImage_EN",
                "NextOfKin_EN",
            ], null);
            sistersHard.CreateNewEnemyEncounterData(
            [
                primarySister,
                "InHerImage_EN",
                "InHerImage_EN",
                "NextOfKin_EN",
            ], null);
            sistersHard.CreateNewEnemyEncounterData(
            [
                primarySister,
                primarySister,
                "MachineGnomes_EN",
                "MachineGnomes_EN",
            ], null);
            sistersHard.CreateNewEnemyEncounterData(
            [
                primarySister,
                secondarySister,
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                sistersHard.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    secondarySister,
                    "Monad_EN",
                ], null);
                sistersHard.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    primarySister,
                    "SkinningHomunculus_EN",
                    "HospitalSign_EN",
                ], null);
                sistersHard.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    primarySister,
                    "SkinningHomunculus_EN",
                    "ParkingSign_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sistersHard.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    secondarySister,
                    "MiniReaper_EN",
                ], null);
                sistersHard.CreateNewEnemyEncounterData(
                [
                    primarySister,
                    primarySister,
                    "BlueFlower_EN",
                    "RedFlower_EN",
                ], null);
            }
            sistersHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_OnesSisters_Hard_EnemyBundle", 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

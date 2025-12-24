using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class RedLogosEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("RedLogos_Sign", ResourceLoader.LoadSprite("LogosTimelineRed", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API redLogosMedium = new EnemyEncounter_API(0, "H_Zone03_CrimsonLogos_Medium_EnemyBundle", "RedLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            redLogosMedium.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
            ], null);
            redLogosMedium.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
                "NextOfKin_EN",
            ], null);
            redLogosMedium.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "MachineGnomes_EN",
                "NextOfKin_EN",
                "NextOfKin_EN",
            ], null);
            redLogosMedium.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            redLogosMedium.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "GigglingMinister_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                redLogosMedium.CreateNewEnemyEncounterData(
                [
                    "CrimsonLogos_EN",
                    "GigglingMinister_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                redLogosMedium.CreateNewEnemyEncounterData(
                [
                    "CrimsonLogos_EN",
                    "AloofEnvoy_EN",
                ], null);
            }
            redLogosMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_CrimsonLogos_Medium_EnemyBundle", 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyEncounter_API redLogosHard = new EnemyEncounter_API(0, "H_Zone03_CrimsonLogos_Hard_EnemyBundle", "RedLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            redLogosHard.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "GigglingMinister_EN",
                "GigglingMinister_EN",
            ], null);
            redLogosHard.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "GigglingMinister_EN",
                "SomeoneSister_EN",
            ], null);
            redLogosHard.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            redLogosHard.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            redLogosHard.CreateNewEnemyEncounterData(
            [
                "CrimsonLogos_EN",
                "GigglingMinister_EN",
                "MachineGnomes_EN",
                "MachineGnomes_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                redLogosHard.CreateNewEnemyEncounterData(
                [
                    "CrimsonLogos_EN",
                    "MachineGnomes_EN",
                    "Monad_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                redLogosHard.CreateNewEnemyEncounterData(
                [
                    "CrimsonLogos_EN",
                    "FrowningChancellor_EN",
                    "GigglingMinister_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                redLogosHard.CreateNewEnemyEncounterData(
                [
                    "CrimsonLogos_EN",
                    "Key_EN",
                    "AloofEnvoy_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                redLogosHard.CreateNewEnemyEncounterData(
                [
                    "CrimsonLogos_EN",
                    "GigglingMinister_EN",
                    "SweatingNosestone_EN",
                ], null);
            }
            redLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_CrimsonLogos_Hard_EnemyBundle", 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

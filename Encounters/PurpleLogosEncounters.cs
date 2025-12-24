using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class PurpleLogosEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("PurpleLogos_Sign", ResourceLoader.LoadSprite("LogosTimelinePurple", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            /*EnemyEncounter_API purpleLogosMedium = new EnemyEncounter_API(0, "H_Zone03_RegentLogos_Medium_EnemyBundle", "PurpleLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            purpleLogosMedium.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
            ], null);
            purpleLogosMedium.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
                "NextOfKin_EN",
            ], null);
            purpleLogosMedium.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "MachineGnomes_EN",
                "NextOfKin_EN",
                "NextOfKin_EN",
            ], null);
            purpleLogosMedium.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            purpleLogosMedium.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "GigglingMinister_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                purpleLogosMedium.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "GigglingMinister_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                purpleLogosMedium.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "AloofEnvoy_EN",
                ], null);
            }
            purpleLogosMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_CrimsonLogos_Medium_EnemyBundle", 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);*/
            EnemyEncounter_API purpleLogosHard = new EnemyEncounter_API(0, "H_Zone03_RegentLogos_Hard_EnemyBundle", "PurpleLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "GigglingMinister_EN",
                "GigglingMinister_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "GigglingMinister_EN",
                "SomeoneSister_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "GigglingMinister_EN",
                "ChoirBoy_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "GigglingMinister_EN",
                "MachineGnomes_EN",
                "MachineGnomes_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "CrimsonLogos_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "CeruleanLogos_EN",
            ], null);
            purpleLogosHard.CreateNewEnemyEncounterData(
            [
                "RegentLogos_EN",
                "AureateLogos_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                purpleLogosHard.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "MachineGnomes_EN",
                    "MachineGnomes_EN",
                    "Eater_Invis_EN",
                ], null);
                purpleLogosHard.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "WRK_EN",
                    "SomeoneSister_EN",
                    "StopSign_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                purpleLogosHard.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "GodsChalice_EN",
                    "Vagabond_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                purpleLogosHard.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "GigglingMinister_EN",
                    "SacredScraps_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                purpleLogosHard.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "Firebird_EN",
                    "Damocles_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks && AApocrypha.CrossMod.HellIslandFell)
            {
                purpleLogosHard.CreateNewEnemyEncounterData(
                [
                    "RegentLogos_EN",
                    "FrowningChancellor_EN",
                    "SweatingNosestone_EN",
                ], null);
            }
            purpleLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_RegentLogos_Hard_EnemyBundle", 3, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

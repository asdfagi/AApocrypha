using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class YellowLogosEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("YellowLogos_Sign", ResourceLoader.LoadSprite("LogosTimelineYellow", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            /*EnemyEncounter_API yellowLogosMedium = new EnemyEncounter_API(0, "H_Zone03_AureateLogos_Medium_EnemyBundle", "YellowLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            yellowLogosMedium.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
            ], null);
            yellowLogosMedium.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
                "NextOfKin_EN",
            ], null);
            yellowLogosMedium.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "MachineGnomes_EN",
                "NextOfKin_EN",
                "NextOfKin_EN",
            ], null);
            yellowLogosMedium.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            yellowLogosMedium.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "GigglingMinister_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                yellowLogosMedium.CreateNewEnemyEncounterData(
                [
                    "AureateLogos_EN",
                    "GigglingMinister_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                yellowLogosMedium.CreateNewEnemyEncounterData(
                [
                    "AureateLogos_EN",
                    "AloofEnvoy_EN",
                ], null);
            }
            yellowLogosMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_CrimsonLogos_Medium_EnemyBundle", 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);*/
            EnemyEncounter_API yellowLogosHard = new EnemyEncounter_API(0, "H_Zone03_AureateLogos_Hard_EnemyBundle", "YellowLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            yellowLogosHard.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "GigglingMinister_EN",
                "GigglingMinister_EN",
            ], null);
            yellowLogosHard.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "GigglingMinister_EN",
                "SomeoneSister_EN",
            ], null);
            yellowLogosHard.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            yellowLogosHard.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            yellowLogosHard.CreateNewEnemyEncounterData(
            [
                "AureateLogos_EN",
                "GigglingMinister_EN",
                "MachineGnomes_EN",
                "MachineGnomes_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                yellowLogosHard.CreateNewEnemyEncounterData(
                [
                    "AureateLogos_EN",
                    "MachineGnomes_EN",
                    "SullenPrioress_EN"
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                yellowLogosHard.CreateNewEnemyEncounterData(
                [
                    "AureateLogos_EN",
                    "FrowningChancellor_EN",
                    "GigglingMinister_EN",
                ], null);
                yellowLogosHard.CreateNewEnemyEncounterData(
                [
                    "AureateLogos_EN",
                    "GodsChalice_EN",
                    "Vagabond_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                yellowLogosHard.CreateNewEnemyEncounterData(
                [
                    "AureateLogos_EN",
                    "Key_EN",
                    "AloofEnvoy_EN",
                ], null);
                yellowLogosHard.CreateNewEnemyEncounterData(
                [
                    "AureateLogos_EN",
                    "GigglingMinister_EN",
                    "Euryale_EN",
                ], null);
            }
            yellowLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_AureateLogos_Hard_EnemyBundle", 5, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

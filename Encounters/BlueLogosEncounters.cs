using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BlueLogosEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("BlueLogos_Sign", ResourceLoader.LoadSprite("LogosTimelineBlue", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            /*EnemyEncounter_API blueLogosMedium = new EnemyEncounter_API(0, "H_Zone03_CeruleanLogos_Medium_EnemyBundle", "BlueLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            blueLogosMedium.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
            ], null);
            blueLogosMedium.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "InHisImage_EN",
                "InHerImage_EN",
                "NextOfKin_EN",
            ], null);
            blueLogosMedium.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "MachineGnomes_EN",
                "NextOfKin_EN",
                "NextOfKin_EN",
            ], null);
            blueLogosMedium.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            blueLogosMedium.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "GigglingMinister_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                blueLogosMedium.CreateNewEnemyEncounterData(
                [
                    "CeruleanLogos_EN",
                    "GigglingMinister_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                blueLogosMedium.CreateNewEnemyEncounterData(
                [
                    "CeruleanLogos_EN",
                    "AloofEnvoy_EN",
                ], null);
            }
            blueLogosMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_CrimsonLogos_Medium_EnemyBundle", 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);*/
            EnemyEncounter_API blueLogosHard = new EnemyEncounter_API(0, "H_Zone03_CeruleanLogos_Hard_EnemyBundle", "BlueLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = "event:/Characters/Enemies/DLC_01/ChoirBoy/CHR_ENM_ChoirBoy_Roar",
            };
            blueLogosHard.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "GigglingMinister_EN",
                "GigglingMinister_EN",
            ], null);
            blueLogosHard.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "GigglingMinister_EN",
                "SomeoneSister_EN",
            ], null);
            blueLogosHard.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            blueLogosHard.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "SkinningHomunculus_EN",
                "ShiveringHomunculus_EN",
                "ShiveringHomunculus_EN",
            ], null);
            blueLogosHard.CreateNewEnemyEncounterData(
            [
                "CeruleanLogos_EN",
                "GigglingMinister_EN",
                "MachineGnomes_EN",
                "MachineGnomes_EN",
            ], null);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                blueLogosHard.CreateNewEnemyEncounterData(
                [
                    "CeruleanLogos_EN",
                    "MachineGnomes_EN",
                    "SullenPrioress_EN"
                ], null);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                blueLogosHard.CreateNewEnemyEncounterData(
                [
                    "CeruleanLogos_EN",
                    "FrowningChancellor_EN",
                    "GigglingMinister_EN",
                ], null);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                blueLogosHard.CreateNewEnemyEncounterData(
                [
                    "CeruleanLogos_EN",
                    "Key_EN",
                    "AloofEnvoy_EN",
                ], null);
                blueLogosHard.CreateNewEnemyEncounterData(
                [
                    "CeruleanLogos_EN",
                    "GigglingMinister_EN",
                    "Euryale_EN",
                ], null);
            }
            blueLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone03_CeruleanLogos_Hard_EnemyBundle", 5, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

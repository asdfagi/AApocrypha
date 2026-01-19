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
            EnemyEncounter_API yellowLogosHard = new EnemyEncounter_API(0, Garden.H.Logos.Yellow.Hard, "YellowLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 2, Enemies.Minister);
            yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, Enemies.Minister, 1, "SomeoneSister_EN");
            yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, Enemies.Skinning, 1, Enemies.Shivering);
            yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, Enemies.Skinning, 2, Enemies.Shivering);
            yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, Enemies.Minister, 2, "MachineGnomes_EN");
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, "MachineGnomes_EN", 1, "SullenPrioress_EN");
                yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, "MachineGnomes_EN", 1, Signs.Purple);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, "GigglingMinister_EN", 1, "FrowningChancellor_EN");
                yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, "GodsChalice_EN", 1, "Vagabond_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, "AloofEnvoy_EN", 1, "Key_EN");
                yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, Enemies.Minister, 1, "Euryale_EN");
            }
            yellowLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Yellow.Hard, 5, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

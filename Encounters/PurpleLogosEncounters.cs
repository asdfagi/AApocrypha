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
            EnemyEncounter_API purpleLogosHard = new EnemyEncounter_API(0, Garden.H.Logos.Purple.Hard, "PurpleLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Enemies.Minister);
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Enemies.Minister, 1, "SomeoneSister_EN");
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Enemies.Minister, 1, "ChoirBoy_EN");
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Enemies.Skinning, 1, Enemies.Shivering);
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Enemies.Skinning, 2, Enemies.Shivering);
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Enemies.Minister, 2, "MachineGnomes_EN");
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Logos.Red);
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Logos.Blue);
            purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, Logos.Yellow);
            if (AApocrypha.CrossMod.UndivineComedy)
            {
                purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, "BellRinger_EN", 1, Enemies.Minister);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 2, "MachineGnomes_EN", 1, "Eater_Invis_EN");
                purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, "WRK_EN", 1, "SomeoneSister_EN", 1, Signs.Red);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, "GodsChalice_EN", 1, "Vagabond_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, "SacredScraps_EN", 1, Enemies.Minister);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, "Firebird_EN", 1, "Damocles_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks && AApocrypha.CrossMod.HellIslandFell)
            {
                purpleLogosHard.SimpleAddEncounter(1, Logos.Purple, 1, "FrowningChancellor_EN", 1, Noses.Yellow);
            }
            purpleLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Purple.Hard, 7, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

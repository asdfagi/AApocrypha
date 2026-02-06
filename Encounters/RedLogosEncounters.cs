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
            EnemyEncounter_API redLogosMedium = new EnemyEncounter_API(0, Garden.H.Logos.Red.Med, "RedLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            redLogosMedium.SimpleAddEncounter(1, Logos.Red, 1, "InHisImage_EN", 1, "InHisImage_EN");
            redLogosMedium.SimpleAddEncounter(1, Logos.Red, 1, "InHisImage_EN", 1, "InHisImage_EN", 1, "NextOfKin_EN");
            redLogosMedium.SimpleAddEncounter(1, Logos.Red, 1, "MachineGnomes_EN", 2, "NextOfKin_EN");
            redLogosMedium.SimpleAddEncounter(1, Logos.Red, 2, Enemies.Shivering);
            redLogosMedium.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Minister);
            redLogosMedium.SimpleAddEncounter(1, Logos.Red, 1, Enlightened.Vessel, 1, Enlightened.Spirit);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                redLogosMedium.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Minister, 1, Signs.Purple);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                redLogosMedium.SimpleAddEncounter(1, Logos.Red, 1, "AloofEnvoy_EN");
            }
            redLogosMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Red.Med, 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyEncounter_API redLogosHard = new EnemyEncounter_API(0, Garden.H.Logos.Red.Hard, "RedLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Minister);
            redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Minister, 1, "SomeoneSister_EN");
            redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Skinning, 1, Enemies.Shivering);
            redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Skinning, 2, Enemies.Shivering);
            redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Minister, 2, "MachineGnomes_EN");
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, "MachineGnomes_EN", 1, "Monad_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Minister, 1, "FrowningChancellor_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, "AloofEnvoy_EN", 1, "Key_EN");
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                redLogosHard.SimpleAddEncounter(1, Logos.Red, 1, Enemies.Minister, 1, Noses.Yellow);
            }
            redLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Red.Hard, 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

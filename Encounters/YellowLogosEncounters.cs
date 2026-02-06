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
            EnemyEncounter_API yellowLogosMedium = new EnemyEncounter_API(0, Garden.H.Logos.Yellow.Med, "YellowLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            yellowLogosMedium.SimpleAddEncounter(1, Logos.Yellow, 1, "InHisImage_EN", 1, "InHisImage_EN");
            yellowLogosMedium.SimpleAddEncounter(1, Logos.Yellow, 1, "InHisImage_EN", 1, "InHisImage_EN", 1, "NextOfKin_EN");
            yellowLogosMedium.SimpleAddEncounter(1, Logos.Yellow, 1, "MachineGnomes_EN", 2, "NextOfKin_EN");
            yellowLogosMedium.SimpleAddEncounter(1, Logos.Yellow, 2, Enemies.Shivering);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                yellowLogosMedium.SimpleAddEncounter(1, Logos.Yellow, 1, Enemies.Minister, 1, Signs.Red);
                yellowLogosMedium.SimpleAddEncounter(1, Logos.Yellow, 2, "WRK_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                yellowLogosMedium.SimpleAddEncounter(1, Logos.Yellow, 1, "AloofEnvoy_EN");
            }
            yellowLogosMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Yellow.Med, 8, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyEncounter_API yellowLogosHard = new EnemyEncounter_API(0, Garden.H.Logos.Yellow.Hard, "YellowLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            yellowLogosHard.SimpleAddEncounter(1, Logos.Yellow, 1, Enemies.Minister);
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

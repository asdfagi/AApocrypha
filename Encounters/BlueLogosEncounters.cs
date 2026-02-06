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
            EnemyEncounter_API blueLogosMedium = new EnemyEncounter_API(0, Garden.H.Logos.Blue.Med, "BlueLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            blueLogosMedium.SimpleAddEncounter(1, Logos.Blue, 1, "InHisImage_EN", 1, "InHisImage_EN");
            blueLogosMedium.SimpleAddEncounter(1, Logos.Blue, 1, "InHisImage_EN", 1, "InHisImage_EN", 1, "NextOfKin_EN");
            blueLogosMedium.SimpleAddEncounter(1, Logos.Blue, 1, "MachineGnomes_EN", 2, "NextOfKin_EN");
            blueLogosMedium.SimpleAddEncounter(1, Logos.Blue, 2, Enemies.Shivering);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                blueLogosMedium.SimpleAddEncounter(1, Logos.Blue, 1, Enemies.Minister, 1, Signs.Purple);
                blueLogosMedium.SimpleAddEncounter(1, Logos.Blue, 1, "WRK_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                blueLogosMedium.SimpleAddEncounter(1, Logos.Blue, 1, "AloofEnvoy_EN");
            }
            blueLogosMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Blue.Med, 8, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyEncounter_API blueLogosHard = new EnemyEncounter_API(0, Garden.H.Logos.Blue.Hard, "BlueLogos_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/TerrorTrack",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Garden.H.ChoirBoy.Easy)._roarReference.roarEvent,
            };
            blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, Enemies.Minister);
            blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, Enemies.Minister, 1, "SomeoneSister_EN");
            blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, Enemies.Skinning, 1, Enemies.Shivering);
            blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, Enemies.Skinning, 2, Enemies.Shivering);
            blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, Enemies.Minister, 2, "MachineGnomes_EN");
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, "MachineGnomes_EN", 1, "SullenPrioress_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, "GigglingMinister_EN", 1, "FrowningChancellor_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, "AloofEnvoy_EN", 1, "Key_EN");
                blueLogosHard.SimpleAddEncounter(1, Logos.Blue, 1, "GigglingMinister_EN", 1, "Euryale_EN");
            }
            blueLogosHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Logos.Blue.Hard, 5, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

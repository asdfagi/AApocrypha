using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class EnlightenedEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("EnlightenedVessel_Sign", ResourceLoader.LoadSprite("EnlightenedVesselTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API enlightenedMed = new EnemyEncounter_API(0, Garden.H.EnlightenedVessel.Med, "EnlightenedVessel_Sign")
            {
                MusicEvent = "event:/AAMusic/Ridiculon/EverlastingHymn",
                RoarEvent = "event:/AAEnemy/Enlightened/EnlightenedRoar",
            };
            enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, "InHisImage_EN", 1, "InHerImage_EN");
            enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit);
            enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, "InHisImage_EN");
            enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 2, "NextOfKin_EN");
            enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, "MachineGnomes_EN");
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, "EyePalm_EN");
                enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Bots.Grey);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, Signs.Blue);
                enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, Enemies.Shivering, 1, Symbols.Blue);
                enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, Jumble.Entropic);
                enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, Jumble.Irid);
            }
            enlightenedMed.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.EnlightenedVessel.Med, 10, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyEncounter_API enlightenedHard = new EnemyEncounter_API(0, Garden.H.EnlightenedVessel.Hard, "EnlightenedVessel_Sign")
            {
                MusicEvent = "event:/AAMusic/Ridiculon/EverlastingHymn",
                RoarEvent = "event:/AAEnemy/Enlightened/EnlightenedRoar",
            };
            enlightenedHard.SimpleAddEncounter(2, Enlightened.Vessel, 1, Enlightened.Spirit);
            enlightenedHard.SimpleAddEncounter(2, Enlightened.Vessel, 1, "ProdigalFoundling_EN");
            enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, Enemies.Minister);
            enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, "ChoirBoy_EN");
            enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, "SomeoneSister_EN");
            enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 3, "MachineGnomes_EN");
            enlightenedMed.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 2, "InHisImage_EN", 1, "InHerImage_EN");
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, "FrowningChancellor_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 2, "PawnA_EN");
                enlightenedHard.SimpleAddEncounter(2, Enlightened.Vessel, 1, "Damocles_EN");
                enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 1, Enlightened.Spirit, 1, "Sundowner_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                enlightenedHard.SimpleAddEncounter(2, Enlightened.Vessel, 1, Enlightened.Spirit, 1, Signs.Purple);
                enlightenedHard.SimpleAddEncounter(1, Enlightened.Vessel, 1, Spoggle.Entropic, 1, Symbols.Red);
            }
            enlightenedHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.EnlightenedVessel.Hard, 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class DuneThresherEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("DuneThresher_Sign", ResourceLoader.LoadSprite("DuneThresherTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API duneThresherHard = new EnemyEncounter_API(0, Shore.H.DuneThresher.Hard, "DuneThresher_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/duneboi",
                RoarEvent = "event:/AAEnemy/EmplacementRoar",
            };
            duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN");
            duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 2, "SandSifter_EN");
            duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, "Mung_EN");
            duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, Spoggle.Blue);
            duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, Spoggle.Yellow);
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, "Scylla_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, Spoggle.Green);
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, "Madman_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, "Enno_EN");
            }
            if (AApocrypha.CrossMod.Colophons)
            {
                duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, Colophon.Red);
                duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, Colophon.Blue);
                duneThresherHard.SimpleAddEncounter(1, "DuneThresher_EN", 1, "SandSifter_EN", 1, Colophon.BlueRedSplit);
            }
            duneThresherHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.DuneThresher.Hard, 8, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard); //8
        }
    }
}

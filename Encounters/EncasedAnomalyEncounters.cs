using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class EncasedAnomalyEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("EncasedAnomaly_Sign", ResourceLoader.LoadSprite("EncasedAnomalyTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API encasedAnomalyMedium = new EnemyEncounter_API(0, Orph.H.Anomaly.Encased.Med, "EncasedAnomaly_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 2, "MusicMan_EN");
            encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, "MusicMan_EN", 1, "Scrungie_EN");
            encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Anomalies.Unbound, 1, "MusicMan_EN", 1, "Scrungie_EN");
            encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.Blue, 1, Jumble.Red);
            encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 3, Enemies.Suckle);
            if (AApocrypha.CrossMod.BismuthBoiler)
            {
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 2, Enemies.Suckle, 1, "FerrousFeaster_EN");
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 2, Enemies.Suckle, 1, "ArgonAccelerator_EN");
            }
            if (AApocrypha.CrossMod.Colophons)
            {
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.Red, 1, Colophon.Yellow);
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.Blue, 1, Colophon.Yellow);
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.BlueYellowSplit, 1, Colophon.Yellow);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.Red, 1, Colophon.Green);
                    encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, "Fanatic_EN", 1, Colophon.PurpleRedSplit);
                }
            };
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.Purple, 1, Spoggle.BlueYellowSplit, 1, "Follower_EN");
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.RedPurpleSplit, 1, "Follower_EN");
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, "Fanatic_EN", 1, "Follower_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 2, Frostbites.Normal);
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.BlueYellowSplit, 1, "Jansuli_EN");
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Spoggle.Purple, 1, "Thunderdome_EN", 1, "MusicMan_EN");
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 2, "Moone_EN");
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, "Moone_EN", 1, "Scrungie_EN");
            }
            if (AApocrypha.CrossMod.EnemyPack)
            {
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 2, "Chapman_EN");
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, Anomalies.Unbound, 1, "NakedGizo_EN");
                encasedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Encased, 1, "NeoplasmHeap_EN");
            }
            encasedAnomalyMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Anomaly.Encased.Med, 15, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

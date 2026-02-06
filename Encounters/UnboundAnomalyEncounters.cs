using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.Encounters.Shore.H;

namespace A_Apocrypha.Encounters
{
    public class UnboundAnomalyEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Anomaly_Sign", ResourceLoader.LoadSprite("AnomalyTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API anomalyEasy = new EnemyEncounter_API(0, Orph.H.Anomaly.Unbound.Easy, "Anomaly_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            anomalyEasy.SimpleAddEncounter(1, Anomalies.Unbound, 1, "MusicMan_EN");
            anomalyEasy.SimpleAddEncounter(1, Anomalies.Unbound, 2, "MusicMan_EN");
            anomalyEasy.SimpleAddEncounter(1, Anomalies.Unbound, 1, "SingingStone_EN");
            anomalyEasy.SimpleAddEncounter(1, Anomalies.Unbound, 1, "MusicMan_EN", 1, Jumble.Blue);
            anomalyEasy.SimpleAddEncounter(1, Anomalies.Unbound, 4, Enemies.Suckle);
            if (AApocrypha.CrossMod.BismuthBoiler)
            {
                anomalyEasy.SimpleAddEncounter(1, Anomalies.Unbound, 3, Enemies.Suckle, 1, "FerrousFeaster_EN");
            }
            anomalyEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Anomaly.Unbound.Easy, 3, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Easy);

            EnemyEncounter_API anomalyMedium = new EnemyEncounter_API(0, Orph.H.Anomaly.Unbound.Med, "Anomaly_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            anomalyMedium.SimpleAddEncounter(2, Anomalies.Unbound, 1, "SingingStone_EN", 1, Jumble.Blue);
            anomalyMedium.SimpleAddEncounter(3, Anomalies.Unbound);
            anomalyMedium.SimpleAddEncounter(2, Anomalies.Unbound, 1, Spoggle.Purple);
            anomalyMedium.SimpleAddEncounter(2, Anomalies.Unbound, 1, Spoggle.BlueYellowSplit);
            if (AApocrypha.CrossMod.Colophons)
            {
                anomalyMedium.SimpleAddEncounter(2, Anomalies.Unbound, 1, Colophon.Yellow);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    anomalyMedium.SimpleAddEncounter(2, Anomalies.Unbound, 1, Colophon.Green);
                }
            }
            anomalyMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Anomaly.Unbound.Med, 4, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

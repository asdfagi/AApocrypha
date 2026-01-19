using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SharpenedAnomalyEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("SharpenedAnomaly_Sign", ResourceLoader.LoadSprite("SharpenedAnomalyTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sharpenedAnomalyMedium = new EnemyEncounter_API(0, Orph.H.Anomaly.Sharpened.Med, "SharpenedAnomaly_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = "event:/AAEnemy/Anomaly1Roar",
            };
            sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 2, "MusicMan_EN");
            sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.Blue, 1, Jumble.Red);
            sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.Yellow, 1, Jumble.Blue);
            sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.RedPurpleSplit, 1, Jumble.Red);
            sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.BlueYellowSplit, 1, Jumble.Yellow);
            sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 3, Enemies.Suckle);
            if (AApocrypha.CrossMod.Colophons)
            {
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Colophon.Yellow, 1, Jumble.Red);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Colophon.Green, 1, Spoggle.BlueYellowSplit);
                }
                if (AApocrypha.CrossMod.pigmentRainbow)
                {
                    sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Colophon.Yellow, 1, Jumble.Rainbow);
                }
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 2, Frostbites.Normal);
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Anomalies.Encased, 1, Frostbites.Normal, 1, Frostbites.Tall);
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, "MusicMan_EN", 1, "BackupDancer_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 2, "MusicMan_EN", 1, Flower.Yellow);
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 2, "MusicMan_EN", 1, "Enigma_EN");
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, "MusicMan_EN", 1, Bots.Red);
                if (AApocrypha.CrossMod.EnemyPack)
                {
                    sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 2, "Neoplasm_EN", 1, Bots.Yellow);
                }
            }
            if (AApocrypha.CrossMod.IntoTheAbyss && AApocrypha.CrossMod.pigmentGilded)
            {
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.BlueYellowSplit, 1, Jumble.Gilded);
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.PurpleRedSplit, 1, Jumble.Gilded);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, "MusicMan_EN", 1, "Hagwitch_EN");
            }
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.Yellow, 1, Jumble.Rainbow);
                sharpenedAnomalyMedium.SimpleAddEncounter(1, Anomalies.Sharpened, 1, Spoggle.YellowBlueSplit, 1, Jumble.Rainbow);
            }
            sharpenedAnomalyMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Anomaly.Sharpened.Med, 15, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

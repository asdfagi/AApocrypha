using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BlueAggregateEncounters
    {
        public static void Add()
        {
            if (Siren.Exists)
            {
                Portals.AddPortalSign("BlueAggregate_Sign", ResourceLoader.LoadSprite("AggregateBlueTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

                EnemyEncounter_API blueMoldEasy = new EnemyEncounter_API(0, Siren.H.Aggregates.Blue.Easy, "BlueAggregate_Sign")
                {
                    MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                    RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                };
                blueMoldEasy.SimpleAddEncounter(1, Aggregates.Blue, 1, "Boiler_EN", 1, "BirdBath_EN");
                blueMoldEasy.SimpleAddEncounter(1, Aggregates.Blue, 1, "Boiler_EN", 1, "HazardHauler_Siren_EN");
                blueMoldEasy.SimpleAddEncounter(1, Aggregates.Blue, 2, "Boiler_EN");
                blueMoldEasy.SimpleAddEncounter(1, Aggregates.Blue, 1, "Boiler_EN", 1, "PetrifiedPuker_EN");
                blueMoldEasy.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Siren.H.Aggregates.Blue.Easy, 12, "TheSiren_Zone1", BundleDifficulty.Easy);

                EnemyEncounter_API blueMoldMed = new EnemyEncounter_API(0, Siren.H.Aggregates.Blue.Med, "BlueAggregate_Sign")
                {
                    MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                    RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
                };
                blueMoldMed.SimpleAddEncounter(1, Aggregates.Blue, 2, "Tassnn_EN", 1, "BirdBath_EN");
                blueMoldMed.SimpleAddEncounter(1, Aggregates.Blue, 1, "Boiler_EN", 1, "BirdBath_EN", 1, "WinterLantern_EN");
                blueMoldMed.SimpleAddEncounter(1, Aggregates.Blue, 2, "Tumult_EN", 1, "BirdBath_EN");
                blueMoldMed.SimpleAddEncounter(1, Aggregates.Blue, 1, "Boiler_EN", 1, Aggregates.Red);
                if (AApocrypha.CrossMod.SaltEnemies)
                {
                    blueMoldMed.SimpleAddEncounter(1, Aggregates.Blue, 1, Ecstasy.Random, 1, "Boiler_EN");
                    blueMoldMed.SimpleAddEncounter(1, Aggregates.Blue, 1, Ecstasy.Random, 1, "BirdBath_EN");
                }
                blueMoldMed.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Siren.H.Aggregates.Blue.Med, 9, "TheSiren_Zone1", BundleDifficulty.Medium);
            }
        }
    }
}

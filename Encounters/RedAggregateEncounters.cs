using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class RedAggregateEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("RedAggregate_Sign", ResourceLoader.LoadSprite("AggregateRedTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API redMoldEasy = new EnemyEncounter_API(0, Shore.H.Aggregates.Red.Easy, "RedAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            redMoldEasy.SimpleAddEncounter(1, Aggregates.Red, 1, "MudLung_EN");
            redMoldEasy.SimpleAddEncounter(1, Aggregates.Red, 2, "Mung_EN");
            redMoldEasy.SimpleAddEncounter(1, Aggregates.Red, 1, "SandSifter_EN");
            redMoldEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Aggregates.Red.Easy, 12, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);

            EnemyEncounter_API redMoldMed = new EnemyEncounter_API(0, Shore.H.Aggregates.Red.Med, "RedAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            redMoldMed.SimpleAddEncounter(1, Aggregates.Red, 1, Enemies.Mungling, 1, "Mung_EN");
            redMoldMed.SimpleAddEncounter(1, Aggregates.Red, 2, "Keko_EN");
            redMoldMed.SimpleAddEncounter(1, Aggregates.Red, 1, "MudLung_EN", 1, "Wringle_EN");
            redMoldMed.SimpleAddEncounter(1, Aggregates.Red, 1, "FungusColumn_EN", 1, "Mung_EN");
            redMoldMed.SimpleAddEncounter(1, Aggregates.Red, 1, Enemies.Mungling, 1, "Flarblet_EN");
            redMoldMed.SimpleAddEncounter(1, Aggregates.Red, 2, "MudLung_EN", 1, "Flarblet_EN");
            if (AApocrypha.CrossMod.Mythos)
            {
                redMoldMed.SimpleAddEncounter(1, Aggregates.Red, 1, "MudLung_EN", 1, "Madman_EN");
            }
            redMoldMed.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Aggregates.Red.Med, 9, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}

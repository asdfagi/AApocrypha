using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class PurpleAggregateEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("PurpleAggregate_Sign", ResourceLoader.LoadSprite("AggregatePurpleTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API purpleMoldEasy = new EnemyEncounter_API(0, Shore.H.Aggregates.Purple.Easy, "PurpleAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            purpleMoldEasy.SimpleAddEncounter(1, Aggregates.Purple, 1, "MudLung_EN");
            purpleMoldEasy.SimpleAddEncounter(1, Aggregates.Purple, 2, "Mung_EN");
            purpleMoldEasy.SimpleAddEncounter(1, Aggregates.Purple, 1, "SandSifter_EN");
            purpleMoldEasy.SimpleAddEncounter(1, Aggregates.Purple, 1, "Acolyte_EN");
            purpleMoldEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Aggregates.Purple.Easy, 12, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);

            EnemyEncounter_API purpleMoldMed = new EnemyEncounter_API(0, Shore.H.Aggregates.Purple.Med, "PurpleAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 1, Enemies.Mungling, 1, "Mung_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 2, "Keko_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 1, "MudLung_EN", 1, "Wringle_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 1, "FungusColumn_EN", 1, "Mung_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 2, "Asterism_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 1, "Asterism_EN", 1, "Acolyte_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 2, "Acolyte_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 1, Enemies.Mungling, 1, "Flarblet_EN");
            purpleMoldMed.SimpleAddEncounter(1, Aggregates.Purple, 2, "MudLung_EN", 1, "Flarblet_EN");
            purpleMoldMed.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Aggregates.Purple.Med, 9, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}

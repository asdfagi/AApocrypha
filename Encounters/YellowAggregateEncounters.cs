using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class YellowAggregateEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("YellowAggregate_Sign", ResourceLoader.LoadSprite("AggregateYellowTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API yellowMoldEasy = new EnemyEncounter_API(0, Orph.H.Aggregates.Yellow.Easy, "YellowAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            yellowMoldEasy.SimpleAddEncounter(1, Aggregates.Yellow, 1, "MusicMan_EN", 1, "SingingStone_EN");
            yellowMoldEasy.SimpleAddEncounter(1, Aggregates.Yellow, 1, "SingingStone_EN", 1, "Scrungie_EN");
            yellowMoldEasy.SimpleAddEncounter(1, Aggregates.Yellow, 2, "MusicMan_EN");
            yellowMoldEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Aggregates.Yellow.Easy, 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Easy);

            EnemyEncounter_API yellowMoldMed = new EnemyEncounter_API(0, Orph.H.Aggregates.Yellow.Med, "YellowAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            yellowMoldMed.SimpleAddEncounter(1, Aggregates.Yellow, 2, "MusicMan_EN", 1, "Scrungie_EN");
            yellowMoldMed.SimpleAddEncounter(1, Aggregates.Yellow, 1, "MusicMan_EN", 2, "SingingStone_EN");
            yellowMoldMed.SimpleAddEncounter(1, Aggregates.Yellow, 1, "MusicMan_EN", 2, "Blemmigan_EN");
            yellowMoldMed.SimpleAddEncounter(1, Aggregates.Yellow, 1, "MusicMan_EN", 1, HiddenBloatfinger.OrpheumRandom);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                yellowMoldMed.SimpleAddEncounter(1, Aggregates.Yellow, 2, Frostbites.Normal);
                yellowMoldMed.SimpleAddEncounter(1, Aggregates.Yellow, 1, "MusicMan_EN", 1, "BackupDancer_EN");
            }
            yellowMoldMed.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Aggregates.Yellow.Med, 9, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

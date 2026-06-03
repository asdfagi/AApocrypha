using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class WhiteAggregateEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("WhiteAggregate_Sign", ResourceLoader.LoadSprite("AggregateWhiteTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API whiteMoldEasy = new EnemyEncounter_API(0, Garden.H.Aggregates.White.Easy, "WhiteAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            whiteMoldEasy.SimpleAddEncounter(1, Aggregates.White, 1, "InHisImage_EN", 1, "NextOfKin_EN");
            whiteMoldEasy.SimpleAddEncounter(1, Aggregates.White, 2, Enemies.Shivering);
            whiteMoldEasy.SimpleAddEncounter(1, Aggregates.White, 2, "InHerImage_EN");
            whiteMoldEasy.SimpleAddEncounter(1, Aggregates.White, 1, "SomeoneSister_EN", 1, "NextOfKin_EN");
            whiteMoldEasy.SimpleAddEncounter(1, Aggregates.White, 1, "MachineGnomes_EN");
            whiteMoldEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Aggregates.White.Easy, 8, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Easy);
            /*
            EnemyEncounter_API whiteMoldMed = new EnemyEncounter_API(0, Garden.H.Aggregates.White.Med, "WhiteAggregate_Sign")
            {
                MusicEvent = "event:/AAMusic/Gingiva/UpWeGo",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            whiteMoldMed.SimpleAddEncounter(1, Aggregates.White, 2, "InHisImage_EN", 1, "ProdigalFoundling_EN");
            whiteMoldMed.SimpleAddEncounter(1, Aggregates.White, 1, "MusicMan_EN", 2, "SingingStone_EN");
            whiteMoldMed.SimpleAddEncounter(1, Aggregates.White, 1, "MusicMan_EN", 2, "Blemmigan_EN");
            whiteMoldMed.SimpleAddEncounter(1, Aggregates.White, 1, "MusicMan_EN", 1, HiddenBloatfinger.OrpheumRandom);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                whiteMoldMed.SimpleAddEncounter(1, Aggregates.White, 2, Frostbites.Normal);
                whiteMoldMed.SimpleAddEncounter(1, Aggregates.White, 1, "MusicMan_EN", 1, "BackupDancer_EN");
            }
            whiteMoldMed.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Aggregates.White.Med, 9, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);*/
        }
    }
}

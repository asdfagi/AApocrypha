using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TearDrinkerEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("TearDrinker_Sign", ResourceLoader.LoadSprite("TearDrinkerTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API tearDrinkerEasy = new EnemyEncounter_API(0, Shore.H.TearDrinker.Easy, "TearDrinker_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/YellowFrog",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Shore.H.Keko.Easy)._roarReference.roarEvent,
            };
            tearDrinkerEasy.SimpleAddEncounter(1, "TearDrinker_EN");
            tearDrinkerEasy.SimpleAddEncounter(1, "TearDrinker_EN", 1, "Mung_EN");
            tearDrinkerEasy.SimpleAddEncounter(1, "TearDrinker_EN", 1, "MudLung_EN", 1, "Mung_EN");
            tearDrinkerEasy.SimpleAddEncounter(1, "TearDrinker_EN", 1, "Mung_EN", 1, "SandSifter_EN");
            tearDrinkerEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.TearDrinker.Easy, 4, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);
            
            EnemyEncounter_API tearDrinkerMedium = new EnemyEncounter_API(0, Shore.H.TearDrinker.Med, "TearDrinker_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/YellowFrog",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Shore.H.Keko.Easy)._roarReference.roarEvent,
            };
            tearDrinkerMedium.SimpleAddEncounter(2, "TearDrinker_EN");
            tearDrinkerMedium.SimpleAddEncounter(2, "TearDrinker_EN", 1, "MudLung_EN");
            tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 2, "MudLung_EN");
            tearDrinkerEasy.SimpleAddEncounter(1, "TearDrinker_EN", 1, Enemies.Mungling, 1, "MudLung_EN", 1, "Mung_EN");
            tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, Jumble.Red, 1, Jumble.Yellow);
            tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, Jumble.Red, 1, Spoggle.Blue);
            if (AApocrypha.CrossMod.Colophons)
            {
                tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, Jumble.Red, 1, Colophon.Blue);
                tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, Jumble.Yellow, 1, Colophon.Red);
                tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, Spoggle.Blue, 1, Colophon.RedBlueSplit);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, Jumble.Red, 1, "Goomba_EN");
                tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, Jumble.Red, 1, Spoggle.Green);
                tearDrinkerMedium.SimpleAddEncounter(2, "TearDrinker_EN", 1, Spoggle.Blue, 1, "Follower_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                tearDrinkerMedium.SimpleAddEncounter(2, "TearDrinker_EN", 1, "NobodyGrave_EN");
                tearDrinkerMedium.SimpleAddEncounter(2, "TearDrinker_EN", 1, "LittleBeak_EN");
                tearDrinkerMedium.SimpleAddEncounter(1, "TearDrinker_EN", 1, "Minana_EN", 1, "MudLung_EN", 1, "Mung_EN");
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                tearDrinkerMedium.SimpleAddEncounter(2, "TearDrinker_EN", 1, Spoggle.Unstable, 1, "MudLung_EN");
            }
            tearDrinkerMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.TearDrinker.Med, 14, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);
        }
    }
}

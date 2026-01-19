using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ColophonDualisticEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("ColophonDualistic_Sign", ResourceLoader.LoadSprite("ColophonDualisticTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API colophonDualisticEasy = new EnemyEncounter_API(0, Shore.H.Colophon.RedBlueSplit.Easy, "ColophonDualistic_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor/HurtPeopleFullCircle",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("ComposedEasy")._roarReference.roarEvent,
            };
            colophonDualisticEasy.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, "MudLung_EN", 1, "Mung_EN");
            colophonDualisticEasy.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, "MunglingMudLung_EN");
            colophonDualisticEasy.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, "SandSifter_EN", 1, "Mung_EN");
            colophonDualisticEasy.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, Colophon.Blue, 1);
            if (AApocrypha.CrossMod.Mythos)
            {
                colophonDualisticEasy.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, "Madman_EN", 1, "Mung_EN");
            }
            colophonDualisticEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Colophon.RedBlueSplit.Easy, 3, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy); //default: 3

            EnemyEncounter_API colophonDualisticMedium = new EnemyEncounter_API(0, Shore.H.Colophon.RedBlueSplit.Med, "ColophonDualistic_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor/HurtPeopleFullCircle",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("ComposedEasy")._roarReference.roarEvent,
            };
            colophonDualisticMedium.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, "MudLung_EN", 1, Colophon.Red);
            colophonDualisticMedium.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, "MunglingMudLung_EN", 1, "TearDrinker_EN");
            colophonDualisticMedium.SimpleAddEncounter(1, Colophon.RedBlueSplit, 1, "SandSifter_EN", 1, "MudLung_EN", 1, Colophon.Blue);
            colophonDualisticMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Colophon.RedBlueSplit.Med, 5, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium); //default: 5
        }
    }
}

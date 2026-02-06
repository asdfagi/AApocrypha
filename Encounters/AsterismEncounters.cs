using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class AsterismEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Asterism_Sign", ResourceLoader.LoadSprite("AsterismTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API asterismMedium = new EnemyEncounter_API(0, Shore.H.Asterism.Med, "Asterism_Sign")
            {
                MusicEvent = "event:/AAMusic/LookOutside/BeautifulButWrong",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.Spoggle.Purple.Med)._roarReference.roarEvent,
            };
            asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "MudLung_EN", 1, "Mung_EN");
            asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "MudLung_EN", 2, "Mung_EN");
            asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "MunglingMudLung_EN", 1, "Mung_EN");
            asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Keko_EN");
            asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 2, "Keko_EN");
            asterismMedium.SimpleAddEncounter(2, "Asterism_EN");
            asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Acolyte_EN");
            asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "SandSifter_EN", 1, "MudLung_EN", 1, "Mung_EN");
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Flakkid_EN");
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Enno_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 2, "Minana_EN");
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Pinano_EN");
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Wall_EN");
            }
            if (AApocrypha.CrossMod.Colophons)
            {
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Mung_EN", 1, Colophon.Red);
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Mung_EN", 1, Colophon.Blue);
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Mung_EN", 1, Colophon.BlueRedSplit);
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Scylla_EN", 1, "Mung_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Goomba_EN");
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Acolyte_EN", 1, Spoggle.Green);
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Surimi_EN");
                asterismMedium.SimpleAddEncounter(1, "Asterism_EN", 1, "Snaurce_EN", 1, "MudLung_EN");
            }
            asterismMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Asterism.Med, 12, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);

            EnemyEncounter_API asterismHard = new EnemyEncounter_API(0, Shore.H.Asterism.Hard, "Asterism_Sign")
            {
                MusicEvent = "event:/AAMusic/LookOutside/BeautifulButWrong",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.Spoggle.Purple.Med)._roarReference.roarEvent,
            };
            asterismHard.SimpleAddEncounter(2, "Asterism_EN", 1, "MunglingMudLung_EN", 1, "MudLung_EN");
            asterismHard.SimpleAddEncounter(4, "Asterism_EN");
            asterismHard.SimpleAddEncounter(3, "Asterism_EN", 1, Spoggle.Blue);
            asterismHard.SimpleAddEncounter(3, "Asterism_EN", 1, Spoggle.Yellow);
            asterismHard.SimpleAddEncounter(2, "Asterism_EN", 1, "FungusColumn_EN", 1, Enemies.Mungling);
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                asterismHard.SimpleAddEncounter(2, "Asterism_EN", 1, "Follower_EN");
                asterismHard.SimpleAddEncounter(3, "Asterism_EN", 1, Spoggle.Green);
            };
            asterismHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Asterism.Hard, 12, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard);
        }
    }
}

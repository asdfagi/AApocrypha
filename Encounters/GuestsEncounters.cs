using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class GuestsEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Guests_Sign", ResourceLoader.LoadSprite("GuestsTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API guestsMed = new EnemyEncounter_API(0, Shore.H.Guests.Cluster.Med, "Guests_Sign")
            {
                MusicEvent = "event:/AAMusic/Downwell/Limbo1",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Shore.Keko.Easy)._roarReference.roarEvent,
            };
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "MudLung_EN", 1, "Mung_EN");
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, Enemies.Mungling);
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 2, "Keko_EN");
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "Asterisn_EN");
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "MudLung_EN", 1, "TearDrinker_EN");
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "Asterisn_EN", 1, Aggregates.Purple);
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "Acolyte_EN", 1, Aggregates.Red);
            guestsMed.SimpleAddEncounter(3, "TangledGuests_EN", 1, Aggregates.Red);
            guestsMed.SimpleAddEncounter(3, "TangledGuests_EN", 1, Jumble.Red);
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, Enemies.Mungling, 1, "FungusColumn_EN");
            guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "MudLung_EN", 1, Spoggle.Blue);
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "MudLung_EN", 1, "Flakkid_EN");
                guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, "MudLung_EN", 1, "UnculturedSwine_EN");
            }
            if (AApocrypha.CrossMod.LeonLegion)
            {
                guestsMed.SimpleAddEncounter(2, "TangledGuests_EN", 1, Martians.Red, 1, "Mung_EN");
            }
            guestsMed.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Guests.Cluster.Med, 9, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium);

            Portals.AddPortalSign("GuestsBig_Sign", ResourceLoader.LoadSprite("GuestsBigTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API guestsBigHard = new EnemyEncounter_API(0, Shore.H.Guests.Colony.Hard, "GuestsBig_Sign")
            {
                MusicEvent = "event:/AAMusic/Downwell/Limbo2",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Shore.H.Kekastle.Hard)._roarReference.roarEvent,
            };
            guestsBigHard.SimpleAddEncounter(1, "GuestColony_EN", 1, "TangledGuests_EN", 1, "MudLung_EN", 1, "Mung_EN");
            guestsBigHard.SimpleAddEncounter(1, "GuestColony_EN", 1, "TangledGuests_EN", 1, Enemies.Mungling, 1, "Mung_EN");
            guestsBigHard.SimpleAddEncounter(1, "GuestColony_EN", 2, "TangledGuests_EN", 1, Aggregates.Red);
            guestsBigHard.SimpleAddEncounter(1, "GuestColony_EN", 1, "TangledGuests_EN", 1, Jumble.Red);
            guestsBigHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Guests.Colony.Hard, 6, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard);
        }
    }
}

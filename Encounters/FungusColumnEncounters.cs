using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class FungusColumnEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("FungusColumn_Sign", ResourceLoader.LoadSprite("FungusColumnTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API fungusMedium = new EnemyEncounter_API(0, Shore.H.FungusColumn.Med, "FungusColumn_Sign")
            {
                MusicEvent = "event:/AAMusic/FallenLondon/WhyWeWearFaces",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, "MudLung_EN");
            fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, Jumble.Yellow);
            fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, Jumble.Yellow, 1, "MudLung_EN");
            fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, Spoggle.Blue);
            fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 2, "Keko_EN");
            if (AApocrypha.CrossMod.Colophons)
            {
                fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, Colophon.Red);
                fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, Colophon.Blue);
                fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, Colophon.RedBlueSplit);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, Spoggle.Green);
                fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, "Follower_EN", 1, "MudLung_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, "NotAn_EN", 1, "MudLung_EN");
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                fungusMedium.SimpleAddEncounter(1, "FungusColumn_EN", 1, "RatThing_EN", 1, "MudLung_EN");
            }
            fungusMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.FungusColumn.Med, 16, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Medium); //default: 16
        }
    }
}

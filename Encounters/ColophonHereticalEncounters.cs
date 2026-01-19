using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ColophonHereticalEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("ColophonHeretical_Sign", ResourceLoader.LoadSprite("ColophonHereticalTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API colophonHereticalMedium = new EnemyEncounter_API(0, Orph.H.Colophon.RedPurpleSplit.Med, "ColophonHeretical_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor/HurtPeopleFullCircle",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("DefeatedEasy")._roarReference.roarEvent,
            };
            colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 2, "MusicMan_EN");
            colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, "MusicMan_EN", 1, "Scrungie_EN");
            colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, Colophon.Yellow, 1, "MusicMan_EN");
            colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, Jumble.Blue, 1, "MusicMan_EN");
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, Jumble.Rainbow, 1, "MusicMan_EN");
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, "StarVampire_EN", 1, "MusicMan_EN");
                colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, "StarVampire_EN", 1, "Scrungie_EN");
                colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, "Lloigor_EN", 1, Colophon.Yellow);
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 2, Frostbites.Normal);
                colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, Frostbites.Normal, 1, Frostbites.Gilded);
                colophonHereticalMedium.SimpleAddEncounter(1, Colophon.RedPurpleSplit, 1, Colophon.Yellow, 1, "Footshroom_EN");
            }
            colophonHereticalMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Colophon.RedPurpleSplit.Med, 6, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium); //default: 6
        }
    }
}

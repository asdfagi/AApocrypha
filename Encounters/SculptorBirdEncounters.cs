using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SculptorBirdEncounters
    {   
        public static void Add()
        {
            Portals.AddPortalSign("SculptorBird_Sign", ResourceLoader.LoadSprite("SculptorBirdTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sculptorBirdMedium = new EnemyEncounter_API(0, Orph.H.SculptorBird.Med, "SculptorBird_Sign")
            {
                MusicEvent = "event:/AAMusic/mudeth/DepressionShop",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.Scrungie.Hard)._roarReference.roarEvent,
            };
            sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, "MusicMan_EN");
            sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 2, "MusicMan_EN");
            sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Spoggle.Blue, 1, Jumble.Red);
            sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Spoggle.PurpleRedSplit, 1, Jumble.Yellow);
            sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 2, "Blemmigan_EN");
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 2, Frostbites.Normal);
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Frostbites.Normal, 1, Frostbites.Tall);
            }
            ;
            if (AApocrypha.CrossMod.Colophons)
            {
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.Purple);
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.Yellow);
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.PurpleRedSplit);
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.Purple, 1, "Scrungie_EN");
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.Yellow, 1, Spoggle.Purple);
                if (AApocrypha.CrossMod.pigmentPeppermint)
                {
                    sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.Peppermint, 1, "Scrungie_EN");
                    sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.Peppermint, 1, Spoggle.Yellow);
                }
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Colophon.Green);
                }
            };
            if (AApocrypha.CrossMod.Mythos)
            {
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, "Lloigor_EN");
                sculptorBirdMedium.SimpleAddEncounter(1, "SculptorBird_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, "StarVampire_EN", 1, "MusicMan_EN");
            }
            sculptorBirdMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.SculptorBird.Med, 18, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

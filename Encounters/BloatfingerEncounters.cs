using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BloatfingerEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Bloatfinger_Sign", ResourceLoader.LoadSprite("BloatfingerTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API bloatfingerMedium = new EnemyEncounter_API(EncounterType.Random, Orph.H.Bloatfinger.Med, "Bloatfinger_Sign")
            {
                MusicEvent = "event:/AAMusic/FallenLondon/WhyWeWearFaces",
                RoarEvent = LoadedAssetsHandler.GetEnemy("SilverSuckle_EN").deathSound,
            };
            bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 2, "MusicMan_EN");
            bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, "MusicMan_EN");
            bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Jumble.Red);
            bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Jumble.Yellow);
            bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Jumble.Blue);
            bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Jumble.Purple);
            bloatfingerMedium.SimpleAddEncounter(2, "Bloatfinger_EN", 1, "Scrungie_EN");
            bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 3, Enemies.Suckle);
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, Jumble.Rainbow);
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, HiddenBloatfinger.OrpheumRandom, 1, Bots.Red);
                bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 1, HiddenBloatfinger.OrpheumRandom, 1, HiddenBloatfinger.OrpheumRandom, 1, Bots.Yellow);
                bloatfingerMedium.SimpleAddEncounter(1, "Bloatfinger_EN", 2, "Foxtrot_EN");
            }
            bloatfingerMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone02_Bloatfinger_Medium_EnemyBundle", 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ColophonSaccharineEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("ColophonSaccharine_Sign", ResourceLoader.LoadSprite("ColophonPeppermintTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API colophonSaccharineMedium = new EnemyEncounter_API(0, Garden.H.Colophon.Peppermint.Med, "ColophonSaccharine_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor/HurtPeopleFullCircle",
                RoarEvent = "event:/AAEnemy/ColophonSaccharineRoar",
            };
            colophonSaccharineMedium.SimpleAddEncounter(1, Colophon.Peppermint, 1, "InHisImage_EN", 1, "InHerImage_EN", 1, "NextOfKin_EN");
            colophonSaccharineMedium.SimpleAddEncounter(1, Colophon.Peppermint, 1, "InHisImage_EN", 1, "NextOfKin_EN");
            colophonSaccharineMedium.SimpleAddEncounter(1, Colophon.Peppermint, 2, "ShiveringHomunculus_EN");
            colophonSaccharineMedium.SimpleAddEncounter(1, Colophon.Peppermint, 1, "GigglingMinister_EN");
            colophonSaccharineMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Colophon.Peppermint.Med, 6, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Medium);
            EnemyEncounter_API colophonSaccharineHard = new EnemyEncounter_API(0, Orph.H.Colophon.Peppermint.Hard, "ColophonSaccharine_Sign")
            {
                MusicEvent = "event:/AAMusic/MaddieDoktor/HurtPeopleFullCircle",
                RoarEvent = "event:/AAEnemy/ColophonSaccharineRoar",
            };
            colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Colophon.Blue);
            colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Colophon.Blue, 1, Colophon.Red);
            colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Colophon.Red);
            colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Colophon.RedPurpleSplit);
            colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Spoggle.Yellow, 1, "MusicMan_EN");
            colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, "Scrungie_EN", 1, "MusicMan_EN");
            colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 2, "Scrungie_EN", 1, "SingingStone_EN");
            if (AApocrypha.CrossMod.Mythos)
            {
                colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Colophon.Blue, 1, "Lloigor_EN");
                colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Colophon.Red, 1, "StarVampire_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 1, Colophon.Blue, 2, "Frostbite_EN");
                colophonSaccharineHard.SimpleAddEncounter(1, Colophon.Peppermint, 3, "Frostbite_EN");
            }
            colophonSaccharineHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Colophon.Peppermint.Hard, 4, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Hard);
        }
    }
}

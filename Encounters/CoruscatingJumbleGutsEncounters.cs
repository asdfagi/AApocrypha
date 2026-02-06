using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CoruscatingJumbleGutsEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("RainbowGuts_Sign", ResourceLoader.LoadSprite("RainbowGutsTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API rainbowGutsMedium = new EnemyEncounter_API(0, Orph.H.Jumble.Rainbow.Med, "RainbowGuts_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/SecondaryColors",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.H.Jumble.Purple.Med)._roarReference.roarEvent,
            };
            rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Jumble.Blue, 1, "MusicMan_EN");
            rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Jumble.Purple, 1, "MusicMan_EN");
            rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Jumble.Purple, 1, Jumble.Red);
            rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Jumble.Purple, 1, Spoggle.Red);
            rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 2, "MusicMan_EN", 1, "SingingStone_EN");
            rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, "MusicMan_EN", 1, "Scrungie_EN");
            if (AApocrypha.CrossMod.Colophons)
            {
                rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Colophon.Yellow, 1, "MusicMan_EN");
                rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Colophon.Purple, 1, "MusicMan_EN", 1, "SingingStone_EN");
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                if (AApocrypha.CrossMod.Colophons)
                {
                    rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Colophon.Green, 1, "MusicMan_EN", 1, "SingingStone_EN");
                }
                if (AApocrypha.CrossMod.pigmentGilded)
                {
                    rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, Jumble.Gilded, 1, "MusicMan_EN");
                }
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                rainbowGutsMedium.SimpleAddEncounter(1, Jumble.Rainbow, 1, "Jansuli_EN", 1, "MusicMan_EN");
            }
            rainbowGutsMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Jumble.Rainbow.Med, 10, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

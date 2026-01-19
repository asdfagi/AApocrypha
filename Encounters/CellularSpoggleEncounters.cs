using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CellularSpoggleEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("CellularSpoggle_Sign", ResourceLoader.LoadSprite("CellularSpoggleTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API cellularSpoggleMedium = new EnemyEncounter_API(0, Orph.H.Spoggle.YellowBlueSplit.Med, "CellularSpoggle_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/WhimperAndWhine",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.H.Spoggle.Red.Med)._roarReference.roarEvent,
            };
            cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, Spoggle.Yellow);
            cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, Spoggle.PurpleRedSplit);
            cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 2, "MusicMan_EN");
            cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, "Scrungie_EN");
            cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 2, Enemies.Suckle);
            cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, "MusicMan_EN", 2, "Blemmigan_EN");
            if (AApocrypha.CrossMod.Colophons)
            {
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, Colophon.Yellow);
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, Colophon.Purple, 1, "SingingStone_EN");
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, Colophon.Green, 1, "SingingStone_EN");
                    cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, Colophon.Green, 1, "SingingStone_EN", 1, "Blemmigan_EN");
                }
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 2, "Frostbite_EN");
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, "MusicMan_EN", 1, "Jansuli_EN");
            }
            if (AApocrypha.CrossMod.EnemyPack)
            {
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, "NakedGizo_EN", 1, "Blemmigan_EN");
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 2, "Chapman_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, "Rabies_EN");
                cellularSpoggleMedium.SimpleAddEncounter(1, Spoggle.YellowBlueSplit, 1, Spoggle.Yellow, 1, Bots.Blue);
            }
            cellularSpoggleMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Spoggle.YellowBlueSplit.Med, 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium);
        }
    }
}

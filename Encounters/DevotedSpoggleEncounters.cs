using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class DevotedSpoggleEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("DevotedSpoggle_Sign", ResourceLoader.LoadSprite("DevotedSpoggleTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API devotedSpoggleMedium = new EnemyEncounter_API(0, Orph.H.Spoggle.PurpleRedSplit.Med, "DevotedSpoggle_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/WhimperAndWhine",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.Spoggle.Purple.Med)._roarReference.roarEvent,
            };
            devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "Acolyte_EN");
            devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Spoggle.Blue);
            devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 2, "MusicMan_EN");
            devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "Scrungie_EN");
            devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 2, Enemies.Suckle);
            devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "Acolyte_EN", 1, "UnboundAnomaly_EN");
            devotedSpoggleMedium.SimpleAddEncounter(2, Spoggle.PurpleRedSplit, 2, "ManicMan_EN");
            devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 3, "ManicMan_EN");
            if (AApocrypha.CrossMod.pigmentRainbow)
            {
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 2, "ManicMan_EN", 1, Jumble.Rainbow);
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "Scrungie_EN", 1, Jumble.Rainbow);
            }
            if (AApocrypha.CrossMod.Colophons)
            {
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Colophon.Yellow);
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Colophon.Yellow, 1, "Blemmigan_EN");
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Colophon.Purple);
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Colophon.PurpleRedSplit);
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Colophon.Green, 1, "MusicMan_EN", 1, "SingingStone_EN");
                }
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 2, "Frostbite_EN");
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "MusicMan_EN", 1, "BackupDancer_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "Pilgrim_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "Rabies_EN", 1, "Blemmigan_EN");
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Spoggle.Blue, 1, Bots.Red);
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, Spoggle.BlueYellowSplit, 1, Bots.Red);
                devotedSpoggleMedium.SimpleAddEncounter(1, Spoggle.PurpleRedSplit, 1, "MusicMan_EN", 1, "Something_EN");
            }
            devotedSpoggleMedium.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Orph.H.Spoggle.RedPurpleSplit.Med, 12, ZoneType_GameIDs.Orpheum_Hard, BundleDifficulty.Medium); //default: 12 - yes, id mismatch is intentional - making sure it works right
        }
    }
}
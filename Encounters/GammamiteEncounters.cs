using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class GammamiteEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Gammamite_Sign", ResourceLoader.LoadSprite("RadtickTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);

            EnemyEncounter_API gammamiteHard = new EnemyEncounter_API(0, Shore.H.Gammamite.Hard, "Gammamite_Sign")
            {
                MusicEvent = "event:/AAMusic/MillieAmp/Raytrot",
                RoarEvent = "event:/AAEnemy/RadtickRoar",
            };
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "MudLung_EN", 1, "Mung_EN");
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "MunglingMudLung_EN");
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, Jumble.Yellow, 1, "MudLung_EN");
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, Jumble.Yellow, 1, "TearDrinker_EN");
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, Jumble.Yellow);
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 2, "Keko_EN");
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, Spoggle.Blue);
            gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "MudLung_EN", 1, "FlaMinGoa_EN");
            if (AApocrypha.CrossMod.Colophons)
            {
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, Colophon.Blue, 1, "TearDrinker_EN");
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, Colophon.BlueRedSplit, 1, Spoggle.Yellow);
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "SandSifter_EN", 1, Spoggle.Green);
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, Jumble.Red, 1, "Follower_EN");
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "Goomba_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "MudLung_EN", 1, "DryBait_EN");
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "Draugr_EN");
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "MudLung_EN", 1, "Keklung_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies && AApocrypha.CrossMod.GlitchsFreaks)
            {
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "NobodyGrave_EN", 1, "DryBait_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "MudLung_EN", 1, "Wall_EN");
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 2, "Waltz_EN");
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "MudLung_EN", 1, "Madman_EN");
                gammamiteHard.SimpleAddEncounter(1, "Gammamite_EN", 1, "TearDrinker_EN", 1, "Mung_EN", 1, "Madman_EN");
            }
            gammamiteHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Gammamite.Hard, 10, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard); //default: 10
        }
    }
}

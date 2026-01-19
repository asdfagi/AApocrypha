using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SmoldergeistEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Smoldergeist_Sign", ResourceLoader.LoadSprite("SmoldergeistTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API smoldergeistHard = new EnemyEncounter_API(0, Shore.H.Smoldergeist.Hard, "Smoldergeist_Sign")
            {
                MusicEvent = "event:/AAMusic/EXCELSIOR/Sodom&Gomorrah",
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.Spoggle.Red.Med)._roarReference.roarEvent,
            };
            smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, "MudLung_EN", 2, "Mung_EN");
            smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 2, "Keko_EN");
            smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, Enemies.Mungling, 1, Spoggle.Yellow);
            smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, Enemies.Mungling, 1, Spoggle.Blue);
            smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, "MudLung_EN", 1, "FlaMinGoa_EN");
            if (AApocrypha.CrossMod.Mythos)
            {
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, "MudLung_EN", 1, "Madman_EN");
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, Enemies.Mungling, 1, "RatThing_EN");
            }
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, "MudLung_EN", 1, "Keklung_EN");
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 2, "Mung_EN", 1, "VanishingHands_EN");
            }
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, "Flakkid_EN", 1, "DryBait_EN");
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, "Flakkid_EN", 1, Spoggle.Blue);
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 2, "Enno_EN");
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, "MudLung_EN", 1, "Snaurce_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                smoldergeistHard.SimpleAddEncounter(1, "Smoldergeist_EN", 1, Enemies.Unmung);
            }
            smoldergeistHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.Smoldergeist.Hard, 9, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Hard);
        }
    }
}

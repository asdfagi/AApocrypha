using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SandSifterEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("SandSifter_Sign", ResourceLoader.LoadSprite("SandSifterTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API sandSifterEasy = new EnemyEncounter_API(0, Shore.H.SandSifter.Easy, "SandSifter_Sign")
            {
                MusicEvent = "event:/AAMusic/Aprils/SieveOurSouls",
                RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
            sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN");
            sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Mung_EN");
            sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "MudLung_EN", 1, "Mung_EN");
            sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Keko_EN");
            sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 2, "Keko_EN");
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Draugr_EN");
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Keklung_EN", 1, "Mung_EN");
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Minana_EN", 1, "Mung_EN");
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "LittleBeak_EN", 1, "Mung_EN");
            }
            if (AApocrypha.CrossMod.StewSpecimens)
            {
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Scylla_EN", 1, "Mung_EN");
            }
            if (AApocrypha.CrossMod.Mythos)
            {
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "RatThing_EN", 1, "Mung_EN");
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Madman_EN");
            }
            if (AApocrypha.CrossMod.MarmoEnemies)
            {
                sandSifterEasy.SimpleAddEncounter(1, "SandSifter_EN", 1, "Surimi_EN");
            }
            sandSifterEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Shore.H.SandSifter.Easy, 10, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy); //10
        }
    }
}

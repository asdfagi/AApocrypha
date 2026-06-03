using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class TuringTarpitEncounter
    {
        public static void Add()
        {
            if (!AApocrypha.CrossMod.pigmentIridescent || !AApocrypha.CrossMod.pigmentEntropic || !AApocrypha.CrossMod.pigmentClusterfuck || !AApocrypha.CrossMod.pigmentWhite) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Malfunction_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Atrophy_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Collapse_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Enamored_ID")) { return; }
            if (!LoadedDBsHandler.StatusFieldDB._StatusEffects.ContainsKey("Disjunct_ID")) { return; }

            if (Abyss.Exists)
            {
                Portals.AddPortalSign("Turing_Sign", ResourceLoader.LoadSprite("TuringTarpitTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API testMedium = new EnemyEncounter_API((EncounterType)1, "H_ZoneAbyss_TuringTarpit_Hard_EnemyBundle", "Turing_Sign")
                {
                    MusicEvent = "event:/AAMusic/LookOutside/Dissociation",
                    RoarEvent = "event:/AAEnemy/BFElemental/BFElementalRoar",
                };
                testMedium.CreateNewEnemyEncounterData(
                [
                    "TuringTarpit_EN",
                ], [1]);
                testMedium.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneAbyss_TuringTarpit_Hard_EnemyBundle", 4, "TheAbyss_Zone3", BundleDifficulty.Hard);
            }
        }
    }
}

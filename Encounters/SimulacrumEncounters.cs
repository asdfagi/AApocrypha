using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SimulacrumEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Simulacrum_Sign", ResourceLoader.LoadSprite("SimulacrumTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API simulacrumHard = new EnemyEncounter_API(0, Garden.H.Simulacrum.Hard, "Simulacrum_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/Homunculus",
                RoarEvent = "event:/AAEnemy/SimulacrumRoar",
            };
            simulacrumHard.SimpleAddEncounter(1, "Simulacrum_EN");
            simulacrumHard.SimpleAddEncounter(1, "Simulacrum_EN", 2, "NextOfKin_EN");
            simulacrumHard.SimpleAddEncounter(1, "Simulacrum_EN", 1, "InHisImage_EN");
            simulacrumHard.SimpleAddEncounter(1, "Simulacrum_EN", 1, "InHerImage_EN");
            simulacrumHard.SimpleAddEncounter(1, "Simulacrum_EN", 2, "MachineGnomes_EN");
            simulacrumHard.SimpleAddEncounter(1, "Simulacrum_EN", 1, Enemies.Shivering);
            simulacrumHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector(Garden.H.Simulacrum.Hard, 7, ZoneType_GameIDs.Garden_Hard, BundleDifficulty.Hard);
            
            if (AApocrypha.CrossMod.IntoTheAbyss && Abyss.Exists) { AbyssAdd(); }
        }

        public static void AbyssAdd()
        {
            EnemyEncounter_API simulacrumAbyssHard = new EnemyEncounter_API(0, Abyss.H.Simulacrum.Hard, "Simulacrum_Sign")
            {
                MusicEvent = "event:/AAMusic/Everhood/Homunculus",
                RoarEvent = "event:/AAEnemy/SimulacrumRoar",
            };
            simulacrumAbyssHard.SimpleAddEncounter(1, "Simulacrum_EN");
            simulacrumAbyssHard.SimpleAddEncounter(1, "Simulacrum_EN", 1, "Mistaken_EN", 1, "Mistake_EN");
            simulacrumAbyssHard.SimpleAddEncounter(1, "Simulacrum_EN", 1, "Mistaken_EN", 1, "YesMan_EN");
            simulacrumAbyssHard.SimpleAddEncounter(1, "Simulacrum_EN", 1, Symbols.Purple, 1, "YesMan_EN");
            simulacrumAbyssHard.SimpleAddEncounter(1, "Simulacrum_EN", 2, "MachineGnomes_EN");
            simulacrumAbyssHard.SimpleAddEncounter(1, "Simulacrum_EN", 1, "Faceless_EN");
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                simulacrumAbyssHard.SimpleAddEncounter(1, "Simulacrum_EN", 2, "EyePalm_EN");
            }
            simulacrumAbyssHard.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Simulacrum.Hard, 7, "TheAbyss_Zone3", BundleDifficulty.Hard);
        }
    }
}

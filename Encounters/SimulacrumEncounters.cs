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
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class SimulacrumEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Simulacrum_Sign", ResourceLoader.LoadSprite("SimulacrumOverworld", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API simulacrumMedium = new EnemyEncounter_API(0, "H_Zone03_Simulacrum_Medium_EnemyBundle", "Simulacrum_Sign")
            {
                MusicEvent = "event:/AAMusic/Homunculus",
                RoarEvent = "event:/AAEnemy/SandSifterRoar",
            };
        }
    }
}

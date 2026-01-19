using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class MaceratorEncounters
    {
        public static void Add()
        {
            Portals.AddPortalSign("Macerator_Sign", ResourceLoader.LoadSprite("MaceratorTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
            EnemyEncounter_API maceratorEasy = new EnemyEncounter_API(0, "H_Zone01_Macerator_Easy_EnemyBundle", "Macerator_Sign")
            {
                MusicEvent = "event:/Music/Mx_Mung",
                RoarEvent = "event:/Characters/Enemies/DLC_01/Keko/CHR_ENM_Keko_Roar",
            };
            maceratorEasy.SimpleAddEncounter(1, "Macerator_EN");
            maceratorEasy.SimpleAddEncounter(1, "Macerator_EN", 1, "Mung_EN");
            maceratorEasy.SimpleAddEncounter(1, "Macerator_EN", 1, "MudLung_EN", 1, "Mung_EN");
            maceratorEasy.SimpleAddEncounter(1, "Macerator_EN", 1, "Keko_EN");
            maceratorEasy.SimpleAddEncounter(2, "Macerator_EN", 1, "Mung_EN");
            maceratorEasy.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToZoneSelector("H_Zone01_Macerator_Easy_EnemyBundle", 1, ZoneType_GameIDs.FarShore_Hard, BundleDifficulty.Easy);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class ClusterfuckOrguisEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Portals.AddPortalSign("OrguisClusterfuckSign", ResourceLoader.LoadSprite("OrguisTimelineClusterfuck", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API clusterfuckOrguisMed = new EnemyEncounter_API(0, Abyss.H.Orguis.Clusterfuck.Med, "OrguisClusterfuckSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                clusterfuckOrguisMed.SimpleAddEncounter(1, Orguis.Clusterfuck, 1, "YesMan_EN", 1, "Streetlight_EN");
                clusterfuckOrguisMed.SimpleAddEncounter(1, Orguis.Clusterfuck, 1, "WanderFellow_EN");
                clusterfuckOrguisMed.SimpleAddEncounter(1, Orguis.Clusterfuck, 1, "Bear_EN", 1, "Streetlight_EN");
                clusterfuckOrguisMed.SimpleAddEncounter(1, Orguis.Clusterfuck, 1, "MachineGnomes_EN", 1, "Streetlight_EN");
                clusterfuckOrguisMed.SimpleAddEncounter(1, Orguis.Clusterfuck, 2, "WRK_EN");
                if (AApocrypha.CrossMod.SaltEnemies)
                {
                    clusterfuckOrguisMed.SimpleAddEncounter(1, Orguis.Clusterfuck, 2, "EyePalm_EN");
                }
                clusterfuckOrguisMed.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.Clusterfuck.Med, 6, "TheAbyss_Zone3", BundleDifficulty.Medium);

                EnemyEncounter_API clusterfuckOrguisHard = new EnemyEncounter_API(0, Abyss.H.Orguis.Clusterfuck.Hard, "OrguisClusterfuckSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                clusterfuckOrguisHard.SimpleAddEncounter(1, Orguis.Clusterfuck, 2, "Bear_EN", 1, "WRK_EN");
                clusterfuckOrguisHard.SimpleAddEncounter(1, Orguis.Clusterfuck, 1, "WRK_EN", 1, "WanderFellow_EN");
                clusterfuckOrguisHard.SimpleAddEncounter(1, Orguis.Clusterfuck, 1, "YesMan_EN", 1, "BasicElemental_EN");
                clusterfuckOrguisHard.SimpleAddEncounter(1, Orguis.Clusterfuck, 1, "Bear_EN", 1, "Faceless_EN");
                clusterfuckOrguisHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.Clusterfuck.Hard, 8, "TheAbyss_Zone3", BundleDifficulty.Hard);
            }
        }
    }
}

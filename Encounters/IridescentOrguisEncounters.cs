using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class IridescentOrguisEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Portals.AddPortalSign("OrguisIridescentSign", ResourceLoader.LoadSprite("OrguisTimelineIridescent", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API iridOrguisMed = new EnemyEncounter_API(0, Abyss.H.Orguis.Iridescent.Med, "OrguisIridescentSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                iridOrguisMed.SimpleAddEncounter(1, Orguis.Iridescent, 1, "YesMan_EN", 1, "Streetlight_EN");
                iridOrguisMed.SimpleAddEncounter(1, Orguis.Iridescent, 1, "YesMan_EN", 1, "WanderFellow_EN");
                iridOrguisMed.SimpleAddEncounter(1, Orguis.Iridescent, 1, "Bear_EN", 1, "Streetlight_EN");
                iridOrguisMed.SimpleAddEncounter(1, Orguis.Iridescent, 1, "MachineGnomes_EN", 1, "Streetlight_EN");
                iridOrguisMed.SimpleAddEncounter(1, Orguis.Iridescent, 2, "WRK_EN");
                if (AApocrypha.CrossMod.SaltEnemies)
                {
                    iridOrguisMed.SimpleAddEncounter(1, Orguis.Iridescent, 2, "EyePalm_EN");
                }
                iridOrguisMed.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.Iridescent.Med, 6, "TheAbyss_Zone3", BundleDifficulty.Medium);

                EnemyEncounter_API iridOrguisHard = new EnemyEncounter_API(0, Abyss.H.Orguis.Iridescent.Hard, "OrguisIridescentSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                iridOrguisHard.SimpleAddEncounter(1, Orguis.Iridescent, 2, "Bear_EN", 1, "WRK_EN");
                iridOrguisHard.SimpleAddEncounter(1, Orguis.Iridescent, 1, "BasicElemental_EN", 1, "WanderFellow_EN");
                iridOrguisHard.SimpleAddEncounter(1, Orguis.Iridescent, 1, "YesMan_EN", 1, "WRK_EN");
                iridOrguisHard.SimpleAddEncounter(1, Orguis.Iridescent, 1, "Bear_EN", 1, "Faceless_EN");
                iridOrguisHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.Iridescent.Hard, 8, "TheAbyss_Zone3", BundleDifficulty.Hard);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class EntropicOrguisEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Portals.AddPortalSign("OrguisEntropicSign", ResourceLoader.LoadSprite("OrguisTimelineEntropic", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API eOrguisMed = new EnemyEncounter_API(0, Abyss.H.Orguis.Entropic.Med, "OrguisEntropicSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                eOrguisMed.SimpleAddEncounter(1, Orguis.Entropic, 1, "YesMan_EN", 1, "Streetlight_EN");
                //eOrguisMed.SimpleAddEncounter(1, Orguis.Entropic, 1, "YesMan_EN", 1, "WanderFellow_EN");
                eOrguisMed.SimpleAddEncounter(1, Orguis.Entropic, 1, "Bear_EN", 1, "Streetlight_EN");
                eOrguisMed.SimpleAddEncounter(1, Orguis.Entropic, 1, "MachineGnomes_EN", 1, "Streetlight_EN");
                //eOrguisMed.SimpleAddEncounter(1, Orguis.Entropic, 2, "WRK_EN");
                eOrguisMed.SimpleAddEncounter(1, Orguis.Entropic, 1, "YesMan_EN", 1, "BasicElemental_EN");
                if (AApocrypha.CrossMod.SaltEnemies)
                {
                    eOrguisMed.SimpleAddEncounter(1, Orguis.Entropic, 2, "EyePalm_EN");
                }
                eOrguisMed.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.Entropic.Med, 6, "TheAbyss_Zone3", BundleDifficulty.Medium);

                EnemyEncounter_API eOrguisHard = new EnemyEncounter_API(0, Abyss.H.Orguis.Entropic.Hard, "OrguisEntropicSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                eOrguisHard.SimpleAddEncounter(1, Orguis.Entropic, 2, "Bear_EN", 1, "WRK_EN");
                //eOrguisHard.SimpleAddEncounter(1, Orguis.Entropic, 1, "MachineGnomes_EN", 1, "WanderFellow_EN");
                eOrguisHard.SimpleAddEncounter(1, Orguis.Entropic, 1, "YesMan_EN", 1, "WRK_EN");
                //eOrguisHard.SimpleAddEncounter(1, Orguis.Entropic, 1, "Bear_EN", 1, "Faceless_EN");
                eOrguisHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.Entropic.Hard, 8, "TheAbyss_Zone3", BundleDifficulty.Hard);
            } 
        }
    }
}

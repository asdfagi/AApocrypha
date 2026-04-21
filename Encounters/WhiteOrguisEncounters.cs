using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class WhiteOrguisEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Portals.AddPortalSign("OrguisWhiteSign", ResourceLoader.LoadSprite("OrguisTimelineBase", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API wOrguisMed = new EnemyEncounter_API(0, Abyss.H.Orguis.White.Med, "OrguisWhiteSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                wOrguisMed.SimpleAddEncounter(1, Orguis.White, 1, "YesMan_EN", 1, "Streetlight_EN");
                wOrguisMed.SimpleAddEncounter(1, Orguis.White, 1, "YesMan_EN", 1, "WanderFellow_EN");
                wOrguisMed.SimpleAddEncounter(1, Orguis.White, 1, "Bear_EN", 1, "Streetlight_EN");
                wOrguisMed.SimpleAddEncounter(1, Orguis.White, 1, "MachineGnomes_EN", 1, "Streetlight_EN");
                wOrguisMed.SimpleAddEncounter(1, Orguis.White, 2, "WRK_EN");
                wOrguisMed.SimpleAddEncounter(1, Orguis.White, 1, "YesMan_EN", 1, "BasicElemental_EN");
                if (AApocrypha.CrossMod.SaltEnemies)
                {
                    wOrguisMed.SimpleAddEncounter(1, Orguis.White, 2, "EyePalm_EN");
                }
                wOrguisMed.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.White.Med, 6, "TheAbyss_Zone3", BundleDifficulty.Medium);

                EnemyEncounter_API wOrguisHard = new EnemyEncounter_API(0, Abyss.H.Orguis.White.Hard, "OrguisWhiteSign")
                {
                    MusicEvent = "event:/AAMusic/MillieAmp/DurianDetonator",
                    RoarEvent = "event:/AAEnemy/LogosDisco/LogosDiscoRoar",
                };
                wOrguisHard.SimpleAddEncounter(1, Orguis.White, 2, "Bear_EN", 1, "WRK_EN");
                wOrguisHard.SimpleAddEncounter(1, Orguis.White, 1, "MachineGnomes_EN", 1, "WanderFellow_EN");
                wOrguisHard.SimpleAddEncounter(1, Orguis.White, 1, "YesMan_EN", 1, "WRK_EN");
                wOrguisHard.SimpleAddEncounter(1, Orguis.White, 1, "Bear_EN", 1, "Faceless_EN");
                wOrguisHard.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.Orguis.White.Hard, 8, "TheAbyss_Zone3", BundleDifficulty.Hard);
            } 
        }
    }
}

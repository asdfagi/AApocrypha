using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class BasicElementalEncounters
    {
        public static void Add()
        {
            if (Abyss.Exists)
            {
                Portals.AddPortalSign("BasicElementalSign", ResourceLoader.LoadSprite("BasicElementalTimeline", new Vector2(0.5f, 0f), 32), Portals.EnemyIDColor);
                EnemyEncounter_API basicEasy = new EnemyEncounter_API(0, Abyss.H.BasicElemental.Easy, "BasicElementalSign")
                {
                    MusicEvent = "event:/AAMusic/Loathing/BumpInTheNight",
                    RoarEvent = "event:/AASFX/Nothing_SFX",
                };
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    if (LoadedAssetsHandler.GetEnemyBundle(Orph.H.Omission.Med) != null)
                    {
                        basicEasy.RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.H.Omission.Med)._roarReference.roarEvent;
                    }
                }
                basicEasy.SimpleAddEncounter(1, "BasicElemental_EN", 1, "WanderFellow_EN");
                basicEasy.SimpleAddEncounter(1, "BasicElemental_EN", 1, "YesMan_EN");
                basicEasy.SimpleAddEncounter(1, "BasicElemental_EN", 1, "MachineGnomes_EN");
                basicEasy.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.BasicElemental.Easy, 8, "TheAbyss_Zone3", BundleDifficulty.Easy);

                EnemyEncounter_API basicMed = new EnemyEncounter_API(0, Abyss.H.BasicElemental.Med, "BasicElementalSign")
                {
                    MusicEvent = "event:/AAMusic/Loathing/BumpInTheNight",
                    RoarEvent = "event:/AASFX/Nothing_SFX",
                };
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    if (LoadedAssetsHandler.GetEnemyBundle(Orph.H.Omission.Med) != null)
                    {
                        basicMed.RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Orph.H.Omission.Med)._roarReference.roarEvent;
                    }
                }
                basicMed.SimpleAddEncounter(2, "BasicElemental_EN", 1, "WanderFellow_EN");
                basicMed.SimpleAddEncounter(1, "BasicElemental_EN", 1, "YesMan_EN", 1, "WRK_EN");
                basicMed.SimpleAddEncounter(1, "BasicElemental_EN", 2, "MachineGnomes_EN");
                basicMed.SimpleAddEncounter(2, "BasicElemental_EN", 1, "YesMan_EN");
                basicMed.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector(Abyss.H.BasicElemental.Med, 8, "TheAbyss_Zone3", BundleDifficulty.Medium);
            } 
        }
    }
}

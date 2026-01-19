using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatSirenEncounters
    {
        public static void Add()
        {
            Debug.Log("AA Compat Encounters | Siren Compat Loaded");
            List<RandomEnemyGroup> piscinaHard = ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("PiscinaHard"))._enemyBundles;
            EnemyEncounter_API piscinaEvil = new EnemyEncounter_API(0, "H_ZoneSiren_PiscinaEvil_Hard_EnemyBundle", LoadedAssetsHandler.GetEnemyBundle("PiscinaHard").m_BundleSignID)
            {
                MusicEvent = LoadedAssetsHandler.GetEnemyBundle("PiscinaHard")._musicEventReference,
                RoarEvent = LoadedAssetsHandler.GetEnemyBundle("PiscinaHard")._roarReference.roarEvent,
            };
            piscinaEvil.CreateNewEnemyEncounterData(
                [
                    "LivingPiscina_EN",
                    "BirdBath_EN",
                    "WinterLantern_EN",
                ], null);
            piscinaEvil.AddEncounterToDataBases();
            EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneSiren_PiscinaEvil_Hard_EnemyBundle", 1, "TheSiren_Zone1", BundleDifficulty.Hard);
        /*}
        public static void Post()
        {*/
            List<RandomEnemyGroup> boilerEasy = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("BoilerEasy"))._enemyBundles)
            {
                new([
                   "Boiler_EN",
                   "Boiler_EN",
                   "SandSifter_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("BoilerEasy"))._enemyBundles = boilerEasy;
            List<RandomEnemyGroup> boilerMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("BoilerMed"))._enemyBundles)
            {
                new([
                   "Boiler_EN",
                   "Boiler_EN",
                   "HazardHauler_Siren_EN",
                ]),
                new([
                   "Boiler_EN",
                   "Boiler_EN",
                   "BirdBath_EN",
                   "WinterLantern_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("BoilerMed"))._enemyBundles = boilerMedium;
            List<RandomEnemyGroup> olmicMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("OlmicMed"))._enemyBundles)
            {
                new([
                   "Olmic_EN",
                   "Boiler_EN",
                   "WinterLantern_EN",
                ]),
                new([
                   "Olmic_EN",
                   "Boiler_EN",
                   "BirdBath_EN",
                   "HazardHauler_Siren_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("OlmicMed"))._enemyBundles = olmicMedium;
            /*if (AApocrypha.CrossMod.SaltEnemies)
            {
                List<RandomEnemyGroup> wolvesMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_WolfColony_Medium_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "WolfColony_EN",
                       "WolfColony_EN",
                       "HazardHauler_Siren_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_WolfColony_Medium_EnemyBundle"))._enemyBundles = wolvesMedium;
            }*/
            /*if (AApocrypha.CrossMod.StewSpecimens)
            {
                List<RandomEnemyGroup> pilgrimHard = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_Pilgrim_Hard_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "Pilgrim_EN",
                       "Boiler_EN",
                       "HazardHauler_Siren_EN",
                    ]),
                    new([
                       "Pilgrim_EN",
                       "BirdBath_EN",
                       "BirdBath_EN",
                       "WinterLantern_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_Pilgrim_Hard_EnemyBundle"))._enemyBundles = pilgrimHard;
            }*/
            if (AApocrypha.CrossMod.HellIslandFell)
            {
                List<RandomEnemyGroup> oneShooterMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_OneShooter_Medium_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "OneShooter_EN",
                       "Boiler_EN",
                       "HazardHauler_Siren_EN",
                    ]),
                    new([
                       "OneShooter_EN",
                       "BirdBath_EN",
                       "BirdBath_EN",
                       "HazardHauler_Siren_EN",
                    ]),
                    new([
                       "OneShooter_EN",
                       "Boiler_EN",
                       "BirdBath_EN",
                       "WinterLantern_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_OneShooter_Medium_EnemyBundle"))._enemyBundles = oneShooterMedium;
            }
            if (AApocrypha.CrossMod.IntoTheAbyss)
            {
                if (AApocrypha.CrossMod.SaltEnemies)
                {
                    List<RandomEnemyGroup> soothsayerMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_Soothsayer_Medium_EnemyBundle"))._enemyBundles)
                        {
                            new([
                                "Soothsayer_EN",
                                "Boiler_EN",
                                "BirdBath_EN",
                                "HazardHauler_Siren_EN",
                            ]),
                        };
                    ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_ZoneSiren_Soothsayer_Medium_EnemyBundle"))._enemyBundles = soothsayerMedium;
                }
            }
        }
    }
}

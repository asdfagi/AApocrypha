using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatSirenEncounters
    {
        public static void Add()
        {
            if (Siren.Exists)
            {
                Debug.Log("AA Compat Encounters | Siren Compat Loaded");
                List<RandomEnemyGroup> piscinaHard = ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("PiscinaHard"))._enemyBundles;
                EnemyEncounter_API piscinaEvil = new EnemyEncounter_API(0, "H_ZoneSiren_PiscinaEvil_Hard_EnemyBundle", LoadedAssetsHandler.GetEnemyBundle("PiscinaHard").m_BundleSignID)
                {
                    MusicEvent = LoadedAssetsHandler.GetEnemyBundle(Siren.H.Piscina.Hard)._musicEventReference,
                    RoarEvent = LoadedAssetsHandler.GetEnemyBundle(Siren.H.Piscina.Hard)._roarReference.roarEvent,
                };
                piscinaEvil.SimpleAddEncounter(1, "LivingPiscina_EN", 1, "BirdBath_EN", 1, "WinterLantern_EN");
                piscinaEvil.AddEncounterToDataBases();
                EnemyEncounterUtils.AddEncounterToCustomZoneSelector("H_ZoneSiren_PiscinaEvil_Hard_EnemyBundle", 1, "TheSiren_Zone1", BundleDifficulty.Hard);
                /*}
                public static void Post()
                {*/
                AddTo sirenAdd = new AddTo(Siren.H.Boiler.Easy);
                sirenAdd.SimpleAddGroup(2, "Boiler_EN", 1, "SandSifter_EN");

                sirenAdd = new AddTo(Siren.H.Boiler.Med);
                sirenAdd.SimpleAddGroup(2, "Boiler_EN", 1, "HazardHauler_Siren_EN");
                sirenAdd.SimpleAddGroup(2, "Boiler_EN", 1, "BirdBath_EN", 1, "WinterLantern_EN");

                sirenAdd = new AddTo(Siren.H.Olmic.Med);
                sirenAdd.SimpleAddGroup(1, "Olmic_EN", 1, "Boiler_EN", 1, "WinterLantern_EN");
                sirenAdd.SimpleAddGroup(1, "Boiler_EN", 1, "Boiler_EN", 1, "BirdBath_EN", 1, "HazardHauler_Siren_EN");
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
                    sirenAdd = new AddTo(Siren.H.OneShooter.Med);
                    sirenAdd.SimpleAddGroup(1, "OneShooter_EN", 1, "Boiler_EN", 1, "HazardHauler_Siren_EN");
                    sirenAdd.SimpleAddGroup(1, "OneShooter_EN", 2, "BirdBath_EN", 1, "HazardHauler_Siren_EN");
                    sirenAdd.SimpleAddGroup(1, "OneShooter_EN", 1, "Boiler_EN", 1, "BirdBath_EN", 1, "WinterLantern_EN");
                }
                if (AApocrypha.CrossMod.IntoTheAbyss)
                {
                    if (AApocrypha.CrossMod.SaltEnemies)
                    {
                        sirenAdd = new AddTo(Siren.H.Soothsayer.Med);
                        sirenAdd.SimpleAddGroup(1, "Soothsayer_EN", 1, "Boiler_EN", 1, "BirdBath_EN", 1, "HazardHauler_Siren_EN");
                    }
                }
            }
        }
    }
}

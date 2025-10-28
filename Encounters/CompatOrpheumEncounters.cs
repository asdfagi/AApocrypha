using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Encounters
{
    public class CompatOrpheumEncounters
    {
        public static void Add()
        {
            List<RandomEnemyGroup> musicmanEasy = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_MusicMan_Easy_EnemyBundle"))._enemyBundles)
            {
                new([
                   "MusicMan_EN",
                   "Acolyte_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_MusicMan_Easy_EnemyBundle"))._enemyBundles = musicmanEasy;
            List<RandomEnemyGroup> scrungieMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Scrungie_Medium_EnemyBundle"))._enemyBundles)
            {
                new([
                   "Scrungie_EN",
                   "Scrungie_EN",
                   "CoruscatingJumbleGuts_EN",
                   "JumbleGuts_Clotted_EN",
                ]),
            };
            ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Scrungie_Medium_EnemyBundle"))._enemyBundles = scrungieMedium;
            if (AApocrypha.CrossMod.GlitchsFreaks)
            {
                List<RandomEnemyGroup> frostbiteMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("FrostbiteMed"))._enemyBundles)
                {
                    new([
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "SculptorBirdSculpture_EN",
                    ]),
                    new([
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "Frostbite_EN",
                       "BloatfingerHiddenOrpheum_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("FrostbiteMed"))._enemyBundles = frostbiteMedium;
            }
            if (AApocrypha.CrossMod.SaltEnemies)
            {
                List<RandomEnemyGroup> mawMedium = new List<RandomEnemyGroup>(((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Maw_Medium_EnemyBundle"))._enemyBundles)
                {
                    new([
                       "Maw_EN",
                       "Acolyte_EN",
                       "Acolyte_EN",
                    ]),
                    new([
                       "Maw_EN",
                       "DevotedSpoggle_EN",
                       "MusicMan_EN",
                    ]),
                    new([
                       "Maw_EN",
                       "CellularSpoggle_EN",
                       "MusicMan_EN",
                    ]),
                };
                ((RandomEnemyBundleSO)LoadedAssetsHandler.GetEnemyBundle("H_Zone02_Maw_Medium_EnemyBundle"))._enemyBundles = mawMedium;
            }
        }
    }
}

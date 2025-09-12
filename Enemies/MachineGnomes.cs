using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class MachineGnomes
    {
        public static void Add()
        {
            Enemy gnomes = new Enemy("Machine Gnomes", "MachineGnomes_EN")
            {
                Health = 50,
                HealthColor = Pigments.Grey,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("SimulacrumTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AnomalyDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("SimulacrumTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Anomaly1Hurt",
                DeathSound = "event:/AAEnemy/Anomaly1Death",
            };
            gnomes.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Simulacrum_Enemy/Simulacrum_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Simulacrum_Enemy/Simulacrum_Giblets.prefab").GetComponent<ParticleSystem>());

            gnomes.AddPassives([]);

            gnomes.AddEnemy(false, false, false);
        }
    }
}

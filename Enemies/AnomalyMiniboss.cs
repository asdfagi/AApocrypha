using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Enemies
{
    public class AnomalyMiniboss
    {
        public static Enemy aanomaly_miniboss;
        public static void Add()
        {
            Enemy abandonedaltar = new Enemy("Abandoned Altar", "AbandonedAltar_EN")
            {
                Health = 30,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AbandonedAltarTimeline", new Vector2(0.5f, 0f), 32),
                OverworldDeadSprite = ResourceLoader.LoadSprite("AbandonedAltarDead", new Vector2(0.5f, 0f), 32),
                OverworldAliveSprite = ResourceLoader.LoadSprite("AbandonedAltarTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").damageSound,
                DeathSound = LoadedAssetsHandler.GetCharacter("Gospel_CH").deathSound,
            };
            abandonedaltar.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/AbandonedAltar_Enemy/AbandonedAltar_Enemy.prefab", AApocrypha.assetBundle, null);
            abandonedaltar.AddPassives([Passives.Inanimate, Passives.Anchored, Passives.GetCustomPassive("DecayAbandonedAltar_PA")]);

            abandonedaltar.AddEnemy(false, false, false);

            Enemy anomalyminiboss = new Enemy("Emissary of ███████", "AAnomalyMiniboss_EN")
            {
                Health = 150,
                HealthColor = Pigments.Purple,
                Size = 1,
                CombatSprite = ResourceLoader.LoadSprite("AnomalyMinibossTimeline", new Vector2(0.5f, 0f), 32),
                DamageSound = "event:/AAEnemy/Anomaly1Hurt",
                DeathSound = "event:/AAEnemy/Anomaly1Death",
                UnitTypes = ["AnomalyID"],
            };
            anomalyminiboss.PrepareEnemyPrefab("Assets/Apocrypha_Enemies/Anomaly_Enemy/Anomaly_Enemy.prefab", AApocrypha.assetBundle, AApocrypha.assetBundle.LoadAsset<GameObject>("Assets/Apocrypha_Enemies/Anomaly_Enemy/Anomaly_Giblets.prefab").GetComponent<ParticleSystem>());
            anomalyminiboss.AddPassives([]);

            anomalyminiboss.AddEnemy(true, false, false);

            aanomaly_miniboss = anomalyminiboss;
        }
    }
}

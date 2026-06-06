using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterStoredValueChangeWithMaxEncounterBonusEffect : CasterStoredValueChangeWithMaxEffect
    {
        public int _specialEnemyBonus = 2; // minibosses and superbosses
        public int _area1BossBonus = 2; // far shore
        public int _area2BossBonus = 3; // orpheum, siren
        public int _area3BossBonus = 5; // garden, abyss
        public int _specialBossBonus = 10; // specific bosses that I think deserve an excessive amount of tablets
        public List<string> _specialEnemyIDs = [
            // Vanilla
            "TaMaGoa_EN", "Xiphactinus_EN",
            // AApocrypha
            "RiftMiniboss_EN", "TarnishedDivinity_EN", "ThresholdGate_EN", "TuringTarpit_EN",
            // Enemy Pack
            "TheFountainofYouth_EN", "Opisthotonus_EN", "Cherubim_EN", "HeinousHighness_EN", "DyingFlarb_EN",
            // Glitch's Freaks
            "SkullHermit_Hidden_EN", "Sign_PrizedCatch_EN",
            // Hell Island Fell
            "Vus_EN", "Boler_EN", "Boojum_EN", "Nevermore_Small_EN", "Nevermore_Medium_EN", "Nevermore_Huge_EN",
            // Stew's Specimens
            "ForgottenEidolon_EN",
            // Undivine Comedy
            "PurityImage_EN",
            // Ruinful Revelry
            "Sebastian_EN",
            // Mythos Friends
            "YgolonacImage_EN",
            // ITA
            "Estelle_EN", "Home_EN", "Malachai_EN", "SoundStone_EN", "Renee_EN", "Minotaur_EN",
            // Memento Mori
            "LoqCat_EN",
            // Sora's Toybox
            "SuspiciousMung_EN",
            // Salt Enemies
            "33_EN", "EmbersofaDeadGod_EN", "SnakeGod_EN", "Postmodern_EN", "War_EN", "TheDeep_EN",
            // Unearthed Sentimentality
            "BeautifulPerson_EN",
        ];
        public List<string> _specialBossIDs = ["Nolocimes_Batretne_BOSS", "Bronzo1_EN"];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            caster.TryGetStoredData(m_unitStoredDataID, out var holder);
            int mainData = holder.m_MainData;
            if (_usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }
            else if (_randomBetweenPrevious)
            {
                entryVariable = UnityEngine.Random.Range(base.PreviousExitValue, entryVariable + 1);
            }
            else if (_useFixedValue)
            {
                entryVariable = _fixedValue;
            }

            var enemies = CombatManager.Instance._stats.EnemiesOnField;

            int adjustedMaximum = _maximumValue;
            if (stats.BundleDifficulty == BundleDifficulty.Boss)
            {
                int zoneID = stats.InfoHolder._run.CurrentZoneID;
                if (zoneID == 0) { adjustedMaximum += _area1BossBonus; } // far shore
                if (zoneID == 1) { adjustedMaximum += _area2BossBonus; } // orpheum, siren
                if (zoneID == 2) { adjustedMaximum += _area3BossBonus; } // garden, abyss
                foreach (EnemyCombat en in enemies.Values)
                {
                    if (_specialBossIDs.Contains(en.EntityID))
                    {
                        adjustedMaximum += _specialBossBonus;
                    }
                }
            }
            else
            {
                foreach (EnemyCombat en in enemies.Values)
                {
                    if (_specialEnemyIDs.Contains(en.EntityID))
                    {
                        adjustedMaximum += _specialEnemyBonus;
                    }
                }
            }

            mainData += (_increase ? entryVariable : (-entryVariable));
            mainData = Mathf.Max(_minimumValue, mainData);
            mainData = Mathf.Min(adjustedMaximum, mainData);
            if (_exitValueIsChange)
            {
                exitAmount = Mathf.Abs(holder.m_MainData - mainData);
            }
            else
            {
                exitAmount = mainData;
            }

            holder.m_MainData = mainData;
            return true;
        }
    }
}

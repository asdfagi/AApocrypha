using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomEffects
{
    public class ResetCasterChthonosophyEffect : EffectSO
    {
        public bool _pureBlock = true;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_pureBlock) { stats.TryAddLockedPassive(Passives.Pure.m_PassiveID); }
            if (caster is CharacterCombat casterCH)
            {
                if (caster.ChangeHealthColor(casterCH.Character.healthColor)) { exitAmount++; }
                casterCH._passiveAbilities = casterCH.Character.passiveAbilities;
            } else if (caster is EnemyCombat casterEN)
            {
                if (caster.ChangeHealthColor(casterEN.Enemy.healthColor)) { exitAmount++; }
                casterEN._passiveAbilities = casterEN.Enemy.passiveAbilities;
            }
            if (_pureBlock) { stats.TryRemoveLockedPassive(Passives.Pure.m_PassiveID); }
            return exitAmount > 0;
        }
    }
}

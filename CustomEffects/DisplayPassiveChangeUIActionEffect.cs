using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class DisplayPassiveChangeUIActionEffect : EffectSO
    {
        public string passiveName = "";
        public bool localPassive = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (caster is CharacterCombat casterCH)
            {
                exitAmount = 1;
                if (localPassive)
                {
                    new ShowPassiveInformationUIAction(casterCH.ID, true, passiveName, ResourceLoader.LoadSprite(passiveName)).Execute(stats);
                }
                else
                {
                    new ShowPassiveInformationUIAction(casterCH.ID, true, passiveName, LoadedAssetsHandler.GetPassive(passiveName).passiveIcon).Execute(stats);
                }
            } 
            else if (caster is EnemyCombat casterEN)
            {
                exitAmount = 1;
                if (localPassive)
                {
                    new ShowPassiveInformationUIAction(casterEN.ID, false, passiveName, ResourceLoader.LoadSprite(passiveName)).Execute(stats);
                }
                else
                {
                    new ShowPassiveInformationUIAction(casterEN.ID, false, passiveName, LoadedAssetsHandler.GetPassive(passiveName).passiveIcon).Execute(stats);
                }
            }
            return exitAmount > 0;
        }
    }
}

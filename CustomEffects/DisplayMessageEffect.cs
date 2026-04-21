using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class WasteTimeUIAction : CombatAction
    {//harvested from Salt Enemies
        public int _id;

        public bool _isUnitCharacter;

        public string _attackName;
        //public int _miliseconds;

        public WasteTimeUIAction(int id, bool isUnitCharacter, string attackName/*, int miliseconds*/)
        {
            _id = id;
            _isUnitCharacter = isUnitCharacter;
            _attackName = attackName;
            //_miliseconds = miliseconds;
        }

        public override IEnumerator Execute(CombatStats stats)
        {
            yield return stats.combatUI.ShowAttackInformation(_id, _isUnitCharacter, _attackName, "");
        }


    }
    public class DisplayMessageEffect : EffectSO
    {
        public string _text = "ORTHALTER"; //VOTV reference :3
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            CombatManager.Instance.AddUIAction(new WasteTimeUIAction(caster.ID, caster.IsUnitCharacter, _text));
            return true;
        }
    }
}

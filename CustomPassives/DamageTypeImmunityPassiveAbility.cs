using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Custom_Passives
{
    public class DamageTypeImmunityPassiveAbility : BasePassiveAbilitySO
    {
        public string _damageType;
        public override void TriggerPassive(object sender, object args)
        {
            IPassiveEffector passiveEffector = sender as IPassiveEffector;
            DamageReceivedValueChangeException damage = args as DamageReceivedValueChangeException;
            bool flag = damage.damageTypeID == _damageType;
            if (flag)
            {
                CombatManager.Instance.AddUIAction(new ShowPassiveInformationUIAction(passiveEffector.ID, passiveEffector.IsUnitCharacter, base.GetPassiveLocData().text, this.passiveIcon));
                damage.AddModifier(new MultiplyIntValueModifier(false, 0));
            }
        }
        public override bool IsPassiveImmediate
        {
            get
            {
                return true;
            }
        }
        public override bool DoesPassiveTrigger
        {
            get
            {
                return true;
            }
        }
        public override void OnPassiveConnected(IUnit unit)
        {
        }
        public override void OnPassiveDisconnected(IUnit unit)
        {
        }
    }
}

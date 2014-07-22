using UnityEngine;
using System.Collections;

public class ShieldBasic : DamageReceiver
{
    public float dmgModifier = 0.5f;
    public float healthBoost = 5;

    public override void ApplyDamage(float Damage)
    {
        ParentReceiver.ApplyDamage(Damage * dmgModifier);
    }

}

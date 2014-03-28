using UnityEngine;
using System.Collections;

public class ShieldBasic : BasicShipPart
{
    public float dmgModifier = 0.5f;

    public override void ApplyDamage(float Damage)
    {
        shipCore.ApplyDamage(Damage * dmgModifier);
    }
}

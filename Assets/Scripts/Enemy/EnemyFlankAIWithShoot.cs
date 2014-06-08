using UnityEngine;
using System.Collections;

public class EnemyFlankAIWithShoot : EnemyFlankAI
{
    public bool IsAttacking = true;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        //isAttacking;
        if (IsAttacking)
        {
            moveModule.LookToPoint(LeadCalculator.FirstOrderInterceptPosition(this.gameObject, WeaponScript.projectileSpeed, target));
            WeaponScript.FireWeapon();
        }
    }
}

using UnityEngine;
using System.Collections;

public class EnemyFlankAIWithShoot : EnemyFlankAI
{
    public bool isAttacking = true;
    public bool isLeadingTarget = true;

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        //isAttacking;
        if (isAttacking)
        {
            if (isLeadingTarget)
            {
                moveModule.LookToPoint(LeadCalculator.FirstOrderInterceptPosition(this.gameObject, WeaponScript.projectileSpeed, target));  //Attempt to lead the target
            }
            else
            {
                moveModule.LookToPoint(target.transform.position);  //Attempt to lead the target
            }

            WeaponScript.FireWeapon();
        }
    }
}

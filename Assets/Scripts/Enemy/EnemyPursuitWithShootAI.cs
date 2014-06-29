using UnityEngine;
using System.Collections;

[RequireComponent (typeof(WeaponBasic))]

public class EnemyPursuitWithShootAI : EnemyPursuitAI{

    public bool isAttacking = true;
    public bool isLeadingTarget = true;
	
	// Update is called once per frame
    protected virtual new void Update()
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

    protected void CleanUpProjectiles(){
        foreach (ProjectileBasic proj in WeaponScript.ExistingProjectiles)
        Destroy(proj);
    }

    protected virtual new void DestroySelf()
    {
        //ResetGame();
        CleanUpProjectiles();

        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

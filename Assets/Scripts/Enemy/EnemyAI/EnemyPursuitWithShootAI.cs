using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(WeaponBasic))]

public class EnemyPursuitWithShootAI : EnemyAIBasic{

    public bool isAttacking = true;
    public bool isLeadingTarget = true;
	
	// Update is called once per frame
    protected virtual new void Update()
    {
        if (Target != null) 
            HomeTowardsPoint(Target.transform.position);

        //isAttacking;
        if (isAttacking)
        {
            if (isLeadingTarget)
            {
                MoveModule.LookToPoint(LeadCalculator.FirstOrderInterceptPosition(this.gameObject, Weapon.projectileSpeed, Target));  //Attempt to lead the target
            }
            else
            {
                MoveModule.LookToPoint(Target.transform.position);  //Attempt to lead the target
            }

            Weapon.FireWeapon();
        }
	}

//    protected void CleanUpProjectiles(){
//        foreach (ProjectileBasic proj in Weapon.ExistingProjectiles)
//        Destroy(proj);
//    }
//
//    protected virtual new void DestroySelf()
//    {
//        //ResetGame();
//        CleanUpProjectiles();
//
//        //gameObject.SetActive(false);
//        Destroy(gameObject);
//    }
}

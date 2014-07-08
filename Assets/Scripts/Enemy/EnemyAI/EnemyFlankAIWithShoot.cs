using UnityEngine;
using System.Collections;

public class EnemyFlankAIWithShoot : EnemyFlankAI
{
    public bool isAttacking = true;
    public bool isLeadingTarget = true;

    override protected void Start()
    {
        if (MoveModule == null)
            MoveModule = GetComponent<EnemyMoveModuleBasic>();
        
        if (EnemyUnit == null)
            EnemyUnit = GetComponent<EnemyBasic>(); 
        
        if (MoveModule == null)
            MoveModule = GetComponent<EnemyMoveModuleBasic>();
        
        if (Weapon == null)
            Weapon = GetComponent<WeaponBasic>();

        getToRandomSurroundingPoint();
    }

    // Update is called once per frame
    public override void Update()
    {
        if (MoveModule.hasReachedDestination)
        {
            getToRandomSurroundingPoint();
        }
        
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
            
            if (Weapon != null && isWithinRangeOfTarget)
            {
                Weapon.FireWeapon();
                
            }
        }
    }
}

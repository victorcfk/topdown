using UnityEngine;
using System.Collections;

[RequireComponent (typeof(WeaponBasic))]

public class EnemyPursuitWithShootAI : EnemyPursuitAI{


    public bool IsAttacking = true;
	
    // Use this for initialization
    protected virtual new void Start()
    {
        if(WeaponScript == null)    WeaponScript = this.GetComponent<WeaponBasic>();
	}
	
	// Update is called once per frame
    protected virtual new void Update()
    {
        base.Update();

        //isAttacking;
        if (IsAttacking)
        {
            moveModule.LookToPoint(target.transform.position);
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

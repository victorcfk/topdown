using UnityEngine;
using System.Collections;

[RequireComponent (typeof(WeaponBasic))]

public class EnemyPursuitWithShoot : EnemyPursuit{
    
    public WeaponBasic WeaponScript;
    public bool IsAttacking = true;
	
    // Use this for initialization
	void Start () {

        if(WeaponScript == null)    WeaponScript = this.GetComponent<WeaponBasic>();
	}
	
	// Update is called once per frame
	void Update () {

        base.Update();

        //isAttacking;
        if (IsAttacking)
        {
            moveModule.LookToPoint(target.transform.position);
            WeaponScript.FireWeapon();
        }
	}

    protected void CleanUpProjectiles(){
        foreach (GameObject proj in WeaponScript.ExistingProjectiles)
        Destroy(proj);
    }

    protected virtual void DestroySelf()
    {
        //ResetGame();
        CleanUpProjectiles();

        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
}

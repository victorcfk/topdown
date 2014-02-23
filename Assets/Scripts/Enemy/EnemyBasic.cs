using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(WeaponBasic))]

public class EnemyBasic : MonoBehaviour {

    public float Health =2 ;
    public EnemyMoveModuleBasic mm;
    	
	// Update is called once per frame
	public void Update () {

        if (Health <= 0) DestroySelf();
	}

    virtual protected void OnCollisionEnter(Collision collision)
    {
        //print("colled");

        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
          //  print("colledisbull");
            Health -= collision.gameObject.GetComponent<ProjectileBasic>().damage;

        }*/
    }

    public void ApplyDamage(float Damage)
    {
        print(name+" applied "+Damage+" to itself");
        Health -= Damage;

        if (Health <= 0)    DestroySelf();
    }

    /*
    protected void CleanUpProjectiles(){
        foreach (GameObject proj in ShootScript.ExistingProjectiles)
            Destroy(proj);
    }*/

    protected virtual void DestroySelf()
    {
        //ResetGame();
        //CleanUpProjectiles();

        //gameObject.SetActive(false);

        Destroy(gameObject);
    }

}

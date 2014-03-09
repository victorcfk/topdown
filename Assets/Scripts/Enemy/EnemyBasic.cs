using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(WeaponBasic))]

public class EnemyBasic : MonoBehaviour {

    public float health = 2 ;
    public float shipCollisionDamage = 1;

    public LayerMask LayersThatDestroyThis;

    public EnemyMoveModuleBasic moveModule;
    public WeaponBasic WeaponScript;

    public Vector3 movePos;
    public GameObject target;
	
	// Update is called once per frame
	public virtual void Update () {

        if (health <= 0) DestroySelf();
	}

    void OnCollisionEnter(Collision collision)
    {
        //Is it able to penetrate the layer?
        if ((LayersThatDestroyThis.value & 1 << collision.gameObject.layer) != 0)
        {
            print(name + " has collided with " + collision.gameObject.name);

            collision.gameObject.BroadcastMessage("ApplyDamage", shipCollisionDamage, SendMessageOptions.DontRequireReceiver);

            //No.
            DestroySelf();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        //Is it able to penetrate the layer?
        if ((LayersThatDestroyThis.value & 1 << other.gameObject.layer) != 0)
        {
            print(name + " has triggered " + other.gameObject.name);

            other.gameObject.BroadcastMessage("ApplyDamage", shipCollisionDamage, SendMessageOptions.DontRequireReceiver);

            //No.
            DestroySelf();
        }

    }

    public void ApplyDamage(float Damage)
    {
        print(name+" applied "+Damage+" to itself");
        health -= Damage;

        if (health <= 0)    DestroySelf();
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

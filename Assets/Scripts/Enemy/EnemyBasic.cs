using UnityEngine;
using System.Collections;

//[RequireComponent (typeof(WeaponBasic))]

public class EnemyBasic : MonoBehaviour {

    public float health = 2 ;
    public float collisionDamage = 1;

    public LayerMask LayersThatDestroyThis;

    public EnemyMoveModuleBasic moveModule;
    public WeaponBasic WeaponScript;

    public Vector3 movePos;
    public GameObject target;

    protected virtual void Start()
    {
        if (target == null)     GameObject.FindGameObjectWithTag("Player");
    }

	// Update is called once per frame
	public virtual void Update () {

        if (health <= 0) DestroySelf();
	}

    void OnCollisionStay(Collision collision)
    {
        //Is it able to penetrate the layer?
        if ((LayersThatDestroyThis.value & 1 << collision.collider.gameObject.layer) != 0)
        {
            print("Me "+ name + " has collided with " + collision.collider.gameObject.name);

            //collision.gameObject.BroadcastMessage("ApplyDamage", collisionDamage, SendMessageOptions.DontRequireReceiver);

            collision.collider.gameObject.GetComponent<BasicShipPart>().ApplyDamage(collisionDamage);
            ApplyDamage(collision.collider.gameObject.GetComponent<BasicShipPart>().collisionDamage);

            //No.
           // DestroySelf();
        }

    }


    void OnTriggerStay(Collider other)
    {
        //Is it able to penetrate the layer?
        if ((LayersThatDestroyThis.value & 1 << other.gameObject.layer) != 0)
        {
            print("Me " + name + " has triggered " + other.gameObject.name);

            //other.gameObject.BroadcastMessage("ApplyDamage", collisionDamage, SendMessageOptions.DontRequireReceiver);

            other.gameObject.GetComponent<BasicShipPart>().ApplyDamage(collisionDamage);
            ApplyDamage(other.gameObject.GetComponent<BasicShipPart>().collisionDamage);

            //No.
           // DestroySelf();
        }

    }

    public virtual void ApplyDamage(float Damage)
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

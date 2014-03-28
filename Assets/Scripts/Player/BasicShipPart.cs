using UnityEngine;
using System.Collections;

public class BasicShipPart : MonoBehaviour {

    public ShipPartType partType;

    public PlayerBasic shipCore;
    public EnemyBasic shipCoreE;

    public float collisionDamage = 1.0f;
    //public LayerMask LayersThatDestroyThis;

	// Use this for initialization
    protected virtual void Start()
    {
        if (shipCoreE == null)
        {
            shipCoreE = this.GetComponent<EnemyBasic>();
        }
	}


    public virtual void ApplyDamage(float Damage)
    {
        print("APPLY!");
        if (shipCore != null) {
            shipCore.ApplyDamage(Damage);
        }
        
        if (shipCoreE != null){
            print("APPLY2");
            shipCoreE.ApplyDamage(Damage);
        }

    }


    //void OnCollisionStay(Collision collision)
    //{
    //    //Is it able to penetrate the layer?
    //    //if ((LayersThatDestroyThis.value & 1 << collision.gameObject.layer) != 0)
    //    //{
    //        print(name + " has collided with " + collision.gameObject.name);

    //        collision.gameObject.BroadcastMessage("ApplyDamage", collisionDamage, SendMessageOptions.DontRequireReceiver);
    //    //}

    //}


    //void OnTriggerStay(Collider other)
    //{
    //    //Is it able to penetrate the layer?
    //    //if ((LayersThatDestroyThis.value & 1 << other.gameObject.layer) != 0)
    //    //{
    //        print(name + " has triggered " + other.gameObject.name);

    //        other.gameObject.BroadcastMessage("ApplyDamage", collisionDamage, SendMessageOptions.DontRequireReceiver);
    //    //}

    //}
}

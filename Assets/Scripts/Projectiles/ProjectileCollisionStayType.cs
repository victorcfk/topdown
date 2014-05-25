using UnityEngine;
using System.Collections;

public class ProjectileCollisionStayType : ProjectileBasic{

    public LayerMask LayersThatAreDamaged;
    public LayerMask LayersThatArePenetrated;

    public float TriggerInterval = 0.1f;
    private float TriggerTimer = 0;
    // Use this for initialization
    protected override void Start()
    {

    }

    protected override void Update()
    {

    }

    void OnCollisionStay(Collision collision)
    {
        if (TriggerTimer > 0)
        {
            TriggerTimer -= Time.deltaTime;

        }
        else
        {
            TriggerTimer = TriggerInterval;

            print(name + " has collided with " + collision.gameObject.name);
            //tempPart = collision.gameObject.GetComponent<BasicShipPart>();

            //if (tempPart != null) tempPart.ApplyDamage(Damage);
            //else
            //Is it able to penetrate the layer?
            if ((LayersThatAreDamaged.value & 1 << collision.collider.gameObject.layer) != 0)
            {
                //No.
                collision.collider.gameObject.GetComponent<DamageReceiver>().ApplyDamage(damage);
            }

            //Is it able to penetrate the layer?
            if ((LayersThatArePenetrated.value & 1 << collision.collider.gameObject.layer) == 0)
            {
                //No.
                DestroySelf();
            }

            
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (TriggerTimer > 0)
        {
            TriggerTimer -= Time.deltaTime;

        }
        else
        {
            TriggerTimer = TriggerInterval;

            print(name + " has triggered " + other.gameObject.name);

            //other.gameObject.BroadcastMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
            //tempPart = other.gameObject.GetComponent<BasicShipPart>();

            //if (tempPart != null) tempPart.ApplyDamage(Damage);
            //else
            if ((LayersThatAreDamaged.value & 1 << other.gameObject.layer) != 0)
            {
                //No.
                other.gameObject.GetComponent<DamageReceiver>().ApplyDamage(damage);
            }

            //Is it able to penetrate the layer?
            if ((LayersThatArePenetrated.value & 1 << other.gameObject.layer) == 0)
            {
                //No.
                DestroySelf();
            }

            //yield return new WaitForSeconds(TriggerInterval);
        }
    }
}

using UnityEngine;
using System.Collections;

public abstract class ProjectileBasic : MonoBehaviour {

    public WeaponBasic weapon;

    public float damage = 1;

    public float MinSpeed = 40;
    public float MaxLifeTime = 1;		// In seconds
    public float MaxDistanceTravelled = 500;

    // Use this for initialization
    protected void Start()
    {

        Invoke("DestroySelf", MaxLifeTime);
        //if ( IsHoming == true && HomingLifeTime > 0 ) {
        //    Invoke ( "StopHoming" , HomingLifeTime );
        //}

        //Emitters = GetComponentsInChildren<ParticleEmitter>();
    }


    protected void DestroySelf()
    {
        CancelInvoke();

        if (weapon != null)
            weapon.clearUpProjectile(gameObject);
         
        if (gameObject != null)
            Destroy(gameObject);
    }


}

using UnityEngine;
using System.Collections;

public abstract class ProjectileBasic : MonoBehaviour {

    public WeaponBasic weapon;

    public Vector3 center;

    //[HideInInspector]
    public float damage = 1;
    
    //[HideInInspector]
    public float range = 10;

    //[HideInInspector]
    public float minSpeed = 10;

    //[HideInInspector]
    public float lifeTime = 1;		// In seconds

    protected Vector3 prevPos;
    public float distTravelled = 0;

    // Use this for initialization
    protected void Start()
    {
        prevPos = transform.position;

        Invoke("DestroySelf", lifeTime);
        //if ( IsHoming == true && HomingLifeTime > 0 ) {
        //    Invoke ( "StopHoming" , HomingLifeTime );
        //}

        //Emitters = GetComponentsInChildren<ParticleEmitter>();
    }

    protected void Update()
    {
        distTravelled += Vector3.Distance(prevPos, transform.position);
        prevPos = transform.position;

        if (distTravelled >= range)
            DestroySelf();
    }


    protected void DestroySelf()
    {
        CancelInvoke();

        if (weapon != null)
            weapon.clearUpProjectile(this);
         
        if (gameObject != null)
            Destroy(gameObject);
    }

    public float getCenter()
    {
        return this.renderer.bounds.extents.z;


    }

}

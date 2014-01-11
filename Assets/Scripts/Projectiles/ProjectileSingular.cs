using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class ProjectileSingular: ProjectileBasic
{

    //internal const float DEFAULT_MAX_DISTANCE = 10000;

    public int classtype = 1;

    public int[] bulletIsDestroyedOnContactWithLayer;

    //public bool IsXPosRelativeToPlayer = false;
    
    //Homing functionality
    //=====================================
    public bool IsHoming = false; 
    public GameObject Target;
    public float maxTurnSpeed = 2.0f;
    //=====================================

    //Accelerating Functionality
    //=====================================
    public bool IsAccelerating = false;
    public float acceleration = 1.1f;
    public float MaxSpeed = 100;
    //=====================================

    public float Damage = 1;

    //internal CollisionTag TargetTagType;
    //private Transform Target;

    

    //protected ParticleEmitter[] Emitters;



    // Update is called once per frame
    virtual protected void Update()
    {

        if (IsAccelerating) AccelerateProjectile();
        if (IsHoming) HomeTowardsTarget(Target);

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxSpeed);

        //rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxSpeed);

       // Target = FindClosestTargetByTag(TargetTagType);
       // HomeToTarget();

       // AccelerateProjectile();

        /*
		float DistanceMoved = Speed * Time.deltaTime;
		transform.position += transform.forward * DistanceMoved;
		Distance -= DistanceMoved;
        */

        /*
		if ( IsHoming == true && HomingDistance > 0 ) {
			HomingDistance -= DistanceMoved;
			if ( HomingDistance <= 0 ) {
				IsHoming = false;
			}
		}
        */
        // Destroy if exceeded maximum distance or out of view
        //if (
        //        (collider.bounds.max.x < Camera.WorldBounds.xMin ||
        //            collider.bounds.min.x > Player.Camera.WorldBounds.xMax ||
        //            collider.bounds.max.z < Player.Camera.WorldBounds.yMin ||
        //            collider.bounds.min.z > Player.Camera.WorldBounds.yMax))
        //{
        //    Kill();
        //}
    }

    void OnCollisionEnter(Collision collision)
    {

        print(collision.gameObject.name);
        foreach (int colLayer in bulletIsDestroyedOnContactWithLayer)
        {
            if (collision.gameObject.layer == colLayer)
            {
                
                collision.gameObject.BroadcastMessage("ApplyDamage", Damage, SendMessageOptions.RequireReceiver);
                    //.DontRequireReceiver);

                DestroySelf();

            }
        }

        // COLLIDED against enemy
    }


    //// On Invisible we delete and remove from memory.
    //void OnBecameInvisible()
    //{
    //    //enabled = false;
    //    DestroySelf();
    //}

    //void OnBecameVisible()
    //{
    //    //enabled = true;
    //}

    protected void AccelerateProjectile()
    {
        rigidbody.velocity += acceleration * transform.forward.normalized;
    }

    protected void HomeTowardsTarget(GameObject target)
    {
        rigidbody.velocity += acceleration*(target.transform.position - this.gameObject.transform.position).normalized;       
    }


}
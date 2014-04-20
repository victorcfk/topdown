using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class ProjectileSingular: ProjectileBasic
{
    //internal const float DEFAULT_MAX_DISTANCE = 10000;

    public int classtype = 1;

    public LayerMask LayersThatArePenetrated;

    protected BasicShipPart tempPart;

    //public LayerMask LayersThatDestroyProjectile;
    //public bool IsXPosRelativeToPlayer = false;
    
    //Homing functionality
    //=====================================
    public bool IsHoming = false; 
    public GameObject HomingTarget;
    public float maxRadiansDelta = 2.0f;
    public float maxMagnitudeDelta = 2.0f;
    //=====================================

    //Accelerating Functionality
    //=====================================
    public bool IsAccelerating = false;
    public float Acceleration = 1.1f;
    public float MaxSpeed = 100;
    //=====================================

    public float Damage = 1;

    //internal CollisionTag TargetTagType;
    //private Transform Target;
    //protected ParticleEmitter[] Emitters;

    // Update is called once per frame
    protected virtual new void Update()
    {
        base.Update();

        if (IsAccelerating) AccelerateProjectile();
        if (IsHoming) HomeTowardsTarget(HomingTarget);

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, minSpeed > MaxSpeed? minSpeed : MaxSpeed);

        /*
        if(MaxSpeed > MinSpeed)
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity,MaxSpeed);
        else
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MinSpeed);
        */

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
        print(name +" has collided with "+collision.gameObject.name);
        tempPart = collision.gameObject.GetComponent<BasicShipPart>();

        if(tempPart != null)    tempPart.ApplyDamage(Damage);

        //Is it able to penetrate the layer?
        if ((LayersThatArePenetrated.value & 1 << collision.gameObject.layer) == 0)
        {
            //No.
            DestroySelf();
        }

    }


    void OnTriggerEnter(Collider other)
    {
        print(name + " has triggered " + other.gameObject.name);

        //other.gameObject.BroadcastMessage("ApplyDamage", Damage, SendMessageOptions.DontRequireReceiver);
        tempPart = other.gameObject.GetComponent<BasicShipPart>();

        if (tempPart != null) tempPart.ApplyDamage(Damage);

        //Is it able to penetrate the layer?
        if ((LayersThatArePenetrated.value & 1 << other.gameObject.layer) == 0)
        {
            //No.
            DestroySelf();
        }

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
        rigidbody.AddForce(transform.forward * Acceleration,ForceMode.Acceleration);// *rigidbody.velocity;

    }

    protected void HomeTowardsTarget(GameObject target)
    {
        //rigidbody.velocity += Acceleration * (target.transform.position - gameObject.transform.position).normalized;

        //rigidbody.AddForce((target.transform.position - gameObject.transform.position).normalized * Acceleration , ForceMode.Acceleration);


        /*
        Vector3 currDir = controlledObj.transform.forward;
        currDir.z = 0;

        Vector3 tarDir = -controlledObj.transform.position + rel;
        tarDir.z = 0;*/

        Vector3 newDir = Vector3.RotateTowards(transform.forward, target.transform.position- transform.position, maxRadiansDelta, maxMagnitudeDelta);
        newDir.z = 0;
        //transform.forward.z = 0;

        transform.rotation = Quaternion.LookRotation(newDir, new Vector3(0,1, 0));    //Force up to be -z to prevent flipping due to quaternion representation

        rigidbody.velocity = transform.forward*rigidbody.velocity.magnitude;
        //controlledObj.transform.localEulerAngles.Set(0, 0, controlledObj.transform.localEulerAngles.z);
    }
    
}
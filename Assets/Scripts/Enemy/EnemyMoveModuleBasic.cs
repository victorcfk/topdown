using UnityEngine;
using System.Collections;

public class EnemyMoveModuleBasic : MonoBehaviour {

    public float MaxSpeed = 100;
    public float BaseSpeed = 10;

    [Range(0,100)]
    public float minDistFromDestination;

    [Range(0,60)]
    public float minAngleForFacing;

    public bool hasReachedDestination { get { return Vector3.Distance(lastKnownDest, transform.position) <= minDistFromDestination; } }

    public Vector3 lastKnownDest;
    public bool isRealisticMotion = false;
    public bool isRealisticTurning = true;

    public float maxRadiansDelta = 2.0f;

	// Use this for initialization
    void Start()
    {
        //OrigDrag = rigidbody.drag;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!hasReachedDestination)
        {
            MoveToPoint(lastKnownDest);
            rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxSpeed);
        } else
        {
            ApplyBrakes();
        }
	}

    public void MoveToPoint(Vector3 destinationPoint)
    {
        //print("movePt");

//        if (isMonitoringDestination)
//        {
        lastKnownDest = destinationPoint;
//            StopAllCoroutines();
//            StartCoroutine(MoveToPointRoutine(destinationPoint));

        MoveInDirection(lastKnownDest - transform.position);
//        }
//        else
//        {   
//            MoveInDirection(destinationPoint - transform.position);
//        }

    }

    /// <summary>
    /// Just simply move in a direction
    /// </summary>
    /// <param name="direction">the direction of movement</param>
    public void MoveInDirection(Vector3 direction)
    {
        //print("moveDir");

        //StopMovement();

        if (isRealisticMotion)
        {


            if(isRealisticTurning ){

                LookToDirection(direction);

                if(!isFacingDirection(direction)){
                    ApplyBrakes();
                }
                else{
                    rigidbody.drag = 0;
                }

                rigidbody.AddForce(transform.forward * BaseSpeed, ForceMode.Force);
            }
            else{

                rigidbody.drag = 0;

                LookToDirection(direction);
                rigidbody.AddForce(direction.normalized * BaseSpeed, ForceMode.Force);
            }
            //rigidbody.drag = 0.1f;
        }
        else
        {
            rigidbody.drag = 0;
           // rigidbody.AddForce(direction.normalized * BaseSpeed, ForceMode.VelocityChange);       //this does not work right when called externally
            rigidbody.velocity = direction.normalized * BaseSpeed;
        }




    }


    public void ApplyBrakes()
    {
        //print("stopmove");

        if (isRealisticMotion)
            rigidbody.drag = BaseSpeed;
        else
            rigidbody.velocity = Vector3.zero;
    }

    /// <summary>
    /// Instantly look at point
    /// </summary>
    /// <param name="point">the point to look at</param>
    public void LookToPoint(Vector3 point)
    {
        if (isRealisticTurning)
            return;

        //transform.LookAt(point);
        
        Vector3 newDir = Vector3.RotateTowards(transform.forward, point - transform.position, Mathf.Deg2Rad*maxRadiansDelta*Time.deltaTime, 0);
        newDir.z = 0;
        //transform.forward.z = 0;
        
        transform.rotation = Quaternion.LookRotation(newDir, Vector3.up);    //Force up to be -z to prevent flipping due to quaternion representation
        
    }

    public bool isFacingPoint(Vector3 target)
    {
        return Vector3.Angle(transform.forward, target - transform.position) < minAngleForFacing;
    }

    /// <summary>
    /// Instantly look at point
    /// </summary>
    /// <param name="point">the point to look at</param>
    public void LookToDirection(Vector3 direction)
    {
        //transform.LookAt(point);
        
        Vector3 newDir = Vector3.RotateTowards(transform.forward, direction, Mathf.Deg2Rad*maxRadiansDelta*Time.deltaTime, 0);
        newDir.z = 0;
        //transform.forward.z = 0;
        
        transform.rotation = Quaternion.LookRotation(newDir, Vector3.up);    //Force up to be -z to prevent flipping due to quaternion representation
        
    }
    
    public bool isFacingDirection(Vector3 direction)
    {
        return Vector3.Angle(transform.forward, direction) < minAngleForFacing;
    }

}

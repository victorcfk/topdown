using UnityEngine;
using System.Collections;

public class EnemyMoveModuleBasic : MonoBehaviour {

    public float Acceleration = 1.1f;
    public float MaxSpeed = 100;
    public float BaseSpeed = 10;

    public float minDistFromDestination;

    public bool hasReachedDestination { get { return Vector3.Distance(lastKnownDest, transform.position) <= minDistFromDestination; } }
    public Vector3 lastKnownDest;

    public bool isRealisticMotion = false;
    public bool isMonitoringDestination = false;

    private Vector3 temp;
    private float OrigDrag;

    public float maxRadiansDelta = 2.0f;
    public float maxMagnitudeDelta = 2.0f;

	// Use this for initialization
    protected virtual void Start()
    {
        OrigDrag = rigidbody.drag;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //rigidbody.AddForce(new Vector3(0.1f,0,0) * BaseSpeed, ForceMode.VelocityChange);
	}

    void Update()
    {
        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxSpeed);
    }

    public void MoveToPoint(Vector3 destinationPoint)
    {
        //print("movePt");

        if (isMonitoringDestination)
        {
            StopAllCoroutines();
            StartCoroutine(MoveToPointRoutine(destinationPoint));
        }
        else
        {   
            MoveInDirection(destinationPoint - transform.position);
        }

    }

    /// <summary>
    /// Move in direction until you stop, coroutine used to monitor whether the destination has been reached.
    /// </summary>
    /// <param name="destinationPoint"></param>
    /// <returns>The point to stop at</returns>
    private IEnumerator MoveToPointRoutine(Vector3 destinationPoint)
    {
        lastKnownDest = destinationPoint;
        
        while (!hasReachedDestination)
        {
            MoveInDirection(lastKnownDest - transform.position);
            yield return null;
        }
        StopMovement();
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
            rigidbody.AddForce(direction.normalized * BaseSpeed, ForceMode.Acceleration);
            rigidbody.drag = 0.1f;
        }
        else
        {
           // rigidbody.AddForce(direction.normalized * BaseSpeed, ForceMode.VelocityChange);       //this does not work right when called externally
            rigidbody.velocity = direction.normalized * BaseSpeed;
        }
    }

    /// <summary>
    /// Instantly look at point
    /// </summary>
    /// <param name="point">the point to look at</param>
    public void LookToPoint(Vector3 point)
    {
        //transform.LookAt(point);

        Vector3 newDir = Vector3.RotateTowards(transform.forward, point - transform.position, maxRadiansDelta, maxMagnitudeDelta);
        newDir.z = 0;
        //transform.forward.z = 0;

        transform.rotation = Quaternion.LookRotation(newDir, new Vector3(0, 1, 0));    //Force up to be -z to prevent flipping due to quaternion representation
        
    }

    public void StopMovement()
    {
        //print("stopmove");

        if (isRealisticMotion)
            rigidbody.drag = BaseSpeed;
        else
            rigidbody.velocity = Vector3.zero;
    }
}

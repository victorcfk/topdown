using UnityEngine;
using System.Collections;

public class EnemyMoveModuleBasic : MonoBehaviour {

    public float Acceleration = 1.1f;
    public float MaxSpeed = 100;
    public float BaseSpeed = 10;

    public float minDistFromDestination;

    public bool hasReachedDestination { get { return Vector3.Distance(lastKnownDest, transform.position) <= minDistFromDestination; } }
    public Vector3 lastKnownDest;

    public bool isRealisticMotion = true;

    private Vector3 temp;
    private float OrigDrag;

	// Use this for initialization
	void Start () {
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
        print("movePt");
        StartCoroutine(MoveToPointRoutine(destinationPoint));
    }

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

    public void MoveInDirection(Vector3 direction)
    {
        print("moveDir");

        //StopMovement();

        if (isRealisticMotion)
        {
            rigidbody.AddForce(direction.normalized * BaseSpeed, ForceMode.Acceleration);
            rigidbody.drag = 0.1f;
        }
        else
            rigidbody.AddForce(direction.normalized * BaseSpeed, ForceMode.VelocityChange);
            //rigidbody.velocity = direction.normalized * BaseSpeed;

        //rigidbody.AddForce(direction, ForceMode.VelocityChange);
            //.velocity = StartPursuitSpeed * (target.transform.position - this.gameObject.transform.position).normalized;

    }

    /// <summary>
    /// Instantly look at point
    /// </summary>
    /// <param name="point">the point to look at</param>
    public void LookToPoint(Vector3 point)
    {
        transform.LookAt(point);
    }

    public void StopMovement()
    {
        print("stopmove");

        if (isRealisticMotion)
            rigidbody.drag = BaseSpeed;
        else
            rigidbody.velocity = Vector3.zero;
    }
}

using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]

public class CameraFollow : MonoBehaviour {

    public Transform followTarget;
    public float smoothTime = 0.1f;
    public float maxSpeed = Mathf.Infinity;

    public Vector3 currentVeloc;

    //private Vector3 currentPos;
    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	   
	}

    void FixedUpdate()
    {
//        velocity = this.rigidbody.velocity;

        //followTarget.rigidbody.velocity;

        Vector3 wantedPos = new Vector3(
            followTarget.transform.position.x +  followTarget.transform.forward.x*1,
            followTarget.transform.position.y +  followTarget.transform.forward.y*1,
             transform.position.z);
        //followTarget.rigidbody.velocity.x 

        this.transform.position = Vector3.SmoothDamp( transform.position, wantedPos, ref velocity,smoothTime);
    }
}

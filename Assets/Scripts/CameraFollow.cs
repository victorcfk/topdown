using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Camera))]

public class CameraFollow : MonoBehaviour {

    public Transform followTarget;
    public float smoothTime = 0.1f;
    public float maxSpeed = Mathf.Infinity;

    //private Vector3 currentPos;
    private Vector3 velocity = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}

    void FixedUpdate()
    {
//        velocity = this.rigidbody.velocity;

        Vector3 wantedPos = new Vector3(
            followTarget.transform.position.x,
            followTarget.transform.position.y,
             transform.position.z);

        this.transform.position = Vector3.SmoothDamp( transform.position, wantedPos, ref velocity,smoothTime);
    }
}

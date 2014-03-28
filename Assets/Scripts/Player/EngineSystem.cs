using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineSystem : MonoBehaviour {

    //[HideInInspector]
    public List<BasicShipPart> engines = new List<BasicShipPart>();

    public Vector3 upDir;
    public Vector3 rightDir;

    public float basicAcceleration = 0.5f;
    public float basicMaxSpeed = 10.0f;
    public float basicMaxRadiansDelta = 0.05f;
    public float basicMaxMagnitudeDelta = 0.05f;
    
    protected float resultantAcceleration = 0.5f;
    protected float resultantMaxSpeed = 10.0f;
    protected float resultantMaxRadiansDelta = 0.05f;
    protected float resultantMaxMagnitudeDelta = 0.05f;


    public GameObject controlObj;

    public float horizontalMoveVal;
    public float verticalMoveVal;
    public Vector3 LookAtVector;

	// Use this for initialization
	void Start () {
	
        horizontalMoveVal = 0;
        verticalMoveVal = 0;

        getAllEngineModifiers();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        getAllEngineModifiers();
        //horiVal = Input.GetAxis("Horizontal");
        //vertVal = Input.GetAxis("Vertical");
        moveHorizontally(horizontalMoveVal);
        moveVertically(verticalMoveVal);
        clampMovement();

        turnToVector(LookAtVector);
        /*
        else
        if (Input.GetKey("right"))
        {
            controlObj.rigidbody.velocity += rightDir;
        }*/

        
        //= Mathf.Clamp(controlObj.rigidbody.velocity.magnitude, -topSpeed, topSpeed);

        /*
        if (Input.GetKey("down"))
        {
            controlObj.rigidbody.velocity += downDir;
        }
         */
	}

    /// <summary>
    /// Based on fed input, move the gameobject left or right
    /// </summary>
    void moveHorizontally(float input)
    {
        controlObj.rigidbody.velocity += rightDir * horizontalMoveVal * resultantAcceleration;
    }

    /// <summary>
    /// Based on fed input, move the gameobject up or down
    /// </summary>
    void moveVertically(float input)
    {
        controlObj.rigidbody.velocity += upDir * verticalMoveVal * resultantAcceleration;
    }

    void turnToVector(Vector3 LookAtVector)
    {
        Vector3 newDir = Vector3.RotateTowards(
            controlObj.transform.forward, 
            LookAtVector,
            resultantMaxRadiansDelta,
            resultantMaxMagnitudeDelta);

        newDir.z = 0;

        controlObj.transform.rotation = Quaternion.LookRotation(newDir, new Vector3(0, 0, -1));    //Force up to be -z to prevent flipping due to quaternion representation
    }

    /// <summary>
    /// Control the maximum speed of movement
    /// </summary>
    void clampMovement()
    {
        controlObj.rigidbody.velocity = Vector3.ClampMagnitude(controlObj.rigidbody.velocity, basicMaxSpeed);
    }

    void getAllEngineModifiers()
    {
        foreach (EngineBasic engine in engines)
        {
            resultantAcceleration   = (basicAcceleration + engine.moveSpeedAdd) * engine.moveSpeedMultiplier;
            resultantMaxSpeed       = (basicMaxSpeed + engine.moveSpeedAdd) * engine.moveSpeedMultiplier;

            resultantMaxRadiansDelta    = (basicMaxRadiansDelta + engine.turnSpeedAdd) * engine.turnSpeedMultiplier;
            resultantMaxMagnitudeDelta  = (basicMaxMagnitudeDelta + engine.turnSpeedAdd) * engine.turnSpeedMultiplier;
        }
    }


}


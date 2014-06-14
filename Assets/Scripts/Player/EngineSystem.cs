using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineSystem : MonoBehaviour {

    //[HideInInspector]
    public List<EngineBasic> engines = new List<EngineBasic>();

    public Vector3 upDir;
    public Vector3 rightDir;

    public float basicAcceleration = 0.5f;
    public float basicMaxSpeed = 10.0f;
    public float basicMaxRadiansDelta = 0.05f;
    
    protected float resultantAcceleration;
    protected float resultantMaxSpeed;
    protected float resultantMaxRadiansDelta;

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
		controlObj.rigidbody.velocity += rightDir * input * resultantAcceleration;
    }

    /// <summary>
    /// Based on fed input, move the gameobject up or down
    /// </summary>
    void moveVertically(float input)
    {
		controlObj.rigidbody.velocity += upDir * input * resultantAcceleration;
    }

    void turnToVector(Vector3 LookAtVector)
    {
        Vector3 newDir = Vector3.RotateTowards(
            controlObj.transform.forward, 
            LookAtVector,
            resultantMaxRadiansDelta/180* Mathf.PI,
            0);

        newDir.z = 0;

        controlObj.transform.rotation = Quaternion.LookRotation(newDir, new Vector3(0, 0, -1));    //Force up to be -z to prevent flipping due to quaternion representation
    }

    /// <summary>
    /// Control the maximum speed of movement
    /// </summary>
    void clampMovement()
    {
        controlObj.rigidbody.velocity = Vector3.ClampMagnitude(controlObj.rigidbody.velocity, resultantMaxSpeed);
    }

    void getAllEngineModifiers()
    {
        float tempAcc = 0;
        float tempMaxSpd = 0;
        float tempMaxTurn = 0;

        foreach (EngineBasic engine in engines)
        {
            tempAcc += engine.moveAccAdd;
            tempMaxSpd += engine.moveSpeedMaxAdd;

            tempMaxTurn += engine.turnSpeedAdd;
        }
        resultantAcceleration = basicAcceleration + tempAcc;
        resultantMaxSpeed = basicMaxSpeed + tempMaxSpd;

        resultantMaxRadiansDelta = basicMaxRadiansDelta + tempMaxTurn;

    }


}


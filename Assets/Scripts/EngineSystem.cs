using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineSystem : MonoBehaviour {

    [HideInInspector]
    public List<BasicShipPart> engines = new List<BasicShipPart>();

    public Vector3 upDir;
    public Vector3 rightDir;

    public float acceleration = 0.5f;
    public float maxSpeed = 10.0f;
    
    public GameObject controlObj;

    public float horizontalMoveVal;
    public float verticalMoveVal;


	// Use this for initialization
	void Start () {
	
        horizontalMoveVal = 0;
        verticalMoveVal = 0;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //horiVal = Input.GetAxis("Horizontal");
        //vertVal = Input.GetAxis("Vertical");


        getAllEngineModifiers();

        moveHorizontally(horizontalMoveVal);
        moveVertically(verticalMoveVal);
        clampMovement();
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
        controlObj.rigidbody.velocity += rightDir * horizontalMoveVal * acceleration;
    }

    /// <summary>
    /// Based on fed input, move the gameobject up or down
    /// </summary>
    void moveVertically(float input)
    {
        controlObj.rigidbody.velocity += upDir * verticalMoveVal * acceleration;
    }

    /// <summary>
    /// Control the maximum speed of movement
    /// </summary>
    void clampMovement()
    {
        controlObj.rigidbody.velocity = Vector3.ClampMagnitude(controlObj.rigidbody.velocity, maxSpeed);
    }

    void getAllEngineModifiers()
    {
        foreach (ShipModifierPart engine in engines)
        {
            acceleration = (acceleration + engine.moveSpeedAdd) * engine.moveSpeedMultiplier;
            maxSpeed = (maxSpeed + engine.moveSpeedAdd) * engine.moveSpeedMultiplier;
        }
    }

}


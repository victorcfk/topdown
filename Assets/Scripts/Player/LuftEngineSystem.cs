using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LuftEngineSystem : MonoBehaviour {
	
	//[HideInInspector]
    public GameObject controlObj;
    public List<BasicShipPart> engines = new List<BasicShipPart>();

	public float basicAcceleration = 1.0f;
	public float basicMaxSpeed = 10.0f;
	public float basicMaxMagnitudeDelta = 1.0f;
	
	protected float resultantAcceleration;
	protected float resultantMaxSpeed;
	protected float resultantMaxRadiansDelta;
	
	public float horizontalMoveVal;
	public float verticalMoveVal;
	
	// Use this for initialization
	void Start () {
		
		horizontalMoveVal = 0;
		verticalMoveVal = 0;
		
		getAllEngineModifiers();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		getAllEngineModifiers();

		turn(horizontalMoveVal);
		accelerate(verticalMoveVal);
		clampMovement();

	}
	
	/// <summary>
	/// Based on fed input, move the gameobject left or right
	/// </summary>
	void turn(float input)
	{   
		controlObj.transform.Rotate (new Vector3(0,1,0)//controlObj.transform.up
		                             , -input * resultantMaxRadiansDelta);
		//Quaternion.
			//rigidbody.velocity += rightDir * horizontalMoveVal * resultantAcceleration;
	}
	
	/// <summary>
	/// Based on fed input, move the gameobject up or down
	/// </summary>
	void accelerate(float input)
	{
        if(input!=0){
            foreach (BasicShipPart e in engines)
                e.activationPS.Play();
        }

        controlObj.rigidbody.velocity += controlObj.transform.forward * input * resultantAcceleration;
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
        float tempAcc=0;
        float tempMaxSpd = 0;
        float tempMaxTurn = 0;

		foreach (EngineBasic engine in engines)
		{
            tempAcc += engine.moveAccAdd;
            tempMaxSpd += engine.moveSpeedMaxAdd;

            tempMaxTurn += engine.turnSpeedAdd;
		}
        resultantAcceleration       = basicAcceleration + tempAcc;
        resultantMaxSpeed           = basicMaxSpeed + tempMaxSpd;

        resultantMaxRadiansDelta  = basicMaxMagnitudeDelta + tempMaxTurn;

	}
	
	
}


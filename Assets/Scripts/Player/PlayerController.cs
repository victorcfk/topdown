using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public PlayerBasic playerUnit;

    private float horizontalMoveInput;
    private float verticalMoveInput;
    private Vector3 LookAtVector;

    private bool isFiringInputActive;

    [HideInInspector]
    //public List<BasicShipPart> engines = new List<BasicShipPart>();

    //public GameObject controlledObj;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update()
    {
        //getAllEngineModifiers();

        {
            this.horizontalMoveInput = Input.GetAxis("Horizontal");
            this.verticalMoveInput = Input.GetAxis("Vertical");

            this.isFiringInputActive = Input.GetButton("Fire1");
                //.GetMouseButton(0);
        }

        playerUnit.isFiringInputActive = this.isFiringInputActive;
        playerUnit.horizontalMoveInput = this.horizontalMoveInput;
        playerUnit.verticalMoveInput = this.verticalMoveInput;

        AimWithMouse();
    }

    void AimWithMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // ray didn't hit any solid object, so return the 
        // intersection point between the ray and 
        // the Y=0 plane (horizontal plane)
        float t = -ray.origin.z / ray.direction.z;
        Vector3 rel = ray.GetPoint(t);

        //Vector3 currDir = playerUnit.transform.forward;
        //currDir.z = 0;

        Vector3 tarDir = rel - playerUnit.transform.position;
        tarDir.z = 0;

        playerUnit.LookAtVector = tarDir;
        
        //Vector3 newDir = Vector3.RotateTowards(currDir, tarDir, maxRadiansDelta, maxMagnitudeDelta);
        //newDir.z = 0;

        //playerUnit.transform.rotation = Quaternion.LookRotation(newDir, new Vector3(0, 0, -1));    //Force up to be -z to prevent flipping due to quaternion representation

    }

    void AimWithGamePad()
    {
        //if()

    }

    //void AimWithKeyboard()
    //{
    //    Vector3 currDir = playerUnit.transform.forward;
    //    currDir.z = 0;

    //    Vector3 tarDir = new Quaternion(0, 0, 0, 0) * playerUnit.transform.forward; 
    //    tarDir.z = 0;

    //    Vector3 newDir = Vector3.RotateTowards(currDir, tarDir, maxRadiansDelta, maxMagnitudeDelta);

    //    //playerUnit.transform.rotation = Quaternion.LookRotation(newDir, new Vector3(0, 0, -1));    //Force up to be -z to prevent flipping due to quaternion representation

    //    playerUnit.transform.Rotate(Vector3.up, 1);
    //}

    //void getAllEngineModifiers()
    //{
    //    foreach (EngineBasic engine in playerUnit.engineSystem.engines)
    //    {
    //        maxRadiansDelta = (maxRadiansDelta + engine.turnSpeedAdd) * engine.turnSpeedMultiplier;
    //        maxMagnitudeDelta = (maxMagnitudeDelta + engine.turnSpeedAdd) * engine.turnSpeedMultiplier;
    //    }
    //}
}
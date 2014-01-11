using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AimingSystem : MonoBehaviour {
    [HideInInspector]
    public List<BasicShipPart> engines = new List<BasicShipPart>();

    public GameObject controlledObj;
    public float maxRadiansDelta = 0.05f;
    public float maxMagnitudeDelta = 0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        getAllEngineModifiers();

        //print(Input.mousePosition);

        //Vector3 rel = new Vector3(Input.mousePosition.x - Screen.width/2,Input.mousePosition.y - Screen.height/2,0);

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //.ScreenPointToRay(Input.mousePosition); 


        //       rel = Camera.mainCamera.ViewportToWorldPoint
        //         (rel);


        // ray didn't hit any solid object, so return the 
        // intersection point between the ray and 
        // the Y=0 plane (horizontal plane)
        float t = -ray.origin.z / ray.direction.z;
        Vector3 rel = ray.GetPoint(t);

        //print(rel);

        //this.gameObject.transform.position = rel;

        Vector3 currDir = controlledObj.transform.forward;
        currDir.z = 0;

        Vector3 tarDir = -controlledObj.transform.position + rel;
        tarDir.z = 0;
    
        Vector3 newDir = Vector3.RotateTowards(currDir, tarDir, maxRadiansDelta, maxMagnitudeDelta);
        newDir.z = 0;

        controlledObj.transform.rotation = Quaternion.LookRotation(newDir, new Vector3(0,0,-1));    //Force up to be -z to prevent flipping due to quaternion representation
        controlledObj.transform.localEulerAngles.Set(0, 0, controlledObj.transform.localEulerAngles.z);
        //print(controlledObj.transform.localEulerAngles);
        

        //controlledObj.transform.localEulerAngles.Set(0, controlledObj.transform.localEulerAngles.y, 0);
        //controlledObj.transform.localEulerAngles.z = 0;
        //controlledObj.transform.LookAt(rel,Vector3.up);

        //controlledObj.transform.up = new Vector3(1, 0,0);
        /*
            .SetFromToRotation(
            controlledObj.transform.forward,
            -controlledObj.transform.position + Input.mousePosition);*/
	}

    void getAllEngineModifiers()
    {
        foreach (ShipModifierPart engine in engines)
        {
            maxRadiansDelta = (maxRadiansDelta + engine.turnSpeedAdd) * engine.turnSpeedMultiplier;
            maxMagnitudeDelta = (maxMagnitudeDelta + engine.turnSpeedAdd) * engine.turnSpeedMultiplier;
        }
    }
}

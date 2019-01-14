using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {



    [HideInInspector]
    static public bool beginSwap    {       get { return Input.GetMouseButtonDown(0);}      }
    static public bool middleOfSwap {       get { return Input.GetMouseButton(0); } }
    static public bool endSwap      {       get { return Input.GetMouseButtonUp(0);  }      }


    //public Camera	Camera		{ get { return Camera.main;		} }

    [HideInInspector]
    static public Vector3 beginSwapPosition;
    [HideInInspector]
    static public Vector3 endSwapPosition
    { 
        get{
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // ray didn't hit any solid object, so return the 
                // intersection point between the ray and 
                // the Y=0 plane (horizontal plane)
                float t = -ray.origin.z / ray.direction.z;

                print("endo " + ray.GetPoint(t));

                return ray.GetPoint(t);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        /*
        if (Input.GetMouseButtonDown(1))
            beginSwap = true;
        else
            beginSwap = false;

        if (Input.GetMouseButtonUp(1))
            endSwap = true;
        else
            endSwap = false;
        */

        /*
        if (beginSwap)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // ray didn't hit any solid object, so return the 
            // intersection point between the ray and 
            // the Y=0 plane (horizontal plane)
            float t = -ray.origin.z / ray.direction.z;
            beginSwapPosition = ray.GetPoint(t);
        }
        else
            if (endSwap)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                // ray didn't hit any solid object, so return the 
                // intersection point between the ray and 
                // the Y=0 plane (horizontal plane)
                float t = -ray.origin.z / ray.direction.z;
                endSwapPosition = ray.GetPoint(t);
            }
        */

	}
}

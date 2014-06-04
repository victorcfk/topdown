using UnityEngine;
using System.Collections;

public class PieceControl : MonoBehaviour {

    //public ShipCoreInfoStore ShipCore;
    public BasicShipPart[] PossibleParts;   //The number of parts that can take up this point
    public float scrollSpeed = 0.1f;
    protected float offset;

    protected Quaternion CurrentPartRotation;
    protected BasicShipPart CurrentPart;
    protected int currPartPtr = 0;
    protected RaycastHit hitInfo;

	// Use this for initialization
	void Start () {
        CurrentPartRotation = this.transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {

        offset = Time.time * scrollSpeed;
		renderer.material.SetTextureOffset ("_MainTex", new Vector2(offset,0));

        //left clicked
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider == this.collider)
            {

                //No part present, create one
                if (CurrentPart == null)
                {
                    CurrentPart = (BasicShipPart)Instantiate
                        (PossibleParts[currPartPtr], 
                        this.transform.position, 
                        this.transform.rotation);

                    CurrentPart.transform.parent = this.transform.parent;
                }
                else
                {
                    Destroy(CurrentPart.gameObject);

                    if (currPartPtr < PossibleParts.Length - 1)
                        currPartPtr++;
                    else
                        currPartPtr = 0;
                    
                    CurrentPart = (BasicShipPart)Instantiate
                        (PossibleParts[currPartPtr], 
                        this.transform.position,
                        CurrentPartRotation);

                    CurrentPart.transform.parent = this.transform.parent;
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider == this.collider)
            {

                //No part present, create one
                if (CurrentPart != null)
                {
                    print("rota");
                    CurrentPart.transform.Rotate(0,45 , 0);
                    CurrentPartRotation = CurrentPart.transform.rotation;
                }
            
            }

        }

	}


}

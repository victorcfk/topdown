using UnityEngine;
using System.Collections.Generic;

public class PartBuildController : MonoBehaviour {

    //public ShipCoreInfoStore ShipCore;
    public List<BasicShipPart> PossibleParts {get {return ShipCoreInfoStore.instance.listOfPartPrefabs;}}   //The number of parts that can take up this point
    public BasicShipPart CurrentPart;
    protected Quaternion CurrentPartRotation;

    protected int currPartPtr = 0;
    protected RaycastHit hitInfo;

    public PartInfo partInfo;

    public float scrollSpeed = 0.1f;
    protected float offset;

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
                GetNextPart();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo) && hitInfo.collider == this.collider)
            {
                RotateCurrentPart();
            }
        }
	}
    
    public void GetNextPart()
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
            
            if (currPartPtr < PossibleParts.Count - 1)
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

    public void RotateCurrentPart()
    {
        if (CurrentPart != null)
        {
            CurrentPart.transform.Rotate(0, 45, 0);
            CurrentPartRotation = CurrentPart.transform.rotation;
        }
    }

//    public void saveAttachedPartIntoInfo(){
//        this.partInfo.savePart(this.CurrentPart);
//    }
//
//    public void loadAttachedPartFromInfo(){
//        this.partInfo.loadPart();
//    }
}

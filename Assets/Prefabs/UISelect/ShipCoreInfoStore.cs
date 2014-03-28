using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum ShipPartType
{
    CORE,
    ENGINE,
    GUN,
    SHIELD,
    LASER
}

public struct Parto{
    ShipPartType type;
    Transform t;
}

public class ShipCoreInfoStore : MonoBehaviour {

    public static ShipCoreInfoStore infoStoreSingleton;

    public GameObject testship;
    public List<BasicShipPart> listOfPartPrefabs;      //Reference to all the part things, needs to be initialized
    
    public static List<GameObject> parts;
    public static List<Vector3> listOfPositionsOfParts;
    public static List<Quaternion> listOfRotationsOfParts;
    public static List<ShipPartType> listOfPartsToAssignToShipCore;

    public bool GetPartsAndNewStage = false;

    PlayerBasic bp;
    BasicShipPart tempPart;

	// Use this for initialization. In dont destroy on load, this is only called once.
	void Start () {

        ShipCoreInfoStore.infoStoreSingleton = this;

        if (listOfPartsToAssignToShipCore == null)
        {
            listOfPartsToAssignToShipCore = new List<ShipPartType>();
        }
        if (listOfPositionsOfParts == null)
        {
            listOfPositionsOfParts = new List<Vector3>();
        }
        if (listOfRotationsOfParts == null)
        {
            listOfRotationsOfParts = new List<Quaternion>();
        }
        if (parts == null)
        {
            parts = new List<GameObject>();
        }

        //if (bp == null)
        //{
        //    bp = FindObjectOfType<BasicPlayer>();

        //}



        if (parts != null)  //This should be assigned at the start of the stage
        {
            assignPartsToShipCore();
        }
        
        //DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update () {

        if (GetPartsAndNewStage)
        {
            GetPartsAndNewStage = !GetPartsAndNewStage;

            print("childCount: " + testship.transform.childCount);

            savePartInfoOnShipCore();

            Application.LoadLevel(Application.loadedLevel + 1);

        }

        //if (parts != null)
        //{
        //    print("Ze Count " + parts.Count);

        //    foreach (GameObject p in parts)
        //    {
        //        print(p.transform);
        //    }

        //}
	}

    void savePartInfoOnShipCore()
    {
        BasicShipPart temp;

        print(testship.transform.childCount);
        for (int i = 0; i < testship.transform.childCount; i++)
        {
            temp = testship.transform.GetChild(i).GetComponent<BasicShipPart>();

            if (temp != null)
            {   //Get the list of parts
                listOfPartsToAssignToShipCore.Add(temp.partType);    //save the type on record
                listOfPositionsOfParts.Add(temp.transform.position);
                listOfRotationsOfParts.Add(temp.transform.rotation);  //save the transform for later.
            }
            //DontDestroyOnLoad(temp);
        }

    }

    void assignPartsToShipCore()
    {
        print(listOfPartsToAssignToShipCore.Count + " , " + listOfRotationsOfParts.Count + " , " + listOfPartPrefabs.Count);

        //find the player, give him the parts;
        //==========================================================================

        bp = FindObjectOfType<PlayerBasic>();

        //foreach(ShipPartType type in typeList)        //Go through the list of recorded parts
        for (int i = 0; i < listOfPartsToAssignToShipCore.Count; i++)
        {
            //foreach (BasicShipPart part in partzPrefab)       //Find the corresponding prefab
            for (int j = 0; j < listOfPartPrefabs.Count; j++)
            {
                if (listOfPartPrefabs[j].partType == listOfPartsToAssignToShipCore[i])
                {
                    tempPart = (BasicShipPart)Instantiate(listOfPartPrefabs[j]);

                    //assign the corresponding transform
                    //=========================================================
                    tempPart.transform.position = listOfPositionsOfParts[i];
                    tempPart.transform.rotation = listOfRotationsOfParts[i];
                    //=========================================================

                    tempPart.transform.parent = bp.transform;

                }
            }
        }
        //==========================================================================

    }

}

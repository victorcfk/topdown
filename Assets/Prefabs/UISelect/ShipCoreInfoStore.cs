using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipCoreInfoStore : MonoBehaviour {

    private static ShipCoreInfoStore _instance;

    public static ShipCoreInfoStore instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<ShipCoreInfoStore>();

                //Tell unity not to destroy this object when loading a new scene!
                //DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            //If I am the first instance, make me the Singleton
            _instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public PlayerBasic ShipCore;
    public PlayerBasic ShipCorePrefab;

    public List<BasicShipPart> listOfPartPrefabs;      //Reference to all the part things, needs to be initialized
    public List<PartInfo> listOfPartInfo;
    
    ////public List<GameObject> parts;
    //public List<ShipPartType> listOfPartsToAssignToShipCore;
    //public List<Vector3> listOfPartsPositions;
    //public List<Quaternion> listOfPartsRotations;    

    public bool buildShipNow = false;
    public bool savePartsAndNewStage = false;

    //PlayerBasic bp;
    BasicShipPart tempPart;

	// Use this for initialization. In dont destroy on load, this is only called once.
	void Start () {

        ShipCoreInfoStore._instance = this;
        if (ShipCore == null) GameObject.FindObjectOfType<PlayerBasic>();
        if(listOfPartInfo == null)  listOfPartInfo = new List<PartInfo>();
        //if (listOfPartsToAssignToShipCore == null)      listOfPartsToAssignToShipCore = new List<ShipPartType>();
        //if (listOfPartsPositions == null)             listOfPartsPositions = new List<Vector3>();
        //if (listOfPartsRotations == null)             listOfPartsRotations = new List<Quaternion>();
        //if (parts == null)                              parts = new List<GameObject>();

        //if (bp == null)
        //{
        //    bp = FindObjectOfType<BasicPlayer>();

        //}

        DontDestroyOnLoad(this.gameObject);
	}

	// Update is called once per frame
	void Update () {

        if (buildShipNow)
        {
            buildShipNow = !buildShipNow;

            if (ShipCore == null) ShipCore = GameObject.FindObjectOfType<PlayerBasic>();

            if (ShipCore == null) ShipCore = (PlayerBasic)Instantiate(ShipCorePrefab);

            if (listOfPartInfo != null) loadPartInfoOnShipCore(ShipCore);       //This should be assigned at the start of the stage

            ShipCore.initAllParts();
            ShipCore.initEngineSystem();
            ShipCore.initWeaponsSystem();
        }

        if (savePartsAndNewStage)
        {
            savePartsAndNewStage = !savePartsAndNewStage;

            print("childCount: " + ShipCore.transform.childCount);

            savePartInfoOnShipCore(ShipCore);

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

    void OnGUI()
    {
        if (GUI.Button(
            new Rect(Screen.width - 80,
                    20,
                    60,
                    30), "Start"))
        {

            Debug.Log("Clicked the button with text");

            ShipCoreInfoStore.instance.savePartsAndNewStage = true;

        }
    }

    void savePartInfoOnShipCore(PlayerBasic ShipCore)
    {
        BasicShipPart tempPart;

        print("Parts On Ship: "+ ShipCore.transform.childCount);

        for (int i = 0; i < ShipCore.transform.childCount; i++)
        {
            tempPart = ShipCore.transform.GetChild(i).GetComponent<BasicShipPart>();

            if(tempPart != null && tempPart.partType != ShipPartType.CORE)
                listOfPartInfo.Add(new PartInfo(tempPart));
        }
    }

    //void saveShipPartInfo(BasicShipPart part)
    //{
    //    listOfPartsToAssignToShipCore.Add(part.partType);    //save the type on record
    //    listOfPartsPositions.Add(part.transform.position);
    //    listOfPartsRotations.Add(part.transform.rotation);  //save the transform for later.
    //}


    void loadPartInfoOnShipCore(PlayerBasic ShipCore)
    {
        //print(listOfPartsToAssignToShipCore.Count + " , " + listOfPartsRotations.Count + " , " + listOfPartPrefabs.Count);

        //find the player, give him the parts;
        //==========================================================================
        
        //foreach(ShipPartType type in typeList)        //Go through the list of recorded parts
        for (int i = 0; i < listOfPartInfo.Count; i++)
        {
            BasicShipPart g = listOfPartInfo[i].loadPart();

            g.transform.parent = ShipCore.transform;

            g.transform.position = listOfPartInfo[i].partPosition;
            g.transform.rotation = listOfPartInfo[i].partRotation;

        }
        //==========================================================================

    }
    
}

using UnityEngine;
using System.Collections;

public enum ShipPartType
{
    CORE,
    ENGINE,
    GUN,
    SHIELD,
    LASER
}

[System.Serializable]
public class PartInfo {

    //static ShipPartType PartPrefabList;

    public Vector3 partPosition;
    public Quaternion partRotation;
    public ShipPartType partType;

    //// Use this for initialization
    //void Start () {
	
    //}
	
    //// Update is called once per frame
    //void Update () {
	
    //}

    public PartInfo()
    {

    }

    public PartInfo(BasicShipPart shipPart)
    {
        savePart(shipPart);
    }

    public BasicShipPart loadPart()
    {
        BasicShipPart tempPart = null;

        for (int j = 0; j < ShipCoreInfoStore.instance.listOfPartPrefabs.Count; j++)
        {
            if (ShipCoreInfoStore.instance.listOfPartPrefabs[j].partType == this.partType)
            {
                //=========================================================
                tempPart = (BasicShipPart)MonoBehaviour.Instantiate(ShipCoreInfoStore.instance.listOfPartPrefabs[j]);    //Create a part

                tempPart.transform.position = partPosition;  //assign a position
                tempPart.transform.rotation = partRotation;  //assign a rotation
                //=========================================================

            }
        }

        return tempPart;
    }

    public void savePart(BasicShipPart shipPart)
    {
        partType = (shipPart.partType);    //save the type on record
        partPosition = (shipPart.transform.position);
        partRotation = (shipPart.transform.rotation);  //save the transform for later.
    }


}

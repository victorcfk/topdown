using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsSystem : MonoBehaviour {

    //[HideInInspector]
    public List<BasicShipPart> weapons;//= new List<BasicShipPart>();

    private bool isFiring;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void fireAllWeapons()
    {
        foreach (BasicShipPart weap in weapons)
        {
            weap.GetComponent<WeaponBasic>().FireWeapon();
        }
    }
}

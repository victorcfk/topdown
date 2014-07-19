using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsSystem : MonoBehaviour {

    //[HideInInspector]
    public List<WeaponBasic> weapons;//= new List<BasicShipPart>();

    public List<WeaponBasic> weaponsSet1;
    public List<WeaponBasic> weaponsSet2;
    public List<WeaponBasic> weaponsSet3;


    private bool isFiring;
	// Use this for initialization
	void Start () {

        if (weapons == null) weapons = new List<WeaponBasic>();
        if( weapons.Count <= 0)
        {
            weapons.AddRange(this.GetComponentsInChildren<WeaponBasic>());
        }
	}
	
	// Update is called once per frame
	void Update () {

	}
    
    public void fireAllWeapons()
    {
        foreach (WeaponBasic weap in weapons)
        {
            weap.FireWeapon();
        }
    }

    public void fireWeaponsSet(int set)
    {
        switch(set){

            case 1:
                foreach (WeaponBasic weap in weaponsSet1)
                {
                    weap.FireWeapon();
                }
                break;

            case 2:
                foreach (WeaponBasic weap in weaponsSet2)
                {
                    weap.FireWeapon();
                }
                break;

            case 3:
                foreach (WeaponBasic weap in weaponsSet3)
                {
                    weap.FireWeapon();
                }
                break;

            default:
                foreach (WeaponBasic weap in weaponsSet1)
                {
                    weap.FireWeapon();
                }
                break;

        }


    }
}

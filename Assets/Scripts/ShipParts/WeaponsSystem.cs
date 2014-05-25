﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponsSystem : MonoBehaviour {

    //[HideInInspector]
    public List<WeaponBasic> weapons;//= new List<BasicShipPart>();

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
}

﻿using UnityEngine;
using System.Collections;

public class StageManager : MonoBehaviour {
    //Here is a private reference only this class can access
    private static StageManager _instance;
    //public Texture2D skyboxMat;

    protected float StageTimer = 0;


    protected float[] SpawnTimingsSeconds;


    //This is the public reference that other classes will use
    public static StageManager instance
    {
        get
        {
            //If _instance hasn't been set yet, we grab it from the scene!
            //This will only happen the first time this reference is used.
            if (_instance == null)
                _instance = GameObject.FindObjectOfType<StageManager>();
            return _instance;
        }
    }

	// Use this for initialization
	void Awake() {

        StageTimer = 0;

        ShipCoreInfoStore.instance.buildShipNow = true;

	}
	
	// Update is called once per frame
	void Update () {
	    //skyboxMat.x
        StageTimer += Time.deltaTime;
	}


    
    public void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - 
                         ShipCoreInfoStore.instance.ShipCore.health / ShipCoreInfoStore.instance.ShipCore.maxHealth * 100, 
                         0, 
                         ShipCoreInfoStore.instance.ShipCore.health / ShipCoreInfoStore.instance.ShipCore.maxHealth * 100, 20), 
                ShipCoreInfoStore.instance.ShipCore.health.ToString());

        GUI.Box(new Rect(Screen.width / 2 - 50, 0, 100, 20),
                (StageTimer/60).ToString("#00.") +" : "+(StageTimer%60).ToString("#00."));
    }
}

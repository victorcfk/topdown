using UnityEngine;
using System.Collections.Generic;

public class StageManager : MonoBehaviour {
    //Here is a private reference only this class can access
    private static StageManager _instance;

    public static int combatStageToLoad = 1;

    protected float stageTimer = 0;
	protected float secToNextWave =0 ;
    public int upcomingWave = 0;

    public List<GameObject> spawnWaves;
    public float[] spawnTimingsSeconds;

    public GUIStyle style;
    public ParticleSystem pg;

    protected float healthGain = 0;

    public bool isBuildingStage { get { return Application.loadedLevel == 0; } }
    public bool isCurrentWaveClear 
    { 
        get 
        {
            //print("waver " + (upcomingWave - 1));

            if((upcomingWave - 1) < 0 )
                return true;


            return (spawnWaves [upcomingWave - 1].transform.childCount <= 0); 
        } 
    }
    public bool isFinalWave { get { return upcomingWave >= spawnWaves.Count; } }

    public bool areAllWavesClear
    {
        get
        {
            //All waves must have no child game objects
            for (int i=0; i <spawnWaves.Count; i++)
            {
                if( spawnWaves[i].transform.childCount > 0 )
                    return false;
            }
            return true;
        }
    }


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

     
	}

    void Start()
    {
        stageTimer = 0;
        upcomingWave = 0;
        
        ShipCoreInfoStore.instance.buildShipNow = true;

        if (!isBuildingStage)   ShipCoreInfoStore.instance.startFlightNow =true;
    }

    void WarnOfNextWave()
    {
        if(!isFinalWave && secToNextWave < 7)
        {
            for (int j=0; j <spawnWaves[upcomingWave].transform.childCount; j++)
                pg.Emit(spawnWaves [upcomingWave].transform.GetChild(j).position, Vector3.zero, 7 - secToNextWave, 0.1f, Color.red);
        }
    }
	
    void checkForCheats()
    {
        if (Input.GetKeyDown(KeyCode.End))
        {
            ++combatStageToLoad;
            Application.LoadLevel(0);
        }
    }

    void fastForwardToUpcomingWave( float secondsToSpawn )
    {
        healthGain = (int)((secToNextWave -secondsToSpawn)/2);
        PlayerController.instance.playerUnit.health += healthGain;
            
        stageTimer = (spawnTimingsSeconds[upcomingWave] - secondsToSpawn);

    }

    void WaveSpawning()
    {
        for (int i=0; i <spawnTimingsSeconds.Length; i++)
        {
            if(stageTimer >= spawnTimingsSeconds[i])
            {
                ++upcomingWave;
                
                //print("Spawnaaaaaaa!!!!!! " +SpawnTimingsSeconds[i]);
                spawnTimingsSeconds[i] = Mathf.Infinity;
                spawnWaves[i].SetActive(true);
            }
        }
    }

    void loadNextStage()
    {
        ++combatStageToLoad;
        Application.LoadLevel(0);
    }

	// Update is called once per frame
	void Update () {

        if (isBuildingStage)
            return;

        stageTimer += Time.deltaTime;

        if( !isFinalWave )
            secToNextWave = spawnTimingsSeconds[upcomingWave] - stageTimer;

        //=====================================================
		
        WarnOfNextWave();

        //=====================================================

        WaveSpawning();

        //=====================================================
       
        if (isCurrentWaveClear)
        {
            //++upcomingWave;

            if (secToNextWave > 5)
            {
                fastForwardToUpcomingWave(5);
            }
        }
       
        if(areAllWavesClear)
        {
           loadNextStage();
        }

        checkForCheats();
        //=====================================================
	}

    public void OnGUI()
    {
        if (Application.loadedLevel == 0)
        {
            if (GUI.Button(
            new Rect(Screen.width - 80,
                 20,
                 60,
                 30), "Start"))
            {    
                ShipCoreInfoStore.instance.savePartsAndNewStage = true;
                Application.LoadLevel(combatStageToLoad);
            }
        } 
        else
        {
            if (PlayerController.instance.playerUnit.isPlayerDead)
            {
                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 15, 100, 30), "Remake Ship"))
                    Application.LoadLevel(0);

                if (GUI.Button(new Rect(Screen.width / 2 - 50, 3 * Screen.height / 4 - 15, 100, 30), "Restart level"))
                    Application.LoadLevel(Application.loadedLevelName);
            } 
            else
            {
                GUI.Box(new Rect((Screen.width - 
                    PlayerController.instance.playerUnit.health / PlayerController.instance.playerUnit.maxHealth * 200) / 2, 
                                 Screen.height - Screen.height/20, 
                         PlayerController.instance.playerUnit.health / PlayerController.instance.playerUnit.maxHealth * 200, Screen.height/20), 
                PlayerController.instance.playerUnit.health.ToString(), style);

//            GUI.Box(new Rect(Screen.width / 2 - 50, 0, 100, 20),
//                             "Time to next wave");

                GUI.Box(new Rect(Screen.width / 2 - 60, 0, 120, 40),
                    "Time to next wave \n" + (secToNextWave / 60).ToString("#00.") + " : " + (secToNextWave % 60).ToString("#00."));

                if (isCurrentWaveClear)
                    GUI.Box(new Rect(Screen.width / 2 - 150, 3 * Screen.height / 4 - 15, 300, 30), "Wave Clear, Health regain: " + healthGain);

            }
        }
    }
}

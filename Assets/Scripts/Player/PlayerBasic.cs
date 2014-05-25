using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//[RequireComponent(typeof(EngineSystem))]
[RequireComponent(typeof(WeaponsSystem))]


public class PlayerBasic : UnitBasic {

	public bool luft;

    public List<BasicShipPart> attachedParts;// = new List<BasicShipPart>();       //The list of attached, parts, may contain weapons. Temp will be initialised by hand
    [HideInInspector]
    public LuftEngineSystem luftEngineSystem;
	[HideInInspector]
	public EngineSystem engineSystem;
    [HideInInspector]
    public WeaponsSystem weaponsSystem;

    [HideInInspector]
    public float horizontalMoveInput;
    [HideInInspector]
    public float verticalMoveInput;
    [HideInInspector]
    public bool isFiringInputActive;
    [HideInInspector]
    public Vector3 LookAtVector;


    // Use this for initialization
	void Start () {
        if (attachedParts == null) attachedParts = new List<BasicShipPart>(); 
        if (engineSystem == null) engineSystem = GetComponent<EngineSystem>();
		if (luftEngineSystem == null) luftEngineSystem = GetComponent<LuftEngineSystem>();
        if (weaponsSystem   == null) weaponsSystem = GetComponent<WeaponsSystem>();
        
        initAllParts();
        initWeaponsSystem();
        initEngineSystem();
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) DestroySelf();

        //print(horizontalMoveInput);
        //print(verticalMoveInput);

		if (!luft) {
                        engineSystem.horizontalMoveVal = horizontalMoveInput;
                        engineSystem.verticalMoveVal = verticalMoveInput;
                        engineSystem.LookAtVector = LookAtVector;
				} else {
						luftEngineSystem.horizontalMoveVal = horizontalMoveInput;
						luftEngineSystem.verticalMoveVal = verticalMoveInput;
				}

        if(isFiringInputActive)
            weaponsSystem.fireAllWeapons();
	}

    protected void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }

    public void initAllParts()
    {
        attachedParts.Clear();
        attachedParts.AddRange(this.GetComponentsInChildren<BasicShipPart>());

        foreach (BasicShipPart part in attachedParts)
                part.shipCore = this;
    }

    public void initWeaponsSystem(){
        WeaponBasic weapon;
        foreach (BasicShipPart part in attachedParts)
        {
            weapon = part.GetComponent<WeaponBasic>();
            if (weapon != null)
                weaponsSystem.weapons.Add(weapon);
        }
        //weaponsSystem.weapons = attachedParts.FindAll(isWeapon);
    }

    public void initEngineSystem()
    {
        if (!luft)
        {
            EngineBasic engine;
            foreach(BasicShipPart part in attachedParts){
                engine = part.GetComponent<EngineBasic>();
                if(engine != null )
                    engineSystem.engines.Add(engine);
            }
            //engineSystem.engines = attachedParts.FindAll(isEngine);
        }
        else
        {
            EngineBasic engine;
            foreach (BasicShipPart part in attachedParts)
            {
                engine = part.GetComponent<EngineBasic>();
                if (engine != null)
                    luftEngineSystem.engines.Add(engine);
            }

            //luftEngineSystem.engines = attachedParts.FindAll(isEngine);

        }
    }

    //utility predicate to find all weapons in a list of ship parts
    private bool isWeapon(BasicShipPart part)
    {

        if (part.GetComponent<WeaponBasic>() != null)   //has weapon
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    
    private bool isEngine(BasicShipPart part)
    {
        if (part.GetComponent<EngineBasic>() != null)
            //(part is EngineBasic)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override void ApplyDamage(float Damage)
    {
        print(name + "got hit"); 
        health -= Damage;

        if (health <= 0) DestroySelf();
    }

    public override void DestroySelf(){
        //ResetGame();
        Invoke("ResetGame", 2);

        gameObject.SetActive(false);
    }

    public void OnGUI()
    {
        GUI.Box(new Rect(Screen.width - health / maxHealth * 100, 0, health / maxHealth * 100, 20), health.ToString());
    }

}


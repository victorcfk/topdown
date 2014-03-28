using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(EngineSystem))]
[RequireComponent(typeof(WeaponsSystem))]


public class PlayerBasic : MonoBehaviour {

    public float health = 2;

    public List<BasicShipPart> attachedParts;// = new List<BasicShipPart>();       //The list of attached, parts, may contain weapons. Temp will be initialised by hand
    private EngineSystem engineSystem;
    private WeaponsSystem weaponsSystem;

    public float acceleration = 0.5f;
    public float maxSpeed = 10.0f;

    [HideInInspector]
    public float horizontalMoveInput;
    [HideInInspector]
    public float verticalMoveInput;
    [HideInInspector]
    public bool isFiringInputActive;

    // Use this for initialization
	void Start () {
        if (engineSystem == null) engineSystem = GetComponent<EngineSystem>();
        if (weaponsSystem == null) weaponsSystem = GetComponent<WeaponsSystem>();
        if (attachedParts == null) attachedParts = new List<BasicShipPart>();

        if (attachedParts.Count <= 0)
        {
            attachedParts.AddRange(this.GetComponentsInChildren<BasicShipPart>());

            foreach (BasicShipPart part in attachedParts)
                part.shipCore = this;
        }

        initWeaponsSystem();
        initEngineSystem();
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0) DestroySelf();

        //print(horizontalMoveInput);
        //print(verticalMoveInput);

        engineSystem.horizontalMoveVal = horizontalMoveInput;
        engineSystem.verticalMoveVal = verticalMoveInput;

        if(isFiringInputActive)
            weaponsSystem.fireAllWeapons();
	}

    protected void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevelName);
    }


    protected void initWeaponsSystem(){
        weaponsSystem.weapons = attachedParts.FindAll(isWeapon);
    }

    protected void initEngineSystem()
    {
        engineSystem.engines = attachedParts.FindAll(isEngine);
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

        if (part is EngineBasic)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    public virtual void ApplyDamage(float Damage){
        print(name + "got hit"); 
        health -= Damage;

        if (health <= 0) DestroySelf();
    }

    protected virtual void DestroySelf(){
        //ResetGame();
        Invoke("ResetGame", 2);

        gameObject.SetActive(false);
    }

}


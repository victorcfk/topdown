using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent(typeof(EngineSystem))]
[RequireComponent(typeof(WeaponsSystem))]


public class BasicPlayer : MonoBehaviour {

    public float Health = 2;

    private EngineSystem engineSystem;
    private WeaponsSystem weaponsSystem;

    public float acceleration = 0.5f;
    public float maxSpeed = 10.0f;

    public List<BasicShipPart> attachedParts;// = new List<BasicShipPart>();       //The list of attached, parts, may contain weapons. Temp will be initialised by hand

    [HideInInspector]
    public float horizontalMoveInput;
    [HideInInspector]
    public float verticalMoveInput;
    [HideInInspector]
    public bool isFiringInputActive;

    // Use this for initialization
	void Start () {
        engineSystem = GetComponent<EngineSystem>();
        weaponsSystem = GetComponent<WeaponsSystem>();

        initWeaponsSystem();
	}
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0) DestroySelf();

        //print(horizontalMoveInput);
        //print(verticalMoveInput);


        engineSystem.horizontalMoveVal = horizontalMoveInput;
        engineSystem.verticalMoveVal = verticalMoveInput;

        if(isFiringInputActive)
            weaponsSystem.fireAllWeapons();
	}

    protected void OnCollisionEnter(Collision collision)
    {
        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            Health -= collision.gameObject.GetComponent<ProjectileBasic>().damage;

        }*/
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
            //.weapons = attachedParts.FindAll(isWeapon);
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

        if (part is ShipModifierPart)
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
        Health -= Damage;

        if (Health <= 0) DestroySelf();
    }

    protected virtual void DestroySelf(){
                    //ResetGame();
            Invoke("ResetGame", 2);

            gameObject.SetActive(false);
    }

}


using UnityEngine;
using System.Collections;

public class EnemyAIBasic : MonoBehaviour
{
    public EnemyBasic EnemyUnit;
    public EnemyMoveModuleBasic MoveModule;
    public WeaponBasic Weapon;

    protected bool isWithinRangeOfTarget
    { get { return Weapon.projectileRange*Weapon.projectileRange >= Vector3.SqrMagnitude(Target.transform.position - transform.position);}}

    protected GameObject _Target;
    public GameObject Target 
    { 
        get {
            if (_Target == null)
                _Target = FindObjectOfType<PlayerBasic>().gameObject;
            
            return _Target;
        }
        
        set { _Target = value; }
    }
    
    
    protected virtual void Start()
    {
        if (MoveModule == null)
            MoveModule = GetComponent<EnemyMoveModuleBasic>();

        if (EnemyUnit == null)
            EnemyUnit = GetComponent<EnemyBasic>(); 

        if (MoveModule == null)
            MoveModule = GetComponent<EnemyMoveModuleBasic>();

        if (Weapon == null)
            Weapon = GetComponent<WeaponBasic>();
    }
    
    // Update is called once per frame
    public virtual new void Update()
    {
        if (Target != null) 
            HomeTowardsPoint(Target.transform.position);
        
        if (Weapon != null && isWithinRangeOfTarget)
        {
            //print(Vector3.Magnitude(Target.transform.position - transform.position));

            Weapon.FireWeapon();

        }
        
    }
    
    public void HomeTowardsPoint(Vector3 position)
    {
        MoveModule.MoveToPoint(position);
    }
    
    protected void HomeTowardsTarget(GameObject target)
    {
        HomeTowardsPoint(target.transform.position);
    }
    
}

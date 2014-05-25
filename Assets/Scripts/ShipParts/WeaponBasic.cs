using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class WeaponBasic : MonoBehaviour
{
    /*Commenting guidelines, left here for posterity
    /// <summary>
    /// Rotates the vehicle with the specified amount.
    /// </summary>
    /// <param name="Degrees">The amount, in degrees, to rotate the vehicle.</param>
    /// <returns>A Boolean value indicating a successful check.</returns>
    */
    public ProjectileBasic projectilePrefab;                       //The projectile the weapon should fire
//    public string projectileName;
    
    public virtual Vector3 fireLocation { get { return transform.position; } }
    public Quaternion fireAngle = Quaternion.LookRotation(Vector3.forward);
    public Vector3 fireDirection;
    //public GameObject target;                           //Any possible Homing/Final Targets

    [HideInInspector]
    public bool isOverrideProjectileSpeed = true;      //Should we use our own speed to apply to the projectile
    public float projectileSpeed = 10;                   //Speed to use when overrriding the projectile's speed    

    [HideInInspector]
    public bool isOverrideProjectileDamage = true;      //Should we use our own speed to apply to the projectile
    public float projectileDamage = 10;                   //Speed to use when overrriding the projectile's speed    

    [HideInInspector]
    public bool isOverrideProjectileRange = true;      //Should we use our own speed to apply to the projectile
    public float projectileRange = 10;                   //Speed to use when overrriding the projectile's speed    

    [HideInInspector]
    public bool isOverrideProjectileLifeTime = true;      //Should we use our own speed to apply to the projectile
    public float projectileLifeTime = 10;                   //Speed to use when overrriding the projectile's speed    

    //public bool isFiring = false;                       //Is the weapon currently firing?
    protected float coolDownTimer;
    public float coolDownBetweenShots = 4;              //CoolDown between shots

    [HideInInspector]
    public List<ProjectileBasic> ExistingProjectiles;        //Indicates the list of projectiles already generated by the weapon. Used for cleanup and the like
    
   // Use this for initialization
    protected void Start()
    {
        //this.InvokeRepeating("LaunchProjectile", 1, 1);

        if (!isOverrideProjectileSpeed)
        {
            projectileSpeed = projectilePrefab.minSpeed;
        }

        if (!isOverrideProjectileDamage)
        {
            projectileDamage = projectilePrefab.damage;
        }

        if (!isOverrideProjectileRange)
        {
            projectileRange = projectilePrefab.range;
        }

        if (!isOverrideProjectileLifeTime)
        {
            projectileLifeTime = projectilePrefab.lifeTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //LaunchProjectile();
        if(coolDownTimer > 0)
            coolDownTimer -= Time.fixedDeltaTime;
                
    }

    /// <summary>
    /// Creates a projectile with properties to be initialised later
    /// </summary>
    /// <returns>The instance of the Projectile.</returns>
    virtual protected ProjectileBasic CreateProjectile(Vector3 spawnLocation, Quaternion initialFacing)
    {
        ProjectileBasic instance = (ProjectileBasic)Instantiate(
            projectilePrefab,
            spawnLocation,
            initialFacing);

        if (isOverrideProjectileDamage) instance.damage = projectileDamage;
        if (isOverrideProjectileRange) instance.range = projectileRange;
        if (isOverrideProjectileSpeed) instance.minSpeed = projectileSpeed;
        if (isOverrideProjectileLifeTime) instance.lifeTime = projectileLifeTime;

        //print(col.radius * 3);
        return instance;
    }

    /// <summary>
    /// Create and Fire off the projectile, assuming it is created at the 
    /// Weapon's position and shares the same forward as the weapon
    /// </summary>
    virtual public void FireWeapon()
    {
        print("imma firing");

        if (coolDownTimer <= 0)
        {
            LaunchProjectile();

            coolDownTimer = coolDownBetweenShots;
        }
        else
            return;
    }

    /// <summary>
    /// Create and Fire off the projectile, assuming it is created at the 
    /// Weapon's position and shares the same forward as the weapon
    /// </summary>
    virtual protected void LaunchProjectile()
    {
        fireAngle = transform.rotation;
        fireDirection = transform.forward;

        ProjectileBasic instance = CreateProjectile(gameObject.transform.position, fireAngle);
        
        instance.rigidbody.velocity = (fireDirection).normalized * projectileSpeed;
    }

    public void clearUpProjectile(ProjectileBasic projectile)
    {
        //print("Clean: " + proj.transform.position);
        ExistingProjectiles.Remove(projectile);
        //ExistingProjectiles.IndexOf(proj);
    }
}
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponProjectileSingular : WeaponBasic
{
    /*Commenting guidelines, left here for posterity
    /// <summary>
    /// Rotates the vehicle with the specified amount.
    /// </summary>
    /// <param name="Degrees">The amount, in degrees, to rotate the vehicle.</param>
    /// <returns>A Boolean value indicating a successful check.</returns>
    */

    //Burst Fire functionality
    //============================
    public bool isBurstFire = false;
    public float delayWithinBurst = 0.2f;       //Time spacing between each projectiles firing in burst
    public int numOfProjectilesInBurst = 3;
    //============================

    public GameObject target;

    //override
    public override void FireWeapon()
    {

        if (coolDownTimer <= 0)
        {
            if (isBurstFire)
                LaunchBurstOfProjectiles();
            else
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
    protected override void LaunchProjectile()
    {

        if (projectilePrefab != null)
        {
            fireAngle = transform.rotation;
            fireDirection = transform.forward;//target.transform.position - gameObject.transform.position;//transform.forward;

            GameObject instance = CreateProjectile(gameObject.transform.position, fireAngle);
            instance.rigidbody.velocity = (fireDirection).normalized * projectileSpeed;
            instance.GetComponent<ProjectileSingular>().HomingTarget = target;
        }
    }
    /// <summary>
    /// Create and Fire off a number of projectiles, assuming it is created at the 
    /// Weapon's position and shares the same forward as the weapon
    /// </summary>
    protected void LaunchBurstOfProjectiles()
    {
        //print("burst");

        for(int i =0; i<numOfProjectilesInBurst; i++){
            Invoke("LaunchProjectile", i * delayWithinBurst);
        }
    }

}
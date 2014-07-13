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

    public override Vector3 fireLocation { get { return transform.position;  } }
    public float angleDeviation = 0;

    public bool isTeleportProjectile =false;

    //Burst Fire functionality
    //============================
    public bool isBurstFire = false;
    public float delayWithinBurst = 0.2f;       //Time spacing between each projectiles firing in burst
    public int numOfProjectilesInBurst = 3;
    //============================

    //Multi shot functionality
    //============================
//    public bool isMultiShot = false;
//    public float FiringAngle = 90;       //Time spacing between each projectiles firing in burst
//    public int numOfProjectilesInMultiShot = 3;
    //============================


    //override
    override public void FireWeapon()
    {
        if (coolDownTimer <= 0)
        {
            /// <summary>
            /// Create and Fire off a number of projectiles, assuming it is created at the 
            /// Weapon's position and shares the same forward as the weapon
            /// </summary>
            if (isBurstFire)
            {
                for (int i = 0; i < numOfProjectilesInBurst; i++)
                {
                    Invoke("LaunchProjectile", i * delayWithinBurst);
                }
            }
            else
            {
                LaunchProjectile();
            }

            coolDownTimer = coolDownBetweenShots;
        }
        else
            return;
    }

    public override void FireWeapon(GameObject Target)
    {
        this.target = Target;
        FireWeapon();
    }


    /// <summary>
    /// Create and Fire off the projectile, assuming it is created at the 
    /// Weapon's position and shares the same forward as the weapon
    /// </summary>
    protected override void LaunchProjectile()
    {
        //print("centar"+projectilePrefab.getCenter());
        if (projectilePrefab != null)
        {

            for(int i=0; i < LaunchLocation.Length; i++){

                ProjectileBasic instance = CreateProjectile(LaunchLocation[i].position,LaunchLocation[i].rotation);

                fireDirection = Quaternion.AngleAxis(Random.Range(-angleDeviation,angleDeviation),Vector3.forward) * LaunchLocation[i].forward;

                instance.rigidbody.velocity = fireDirection * projectileSpeed;

                instance.target = target;

            }
        }
    }
}
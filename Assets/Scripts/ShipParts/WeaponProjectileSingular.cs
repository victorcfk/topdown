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
    public bool isMultiShot = false;
    public float FiringAngle = 90;       //Time spacing between each projectiles firing in burst
    public int numOfProjectilesInMultiShot = 3;
    //============================


    public GameObject target;

    //override
    public override void FireWeapon()
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
                if (isMultiShot)
                {
                    for (int i = 0; i < numOfProjectilesInMultiShot; i++)
                    {
                        LaunchProjectile(
                        Quaternion.AngleAxis(
                            -FiringAngle / 2 +
                            (FiringAngle / (numOfProjectilesInMultiShot - 1) * i), Vector3.forward) *
                            transform.forward
                        );

                    }
                }
                else
                    LaunchProjectile();
                //launchAtTarget();
            }

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
        //print("centar"+projectilePrefab.getCenter());
        if (projectilePrefab != null)
        {
            fireAngle = transform.rotation;
            //print("ang" + fireAngle);
            //fireAngle.Set(fireAngle.x, fireAngle.y , fireAngle.z, fireAngle.w);
            //print("ang"+fireAngle);
            
            fireDirection = transform.forward;//target.transform.position - gameObject.transform.position;//transform.forward;
            fireDirection = Quaternion.AngleAxis(Random.Range(-angleDeviation,angleDeviation),Vector3.forward) * fireDirection;

            ProjectileBasic instance = CreateProjectile(fireLocation, fireAngle);
            instance.rigidbody.velocity = (fireDirection).normalized * projectileSpeed;
            instance.GetComponent<ProjectileSingular>().HomingTarget = target;


            //if(activationPS != null)
            //    activationPS.Play();
        }
    }

    protected void LaunchProjectile(Vector3 targetDir)
    {
        //print("centar"+projectilePrefab.getCenter());
        if (projectilePrefab != null)
        {
            fireAngle = transform.rotation;

            fireDirection = targetDir;
                //targetPos - transform.position;//target.transform.position - gameObject.transform.position;//transform.forward;
            //fireDirection = Quaternion.AngleAxis(Random.Range(-angleDeviation, angleDeviation), Vector3.forward) * fireDirection;

            ProjectileBasic instance = CreateProjectile(fireLocation, fireAngle);
            instance.rigidbody.velocity = (fireDirection).normalized * projectileSpeed;
            instance.GetComponent<ProjectileSingular>().HomingTarget = target;


            //if(activationPS != null)
            //    activationPS.Play();
        }
    }



}
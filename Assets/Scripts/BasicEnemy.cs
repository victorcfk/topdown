using UnityEngine;
using System.Collections;

[RequireComponent (typeof(WeaponBasic))]

public class BasicEnemy : MonoBehaviour {

    public float Health =2 ;
    public float acceleration = 1.1f;

    public GameObject target;
    public float maxSpeed = 10;

    [HideInInspector]
    public WeaponBasic ShootScript;

	// Use this for initialization
	void Start () {
        ShootScript = this.GetComponent<WeaponBasic>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Health <= 0) DestroySelf();

        HomeTowardsTarget(target);
	}


    virtual protected void OnCollisionEnter(Collision collision)
    {
        //print("colled");

        /*
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
          //  print("colledisbull");
            Health -= collision.gameObject.GetComponent<ProjectileBasic>().damage;

        }*/
    }

    protected virtual void ApplyDamage(float Damage)
    {
        print(name+"got hit");
        Health -= Damage;

        if (Health <= 0)    DestroySelf();
    }

    protected void CleanUpProjectiles(){
        foreach (GameObject proj in ShootScript.ExistingProjectiles)
            Destroy(proj);
    }

    protected virtual void DestroySelf()
    {
        //ResetGame();
        CleanUpProjectiles();

        gameObject.SetActive(false);
    }


    protected void HomeTowardsTarget(GameObject target)
    {
        rigidbody.velocity += acceleration * (target.transform.position - this.gameObject.transform.position).normalized;
    }


}

using UnityEngine;
using System.Collections;

public class EnemyPursuit : EnemyBasic
{
    public float PursuitAcceleration = 1.1f;
    public float MaxPursuitSpeed = 10;
    public float StartPursuitSpeed = 10;

    public GameObject target;

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        if(target != null)  HomeTowardsTarget(target);
    }

    protected void HomeTowardsTarget(GameObject target)
    {
        rigidbody.velocity = StartPursuitSpeed * (target.transform.position - this.gameObject.transform.position).normalized;
        //rigidbody.velocity += PursuitAcceleration * (target.transform.position - this.gameObject.transform.position).normalized;

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxPursuitSpeed);


        gameObject.transform.LookAt(target.transform);
            //.SetLookRotation(rigidbody.velocity, Vector3.up);
    }

}

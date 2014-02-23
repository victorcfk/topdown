using UnityEngine;
using System.Collections;

public class EnemyPursuit : EnemyBasic
{
    public float PursuitAcceleration = 1.1f;
    public float MaxPursuitSpeed = 10;
    public float StartPursuitSpeed = 10;

    public GameObject target;

    public void Start()
    {
        InvokeRepeating("thing", 1, 5);
    }

    Vector3 g;

    // Update is called once per frame
    public void Update()
    {
        //base.Update();

        //if(target != null)  HomeTowardsTarget(target);

        //waito(1000);

        //HomeTowardsPoint(g);
    }

    public void thing()
    {
        g = gopoint();

        mm.MoveToPoint(g);
        
        Debug.DrawLine(transform.position, g, Color.red, 2, false);
    }

    protected void HomeTowardsTarget(GameObject target)
    {
        rigidbody.velocity = StartPursuitSpeed * (target.transform.position - this.gameObject.transform.position).normalized;
        //rigidbody.velocity += PursuitAcceleration * (target.transform.position - this.gameObject.transform.position).normalized;

        rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxPursuitSpeed);


        gameObject.transform.LookAt(target.transform);
            //.SetLookRotation(rigidbody.velocity, Vector3.up);
    }

    public void HomeTowardsPoint(Vector3 pos)
    {

        //rigidbody.velocity = StartPursuitSpeed * (pos - this.gameObject.transform.position).normalized;
        ////rigidbody.velocity += PursuitAcceleration * (target.transform.position - this.gameObject.transform.position).normalized;

        //rigidbody.velocity = Vector3.ClampMagnitude(rigidbody.velocity, MaxPursuitSpeed);
        
        //gameObject.transform.LookAt(pos);
        //.SetLookRotation(rigidbody.velocity, Vector3.up);
        CharacterController cc;
        cc = this.GetComponent<CharacterController>();

        if (!HasReachedPoint(pos, 1))
            cc.Move(StartPursuitSpeed * (pos - transform.position).normalized);// rigidbody.velocity = 
        else
        {
            print("re");
            print(cc.velocity);// = Vector3.zero;
        }


    }

    public bool HasReachedPoint(Vector3 Point, float MinDist)
    {
        return (transform.position - Point).magnitude < MinDist * MinDist;
    }


    public Vector3 gopoint()
    {
        return this.GetComponent<FindRandomSurroundingPoint>().GetRandomPointWithRayCast(target.transform.position, 10);
    }

    IEnumerator waito(float sec)
    {
        //transform.
        yield return new WaitForSeconds(sec);
        //renderer.enabled = false;
    }

}

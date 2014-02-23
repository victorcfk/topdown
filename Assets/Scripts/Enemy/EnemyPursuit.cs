using UnityEngine;
using System.Collections;

public class EnemyPursuit : EnemyBasic
{
    public float PursuitAcceleration = 1.1f;
    public float MaxPursuitSpeed = 10;
    public float StartPursuitSpeed = 10;

    public GameObject target;
    Vector3 g;

    public void Start()
    {
        //InvokeRepeating("thing", 1, 0.5f);
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();

        if(target != null)  HomeTowardsTarget(target);

        //waito(1000);

        //HomeTowardsPoint(g);
    }

    protected void HomeTowardsTarget(GameObject target)
    {
        moveModule.MoveToPoint(target.transform.position);
    }

    public void HomeTowardsPoint(Vector3 position)
    {
        moveModule.MoveToPoint(position);
    }

    public void thing()
    {
        g = gopoint();

        moveModule.MoveToPoint(g);

        Debug.DrawLine(transform.position, g, Color.red, 2, false);
    }

    public Vector3 gopoint()
    {
        return this.GetComponent<FindRandomSurroundingPoint>().GetRandomPointWithRayCast(target.transform.position, 10);
    }

}

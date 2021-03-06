using UnityEngine;
using System.Collections;

public class EnemyPursuitAI : EnemyBasic
{

    // Update is called once per frame
    public virtual new void Update()
    {
        base.Update();

        if (target != null) HomeTowardsPoint(target.transform.position);

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

}

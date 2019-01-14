﻿using UnityEngine;
using System.Collections;

public class EnemyFlankAI : EnemyBasic {

    Vector3 temp;
    Vector3 vec3holder;
    RaycastHit raycasthitholder;

    public float flankDistance = 10;
    public LayerMask collisionlayermask;// = ~(1 <<10);

    public float destinationChangeInterval = 1.5f;

    // Use this for initialization
    protected override void Start()
    {
        InvokeRepeating("getToRandomSurroundingPoint", 1, destinationChangeInterval);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

    }

    /// <summary>
    /// We attempt to raycast in a direction from a point, if nothing is hit, we take the point on the ring. Else, we take the point on the wall
    /// </summary>
    /// <param name="center"></param>
    public Vector3 GetRandomPointWithRayCast(Vector3 center, float dist)
    {
        vec3holder = (Vector3)Random.insideUnitCircle;

        // print(temp);

        //Do we raycast to hit something?
        if (Physics.Raycast(center, vec3holder, out raycasthitholder, dist, collisionlayermask))
        {//Yes. Take point on wall
            print("coll");
            return raycasthitholder.point;
        }
        else
        {
            return vec3holder.normalized * dist + center;
        }
    }

    /// <summary>
    /// We attempt to raycast in a direction from a point, if nothing is hit, we take the point on the ring. Else, we take the point on the wall
    /// </summary>
    /// <param name="center"></param>
    public Vector3 GetRandomPointWithSphereCast(Vector3 center, float dist, float sphereRadius)
    {
        vec3holder = (Vector3)Random.insideUnitCircle;

        // print(temp);

        //Do we raycast to hit something?
        if (Physics.SphereCast(center, sphereRadius, vec3holder, out raycasthitholder, dist, collisionlayermask))
        {//Yes. Take point on wall
            return raycasthitholder.point + (-1 * vec3holder) * sphereRadius * 2; //calculate a dist from the point of collision
        }
        else
        {
            return vec3holder.normalized * dist + center;
        }
    }

    public void getToRandomSurroundingPoint()
    {
        temp = GetRandomPointWithRayCast(target.transform.position, flankDistance);

        moveModule.MoveToPoint(temp);

        Debug.DrawLine(transform.position, temp, Color.red, 2, false);
    }

    //public Vector3 gopoint()
    //{
    //    return GetRandomPointWithRayCast(target.transform.position, distance);
    //}

}

using UnityEngine;
using System.Collections;

public class BasicShipPart : MonoBehaviour {

    public BasicPlayer shipCore;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void ApplyDamage(float Damage)
    {
        shipCore.ApplyDamage(Damage);
    }
}

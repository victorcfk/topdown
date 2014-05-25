using UnityEngine;
using System.Collections;

public class DamageReceiver : MonoBehaviour {

    public BasicShipPart ParentReceiver;

	// Use this for initialization
	void Start () {
        if (ParentReceiver == null)
            ParentReceiver = this.GetComponent<BasicShipPart>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ApplyDamage(float Damage = 1)
    {
        if (ParentReceiver != null)
            ParentReceiver.ApplyDamage(Damage);
        else
            Debug.LogError("No parent to apply damage to!");
    }


}

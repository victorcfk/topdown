using UnityEngine;
using System.Collections;

public class DestroyOnTime : MonoBehaviour {

    protected float timeOutDuration = 1;
	
    // Use this for initialization
	void Start () 
    {
        timeOutDuration = GetComponent<ParticleSystem>().duration;

        Invoke("DestroySelf", timeOutDuration);
	}
	
    void DestroySelf()
    {
        Destroy(gameObject);
    }

}

using UnityEngine;
using System.Collections;

public class ResetGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetButtonDown("Jump"))
        {
            Application.LoadLevel(Application.loadedLevelName);
        }
	}
}

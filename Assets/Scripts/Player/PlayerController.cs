using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public PlayerBasic player;

    private float horizontalMoveInput;
    private float verticalMoveInput;

    private bool isFiringInputActive;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
    void Update()
    {

        {
            this.horizontalMoveInput = Input.GetAxis("Horizontal");
            this.verticalMoveInput = Input.GetAxis("Vertical");

            this.isFiringInputActive = Input.GetMouseButton(0);
        }

        player.isFiringInputActive = this.isFiringInputActive;
        player.horizontalMoveInput = this.horizontalMoveInput;
        player.verticalMoveInput = this.verticalMoveInput;
    }
}

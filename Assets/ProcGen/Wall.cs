using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wall : MonoBehaviour {

    public List<Gate> gates = new List<Gate>();
    public List<GameObject> wallPieces = new List<GameObject>();

    public List<Room> adjRooms = new List<Room>();

    public Vector3 middle;


}

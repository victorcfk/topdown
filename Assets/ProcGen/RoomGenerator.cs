using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum WALL_TEMPLATE
{
    NO_WALL,
    BLOCKED,
    ONE_OPENINGS,
    TWO_OPENINGS,
    THREE_OPENINGS,    
}

public enum FUNITURE_TEMPLATE
{
    CORRIDOR,
    OPEN,
    CENTRAL,
    SCATTERED,
}

public class RoomGenerator : MonoBehaviour {

    public Vector3 roomCenter;
    public float roomHeight;
    public float roomWidth;

    int roomHeightCount;
    int roomWidthCount;

    public GameObject[] funiture_list;
    public GameObject CornerObj;
    public GameObject CenterObj;

    public Room RoomParent;
    public Wall LeftWallParent;
    public Wall RightWallParent;
    public Wall TopWallParent;
    public Wall BtmWallParent;
    
    Vector3 roomCent;
    
    Vector3 btmLeft;
    Vector3 topLeft;

    Vector3 btmRight;
    Vector3 topRight;

    

    //Template type object.
    
	// Use this for initialization
	void Start () {

        GenerateRoom();

        roomCenter += new Vector3(100, 100, 0);

        GenerateRoom();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.DrawLine(btmLeft, topLeft, Color.red);
        Debug.DrawLine(topLeft, topRight, Color.white);
        Debug.DrawLine(topRight, btmRight, Color.blue);
        Debug.DrawLine(btmRight, btmLeft, Color.green);
	}

    void GenerateRoom()
    {
        if (RoomParent == null)
        {
            RoomParent = this.gameObject.AddComponent<Room>();
        }

        //RoomParent.GetComponent<Room>().walls.Add()

        if (LeftWallParent == null)
        {
            LeftWallParent = (new GameObject("LeftWallParent")).AddComponent<Wall>();
            
            LeftWallParent.transform.position = RoomParent.transform.position;
            LeftWallParent.transform.parent = RoomParent.transform;
        }

        if (RightWallParent == null)
        {
            RightWallParent = (new GameObject("RightWallParent")).AddComponent<Wall>();
            
            RightWallParent.transform.position = RoomParent.transform.position;
            RightWallParent.transform.parent = RoomParent.transform;
        }

        if (TopWallParent == null)
        {
            TopWallParent = (new GameObject("TopWallParent")).AddComponent<Wall>();
            
            TopWallParent.transform.position = RoomParent.transform.position;
            TopWallParent.transform.parent = RoomParent.transform;
        }

        if (BtmWallParent == null)
        {
            BtmWallParent = (new GameObject("BtmWallParent")).AddComponent<Wall>();
            
            BtmWallParent.transform.position = RoomParent.transform.position;
            BtmWallParent.transform.parent = RoomParent.transform;
        }

        DefineBoundaries(roomCenter);

        GenerateWall(btmLeft, topLeft, CornerObj, WALL_TEMPLATE.BLOCKED, 0, LeftWallParent);

        //for (int i = 0; i < 100; i++)
        //{
        //    //PlaceObjectWithinBoxBoundary(btmLeft -  new Vector3(10.5f,0,0), topLeft+new Vector3(10.5f,0,0),CenterObj);
        //    PlaceObjectWithinRadius(btmLeft, 5, CenterObj);
        //}

        //GenerateLineOfPrefabs(topLeft, topRight, CornerObj,TopWallParent);
        GenerateWall(topLeft, topRight, CornerObj, WALL_TEMPLATE.BLOCKED, 0, TopWallParent);
        //GenerateLineOfPrefabs(topRight, btmRight, CornerObj,RightWallParent);
        GenerateWall(topLeft, topRight, CornerObj, WALL_TEMPLATE.NO_WALL, 0, RightWallParent);
        //GenerateLineOfPrefabs(btmRight, btmLeft, CornerObj, BtmWallParent);
        GenerateWall(btmRight, btmLeft, CornerObj, WALL_TEMPLATE.ONE_OPENINGS, 10, BtmWallParent);

        RoomParent.GetComponent<Room>().walls.Add(LeftWallParent.GetComponent<Wall>());
        LeftWallParent.GetComponent<Wall>().adjRooms.Add(RoomParent.GetComponent<Room>());

        RoomParent.GetComponent<Room>().walls.Add(TopWallParent.GetComponent<Wall>());
        TopWallParent.GetComponent<Wall>().adjRooms.Add(RoomParent.GetComponent<Room>());

        RoomParent.GetComponent<Room>().walls.Add(RightWallParent.GetComponent<Wall>());
        RightWallParent.GetComponent<Wall>().adjRooms.Add(RoomParent.GetComponent<Room>());

        RoomParent.GetComponent<Room>().walls.Add(BtmWallParent.GetComponent<Wall>());
        BtmWallParent.GetComponent<Wall>().adjRooms.Add(RoomParent.GetComponent<Room>());


        FillRoom(btmLeft, topRight, FUNITURE_TEMPLATE.SCATTERED, funiture_list, RoomParent);

        LeftWallParent =null;
        RightWallParent = null;
        BtmWallParent = null;
        TopWallParent = null;

    }


    /// <summary>
    /// Assume room is at 0,0, then shift?
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    void DefineBoundaries(Vector3 roomCent)
    {
        btmLeft = roomCent + new Vector3(-roomWidth / 2, -roomHeight / 2, 0);
        topLeft = roomCent + new Vector3(-roomWidth / 2, roomHeight / 2, 0);

        btmRight = roomCent + new Vector3(roomWidth / 2, -roomHeight / 2, 0);
        topRight = roomCent + new Vector3(roomWidth / 2, roomHeight / 2, 0);

        print(roomCent);
        print(btmLeft);
        print(topLeft);
        print(btmRight);
        print(topRight);


        //GameObject.Instantiate(CenterObj, roomCent, CenterObj.transform.rotation);

        //GameObject.Instantiate(CornerObj, btmLeft, CenterObj.transform.rotation);    //bottom Left
        //GameObject.Instantiate(CornerObj, topLeft, CenterObj.transform.rotation);      //top left

        //GameObject.Instantiate(CornerObj, btmRight, CenterObj.transform.rotation);      //bottom right
        //GameObject.Instantiate(CornerObj, topRight, CenterObj.transform.rotation);      //top right

    }

    void GenerateWall(Vector3 start, Vector3 end, GameObject WallBrick, WALL_TEMPLATE wall_temp,int openingBrickSize =0,Wall Parent =null)
    {

        float PrefabRadius = 1;// = Vector3.Distance(WallBrick.collider.bounds.max , WallBrick.collider.bounds.min);

        switch (wall_temp)
        {
            case WALL_TEMPLATE.BLOCKED:
                GenerateLineOfPrefabs(start, end, WallBrick, Parent.gameObject, Parent.wallPieces);
                break;

            case WALL_TEMPLATE.NO_WALL:
                //GenerateLineOfPrefabs(start, end, WallBrick, Parent);
                //do nothing
                break;

            case WALL_TEMPLATE.ONE_OPENINGS:

                Vector3 middleStart = (end + start) / 2 - (end - start).normalized * openingBrickSize * PrefabRadius;
                Vector3 middleEnd = (end + start) / 2 + (end - start).normalized * openingBrickSize * PrefabRadius;

                GenerateLineOfPrefabs(start, middleStart, WallBrick, Parent.gameObject, Parent.wallPieces);
                GenerateLineOfPrefabs(middleEnd, end, WallBrick, Parent.gameObject, Parent.wallPieces);
                break;

            default:
                break;
            //case WALL_TEMPLATE.BLOCKED:
            //    GenerateLineOfPrefabs(start, end, WallBrick, Parent);
            //    break;
        }



    }

    void FillRoom(Vector3 btmLeft, Vector3 topRight, FUNITURE_TEMPLATE fun_temp,GameObject[] funiture_list, Room Parent = null)
    {
        /*
         * public enum FUNITURE_TEMPLATE
{
    CORRIDOR,
    OPEN,
    CENTRAL,
    SCATTERED,
}
         * */

        GameObject tempObj;
        switch (fun_temp)
        {
            case FUNITURE_TEMPLATE.OPEN:
                //do nothing
                break;
            case FUNITURE_TEMPLATE.CENTRAL:

                tempObj = funiture_list[Random.Range(0, funiture_list.Length)];

                Instantiate(tempObj, (btmLeft + topRight) / 2, tempObj.transform.rotation);
                //PlaceObjectWithinBoxBoundary((btmLeft + topRight) / 2, (btmLeft + topRight) / 2, tempObj, Parent);
                break;

            case FUNITURE_TEMPLATE.SCATTERED:

                int num = Random.Range(10, 20);
                for (int i = 0; i < num; i++)
                {
                    tempObj = funiture_list[Random.Range(0, funiture_list.Length)];

                    PlaceObjectWithinBoxBoundary(btmLeft, topRight, tempObj, Parent.gameObject);
                }
                //Instantiate(tempObj, (btmLeft + topRight) / 2, tempObj.transform.rotation);
                break;

            //case FUNITURE_TEMPLATE.CORRIDOR:

            //    int num = Random.Range(10, 20);
            //    for (int i = 0; i < num; i++)
            //    {
            //        tempObj = funiture_list[Random.Range(0, funiture_list.Length)];

            //        PlaceObjectWithinBoxBoundary(btmLeft, topRight, tempObj, Parent);
            //    }
            //    //Instantiate(tempObj, (btmLeft + topRight) / 2, tempObj.transform.rotation);
            //    break;    

            default:
                break;
        }
    }

    void ConnectWallsViaReplacement(Wall a, Wall b)
    {
        b.adjRooms[0].walls.Add(a);
        b.adjRooms[0].walls.Remove(b);

        Destroy(b.gameObject);
    }

    void ConnectWallsViaCorridor(Wall a, Wall b)
    {

    }


    void GenerateLineOfPrefabs(Vector3 start, Vector3 end, GameObject WallBrick, GameObject Parent =null, List<GameObject> ReferenceList =null)
    {
        float PrefabRadius;// = Vector3.Distance(WallBrick.collider.bounds.max , WallBrick.collider.bounds.min);

        //print("PrefabRadius: " + PrefabRadius);

        //print(WallBrick.GetComponent<MeshFilter>().mesh.bounds.center);
        //print(WallBrick.GetComponent<MeshFilter>().mesh.bounds.extents);
        //print(WallBrick.GetComponent<MeshFilter>().mesh.bounds.max);
        //print(WallBrick.GetComponent<MeshFilter>().mesh.bounds.min);

        PrefabRadius = 1;
        print("PrefabRadius: " + PrefabRadius);

        int numOfPrefabsToGen = (int)(Vector3.Distance(start, end) / PrefabRadius);

        print("numOfPrefabsToGen: " + numOfPrefabsToGen);

        Vector3 DirOfGen = (end - start).normalized;
        Vector3 pointer = start;

        GameObject temp;
        for (int i = 0; i < numOfPrefabsToGen; i++)
        {

            temp= (GameObject)Instantiate(WallBrick, pointer, WallBrick.transform.rotation);

            if (Parent != null)
                temp.transform.parent = Parent.transform;

            if (ReferenceList != null)
                ReferenceList.Add(temp);

            pointer += (DirOfGen * PrefabRadius);
        }

    }

    /// <summary>
    /// Place objects within a AABB, still requires defensive prog work
    /// </summary>
    /// <param name="btmLeft"></param>
    /// <param name="topRight"></param>
    /// <param name="obj"></param>
    void PlaceObjectWithinBoxBoundary(Vector3 btmLeft, Vector3 topRight, GameObject obj, GameObject Parent = null)
    {
        float PrefabRadius = 2;

        float CorrectedTopRightX = topRight.x - PrefabRadius;
        float CorrectedTopRightY = topRight.y - PrefabRadius;

        float CorrectedBtmLeftX = btmLeft.x + PrefabRadius;
        float CorrectedBtmLeftY = btmLeft.y + PrefabRadius;

        if (CorrectedTopRightX < CorrectedBtmLeftX)
            CorrectedTopRightX = CorrectedBtmLeftX = (topRight.x + topLeft.x)/2;

        if (CorrectedTopRightY < CorrectedBtmLeftY)
            CorrectedTopRightY = CorrectedBtmLeftY = (topRight.y + topLeft.y) / 2;

        float xPos = Random.Range(CorrectedBtmLeftX, CorrectedTopRightX);
        float yPos = Random.Range(CorrectedBtmLeftY, CorrectedTopRightY);

        //GameObject.Instantiate(obj, new Vector3(xPos, yPos, 0), obj.transform.rotation);

        if (Parent == null)
            Instantiate(obj, new Vector3(xPos, yPos, 0), obj.transform.rotation);
        else
            ((GameObject)Instantiate(obj, new Vector3(xPos, yPos, 0), obj.transform.rotation)).transform.parent = Parent.transform;
            
    }

    /// <summary>
    /// Place objects within a circle lying on the xy plane
    /// </summary>
    /// <param name="center">Center Of Circle</param>
    /// <param name="radius">Radius Of Circle</param>
    /// <param name="obj">the object</param>
    void PlaceObjectWithinRadius(Vector3 center, float radius, GameObject obj, GameObject Parent = null)
    {
        if (Parent == null)
            Instantiate(obj, Vec2to3(Random.insideUnitCircle * radius) + center, obj.transform.rotation);
        else
            ((GameObject)Instantiate(obj, Vec2to3(Random.insideUnitCircle * radius) + center, obj.transform.rotation)).transform.parent = Parent.transform;
    }

    static Vector3 Vec2to3(Vector2 val)
    {
        return new Vector3(val.x, val.y);
    }

}

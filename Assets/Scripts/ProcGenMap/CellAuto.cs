using UnityEngine;
using System.Collections;

public class CellAuto : MonoBehaviour {


    public GameObject itemOnGrid;

	//public ruleset[256] configRule;

	// Use this for initialization
	void Start () {
		MapHandler m = new MapHandler(60,30,45);
		m.MakeCaverns();

		PopulateFromGrid(m);


		Debug.Log(m.MapToString());
	}


	public void PopulateFromGrid(MapHandler m)
	{	
		for(int column=0,row=0; row < m.MapHeight; row++ ) {
			for( column = 0; column < m.MapWidth; column++ )
			{
				if(m.Map[column,row]== 1)
				{
					Instantiate(itemOnGrid,new Vector3(-column,-row),Quaternion.identity);
				}
			}
		}
	}

}

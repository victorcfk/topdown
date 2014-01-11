using UnityEngine;
using System.Collections;

public class DrawSwapUI : MonoBehaviour {

	public bool drawSwapUINow;

    public float outerCircleRadius;
    public float innerCircleRadius;

    public Mesh sphereMesh;
    public Material innerCircleMaterial;
    public Material outerCircleMaterial;

    public Camera UICamera;

	// Use this for initialization
	void Start () {
		//drawGizmoNow = false;
	}
	
	// Update is called once per frame
	void Update () {

		//Gizmos.DrawWireSphere(gameObject,10);


        //defMaterial.renderQueue = 0;
        //this.GetComponent<MeshRenderer>().material.renderQueue = 100;

        if (drawSwapUINow)
        {
            DrawUICircle(gameObject.transform.position, outerCircleRadius, outerCircleMaterial);
            DrawUICircle(gameObject.transform.position, innerCircleRadius, innerCircleMaterial);
        }
            
	
	}

    void DrawUICircle(Vector3 center, float radius,Material material)
    {
        
        Graphics.DrawMesh(
            sphereMesh,
            Matrix4x4.TRS(center,Quaternion.Euler(90,0,0),new Vector3(radius*2,0.1f,radius*2)),
            material,
            LayerMask.NameToLayer("UI"),
            UICamera);
        //static void DrawMesh(Mesh mesh, Matrix4x4 matrix, Material material, int layer, Camera camera = null, int submeshIndex = 0, MaterialPropertyBlock properties = null);
        //static void DrawMesh(Mesh mesh, Vector3 position, Quaternion rotation, Material material, int layer, Camera camera = null, int submeshIndex = 0, MaterialPropertyBlock properties = null);
        

    }
}

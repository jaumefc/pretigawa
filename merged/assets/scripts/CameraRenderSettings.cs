using UnityEngine;
using System.Collections;

//Script for attach on Cameras
//This script is used to control and moddify settings on render
//Calls to GL functions
[RequireComponent(typeof(Camera))]
public class CameraRenderSettings : MonoBehaviour {
	
	private GizmoDebug gd;
	private Material lineMaterial;

	private GameObject player;
	private GameObject[] triggers;
	private interactuable[] interactObjs;

	// Use this for initialization
	void Start () {
		gd = GameObject.FindObjectOfType<GizmoDebug>().GetComponent<GizmoDebug>();
		
		player = GameObject.Find ("Player");
		triggers = GameObject.FindGameObjectsWithTag ("CameraZone");
		interactObjs = GameObject.FindObjectsOfType<interactuable> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	
	void OnPreRender () {
		if(gd.GetWire())
			GL.wireframe = true;//It runs only when the script is attached on a Camera
	}
	
	void OnPostRender () {
		if(gd.GetWire())
			GL.wireframe = false;//It runs only when the script is attached on a Camera


		if (gd.GetGizmos ()) {
						renderGizObj (player, Color.green);

						for (int i=0; i<triggers.Length; i++) {
								renderGizObj (triggers [i], Color.yellow);
						}

						Gizmos.color = Color.blue;
						for (int i=0; i<interactObjs.Length; i++) {
								renderGizObj (interactObjs [i].gameObject, Color.blue);
						}
				}
	}

	private void renderGizObj(GameObject obj, Color clr) {
		CreateLineMaterial ();
		lineMaterial.SetPass (0);
		GL.PushMatrix();
			GL.Begin (GL.LINES);
			GL.Color (clr);

			GL.Vertex3(-0.5f,-0.5f,-0.5f);
			GL.Vertex3(0.5f,-0.5f,-0.5f);

			GL.Vertex3(-0.5f,0.5f,-0.5f);
			GL.Vertex3(0.5f,0.5f,-0.5f);

			GL.Vertex3(-0.5f,0.5f,0.5f);
			GL.Vertex3(0.5f,0.5f,0.5f);

			GL.Vertex3(-0.5f,-0.5f,0.5f);
			GL.Vertex3(0.5f,-0.5f,0.5f);


			GL.Vertex3(-0.5f,-0.5f,-0.5f);
			GL.Vertex3(-0.5f,-0.5f,0.5f);

			GL.Vertex3(-0.5f,0.5f,-0.5f);
			GL.Vertex3(-0.5f,0.5f,0.5f);

			GL.Vertex3(0.5f,0.5f,-0.5f);
			GL.Vertex3(0.5f,0.5f,0.5f);

			GL.Vertex3(0.5f,-0.5f,-0.5f);
			GL.Vertex3(0.5f,-0.5f,0.5f);


			GL.Vertex3(-0.5f,-0.5f,-0.5f);
			GL.Vertex3(-0.5f,0.5f,-0.5f);

			GL.Vertex3(0.5f,-0.5f,-0.5f);
			GL.Vertex3(0.5f,0.5f,-0.5f);

			GL.Vertex3(0.5f,-0.5f,0.5f);
			GL.Vertex3(0.5f,0.5f,0.5f);

			GL.Vertex3(-0.5f,-0.5f,0.5f);
			GL.Vertex3(-0.5f,0.5f,0.5f);
			Matrix4x4 mat = new Matrix4x4 ();
			Vector3 scale;
			Vector3 pos = obj.transform.position;
			if (obj.GetComponent<CharacterController> ()) {
				CharacterController carcon = (CharacterController)obj.GetComponent<CharacterController>();
				scale.y = carcon.height;
				scale.x = scale.z = carcon.radius*2;
				pos = pos + carcon.center; 
			} else if (obj.GetComponent<CapsuleCollider> ()) {
				CapsuleCollider capcol = ((CapsuleCollider)obj.GetComponent<CapsuleCollider> ());
				scale.y = capcol.height;
				scale.x = scale.z = capcol.radius * 2;
			}
			else
			{
				scale = obj.transform.lossyScale;
			}
			mat.SetTRS (pos, obj.transform.rotation, scale);
			GL.MultMatrix (mat);
			GL.End ();


		GL.PopMatrix();
	}


	private void CreateLineMaterial() {
		if( !lineMaterial ) {
			lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
			                            "SubShader { Pass { " +
			                            "    Blend SrcAlpha OneMinusSrcAlpha " +
			                            "    ZWrite Off Cull Off Fog { Mode Off } " +
			                            "    BindChannels {" +
			                            "      Bind \"vertex\", vertex Bind \"color\", color }" +
			                            "} } }" );
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
		}
	}
}

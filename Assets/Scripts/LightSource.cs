﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{

	[Range(3, 200)]
	public int numRays = 20;
	public float distance = 3f;

	public Material mat;

	MeshFilter filter;
	MeshRenderer render;

	Texture2D tex;

    // Start is called before the first frame update
    void Start()
    {

		filter = this.gameObject.AddComponent<MeshFilter>();
		render = this.gameObject.AddComponent<MeshRenderer>();
		render.material = mat;
		filter.mesh = generateMesh(doCircleRays());	
	}

    // Update is called once per frame
    void LateUpdate(){
		filter.mesh = generateMesh(doCircleRays());
	}

	Vector3[] doCircleRays() {

		Stack<Vector3> points = new Stack<Vector3>();
		for (float i = 0; i < 2 * Mathf.PI; i += 2 * Mathf.PI / numRays) {
			//i == theta or angle
			float x = Mathf.Cos(i), y = Mathf.Sin(i);
			Vector2 dir = new Vector2(x, y);

			RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir, distance, ~LayerMask.GetMask("Player", "Enviroment"));

			if (hit == false || i < Mathf.PI) {
				hit.point = (Vector2)this.transform.position + dir * distance;	
			}

			/*if (hit.transform.tag == "LightBox") {

			} else if (hit.transform.tag == "Enemy") {
				hit.transform.gameObject.GetComponent<EnemyController>().isLit = true;
			}*/
			//Debug.DrawLine(this.transform.position, hit.point);

			points.Push(hit.point - (Vector2)this.transform.position);

		}
		points.Push(Vector3.zero);
		return points.ToArray();
	}

	Mesh generateMesh(Vector3[] points){
		Mesh mesh = new Mesh();
		Vector2[] uv = new Vector2[points.Length];
		int[] triangles = new int[(points.Length) * 3];

		for (int i = 0; i < points.Length; i++) {
			//uv[i] = points[i];

			if (i < points.Length-2){
				triangles[i * 3] = 0;
				triangles[i * 3 + 1] = i + 1;
				triangles[i * 3 + 2] = i + 2;
			}

		}

		triangles[(points.Length - 2) * 3] = 0;
		triangles[(points.Length - 2) * 3 + 1] = points.Length - 2;
		triangles[(points.Length - 2) * 3 + 2] = 1;

		mesh.vertices = points;
		//mesh.uv = uv;
		mesh.triangles = triangles;

		//mesh.RecalculateBounds();
		mesh.RecalculateNormals();
		return mesh;

	}
}

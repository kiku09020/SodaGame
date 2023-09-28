using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSetter : MonoBehaviour {
	/* Fields */
	[SerializeField] GameObject bg1;
	[SerializeField] GameObject bg2;

	[SerializeField] float size = 16;

	List<GameObject> bgs = new List<GameObject>();

	//-------------------------------------------------------------------
	/* Properties */

	//-------------------------------------------------------------------
	/* Events */
	void Awake()
	{
		bgs.Add(bg1);
		bgs.Add(bg2);
	}

	void FixedUpdate()
	{
		var cameraPos=Camera.main.transform.position;

		foreach (GameObject bg in bgs) {
			if(cameraPos.y - bg.transform.position.y > size) {
				bg.transform.position += Vector3.up * size * 2;
			}
		}
	}

	//-------------------------------------------------------------------
	/* Methods */

}

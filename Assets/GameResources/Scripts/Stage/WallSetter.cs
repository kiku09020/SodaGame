using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 壁配置クラス </summary>
public class WallSetter : MonoBehaviour {
	/* Fields */
	[SerializeField] GameObject wallPrefab;
	[SerializeField] Transform wallParent;

	[SerializeField] float posX = 7.5f;
	[SerializeField] float sizeY = 128;

	List<GameObject> walls = new List<GameObject>();

	//-------------------------------------------------------------------
	/* Properties */

	//-------------------------------------------------------------------
	/* Events */
	void Awake()
	{
		// 左側生成
		var generatePos = new Vector2(posX, sizeY / 2);
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));

		// 右側生成
		generatePos.x *= -1;
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));

		// 右上
		generatePos.y += sizeY;
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));

		// 左上
		generatePos.x *= -1;
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));
	}

	void FixedUpdate()
	{
		var cameraPos = Camera.main.transform.position;

		// カメラと壁のYの差がサイズ以上になったら、サイズ分上に移動
		foreach (var wall in walls) {
			if (cameraPos.y - wall.transform.position.y >= sizeY) {
				wall.transform.position += Vector3.up * sizeY * 2;
			}
		}
	}

	//-------------------------------------------------------------------
	/* Methods */

}

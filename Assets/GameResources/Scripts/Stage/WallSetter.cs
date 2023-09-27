using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> �ǔz�u�N���X </summary>
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
		// ��������
		var generatePos = new Vector2(posX, sizeY / 2);
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));

		// �E������
		generatePos.x *= -1;
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));

		// �E��
		generatePos.y += sizeY;
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));

		// ����
		generatePos.x *= -1;
		walls.Add(Instantiate(wallPrefab, generatePos, Quaternion.identity, wallParent));
	}

	void FixedUpdate()
	{
		var cameraPos = Camera.main.transform.position;

		// �J�����ƕǂ�Y�̍����T�C�Y�ȏ�ɂȂ�����A�T�C�Y����Ɉړ�
		foreach (var wall in walls) {
			if (cameraPos.y - wall.transform.position.y >= sizeY) {
				wall.transform.position += Vector3.up * sizeY * 2;
			}
		}
	}

	//-------------------------------------------------------------------
	/* Methods */

}

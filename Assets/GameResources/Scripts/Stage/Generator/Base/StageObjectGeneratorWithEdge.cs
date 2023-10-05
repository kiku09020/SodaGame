using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class StageObjectGeneratorWithEdge<T> : StageObjectGeneratorBase<T> where T : StageObjectBase<T> {
		/* Fields */
		[SerializeField] float edgePosX = 5;

		protected int dir;

		//-------------------------------------------------------------------
		/* Methods */
		protected override void SetObjectPosition(T stage)
		{
			// ç∂Ç©âEÇéÊìæ
			bool isLeft = Random.Range(0, 2) == 0;
			dir = isLeft ? -1 : 1;

			stage.transform.position = new Vector2(dir * edgePosX, genPosY);
		}
	}
}
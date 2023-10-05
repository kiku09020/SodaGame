using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class StageObjectGeneratorWithXRange<T> : StageObjectGeneratorBase<T> where T : StageObjectBase<T> {
		/* Fields */
		[SerializeField] float xRange = 3;

		//-------------------------------------------------------------------
		/* Methods */
		protected override void SetObjectPosition(T stage)
		{
			// XÀ•Wƒ‰ƒ“ƒ_ƒ€w’è
			float randomX = Random.Range(-xRange, xRange);
			stage.transform.position = new Vector3(randomX, genPosY);
		}
	}
}
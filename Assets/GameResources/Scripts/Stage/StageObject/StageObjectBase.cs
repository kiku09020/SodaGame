using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
    public class StageObjectBase<T> : PooledObject<T> where T:StageObjectBase<T> {
		/* Fields */

		//-------------------------------------------------------------------
		/* Methods */
		public void SetWidth(float width)
		{
			transform.localScale = Vector2.one * width;
		}
	}
}
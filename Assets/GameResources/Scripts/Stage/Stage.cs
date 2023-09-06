using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class Stage : PooledObject<Stage> {
		/* Fields */
		[SerializeField] SpriteRenderer rend;

		//-------------------------------------------------------------------
		/* Properties */
		public SpriteRenderer Renderer => rend;

		//-------------------------------------------------------------------
		/* Events */


		//-------------------------------------------------------------------
		/* Methods */
		public void SetWidth(float width)
		{
			rend.size = new Vector2(width, 1);
		}
	}
}
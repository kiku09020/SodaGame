using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerComponent : ObjectComponentBase<PlayerCore> {
        /* Fields */
        [Header("Debug")]
        [SerializeField,Tooltip("デバッグ中かどうか")] protected bool isDebug = true;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		protected override void OnStart()
		{
			// デバッグビルドが無効な場合、自動で全てfalseにする
			if(!Debug.isDebugBuild) {
				isDebug = false;
			}
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
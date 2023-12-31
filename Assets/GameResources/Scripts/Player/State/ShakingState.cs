using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.State {
    public class ShakingState : PlayerStateBase {
		/* Fields */
		[SerializeField] PlayerShaker shaker;

		//-------------------------------------------------------------------
		/* Properties */
		//-------------------------------------------------------------------
		/* Events */
		public override void OnStateEnter()
		{
			rendererController.ChangeFace(PlayerRendererController.PlayerFace.shaked);
		}

		public override void OnStateUpdate()
		{
			shaker.Shake();
		}

		public override void OnStateExit()
		{

		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
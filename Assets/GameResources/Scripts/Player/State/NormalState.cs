using GameController.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.State {
    public class NormalState : PlayerStateBase {
		/* Fields */
		[SerializeField] PlayerRollingMover mover;

		//-------------------------------------------------------------------
		/* Properties */
		//-------------------------------------------------------------------
		/* Events */
		public override void OnStateEnter()
		{
			rendererController.ChangeFace(PlayerRendererController.PlayerFace.normal);
		}

		public override void OnStateUpdate()
		{
			if (!GameManager.IsGameOvered) {
				mover.Move();
			}
		}

		public override void OnStateExit()
		{

		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
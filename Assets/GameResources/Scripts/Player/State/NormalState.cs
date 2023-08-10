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

		}

		public override void OnStateUpdate()
		{
			mover.Move();
		}

		public override void OnStateExit()
		{

		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
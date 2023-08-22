using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.State {
    public class SodaState : PlayerStateBase {
		/* Fields */
		[SerializeField] PlayerSodaMover mover;

		//-------------------------------------------------------------------
		/* Properties */
		//-------------------------------------------------------------------
		/* Events */
		public override void OnStateEnter()
		{

		}

		public override void OnStateUpdate()
		{
			mover.MoveWithSoda();
		}

		public override void OnStateExit()
		{

		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
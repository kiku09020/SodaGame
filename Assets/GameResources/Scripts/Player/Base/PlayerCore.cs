using Game.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerCore : ObjectCore {
		/* Fields */
		[SerializeField] PlayerStateMachine stateMachine;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		private void Start()
		{
			stateMachine.StateSetup();
		}

		public void FixedUpdate()
		{
			stateMachine.StateUpdate();
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
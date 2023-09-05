using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.State {
    public class SodaState : PlayerStateBase {
		/* Fields */
		[SerializeField] PlayerSodaMover mover;
		[SerializeField] PlayerSodaManager sodaManager;

		[SerializeField] ParticleSystem sodaParticle;

		//-------------------------------------------------------------------
		/* Properties */
		//-------------------------------------------------------------------
		/* Events */
		public override void OnStateEnter()
		{
			sodaParticle.Play();
			NAudioController.Play("sodaFryingVibration");
		}

		public override void OnStateUpdate()
		{
			sodaManager.SodaUpdate();
			mover.MoveWithSoda();
		}

		public override void OnStateExit()
		{
			sodaParticle.Stop();
			NAudioController.Stop("sodaFryingVibration");
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
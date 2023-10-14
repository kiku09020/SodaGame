using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.State {
    public class SodaState : PlayerStateBase {
		/* Fields */
		[SerializeField] PlayerSodaMover mover;
		[SerializeField] PlayerSodaManager sodaManager;

		[SerializeField] ParticleSystem sodaParticle;
		[SerializeField] SEManager seManager;

		//-------------------------------------------------------------------
		/* Properties */
		//-------------------------------------------------------------------
		/* Events */
		public override async void OnStateEnter()
		{
			rendererController.ChangeFace(PlayerRendererController.PlayerFace.splashing);

			sodaParticle.Play();
			NAudioController.Play("sodaFryingVibration_loop");
			await seManager.SetLoop()
				.PlayAudio("Soda");
		}

		public override void OnStateUpdate()
		{
			mover.MoveWithSoda();
			sodaManager.SodaUpdate();
		}

		public override void OnStateExit()
		{
			sodaParticle.Stop();
			NAudioController.Stop("sodaFryingVibration_loop");
			seManager
				.SetLoop(false)
				.StopAudio();

			mover.isFirstSoda = false;
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
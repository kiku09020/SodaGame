using Game.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	public class PlayerCore : ObjectCore {
		/* Fields */
		[SerializeField] PlayerStateMachine stateMachine;

		[SerializeField] ParticleSystem hitEffect;

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

		private void OnCollisionEnter2D(Collision2D collision)
		{
			NAudioController.Play("Hit");

			Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
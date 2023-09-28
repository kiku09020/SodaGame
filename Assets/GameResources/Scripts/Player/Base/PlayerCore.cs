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
		/// <summary> ‹ó‹C’†‚É‚¢‚é‚© </summary>
		public bool IsInAir { get; private set; }

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

		// •Ç‚Æ‚©
		private void OnCollisionEnter2D(Collision2D collision)
		{
			NAudioController.Play("Hit");

			Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
		}

		// –A
		private void OnTriggerEnter2D(Collider2D collision)
		{
			IsInAir = true;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			IsInAir = false;
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
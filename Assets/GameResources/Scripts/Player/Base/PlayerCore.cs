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
		/// <summary> 空気中にいるか </summary>
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

		// 壁とか
		private void OnCollisionEnter2D(Collision2D collision)
		{
			NAudioController.Play("Hit");

			Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
		}

		// 泡
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
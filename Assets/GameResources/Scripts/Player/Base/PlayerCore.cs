using Game.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
	public class PlayerCore : ObjectCore {
		/* Fields */
		[SerializeField] PlayerStateMachine stateMachine;

		[SerializeField] ParticleSystem hitEffect;
		[SerializeField] ParticleSystem hitAirEffect;

		//-------------------------------------------------------------------
		/* Properties */
		/// <summary> �����ɐG��Ă��邩 </summary>
		public bool IsTouching { get; private set; }

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

		// �ǂƂ�
		private void OnCollisionEnter2D(Collision2D collision)
		{
			NAudioController.Play("Hit");

			Instantiate(hitEffect, collision.contacts[0].point, Quaternion.identity);
		}

		// �A
		private void OnTriggerEnter2D(Collider2D collision)
		{
			if (!IsTouching) {
				IsTouching = true;

				HitEffect(collision);
			}
		}

		private void OnTriggerStay2D(Collider2D collision)
		{
			IsTouching = true;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			if (IsTouching) {
				IsTouching = false;

				HitEffect(collision);
			}
		}

		//-------------------------------------------------------------------
		/* Methods */
		void HitEffect(Collider2D collision)
		{
			if (collision.gameObject.layer == LayerMask.NameToLayer("Air")) {
				var pos = (transform.position + collision.gameObject.transform.position) / 2;

				Instantiate(hitAirEffect, pos, Quaternion.identity);
			}
		}
	}
}
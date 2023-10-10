using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class ExplosionAir : Air {
		/* Fields */

		[Header("Explosion")]
		[SerializeField] float explosionDelay = 3;
		[SerializeField] float explosionPower = 100;

		float explosionTimer;
		bool isHitFirst;

		Collider2D hitObject;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		protected override void Awake()
		{
			base.Awake();
		}

		protected override void FixedUpdate()
		{
			base.FixedUpdate();

			if (isHitFirst) {
				explosionTimer += Time.deltaTime;

				// ŽžŠÔŒo‰ßŒãA”š”­
				if (explosionTimer > explosionDelay) {
					Explosion(hitObject);

					explosionTimer = 0;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			isHitFirst = true;
			hitObject = collision;
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			hitObject = null;
		}

		//-------------------------------------------------------------------
		/* Methods */

		void Explosion(Collider2D collider)
		{
			// rbŽæ“¾
			var rb = collider?.GetComponentInChildren<Rigidbody2D>();
			if (rb == null) return;

			// •ûŒü’è‚ß‚Ä”š”­
			var dir = collider.transform.position - transform.position;
			rb.AddForce(dir.normalized * explosionPower * (transform.localScale.x * .5f));

			isHitFirst = false;

			OnDeadProcess();
		}
	}
}
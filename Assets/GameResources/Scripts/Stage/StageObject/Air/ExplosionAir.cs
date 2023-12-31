using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class ExplosionAir : Air {
		/* Fields */

		[Header("Explosion")]
		[SerializeField] float explosionDelay = 3;
		[SerializeField] float explosionPower = 100;

		[Header("Components")]
		[SerializeField] SEManager seManager;
		[SerializeField] ExplosionRegionChecker regionChecker;

		float explosionTimer;
		bool isHitFirst;

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
				Blinking();

				explosionTimer += Time.deltaTime;

				// 時間経過後、爆発
				if (explosionTimer > explosionDelay) {
					OnDeadProcess();

					explosionTimer = 0;
				}
			}
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			isHitFirst = true;
		}

		protected override void OnDeadProcess()
		{
			base.OnDeadProcess();

			Explosion();
		}

		//-------------------------------------------------------------------
		/* Methods */

		async void Explosion()
		{
			foreach (var col in regionChecker?.Colliders) {
				// rb取得
				var rb = col?.GetComponentInChildren<Rigidbody2D>();
				if (rb == null) continue;

				// 方向定めて爆発
				var dir = col.transform.position - transform.position;
				rb.AddForce(dir.normalized * explosionPower * (transform.localScale.x * .5f));

			}

			isHitFirst = false;
			await seManager.PlayAudio("Bomb");
		}
	}
}
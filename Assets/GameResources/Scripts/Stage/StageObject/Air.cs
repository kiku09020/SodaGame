using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class Air : StageObjectBase<Air> {
		/* Fields */
		[Header("Components")]
		[SerializeField] SpriteRenderer rend;
		[SerializeField] BuoyancyEffector2D buoyancy;
		[SerializeField] ParticleSystem deadEffect;


		[Header("Parameters")]
		[SerializeField, Tooltip("移動速度")] float upSpeedMin = .0001f;
		[SerializeField, Tooltip("")] float upSpeedMax = .01f;

		[SerializeField, Tooltip("寿命時間")] float airLifeTime = 7.5f;
		[SerializeField, Tooltip("点滅時間")] float airBlinkingTime = 3;
		[SerializeField] float cycle = 1;

		float lifeTimer;
		float upSpeed;

		Color startColor;

		//-------------------------------------------------------------------
		/* Properties */
		public SpriteRenderer Renderer => rend;

		//-------------------------------------------------------------------
		/* Events */
		protected virtual void Awake()
		{
			startColor = rend.color;
		}

		protected virtual void FixedUpdate()
		{
			// 移動
			transform.Translate(0, upSpeed, 0);

			LifeTimer();
		}

		public override void OnReleased()
		{
			base.OnReleased();

			lifeTimer = 0;
			rend.color = startColor;

			buoyancy.enabled = true;
			rend.enabled = true;
		}

		public override void OnGetted()
		{
			base.OnGetted();

			SetUpSpeed();
		}

		/// <summary> 死亡処理 </summary>
		protected virtual void OnDeadProcess()
		{
			lifeTimer = 0;

			// 非表示、浮力無効化
			rend.enabled = false;
			buoyancy.enabled = false;

			// 再生(再生終了時にRelease)
			deadEffect.Play();
		}

		//-------------------------------------------------------------------
		/* Methods */
		void SetUpSpeed()
		{
			upSpeed = Random.Range(upSpeedMin, upSpeedMax);
		}

		void LifeTimer()
		{
			// 寿命
			lifeTimer += Time.deltaTime;

			// 点滅
			if (lifeTimer >= airLifeTime - airBlinkingTime) {
				Blinking();
			}

			if (lifeTimer >= airLifeTime) {
				OnDeadProcess();
			}
		}

		// 点滅処理
		protected void Blinking()
		{
			var a = Mathf.Cos((float)(2 * Mathf.PI * lifeTimer / cycle)) * .5f + .5f;

			var color = rend.color;
			color.a = a;
			rend.color = color;
		}
	}
}
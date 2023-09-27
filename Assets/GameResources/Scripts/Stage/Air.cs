using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class Air : PooledObject<Air> {
		/* Fields */
		[SerializeField] SpriteRenderer rend;

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
		private void Awake()
		{
			startColor = rend.color;
		}

		private void FixedUpdate()
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
		}

		public override void OnGetted()
		{
			base.OnGetted();

			SetUpSpeed();
		}

		//-------------------------------------------------------------------
		/* Methods */
		void SetUpSpeed()
		{
			upSpeed=Random.Range(upSpeedMin, upSpeedMax);
		}

		public void SetWidth(float width)
		{
			//rend.size = new Vector2(width, width);

			transform.localScale = Vector2.one * width;
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
				lifeTimer = 0;
				Release();
			}
		}

		// 点滅処理
		void Blinking()
		{
			var a = Mathf.Cos((float)(2 * Mathf.PI * lifeTimer / cycle)) * .5f + .5f;

			var color = rend.color;
			color.a = a;
			rend.color = color;
		}
	}
}
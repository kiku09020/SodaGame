using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class Air : PooledObject<Air> {
		/* Fields */
		[SerializeField] SpriteRenderer rend;

		[SerializeField, Tooltip("�ړ����x")] float upSpeedMin = .0001f;
		[SerializeField, Tooltip("")] float upSpeedMax = .01f; 

		[SerializeField, Tooltip("��������")] float airLifeTime = 7.5f;
		[SerializeField, Tooltip("�_�Ŏ���")] float airBlinkingTime = 3;
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
			// �ړ�
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
			// ����
			lifeTimer += Time.deltaTime;

			// �_��
			if (lifeTimer >= airLifeTime - airBlinkingTime) {
				Blinking();
			}

			if (lifeTimer >= airLifeTime) {
				lifeTimer = 0;
				Release();
			}
		}

		// �_�ŏ���
		void Blinking()
		{
			var a = Mathf.Cos((float)(2 * Mathf.PI * lifeTimer / cycle)) * .5f + .5f;

			var color = rend.color;
			color.a = a;
			rend.color = color;
		}
	}
}
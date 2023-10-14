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
		protected virtual void Awake()
		{
			startColor = rend.color;
		}

		protected virtual void FixedUpdate()
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

			buoyancy.enabled = true;
			rend.enabled = true;
		}

		public override void OnGetted()
		{
			base.OnGetted();

			SetUpSpeed();
		}

		/// <summary> ���S���� </summary>
		protected virtual void OnDeadProcess()
		{
			lifeTimer = 0;

			// ��\���A���͖�����
			rend.enabled = false;
			buoyancy.enabled = false;

			// �Đ�(�Đ��I������Release)
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
			// ����
			lifeTimer += Time.deltaTime;

			// �_��
			if (lifeTimer >= airLifeTime - airBlinkingTime) {
				Blinking();
			}

			if (lifeTimer >= airLifeTime) {
				OnDeadProcess();
			}
		}

		// �_�ŏ���
		protected void Blinking()
		{
			var a = Mathf.Cos((float)(2 * Mathf.PI * lifeTimer / cycle)) * .5f + .5f;

			var color = rend.color;
			color.a = a;
			rend.color = color;
		}
	}
}
using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public abstract class StageObjectGeneratorBase<T> : MyObjectPool<T> where T : StageObjectBase<T> {
		/* Fields */
		[Header("Base Parameters")]
		[SerializeField, Tooltip("生成開始位置")]
		float startGenPosY = 15;
		[SerializeField, Tooltip("カメラ位置からY分上の座標")]
		float genPosDistance = 10;
		[SerializeField, Tooltip("次の生成位置までの間隔")]
		float genPosDuration = 5;

		[SerializeField, Tooltip("カメラからX分離れたら削除される距離")]
		float destroyDistance = 10;

		[Header("Changing Size")]
		[SerializeField, Tooltip("サイズ変更するか")] bool isChangeSize = true;
		[SerializeField, Tooltip("ランダム最小サイズ")] protected float randMinSize = 1;
		[SerializeField, Tooltip("ランダム最大サイズ")] protected float randMaxSize = 3;

		float camPosY;              // カメラのY位置
		protected float genPosY;    // 生成Y位置

		List<T> stageObjects = new List<T>();

		//-------------------------------------------------------------------
		/* Events */
		private void Start()
		{
			genPosY = startGenPosY;
		}

		private void FixedUpdate()
		{
			// カメラ位置更新
			camPosY = Camera.main.transform.position.y;

			// 生成位置と現在のカメラの位置の差が指定距離より近づけば生成
			if ((genPosY - camPosY) < genPosDistance) {
				Generate();

				SetGeneratePosition(genPosDuration);
			}

			// 削除
			DestroyGeneratedObj();
		}

		protected override void OnGetFromPool(T obj)
		{
			base.OnGetFromPool(obj);

			stageObjects.Add(obj);
		}

		protected override void OnReleaseToPool(T obj)
		{
			base.OnReleaseToPool(obj);

			stageObjects.Remove(obj);
		}

		//-------------------------------------------------------------------
		/* Methods */
		void Generate()
		{
			var obj = pool.Get();

			SetObjectPosition(obj);

			if (isChangeSize) {
				SetObjectSize(obj);
			}
		}

		/// <summary> 生成位置の指定 </summary>
		protected virtual void SetGeneratePosition(float genDuration)
		{
			genPosY += genDuration;
		}

		// 位置指定
		protected abstract void SetObjectPosition(T stage);

		/// <summary> 大きさ指定 </summary>
		protected virtual void SetObjectSize(T obj)
		{
			float randomSize = Random.Range(randMinSize, randMaxSize);
			obj.SetSize(randomSize);
		}

		// 生成オブジェクトを削除
		void DestroyGeneratedObj()
		{
			foreach (var stage in stageObjects) {
				if ((camPosY - stage.transform.position.y) > destroyDistance &&
					camPosY > stage.transform.position.y) {

					if (!stage.IsReleased) {
						pool.Release(stage);
					}

					break;
				}
			}
		}
	}
}
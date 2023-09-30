using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class StageObjectGenerator<T> : MyObjectPool<T> where T : StageObjectBase<T> {
		/* Fields */
		[Header("Parameters")]
		[SerializeField] float xRange = 3;
		[SerializeField, Tooltip("生成位置からカメラ位置の距離")]
		float generateDistance = 10;
		[SerializeField, Tooltip("カメラ位置からの次の生成位置までの距離")]
		float generateOffset = 15;

		[SerializeField] float destroyDistance = 10;

		[SerializeField] float minWidth = 3;
		[SerializeField] float maxWidth = 6;

		float generateY;                // 生成Y位置

		float currentCameraPosY;        // 現在のカメラのY位置

		List<T> stages = new List<T>();

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		private void FixedUpdate()
		{
			// カメラ位置更新
			currentCameraPosY = Camera.main.transform.position.y;

			// 生成位置と現在のカメラの位置の差が指定距離より近づけば生成
			if ((generateY - currentCameraPosY) < generateDistance) {
				Generate();
			}

			// 削除
			DestroyGeneratedObj();
		}

		protected override void OnGetFromPool(T obj)
		{
			base.OnGetFromPool(obj);

			stages.Add(obj);
		}

		protected override void OnReleaseToPool(T obj)
		{
			base.OnReleaseToPool(obj);

			stages.Remove(obj);
		}

		//-------------------------------------------------------------------
		/* Methods */
		void Generate()
		{
			var obj = pool.Get();

			SetGenrerateHeight();
			SetStageTransform(obj);
		}

		// 位置とか大きさ指定する
		void SetStageTransform(T stage)
		{
			// 位置指定
			float randomX = Random.Range(-xRange, xRange);

			stage.transform.position = new Vector2(randomX, 10);

			// 幅指定
			float randomWidth = Random.Range(minWidth, maxWidth);
			stage.SetWidth(randomWidth);

			stage.transform.position = new Vector2(randomX, generateY);
		}

		void SetGenrerateHeight()
		{
			// カメラのY座標+オフセット位置
			generateY = currentCameraPosY + generateOffset;
		}

		void DestroyGeneratedObj()
		{
			foreach (var stage in stages) {
				if ((currentCameraPosY - stage.transform.position.y) > destroyDistance &&
					currentCameraPosY > stage.transform.position.y) {

					if (!stage.IsReleased) {
						pool.Release(stage);
					}

					break;
				}
			}
		}
	}
}
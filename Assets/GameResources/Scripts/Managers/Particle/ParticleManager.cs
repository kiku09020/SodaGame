using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Particle
{
	public class ParticleManager : ObjectPoolList<ParticleUnit>
	{
		/* Fields */
		[SerializeField] ParticleList particleList;

		static List<ParticleManager> managerList = new();

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		private void Awake()
		{
			managerList.Add(this);      // 登録
		}

		private void OnDestroy()
		{
			managerList.Clear();        // 登録解除
		}

		//-------------------------------------------------------------------
		/* Generate Methods */

		public void Generate(string name,Transform parent=null)
		{
			ParticleUnit particle= particleList.GetParticle(name);

			AddObjList(particle);
		}

		public void Generate(int index)
		{

		}

		//-------------------------------------------------------------------
		/* Play Methods */
		#region Play
		/// <summary> パーティクルを再生する </summary>
		public void Play(string name)
		{ Play(name, Vector3.zero, Quaternion.identity); }

		/// <summary> パーティクルを再生する </summary>
		public void Play(string name, Vector3 position)
		{ Play(name, position, Quaternion.identity); }

		/// <summary> パーティクルを再生する </summary>
		public void Play(string name, Quaternion rotation)
		{ Play(name, Vector3.zero, rotation); }

		/// <summary> パーティクルを再生する </summary>
		public void Play(string name, Vector3 position, Quaternion rotation)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.transform.position = position;
			particle.transform.rotation = rotation;
			particle.Particle.Play();
		}

		/// <summary> パーティクルを再生する </summary>
		public void Play(int index)
		{ Play(index, Vector3.zero, Quaternion.identity); }

		/// <summary> パーティクルを再生する </summary>
		public void Play(int index, Vector3 position)
		{ Play(index, position, Quaternion.identity); }

		/// <summary> パーティクルを再生する </summary>
		public void Play(int index, Quaternion rotation)
		{ Play(index, Vector3.zero, rotation); }

		/// <summary> パーティクルを再生する </summary>
		public void Play(int index, Vector3 position, Quaternion rotation)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.transform.position = position;
			particle.transform.rotation = rotation;
			particle.Particle.Play();
		}

		/// <summary> リスト内の全てのパーティクルを再生する </summary>
		public void PlayAllInList()
		{
			foreach (var part in particleList.Particles) {

				part.Particle.Play();
			}
		}

		/// <summary> 指定されたマネージャーのリスト内の全てのパーティクルを再生する </summary>
		public static void PlayAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.PlayAllInList();
		}

		/// <summary> 全てのパーティクルを再生する </summary>
		public static void PlayAll()
		{
			foreach (var manager in managerList) {
				manager.PlayAllInList();
			}
		}

		#endregion
		//-------------------------------------------------------------------
		/* Stop Methods */
		// 停止：パーティクルの生成は停止するが、再生中のパーティクルは停止されない

		#region Stop
		/// <summary> パーティクルを停止する </summary>
		public void Stop(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Stop();
		}

		/// <summary> パーティクルを停止する </summary>
		public void Stop(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Stop();
		}

		/// <summary> リスト内の全てのパーティクルを停止する </summary>
		public void StopAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Stop();
			}
		}

		/// <summary> 指定されたマネージャーのリスト内の全てのパーティクルを停止する </summary>
		public static void StopAllInList(string managerName)
		{
			var manager = managerList.Find(m => m.name == managerName);
			manager.StopAllCoroutines();
		}

		/// <summary> 全てのパーティクルを停止する </summary>
		public static void StopAll()
		{
			foreach (var manager in managerList) {
				manager.StopAllInList();
			}
		}

		#endregion
		//-------------------------------------------------------------------
		/* Pause Methods */
		// 一時停止：パーティクルの生成を停止し、再生中のパーティクルの再生も停止する

		#region Pause
		/// <summary> パーティクルを一時停止する </summary>
		public void Pause(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Pause();
		}

		/// <summary> パーティクルを一時停止する </summary>
		public void Pause(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Pause();
		}

		/// <summary> リスト内の全てのパーティクルを一時停止する </summary>
		public void PauseAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Pause();
			}
		}

		/// <summary> 指定されたマネージャーのリスト内の全てのパーティクルを一時停止する </summary>
		public static void PauseAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.PauseAllInList();
		}

		/// <summary>
		/// すべてのパーティクルを一時停止する
		/// </summary>
		public static void PauseAll()
		{
			foreach (var manager in managerList) {

				manager.PauseAllInList();
			}
		}

		#endregion
		//-------------------------------------------------------------------	
		/* Clear Methods */
		// Clear:　再生中のパーティクルを削除する

		#region Clear
		/// <summary> パーティクルを削除する </summary>
		public void Clear(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Clear();
		}

		/// <summary> パーティクルを削除する </summary>
		public void Clear(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Clear();
		}

		/// <summary> リスト内の全てのパーティクルを削除する </summary>
		public void ClearAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Clear();
			}
		}

		/// <summary> 指定されたマネージャーのリスト内の全てのパーティクルを削除する </summary>
		public static void ClearAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.ClearAllInList();
		}

		/// <summary> 全てのパーティクルを削除する </summary>
		public static void ClearAll()
		{
			foreach (var manager in managerList) {
				manager.ClearAllInList();
			}
		}

		#endregion

		//-------------------------------------------------------------------
		/* Restart Methods */
		// Restart:　再生中のパーティクルを削除し、再び再生する

		#region Restart
		/// <summary> 再生中のパーティクルを削除し、再び再生する </summary>
		public void Restart(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Clear();
			particle.Particle.Play();
		}

		/// <summary> 再生中のパーティクルを削除し、再び再生する </summary>
		public void Restart(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Clear();
			particle.Particle.Play();
		}

		/// <summary> リスト内の全ての再生中のパーティクルを削除し、再び再生する </summary>
		public void RestartAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Clear();
				part.Particle.Play();
			}
		}

		/// <summary> 指定されたマネージャーのリスト内の全ての再生中のパーティクルを削除し、再び再生する </summary>
		public static void RestartAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.RestartAllInList();
		}

		/// <summary> 全ての再生中のパーティクルを削除し、再び再生する </summary>
		public static void RestartAll()
		{
			foreach (var manager in managerList) {
				manager.RestartAllInList();
			}
		}

		#endregion

		//-------------------------------------------------------------------
		/* Destroy Methods */
	}
}
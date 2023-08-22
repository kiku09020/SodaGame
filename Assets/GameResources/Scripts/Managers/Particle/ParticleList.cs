using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameController.Particle
{
	[CreateAssetMenu(fileName = "ParticleList", menuName = "Scriptable/ParticleList")]
	public class ParticleList : ScriptableObject
	{
		[Header("Particle")]
		[SerializeField] List<ParticleUnit> particles;

		public IReadOnlyList<ParticleUnit> Particles => particles;

		//-------------------------------------------------------------------
		/// <summary> パーティクルを取得する </summary>
		/// <param name="name">パーティクル名</param>
		public ParticleUnit GetParticle(string name)
		{
			return particles.Find(particle => particle.name == name);
		}

		/// <summary> パーティクルを取得する </summary>
		/// <param name="index">要素番号</param>
		public ParticleUnit GetParticle(int index)
		{
			return particles[index];
		}


	}
}
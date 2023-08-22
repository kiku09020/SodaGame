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
		/// <summary> �p�[�e�B�N�����擾���� </summary>
		/// <param name="name">�p�[�e�B�N����</param>
		public ParticleUnit GetParticle(string name)
		{
			return particles.Find(particle => particle.name == name);
		}

		/// <summary> �p�[�e�B�N�����擾���� </summary>
		/// <param name="index">�v�f�ԍ�</param>
		public ParticleUnit GetParticle(int index)
		{
			return particles[index];
		}


	}
}
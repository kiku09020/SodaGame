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
			managerList.Add(this);      // �o�^
		}

		private void OnDestroy()
		{
			managerList.Clear();        // �o�^����
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
		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(string name)
		{ Play(name, Vector3.zero, Quaternion.identity); }

		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(string name, Vector3 position)
		{ Play(name, position, Quaternion.identity); }

		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(string name, Quaternion rotation)
		{ Play(name, Vector3.zero, rotation); }

		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(string name, Vector3 position, Quaternion rotation)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.transform.position = position;
			particle.transform.rotation = rotation;
			particle.Particle.Play();
		}

		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(int index)
		{ Play(index, Vector3.zero, Quaternion.identity); }

		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(int index, Vector3 position)
		{ Play(index, position, Quaternion.identity); }

		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(int index, Quaternion rotation)
		{ Play(index, Vector3.zero, rotation); }

		/// <summary> �p�[�e�B�N�����Đ����� </summary>
		public void Play(int index, Vector3 position, Quaternion rotation)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.transform.position = position;
			particle.transform.rotation = rotation;
			particle.Particle.Play();
		}

		/// <summary> ���X�g���̑S�Ẵp�[�e�B�N�����Đ����� </summary>
		public void PlayAllInList()
		{
			foreach (var part in particleList.Particles) {

				part.Particle.Play();
			}
		}

		/// <summary> �w�肳�ꂽ�}�l�[�W���[�̃��X�g���̑S�Ẵp�[�e�B�N�����Đ����� </summary>
		public static void PlayAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.PlayAllInList();
		}

		/// <summary> �S�Ẵp�[�e�B�N�����Đ����� </summary>
		public static void PlayAll()
		{
			foreach (var manager in managerList) {
				manager.PlayAllInList();
			}
		}

		#endregion
		//-------------------------------------------------------------------
		/* Stop Methods */
		// ��~�F�p�[�e�B�N���̐����͒�~���邪�A�Đ����̃p�[�e�B�N���͒�~����Ȃ�

		#region Stop
		/// <summary> �p�[�e�B�N�����~���� </summary>
		public void Stop(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Stop();
		}

		/// <summary> �p�[�e�B�N�����~���� </summary>
		public void Stop(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Stop();
		}

		/// <summary> ���X�g���̑S�Ẵp�[�e�B�N�����~���� </summary>
		public void StopAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Stop();
			}
		}

		/// <summary> �w�肳�ꂽ�}�l�[�W���[�̃��X�g���̑S�Ẵp�[�e�B�N�����~���� </summary>
		public static void StopAllInList(string managerName)
		{
			var manager = managerList.Find(m => m.name == managerName);
			manager.StopAllCoroutines();
		}

		/// <summary> �S�Ẵp�[�e�B�N�����~���� </summary>
		public static void StopAll()
		{
			foreach (var manager in managerList) {
				manager.StopAllInList();
			}
		}

		#endregion
		//-------------------------------------------------------------------
		/* Pause Methods */
		// �ꎞ��~�F�p�[�e�B�N���̐������~���A�Đ����̃p�[�e�B�N���̍Đ�����~����

		#region Pause
		/// <summary> �p�[�e�B�N�����ꎞ��~���� </summary>
		public void Pause(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Pause();
		}

		/// <summary> �p�[�e�B�N�����ꎞ��~���� </summary>
		public void Pause(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Pause();
		}

		/// <summary> ���X�g���̑S�Ẵp�[�e�B�N�����ꎞ��~���� </summary>
		public void PauseAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Pause();
			}
		}

		/// <summary> �w�肳�ꂽ�}�l�[�W���[�̃��X�g���̑S�Ẵp�[�e�B�N�����ꎞ��~���� </summary>
		public static void PauseAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.PauseAllInList();
		}

		/// <summary>
		/// ���ׂẴp�[�e�B�N�����ꎞ��~����
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
		// Clear:�@�Đ����̃p�[�e�B�N�����폜����

		#region Clear
		/// <summary> �p�[�e�B�N�����폜���� </summary>
		public void Clear(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Clear();
		}

		/// <summary> �p�[�e�B�N�����폜���� </summary>
		public void Clear(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Clear();
		}

		/// <summary> ���X�g���̑S�Ẵp�[�e�B�N�����폜���� </summary>
		public void ClearAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Clear();
			}
		}

		/// <summary> �w�肳�ꂽ�}�l�[�W���[�̃��X�g���̑S�Ẵp�[�e�B�N�����폜���� </summary>
		public static void ClearAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.ClearAllInList();
		}

		/// <summary> �S�Ẵp�[�e�B�N�����폜���� </summary>
		public static void ClearAll()
		{
			foreach (var manager in managerList) {
				manager.ClearAllInList();
			}
		}

		#endregion

		//-------------------------------------------------------------------
		/* Restart Methods */
		// Restart:�@�Đ����̃p�[�e�B�N�����폜���A�ĂэĐ�����

		#region Restart
		/// <summary> �Đ����̃p�[�e�B�N�����폜���A�ĂэĐ����� </summary>
		public void Restart(string name)
		{
			ParticleUnit particle = particleList.GetParticle(name);
			particle.Particle.Clear();
			particle.Particle.Play();
		}

		/// <summary> �Đ����̃p�[�e�B�N�����폜���A�ĂэĐ����� </summary>
		public void Restart(int index)
		{
			ParticleUnit particle = particleList.GetParticle(index);
			particle.Particle.Clear();
			particle.Particle.Play();
		}

		/// <summary> ���X�g���̑S�Ă̍Đ����̃p�[�e�B�N�����폜���A�ĂэĐ����� </summary>
		public void RestartAllInList()
		{
			foreach (var part in particleList.Particles) {
				part.Particle.Clear();
				part.Particle.Play();
			}
		}

		/// <summary> �w�肳�ꂽ�}�l�[�W���[�̃��X�g���̑S�Ă̍Đ����̃p�[�e�B�N�����폜���A�ĂэĐ����� </summary>
		public static void RestartAllInList(string name)
		{
			var manager = managerList.Find(m => m.name == name);
			manager.RestartAllInList();
		}

		/// <summary> �S�Ă̍Đ����̃p�[�e�B�N�����폜���A�ĂэĐ����� </summary>
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
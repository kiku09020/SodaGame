using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
    /// <summary> �����͈͂̎擾 </summary>
    public class ExplosionRegionChecker : MonoBehaviour {
        /* Fields */

        //-------------------------------------------------------------------
        /* Properties */
		/// <summary> �����ΏۃI�u�W�F�N�g </summary>
        public List<Collider2D> Colliders { get; private set; }

		//-------------------------------------------------------------------
		/* Events */
		private void Awake()
		{
			Colliders = new List<Collider2D>();
		}


		//-------------------------------------------------------------------
		/* Methods */
		private void OnTriggerEnter2D(Collider2D collision)
		{
			Colliders.Add(collision);
		}

		private void OnTriggerExit2D(Collider2D collision)
		{
			Colliders.Remove(collision);
		}
	}
}
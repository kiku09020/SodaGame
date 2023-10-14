using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
    /// <summary> 爆発範囲の取得 </summary>
    public class ExplosionRegionChecker : MonoBehaviour {
        /* Fields */

        //-------------------------------------------------------------------
        /* Properties */
		/// <summary> 爆発対象オブジェクト </summary>
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
using Game.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item {
	public class Ebi : MonoBehaviour {
		/* Fields */
		[SerializeField] float addedSodaPower = 10;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		private void OnTriggerEnter2D(Collider2D collision)
		{
			var sodaManager = collision.GetComponentInChildren<PlayerSodaManager>();

			sodaManager.AddPower(addedSodaPower);
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
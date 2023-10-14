using Game.Player;
using Game.Stage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Item {
	public class Ebi : StageObjectBase<Ebi> {
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
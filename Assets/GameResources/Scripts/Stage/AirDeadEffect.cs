using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
    public class AirDeadEffect : MonoBehaviour {
		/* Fields */
		[SerializeField] Air air;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */
		private void OnParticleSystemStopped()
		{
			air.Release();
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
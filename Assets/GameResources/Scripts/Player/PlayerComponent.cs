using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerComponent : ObjectComponentBase<PlayerCore> {
        /* Fields */
        [Header("Debug")]
        [SerializeField,Tooltip("�f�o�b�O�����ǂ���")] protected bool isDebug = true;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		protected override void OnStart()
		{
			// �f�o�b�O�r���h�������ȏꍇ�A�����őS��false�ɂ���
			if(!Debug.isDebugBuild) {
				isDebug = false;
			}
		}

		//-------------------------------------------------------------------
		/* Methods */

	}
}
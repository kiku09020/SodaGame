using Game.Player.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player {
    public class PlayerComponent : ObjectComponentBase<PlayerCore> {
        /* Fields */
        [Header("Debug")]
        [SerializeField,Tooltip("�f�o�b�O�����ǂ���")] protected bool isDebug = true;

		[Header("Components")]
		[SerializeField] protected PlayerStateMachine stateMachine;

		//-------------------------------------------------------------------
		/* Properties */

		//-------------------------------------------------------------------
		/* Events */

		void Start()
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
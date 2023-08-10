using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameController.State {
	public interface IStateBase {
		public string Name { get; }

		public abstract void OnStateEnter();
		public abstract void OnStateUpdate();
		public abstract void OnStateExit();
	}
}
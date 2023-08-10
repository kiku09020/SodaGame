using GameController.State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Player.State {
    
    public abstract class PlayerStateBase : ObjectComponentBase<PlayerCore>, IStateBase {
        [Header("State")]
        [SerializeField] protected string stateName;

        public string Name => stateName;

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
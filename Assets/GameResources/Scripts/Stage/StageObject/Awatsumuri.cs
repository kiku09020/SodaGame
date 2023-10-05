using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Enemy {
    public class Awatsumuri : StageObjectBase<Awatsumuri> {
        /* Fields */
        [SerializeField] float moveSpeed = .01f;

        //-------------------------------------------------------------------
        /* Properties */

        //-------------------------------------------------------------------
        /* Events */
        void FixedUpdate()
        {
            Move();
        }

        //-------------------------------------------------------------------
        /* Methods */
        void Move()
        {
            transform.position += transform.right * moveSpeed;
        }
    }
}
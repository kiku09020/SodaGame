using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage.Enemy {
    public class AwatsumuriGenerator : StageObjectGeneratorWithEdge<Awatsumuri> {
		protected override void SetObjectPosition(Awatsumuri stage)
		{
			base.SetObjectPosition(stage);

			stage.transform.rotation = Quaternion.Euler(0, 0, 90 * dir);
		}
	}
}
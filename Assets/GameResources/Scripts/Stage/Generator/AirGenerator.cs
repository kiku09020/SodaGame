using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class AirGenerator : StageObjectGeneratorWithXRange<Air> {

		[SerializeField] float addedGenDuration = 5;
		[SerializeField] float removedSize = .1f;

		protected override void SetGeneratePosition(float genDuration)
		{
			genPosY += (genDuration + addedGenDuration * StageLevelChecker.DifficultyLevel);
		}

		protected override void SetObjectSize(Air obj)
		{
			float fixedRandMaxSize = randMaxSize - removedSize * StageLevelChecker.DifficultyLevel;

			if (fixedRandMaxSize < 0.5f) {
				fixedRandMaxSize = .5f;
			}

			float fixedRandomSize = Random.Range(randMinSize, fixedRandMaxSize);
			obj.SetSize(fixedRandomSize);
		}
	}
}
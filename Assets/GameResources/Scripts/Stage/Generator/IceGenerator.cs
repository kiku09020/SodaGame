using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stage {
	public class IceGenerator : StageObjectGeneratorWithXRange<Ice> {
		[SerializeField] float removedGenDuration = .5f;
		[SerializeField] float addedSize = .2f;
		[SerializeField] float generatedMaxSize = 7.5f;

		protected override void SetGeneratePosition(float genDuration)
		{
			genPosY += genDuration + removedGenDuration * StageLevelChecker.DifficultyLevel;
		}

		protected override void SetObjectSize(Ice obj)
		{
			float fixedRandMinSize = randMinSize + addedSize * StageLevelChecker.DifficultyLevel;
			float fixedRandMaxSize = randMaxSize + addedSize * StageLevelChecker.DifficultyLevel;

			if (fixedRandMaxSize > generatedMaxSize) {
				fixedRandMaxSize = generatedMaxSize;
			}

			if (fixedRandMinSize > fixedRandMaxSize) {
				fixedRandMinSize = fixedRandMaxSize;
			}

			float fixedRandomSize = Random.Range(fixedRandMinSize, fixedRandMaxSize);
			obj.SetSize(fixedRandomSize);
		}
	}
}

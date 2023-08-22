using Base.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleUnit : PooledObject<ParticleUnit>
{
	/* Fields */
	[SerializeField] ParticleSystem particle;

	//-------------------------------------------------------------------
	/* Properties */
	public ParticleSystem Particle => particle;

	//-------------------------------------------------------------------
	/* Methods */
	public override void OnGetted()
	{
		base.OnGetted();
	}

	public override void OnReleased()
	{
		base.OnReleased();
	}
}

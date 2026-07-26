using Godot;
using System;

public partial class GlobalWorldEnvironment : WorldEnvironment
{
	public float Saturation;
	public void SetSaturation(float Saturation)
	{
		Environment.AdjustmentSaturation = Saturation;
	}
}

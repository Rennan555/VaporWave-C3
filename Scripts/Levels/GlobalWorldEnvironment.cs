using Godot;
using System;

public partial class GlobalWorldEnvironment : WorldEnvironment
{
	public void SetSaturation(float number)
	{
		Environment.AdjustmentSaturation = number;
	}
}

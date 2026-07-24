using Godot;
using System;

public partial class Cascade : Enemy
{
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
		if (IsOnFloor())
		{
			EnemyDie();
		}
	}
}

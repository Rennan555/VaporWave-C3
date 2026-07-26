using Godot;
using System;

public partial class MouseTrigger : Area2D
{
	// Signals
	[Signal]
	public delegate void CallDamageTakenEventHandler();
	
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
		Position = GetGlobalMousePosition();
	}
	
	public void BodyEntered(Node2D body)
	{
		if (body is Enemy enemy)
		{
			GD.Print(enemy.Name);
			EmitSignal(SignalName.CallDamageTaken);
		}
	}
}

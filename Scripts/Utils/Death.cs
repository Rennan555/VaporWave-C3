using Godot;
using System;

public partial class Death : Node
{
	private PackedScene GameOverPack;
	
	private const float Duration = 4f;
	private float Timer = 0.0f;
	
	public override void _Ready()
	{
		GameOverPack = GD.Load<PackedScene>("res://Scenes/Telas/GameOverMenu.tscn");
	}
	
	public override void _Process(double delta)
	{
		if (Timer >= 0)
		{
			Timer -= (float)delta;
		}
		else
		{
			GetTree().ChangeSceneToPacked(this.GameOverPack);
		}
	}
	
	public void CallGameOver(Node node)
	{
		this.Timer = Duration;
		node.AddChild(this);
	}
}

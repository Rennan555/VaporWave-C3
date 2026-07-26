using Godot;
using System;

public partial class GameOverMenu : Control
{
	[Export]
	private PackedScene MenuScene;
	
	public override void _Ready()
	{
	}
	
	public override void _Process(double delta)
	{
	}
	
	public void BackToMenu()
	{
		if (MenuScene != null)
		{
			GetTree().ChangeSceneToPacked(MenuScene);
		}
	}
}

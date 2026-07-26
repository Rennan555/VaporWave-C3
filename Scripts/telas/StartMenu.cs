using Godot;
using System;

public partial class StartMenu : Control
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//teste
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	//sinais dos botões
	public void _on_start_pressed()
	{
		GD.Print("apertou start");
		
		GetTree().ChangeSceneToFile("res://Scenes/Levels/LevelManager/MainManagetLevel.tscn");
	}

	

	public void _on_creditos_pressed()
	{
		GD.Print("apertou creditos");
		
		GetTree().ChangeSceneToFile("res://Scenes/Telas/creditos_menu.tscn");
	}

	public void _on_sair_pressed()
	{
		GD.Print("apertou sair");
		
		GetTree().Quit();
	}
}

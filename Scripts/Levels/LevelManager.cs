using Godot;
using System;

public partial class LevelManager : Node2D
{
	// Atributos de Node
	[Export]
	private PackedScene PackedCurrentLevel = null;
	private Node2D SceneNode;
	
	// Atributos dos Levels
	private int TotalScore = 0;
	
	// Signal de receber pontuação do Level
	[Signal]
	public delegate void CallSumTotalScoreEventHandler(int score);
	
	// Signal para mudar de Level
	[Signal]
	public delegate void CallChangeLevelEventHandler(PackedScene pack);
	
	public override void _Ready()
	{
		// Conecta Signal de somar pontuação e  mudar Level
		this.CallSumTotalScore += SumTotalScore;
		
		// Inicializa Level inicial
		this.SceneNode = GetNode<Node2D>("CurrentSceneNode");
		
		if (this.PackedCurrentLevel == null)
		{
			GD.PrintErr("Sem Scene inicial adicionada!");
		}
		else
		{
			Node2D CurrentLevelNode = this.PackedCurrentLevel.Instantiate<Node2D>();
			this.SceneNode.AddChild(CurrentLevelNode);
		}
	}
	
	public override void _Process(double delta)
	{
	}
	
	// Remove Level atual a adiociona novo via PackedScene
	public void ChangeLevel(PackedScene pack)
	{
		foreach (Node child in this.SceneNode.GetChildren())
		{
			child.QueueFree();
		}
		
		Node2D newNode = pack.Instantiate<Node2D>();
		this.SceneNode.AddChild(newNode);
	}
	
	// Soma a pontuação nova com a total atual
	public void SumTotalScore(int score)
	{
		this.TotalScore += score;
	}
}

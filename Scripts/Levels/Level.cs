using Godot;
using System;

public partial class Level : Node2D
{
	// Atributos de Node
	public PackedScene nextScenePacked;
	private LevelManager ManagerNode;
	private Camera2D CameraNode;
	private Player CameraAnchor;
	
	// Atributos de Node Platform
	[Export]
	public Label ScoreLabel;
	
	// Atributos do Level
	public int LevelScore = 0;
	
	// Signal de receber pontuação dos inimigos
	[Signal]
	public delegate void CallSumLevelScoreEventHandler(int score);
	
	public override void _Ready()
	{
		// Conecta Signal de somar pontuação
		this.CallSumLevelScore += SumLevelScore;
		
		// Pega Node Manager
		this.ManagerNode = GetNode<LevelManager>("../..");
		if (!(this.ManagerNode is LevelManager))
		{
			GD.PrintErr("Scene geral do Level não é LevelManager!");
		}
		
		// Pega Camera2D
		this.CameraNode = GetNode<Camera2D>("Camera2D");
		
		// Pega Anchor
		this.CameraAnchor = GetNode<Player>("PlayerBody");
	}
	
	public override void _Process(double delta)
	{
		if (this.ScoreLabel != null)
		{
			string scoreText = $"Score: {this.LevelScore}";
			this.ScoreLabel.Text = scoreText;
		}
	}
	
	// Soma a pontuação nova com a atual
	public void SumLevelScore(int score)
	{
		this.LevelScore += score;
		this.ManagerNode.EmitSignal(LevelManager.SignalName.CallSumTotalScore, score);
	}
}

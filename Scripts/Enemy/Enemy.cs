using Godot;
using System;

public abstract partial class Enemy : Character
{
	// Atributos de Node
	private Level CurrentLevel;
	
	// Atributos de inimigos
	[Export] private float Damage = 1.0f;
	[Export] private int Score = 10;
	
	public override void _Ready()
	{
		// Pega Level pai
		this.CurrentLevel = GetNode<Level>("..");
		if (!(CurrentLevel is Level))
		{
			GD.PrintErr($"Inimigo '{this.Name}' instaciado em Scene não Level!");
		}
	}
	
	public override void _Process(double delta)
	{
	}
	
	public void DamagePlayer(Node2D body)
	{
		if (body is Player)
		{
			body.EmitSignal(Player.SignalName.DamageTaken, this.Damage);
		}
	}
	
	// Por enquanto, inimigo somente some da tela
	public void EnemyDie()
	{
		QueueFree();
		this.CurrentLevel.EmitSignal(Level.SignalName.CallSumLevelScore, this.Score);
	}
}

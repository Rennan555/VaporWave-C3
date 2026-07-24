using Godot;
using System;

public abstract partial class Enemy : Character
{
	[Export]
	private float Damage = 1.0f;
	
	public override void _Ready()
	{
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
	}
}

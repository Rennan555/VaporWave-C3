using Godot;
using System;

public partial class Inimigo3 : CharacterBody2D
{

	//variaves
	[Export] public Node2D target;
	[Export] public float speed = 100f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		//seguir o player
		if (target != null)
		{
			Vector2 dir = (target.GlobalPosition - GlobalPosition).Normalized();
			Velocity = dir * speed;
		}

		MoveAndSlide();

		
	}

	
	
	
	//detectar a colisão player
	public void _on_area_2d_body_entered(Node2D body)
	{
		if (body.IsInGroup("Player"))
		{
			GD.Print("player detectado");
		}
		
	}
	//adicionar o timer
	//surgir em um espiral
	
}

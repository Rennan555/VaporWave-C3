using Godot;
using System;

public partial class Inimigo2 : CharacterBody2D
{
	[Export]
	public float Speed = 450f;
	private Vector2 _dir = new Vector2(1, 1).Normalized();
	
	public override void _PhysicsProcess(double delta)
	{
		Velocity = _dir * Speed;
		
		KinematicCollision2D collision = MoveAndCollide(Velocity * (float)delta);
		
		if (collision != null)
		{
			_dir = _dir.Bounce(collision.GetNormal()).Normalized();
		}
	}
}

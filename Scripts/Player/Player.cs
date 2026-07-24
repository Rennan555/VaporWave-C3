using Godot;
using System;

public partial class Player : Character
{
	// Flags de movimentação
	private bool CanWalk = true;
	
	// Flags de colisão de parede
	private bool wallOnLeft = false;
	private bool wallOnRight = false;
	
	// Timers do wall jump
	private const float WallJumpDuration = 0.2f;
	private float WallJumpTimer = 0.0f;
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		// Gravidade
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}
		
		// Pulo
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		
		// Timer do Wall Jump
		if (this.WallJumpTimer > 0.0f)
		{
			this.WallJumpTimer -= (float)delta;
		}
		else
		{
			this.CanWalk = true;
		}
		
		// Wall Jump
		if (Input.IsActionJustPressed("ui_accept") && IsOnWall() && !IsOnFloor())
		{
			if (this.wallOnLeft){
				Vector2 wallNormal = GetWallNormal();
				velocity.Y = JumpVelocity;
				velocity.X = Speed * wallNormal.X;
				GD.Print("Parede esquerda");
			}
			else if (this.wallOnRight)
			{
				Vector2 wallNormal = GetWallNormal();
				velocity.Y = JumpVelocity;
				velocity.X = Speed * wallNormal.X;
				GD.Print("Parede direita");
			}
			
			this.CanWalk = false;
			this.WallJumpTimer = WallJumpDuration;
		}
		
		// Movimentação horizontal
		if (this.CanWalk)
		{
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			}
		}
		
		// Aplicação do movimento
		Velocity = velocity;
		MoveAndSlide();
	}
	
	// Checa parede esquerda
	public void LeftWallEntered(Node2D body)
	{
		if (body is StaticBody2D)
		{
			this.wallOnLeft = true;
		}
		GD.Print("Entrou lado esquerdo", body);
	}
	
	public void LeftWallExited(Node2D body)
	{
		if (body is StaticBody2D)
		{
			this.wallOnLeft = false;
		}
		GD.Print("Saiu lado esquerdo", body);
	}
	
	// Checa parede direita
	public void RightWallEntered(Node2D body)
	{
		if (body is StaticBody2D)
		{
			this.wallOnRight = true;
		}
		GD.Print("Entrou lado direito", body);
	}
	
	public void RightWallExited(Node2D body)
	{
		if (body is StaticBody2D)
		{
			this.wallOnLeft = false;
		}
		GD.Print("Saiu lado direito", body);
	}
}

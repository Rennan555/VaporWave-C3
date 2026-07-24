using Godot;
using System;

public partial class Player : Character
{
	// Flags de movimentação
	private bool CanWalk = true;
	
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
			Vector2 wallNormal = GetWallNormal();
			
			velocity.Y = JumpVelocity;
			velocity.X = Speed * wallNormal.X;
			
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
}

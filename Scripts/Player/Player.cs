using Godot;
using System;

public partial class Player : Character
{
	// Status do Player
	private float Life = 3.0f;
	private float DashSpeed = 1200.0f;
	
	// Flags de movimentação
	private bool CanWalk = true;
	
	// Timers do wall jump
	private const float WallJumpDuration = 0.2f;
	private float WallJumpTimer = 0.0f;
	
	// Timers de dash
	private const float DashDuration = 0.2f;
	private float DashTimer = 0.0f;
	
	// Signal de Dano tomado
	[Signal]
	public delegate void DamageTakenEventHandler(float damage);
	
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
		if (this.WallJumpTimer >= 0.0f || this.DashTimer >= 0.0f)
		{
			this.WallJumpTimer -= (float)delta;
			this.DashTimer -= (float)delta;
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
		
		// Dash
		if (Input.IsActionJustPressed("Dash") && !IsOnWall())
		{
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * this.DashSpeed;
				
				this.CanWalk = false;
				this.DashTimer = DashDuration;
			}
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
	
	// Função de morte
	public void PlayerDie()
	{
		QueueFree();
	}
	
	// Função de dano tomado
	public void TakeDamage(float damage)
	{
		GD.Print($"Dano tomado: {damage}, Vida: {this.Life}");
		this.Life -= damage;
		
		if (this.Life <= 0)
		{
			PlayerDie();
		}
	}
}

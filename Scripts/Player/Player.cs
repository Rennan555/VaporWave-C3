using Godot;
using System;

public partial class Player : Character
{
	// Status do Player
	public float Life = 3.0f;
	private float DashSpeed = 1000.0f;
	
	// Node de Player
	private Area2D ActionArea;
	private AnimatedSprite2D AnimatedNode;
	
	// Flags de movimentação
	private bool CanWalk = true;
	private bool CanDash = true;
	
	// Timers do wall jump
	private const float WallJumpDuration = 0.1f;
	private float WallJumpTimer = 0.0f;
	
	// Timers de dash
	private const float DashDuration = 0.07f;
	private const float DashCoolDownDuration = 0.3f;
	private float DashTimer = 0.0f;
	private float DashCoolDownTimer = 0.0f;
	
	// Signal de Dano tomado
	[Signal]
	public delegate void DamageTakenEventHandler(float damage);
	
	public override void _Ready()
	{
		this.ActionArea = GetNode<Area2D>("ActionArea");
		this.AnimatedNode = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		// Lado do Sprite
		if (Input.IsActionJustPressed("ui_left"))
		{
			this.AnimatedNode.FlipH = true;
		}
		else if (Input.IsActionJustPressed("ui_right"))
		{
			this.AnimatedNode.FlipH = false;
		}
		
		// Gravidade
		if (!IsOnFloor())
		{
			if(!IsOnWall())
			{
				velocity += GetGravity() * (float)delta;
			}
			else
			{
				// Grab wall
				Vector2 wallNormal = GetWallNormal();
				Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
				
				GD.Print(wallNormal.X);
				GD.Print(direction.X);
				if (wallNormal.X != direction.X && (Input.IsActionPressed("ui_left") || Input.IsActionPressed("ui_right")))
				{
					velocity += GetGravity() * (float)delta;
					velocity.Y *= 0.8f;
				}
				else
				{
					velocity += GetGravity() * (float)delta;
				}
			}
		}
		
		// Pulo
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}
		
		// Timer de Movimentação
		if (this.WallJumpTimer >= 0.0f || this.DashTimer >= 0.0f)
		{
			this.WallJumpTimer -= (float)delta;
			this.DashTimer -= (float)delta;
		}
		else
		{
			this.CanWalk = true;
		}
		
		// Timer de Dash
		if (this.DashCoolDownTimer >= 0.0f)
		{
			this.DashCoolDownTimer -= (float)delta;
		}
		else
		{
			if (IsOnFloor())
			this.CanDash = true;
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
		if (Input.IsActionJustPressed("Dash") && !IsOnWall() && this.CanWalk && this.CanDash)
		{
			Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
			
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * this.DashSpeed;
				
				this.CanWalk = false;
				this.CanDash = false;
				this.DashTimer = DashDuration;
				this.DashCoolDownTimer = DashCoolDownDuration;
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
		
		if (Input.IsActionJustPressed("Action"))
		{
			Godot.Collections.Array<Node2D> bodies = this.ActionArea.GetOverlappingBodies();
			foreach (Node2D body in bodies)
			{
				if (body is Mascot mascoteNode)
				{
					mascoteNode.EmitSignal(Mascot.SignalName.StartPath);
				}
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

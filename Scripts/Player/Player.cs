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

	//gerenciamento de saturação pelo world environment
	private GlobalWorldEnvironment GlobalEnv;
	
	// Flags de movimentação
	private bool CanWalk = true;
	private bool CanDash = true;
	private bool CanTakeDamage = true;
	
	// Flags de animação
	private string CurrentState = "idle";
	
	// Timers do wall jump
	private const float WallJumpDuration = 0.1f;
	private float WallJumpTimer = 0.0f;
	
	// Timers de dash
	private const float DashDuration = 0.07f;
	private const float DashCoolDownDuration = 0.3f;
	private float DashTimer = 0.0f;
	private float DashCoolDownTimer = 0.0f;
	
	// Timers de dano
	private const float DamageDuration = 0.3f;
	private float DamageTimer = 0.0f;
	
	// Signal de Dano tomado
	[Signal]
	public delegate void DamageTakenEventHandler(float damage);

	
	public override void _Ready()
	{
		this.ActionArea = GetNode<Area2D>("ActionArea");
		this.AnimatedNode = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		//inicia variável de modificação de saturação
		GlobalEnv = GetNode<GlobalWorldEnvironment>("/root/GlobalWorldEnvironment");
	}
	
	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;
		
		CheckState();
		
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
				Vector2 direction = Input.GetVector("left", "right", "up", "down");
				
				if (wallNormal.X != direction.X && (Input.IsActionPressed("left") || Input.IsActionPressed("right")))
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
		if (Input.IsActionJustPressed("up") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
			AudioManager.Instance.PlayPlayerJump();
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
		
		// Timer de dano
		if (this.DamageTimer >= 0.0f)
		{
			this.DamageTimer -= (float)delta;
		}
		else
		{
			this.CanTakeDamage = true;
			
			ShaderMaterial material = (ShaderMaterial)this.AnimatedNode.Material;
			material.SetShaderParameter("active", false);
		}
		
		// Wall Jump
		if (Input.IsActionJustPressed("up") && IsOnWall() && !IsOnFloor())
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
			Vector2 direction = Input.GetVector("left", "right", "up", "down");
			
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
			Vector2 direction = Input.GetVector("left", "right", "up", "down");
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
				if (!AudioManager.Instance.IsPlayerWalkPlaying()) AudioManager.Instance.PlayPlayerWalk();
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				AudioManager.Instance.StopPlayerWalk();
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
	
	public void CheckState()
	{
		if (Input.IsActionJustPressed("left"))
		{
			this.AnimatedNode.FlipH = true;
		}
		else if (Input.IsActionJustPressed("right"))
		{
			this.AnimatedNode.FlipH = false;
		}
		
		Vector2 velocity = Velocity;
		
		if (IsOnFloor())
		{
			if (velocity.X == 0) this.CurrentState = "idle";
			else this.CurrentState = "walk";
		}
		else
		{
			if (velocity.Y < 0) this.CurrentState = "jump_start";
			else
			{
				Vector2 direction = Input.GetVector("left", "right", "up", "down");
				if (IsOnWall() && direction.X != 0) this.CurrentState = "grab";
				else this.CurrentState = "jump_end";
			}
		}
		
		if (!this.CanDash)
		{
			this.CurrentState = "dash";
		}
		
		if (this.AnimatedNode.Animation != this.CurrentState) this.AnimatedNode.Play(this.CurrentState);
	}
	
	public void PlaySFX(string effect)
	{
		
	}
	
	// Função de morte
	public void PlayerDie()
	{
		AudioManager.Instance.PlayPlayerDie();
		
		Death death = new Death();
		death.CallGameOver(this);
		
		Visible = false;
	}
	
	// Função de dano tomado
	public void TakeDamage(float damage)
	{
		if (this.CanTakeDamage)
		{
			GD.Print($"Dano tomado - {damage}, Vida: {this.Life}");
			this.Life -= damage;
			
			ShaderMaterial material = (ShaderMaterial)this.AnimatedNode.Material;
			material.SetShaderParameter("active", true);

			GlobalEnv.Saturation -= 0.3f;
			GlobalEnv.SetSaturation(GlobalEnv.Saturation);
			GD.Print(GlobalEnv.Saturation);
			
			if (this.Life <= 0)
			{
				PlayerDie();
			}
			
			this.DamageTimer = DamageDuration;
			this.CanTakeDamage = false;

			AudioManager.Instance.PlayPlayerDamage();
		}
		else
		{
			GD.Print($"Dano pulado - Vida: {this.Life}");
		}
	}
}

using Godot;
using Godot.Collections;
using System;

public partial class Mascot : Character
{
	[Export] public Path2D Path = null;
	[Export] public PathFollow2D PathFollow  = null;
	
	private Sprite2D testSprite;
	private AnimatedSprite2D animatedSprite;
	private Label ActionLabel;
	
	[Export] Dictionary<float,MascotAnimation> ChangeAnimationPoints = new ();
	
	private bool IsActive = false;
	private bool IsMoving = false;
	private int MoveSpeed = 5;
	
	// Signal de começar caminho do movimento
	[Signal]
	public delegate void StartPathEventHandler();
	
	public override void _Ready()
	{
		this.testSprite = GetNode<Sprite2D>("TestSprite");
		this.animatedSprite = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		this.ActionLabel = GetNode<Label>("ActionLabel");
		
		if (this.Path != null && this.PathFollow != null)
		{
			Sprite2D newSprite = (Sprite2D)this.testSprite.Duplicate();
			AnimatedSprite2D newAnimation = (AnimatedSprite2D)this.animatedSprite.Duplicate();
			
			this.testSprite.QueueFree();
			//this.animatedSprite.QueueFree();
			
			this.PathFollow.AddChild(newSprite);
			this.PathFollow.AddChild(newAnimation);
			this.PathFollow.Visible = false;
		}
		else
		{
			GD.PrintErr("Path e/ou PathFollow não atribuídas!");
		}
	}

	public override void _Process(double delta)
	{
		if (this.IsMoving && this.ChangeAnimationPoints.Count > 0)
		{
			this.PathFollow.Progress += this.MoveSpeed;
			float? keyToRemove = null;
			
			foreach (var pair in this.ChangeAnimationPoints)
			{
				if (pair.Key < this.PathFollow.ProgressRatio + 0.1f && pair.Key > this.PathFollow.ProgressRatio - 0.1f)
				{
					var change = pair.Value;
					
					AnimatedSprite2D animation = this.PathFollow.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
					animation.Play(change.Animation);
					animation.FlipH = change.InvertH;
					animation.FlipV = change.InvertV;
					
					keyToRemove = pair.Key;
					break;
				}
			}
			
			if (keyToRemove.HasValue)
			this.ChangeAnimationPoints.Remove(keyToRemove.Value);
		}
	}
	
	// Permite mascote andar ao ser ativado
	public void EnableToWalk(Node2D body)
	{
		if (body is Player && !this.IsMoving)
		{
			this.ActionLabel.Visible = true;
			this.IsActive = true;
		}
	}
	
	// Desativa mascote ao Player sair de perto
	public void DisableToWalk(Node2D body)
	{
		if (body is Player)
		{
			this.ActionLabel.Visible = false;
			this.IsActive = false;
		}
	}
	
	// Ativa movimento do mascote via Signal
	public void StartPathMovement()
	{
		this.animatedSprite.Visible = false;
		this.PathFollow.Visible = true;
		
		this.IsMoving = true;
		this.ActionLabel.Visible = false;
		this.IsActive = false;
	}
}

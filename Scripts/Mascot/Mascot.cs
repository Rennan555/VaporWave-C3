using Godot;
using System;

public partial class Mascot : Character
{
	[Export] public Path2D Path = null;
	[Export] public PathFollow2D PathFollow  = null;
	
	private Sprite2D testSprite;
	private Label ActionLabel;
	
	private bool IsActive = false;
	private bool IsMoving = false;
	private int Speed = 5;
	
	// Signal de começar caminho do movimento
	[Signal]
	public delegate void StartPathEventHandler();
	
	public override void _Ready()
	{
		this.testSprite = GetNode<Sprite2D>("TestSprite");
		this.ActionLabel = GetNode<Label>("ActionLabel");
		
		if (this.Path != null && this.PathFollow != null)
		{
			Sprite2D newSprite = (Sprite2D)this.testSprite.Duplicate();
			this.testSprite.QueueFree();
			this.PathFollow.AddChild(newSprite);
		}
		else
		{
			GD.PrintErr("Path e/ou PathFollow não atribuídas!");
		}
	}

	public override void _Process(double delta)
	{
		if (this.IsMoving)
		{
			this.PathFollow.Progress += this.Speed;
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
		this.IsMoving = true;
		
		this.ActionLabel.Visible = false;
		this.IsActive = false;
	}
}

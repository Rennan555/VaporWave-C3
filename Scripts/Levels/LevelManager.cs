using Godot;
using System;

public partial class LevelManager : Node2D
{
	[Export]
	private PackedScene PackedCurrentLevel = null;
	private Node2D SceneNode;
	
	// Signal para mudar de Level
	[Signal]
	public delegate void CallChangeLevelEventHandler(PackedScene pack);
	
	public override void _Ready()
	{
		this.SceneNode = GetNode<Node2D>("CurrentSceneNode");
		
		if (this.PackedCurrentLevel == null)
		{
			GD.PrintErr("Sem Scene inicial adicionada!");
		}
		else
		{
			Node2D CurrentLevelNode = this.PackedCurrentLevel.Instantiate<Node2D>();
			this.SceneNode.AddChild(CurrentLevelNode);
		}
	}
	
	public override void _Process(double delta)
	{
	}
	
	// Remove Level atual a adiociona novo via PackedScene
	public void ChangeLevel(PackedScene pack)
	{
		foreach (Node child in this.SceneNode.GetChildren())
		{
			child.QueueFree();
		}
		
		Node2D newNode = pack.Instantiate<Node2D>();
		this.SceneNode.AddChild(newNode);
	}
}

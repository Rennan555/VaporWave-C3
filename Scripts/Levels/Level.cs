using Godot;
using System;

public partial class Level : Node2D
{
	public PackedScene nextScenePacked;
	private Node2D ManagerNode;
	
	public override void _Ready()
	{
		this.ManagerNode = GetNode<Node2D>("../..");
		GD.Print(this.ManagerNode.Name);
	}
	
	public override void _Process(double delta)
	{
	}
}

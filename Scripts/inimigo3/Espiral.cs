using Godot;
using System;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;

public partial class Espiral : Node2D
{
	[Export] public PackedScene enemy = GD.Load<PackedScene>("res://Scenes/Entities/inimigo3/inimigo_3.tscn"); //objeto que será spwanado
	[Export] public Node2D anchor; //usado para manter o centro do surgimento no player
	
	Node2D ponto; 
	int pos = 1;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ponto = GetNode<Node2D>("ponto1");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (enemy != null)
		{
			
			
		}
	}

	public void instance(int pos)
	{
		//instancia
		Inimigo3 instance = (Inimigo3) enemy.Instantiate();	
		//alterar a posicao
		instance.Position = ponto.Position;
		//atribuindo o alvo
		instance.target = anchor;
		//print 
		GD.Print($"ponto{pos}");
		//addicioando
		AddChild(instance);
			
			
		
		
	}

	public void _on_timer_timeout()
	{	
		//adicionar o numero
		pos += 1;
		
		ponto = GetNode<Node2D>($"ponto{pos}");

		instance(pos);

		if (pos == 11)
		{
			pos = 1;
		}
		
	}
}

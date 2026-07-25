using Godot;
using System;

/*Algoritmo para gerenciar o bloco de notas:
os textos,
e a janela pop up*/
public partial class PopUpNotas : Area2D
{
	//onready --> variaveis que se referem a nós presentes na arvore
	private RichTextLabel text;
	private Control popUp;

	//variaveis 
	private bool canClose; // será usado para determinar quando fechar a janela e desativar o bloco de notas
	private int linhaTexto;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//chamado as variaveis
		text = GetNode<RichTextLabel>("Control/RichTextLabel");
		popUp = GetNode<Control>("popUp");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	public void _on_close_pressed()
	{
		//QueueFree();
		if (canClose) {
		popUp.Visible = false;
		}
	}

	public void showPopUp()
	{
		popUp.Visible = true;
	}

	public void editText()
	{
		
	}
}

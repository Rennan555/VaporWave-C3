using Godot;
using System;

public partial class PopWindows11 : Control
{
	/*
	//sinal 
	[Signal]
	public delegate void BtnceitarEventHandler();
	[Signal]
	public delegate void BtnRecusarEventHandler();*/

	[Export] public bool condicao;
	private bool choice;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (choice) {
			GD.Print(condicao);
		}
	}

	public void _on_btn_recusar_pressed()
	{
		if (!choice) {
		condicao = true;
		choice = true;
		EmitSignal(SignalName.Resposta, condicao);
		}
		AudioManager.Instance.PlayMouseClick();
	}

	public void _on_btn_aceitar_pressed()
	{	
		if (!choice) {
		condicao = false;

		AudioManager.Instance.PlayMouseClick();
		choice = true;

		Death death = new Death();
		death.CallGameOver(this);
		}
	}


	[Signal]
	public delegate void RespostaEventHandler(bool response);
}

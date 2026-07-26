using Godot;
using System;

public partial class ScreenLevel : Node2D
{
	// Gerenciamento de saturação pelo world environment
	private GlobalWorldEnvironment GlobalEnv;
	
	// Variáveis do Level
	private Rect2 BoundaryRect = new Rect2(71, 175, 338, 61);
	private bool CanMoveMouse = true;
	
	public override void _Ready()
	{
		//inicia variável de modificação de saturação
		GlobalEnv = GetNode<GlobalWorldEnvironment>("/root/GlobalWorldEnvironment");
		GlobalEnv.Saturation = 1f;
		GlobalEnv.SetSaturation(GlobalEnv.Saturation);
	}
	
	public override void _Process(double delta)
	{
		Vector2 mousePos = GetViewport().GetMousePosition();
		
		if (!this.BoundaryRect.HasPoint(mousePos) && !CanMoveMouse)
		{
			float clampX = Mathf.Clamp(mousePos.X, BoundaryRect.Position.X, BoundaryRect.End.X);
			float clampY = Mathf.Clamp(mousePos.Y, BoundaryRect.Position.Y, BoundaryRect.End.Y);
			
			GetViewport().WarpMouse(new Vector2(clampX, clampY));
		}
	}
}

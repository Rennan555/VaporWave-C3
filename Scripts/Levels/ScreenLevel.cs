using Godot;
using System;

public partial class ScreenLevel : Node2D
{
    //gerenciamento de saturação pelo world environment
	private GlobalWorldEnvironment GlobalEnv;
    
    public override void _Ready()
    {
        //inicia variável de modificação de saturação
		GlobalEnv = GetNode<GlobalWorldEnvironment>("/root/GlobalWorldEnvironment");
		GlobalEnv.Saturation = 1f;
		GlobalEnv.SetSaturation(GlobalEnv.Saturation);
    }
}

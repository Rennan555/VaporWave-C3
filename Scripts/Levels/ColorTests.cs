using Godot;
using System;

public partial class ColorTests : Node2D
{
    private GlobalWorldEnvironment GlobalEnv;


    public override void _Ready()
    {
        GlobalEnv = GetNode<GlobalWorldEnvironment>("/root/GlobalWorldEnvironment");
        GlobalEnv.SetSaturation(0f);
        //menção honrosa joao miguel
    }


}

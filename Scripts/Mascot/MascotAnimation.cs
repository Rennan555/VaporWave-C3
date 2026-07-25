using Godot;
using System;

[GlobalClass]
public partial class MascotAnimation : Resource
{
	[Export] public bool InvertV = false;
	[Export] public bool InvertH = false;
	[Export] public string Animation = "";
}

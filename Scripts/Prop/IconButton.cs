using Godot;
using System;

[GlobalClass]
public partial class IconButton : Button
{
	public override void _Ready()
	{
		// Inicializa Styles do botão
		StyleBoxFlat normalStyle = new StyleBoxFlat();
		StyleBoxFlat hoverStyle = new StyleBoxFlat();
		StyleBoxFlat pressedStyle = new StyleBoxFlat();
		
		// Declara cores de BG
		normalStyle.BgColor = Color.Color8(228, 240, 242, 0);
		hoverStyle.BgColor = Color.Color8(228, 240, 242, 100);
		pressedStyle.BgColor = Color.Color8(228, 240, 242, 180);
		
		// Declara raio das pontas
		hoverStyle.CornerRadiusTopLeft = 4;
		hoverStyle.CornerRadiusTopRight = 4;
		hoverStyle.CornerRadiusBottomLeft = 4;
		hoverStyle.CornerRadiusBottomRight = 4;
		
		pressedStyle.CornerRadiusTopLeft = 4;
		pressedStyle.CornerRadiusTopRight = 4;
		pressedStyle.CornerRadiusBottomLeft = 4;
		pressedStyle.CornerRadiusBottomRight = 4;
		
		// Adiciona aos Styles do botão
		AddThemeStyleboxOverride("normal", normalStyle);
		AddThemeStyleboxOverride("hover", hoverStyle);
		AddThemeStyleboxOverride("pressed", pressedStyle);
	}
	
	public override void _Process(double delta)
	{
	}
}

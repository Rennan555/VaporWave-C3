using Godot;
using System;

public partial class Slider : Node2D
{
	// Atributos do Slider
	private const int MaxScore = 3;
	private int Score = 0;
	
	// Atributos do Pointer
	[Export] private float PointerSpeed = 0.1f;
	[Export] private float PointerAngle = 0.0f;
	private const int PointerLenght = 28;
	private bool IsRotating = true;
	
	// Atributos dos Markers
	private float FirstMarkerAngle;
	private float SecondMarkerAngle;
	private bool DrawnArc = false;
	[Export] private float BaseMarkersDistance = 80.0f;
	
	// Nodes dos Sprites
	private Sprite2D SliderSprite;
	private Sprite2D FirstMarkerSprite;
	private Sprite2D SecondMarkerSprite;
	
	public override void _Ready()
	{
		this.SliderSprite = GetNode<Sprite2D>("SliderSprite");
		this.FirstMarkerSprite = GetNode<Sprite2D>("FirstMarkerSprite");
		this.SecondMarkerSprite = GetNode<Sprite2D>("SecondMarkerSprite");
		
		ArrangeMarkers();
	}
	
	public override void _Process(double delta)
	{
		if (this.IsRotating)
		{
			this.PointerAngle += this.PointerSpeed;
			this.SliderSprite.Rotation = this.PointerAngle;
		}
		
		if (Input.IsActionJustPressed("Action"))
		{
			float pointerRotation = this.SliderSprite.Rotation % Mathf.DegToRad(360);
			float firstRotation = this.FirstMarkerSprite.Rotation % Mathf.DegToRad(360);
			float secondRotation = this.SecondMarkerSprite.Rotation % Mathf.DegToRad(360);
			
			bool inside;
			
			if (firstRotation <= secondRotation)
			{
				inside = pointerRotation >= firstRotation &&
				pointerRotation <= secondRotation;
			}
			else
			{
				inside = pointerRotation >= firstRotation ||
				pointerRotation <= secondRotation;
			}
			
			if (inside)
			{
				this.Score++;
				this.IsRotating = false;
				//ArrangeMarkers();
				GD.Print($"Acertou: {this.Score}");
			}
			else
			{
				GD.Print("Errou");
			}
		}
	}
	
	public void _Drawn()
	{
		Vector2 center = Vector2.Zero;
		float radius = (float)PointerLenght + 2;
		float startAngle = Mathf.DegToRad(this.FirstMarkerAngle);
		float endAngle = Mathf.DegToRad(this.SecondMarkerAngle);
		int pointCount = 32;
		Color color = Colors.Green;
		float width = 4.0f;
		bool antialiased = true;
		
		DrawArc(
			center,
			radius,
			startAngle,
			endAngle,
			pointCount,
			color,
			width,
			antialiased
		);
	}
	
	private void ResetDraw()
	{
		if (this.DrawnArc)
		{
			QueueRedraw();
		}
	}
	
	private void ArrangeMarkers()
	{
		ResetDraw();
		
		// Cria ângulos aleatórios
		this.FirstMarkerAngle = (float)GD.RandRange(0, 360);
		this.SecondMarkerAngle = this.FirstMarkerAngle + this.BaseMarkersDistance;
		
		// Converte ângulos em radianos
		float length = (float)PointerLenght;
		float firstAngle = Mathf.DegToRad(this.FirstMarkerAngle);
		float secondAngle = Mathf.DegToRad(this.SecondMarkerAngle);
		
		// Rotaciona Sprites
		this.FirstMarkerSprite.Rotation = firstAngle + Mathf.Pi / 2;
		this.SecondMarkerSprite.Rotation = secondAngle + Mathf.Pi / 2;
		
		// Posiciona Sprites
		this.FirstMarkerSprite.Position = new Vector2(Mathf.Cos(firstAngle), Mathf.Sin(firstAngle)) * length;
		this.SecondMarkerSprite.Position = new Vector2(Mathf.Cos(secondAngle), Mathf.Sin(secondAngle)) * length;
		
		this.DrawnArc = true;
	}
}

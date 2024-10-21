using Godot;
using System;
using System.Diagnostics;
using System.Drawing;

public partial class PlayerControl : CharacterBody2D
{
	private float Speed { get; set; }
	public override void _Ready()
	{
		_rayCast = GetNode<RayCast2D>("RayCast2D");
	}
	
	private RayCast2D _rayCast;
	
	public override void _Process(double delta)
	{
		//var mousepos = GetGlobalMousePosition();
		//Rotation = Mathf.Lerp(GetGlobalMousePosition().X, GetGlobalMousePosition().Y, 0.3f);
		//LookAt(smoothedMousePos);
		
		Speed = 5;
		float posY = 0;
		float posX = 0;
		
		//Отключение следования за мышью, если нажат шифт
		if(!Input.IsKeyPressed(Key.Shift)) LookAt(GetGlobalMousePosition());
		
		//Стандарт логика передвижения
		if (Input.IsKeyPressed(Key.W))
		{
			Position += new Vector2(0, -Speed);
			posY = Position.Y -Speed;
		}
		if (Input.IsKeyPressed(Key.S))
		{
			Position += new Vector2(0, Speed);
			posY = Position.Y + Speed;
		}
		
		if (Input.IsKeyPressed(Key.A))
		{
			Position += new Vector2(-Speed, 0);
			posX = Position.X - Speed;
		}
		
		if (Input.IsKeyPressed(Key.D))
		{
			Position += new Vector2(Speed, 0);
			posX = Position.X + Speed;
		}
		
		//Логика передвижения с шифтом по диагонали
		if (Input.IsKeyPressed(Key.Shift) 
			&& Input.IsKeyPressed(Key.W) && Input.IsKeyPressed(Key.A) || 
			Input.IsKeyPressed(Key.Shift) 
			&& Input.IsKeyPressed(Key.W) && Input.IsKeyPressed(Key.D) ||
			Input.IsKeyPressed(Key.Shift) 
			&& Input.IsKeyPressed(Key.S) && Input.IsKeyPressed(Key.A) ||
			Input.IsKeyPressed(Key.Shift) 
			&& Input.IsKeyPressed(Key.S) && Input.IsKeyPressed(Key.D))
		{
			Speed = 15;
			LookAt(new Vector2(posX, posY));
		}
		//Логика передвижения с шифтом вверх вниз
		if (Input.IsKeyPressed(Key.Shift) && (Input.IsKeyPressed(Key.W) || 
											  Input.IsKeyPressed(Key.S)) && !Input.IsKeyPressed(Key.A) && !Input.IsKeyPressed(Key.D))
		{
			Speed = 15;
			LookAt(new Vector2(this.Position.X, posY));
		}
		//Логика передвижения с шифтом право лево
		if (Input.IsKeyPressed(Key.Shift) && (Input.IsKeyPressed(Key.A) || 
											  Input.IsKeyPressed(Key.D)) && !Input.IsKeyPressed(Key.S) && !Input.IsKeyPressed(Key.W))
		{
			Speed = 15;
			LookAt(new Vector2(posX, Position.Y));
		}
		
		MoveAndSlide();
	}
}

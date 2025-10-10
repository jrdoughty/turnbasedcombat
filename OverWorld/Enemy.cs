using Godot;
using System;
namespace TwoDGame
{
	public partial class Enemy : CharacterBody2D, IDamagable
	{
		public const float Speed = 300.0f;
		public Vector2 direction;
		[Export]
		public int maxHealth = 8;
		[Export]
		public int health = 8;

		private ProgressBar healthBar; 


		public override void _Ready()
		{
			healthBar = (ProgressBar)GetNode("HealthBar");
		}
		
		public override void _Process(double delta)
		{
			healthBar.Value = (float)health/maxHealth;
			if(healthBar.Value == 1)
			{
				healthBar.Visible = false;
			}
			else
			{
				healthBar.Visible = true;
			}
		}

		public override void _PhysicsProcess(double delta)
		{
			Vector2 velocity = Velocity;

			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * Speed;
				velocity.Y = direction.Y * Speed;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
				velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
			}

			Velocity = velocity;
			MoveAndSlide();
		}

		public void hit (int dmg)
		{
			health -= dmg;
			if (health <= 0)
			{
				QueueFree();
			}
		}
	}
}
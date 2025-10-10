using Godot;
using System;
namespace TwoDGame
{
	public enum ActionType
	{
		None,
		Interact,
		Attack
	}
	public partial class OverworldPlayer : CharacterBody2D
	{
		[Export] 
		public Godot.Collections.Array items = new Godot.Collections.Array{};
		public Godot.Collections.Array interactables = new Godot.Collections.Array{};
		[Export]
		private bool actionActive = false; 
		[Export]
		private ActionType actionType = ActionType.None; 
		[Export]
		private int damage = 3;
		private const float SPEED = 100.0f;
		private const string LEFT = "ui_left";
		private const string RIGHT = "ui_right";
		private const string UP = "ui_up";
		private const string DOWN = "ui_down";
		private const string ACCEPT = "ui_accept";
		private const string ATTACK = "attack";
		private Vector2 direction;
		private Vector2 lastDirection = Vector2.Down;
		private AnimationPlayer characterAnimPlayer;//Character Anim Player
		private AnimationPlayer interactionAnimPlayer;//Attack Anim Player
		private Area2D actionArea2D;
		private int frameTimer = 0;
		private CollisionShape2D collisionShape;


		public override void _Ready()
		{
			
			characterAnimPlayer = (AnimationPlayer)GetNode("CharAnimPlayer");
			interactionAnimPlayer = (AnimationPlayer)GetNode("InteractAnimPlayer");
			actionArea2D = (Area2D)GetNode("Action");
			actionActive = false;
			collisionShape = (CollisionShape2D)GetNode("Action/CollisionShape2D");
			collisionShape.Disabled = true;
		}
		public override void _Process(double delta)
		{
			if (actionType == ActionType.Interact)
			{
				//GD.Print("action interaction active");
				if(frameTimer >= 3)//have to guarantee there's been ~ 3 frames the character could have interacted with the object, any less seems unreliable
				{
					frameTimer = 0;
					//actionType = ActionType.None;
					collisionShape.Disabled = true;
					//GD.Print("disabled properly");
				}
				else
				{
					frameTimer++;
					//GD.Print("timer increment");
				}
			}
				
			if (actionActive && characterAnimPlayer.CurrentAnimationPosition == characterAnimPlayer.CurrentAnimationLength && actionType != ActionType.None)
			{
				actionActive = false;
				actionType = ActionType.None;
				//GD.Print("reset at end of anim");
			}
			else if(!actionActive)
			{
				//GD.Print("test 5");

				if(direction != Vector2.Zero)
					lastDirection = direction;

				if(Input.IsActionJustPressed(ACCEPT))
				{
					actionType = ActionType.Interact;
					collisionShape.Disabled = false;
				}
				else if (Input.IsActionJustPressed(ATTACK))
				{
					Attack();
					GD.Print("attack pressed");
				}
				else
				{
					Move();
				}
			}
			
		}

		private void Attack()
		{
			if(lastDirection.X > 0)
			{
				characterAnimPlayer.Play("AttackRight");
				interactionAnimPlayer.Play("Right");
			}
			else if (lastDirection.X < 0)
			{
				characterAnimPlayer.Play("AttackLeft");
				interactionAnimPlayer.Play("Left");
			}
			else if (lastDirection.Y < 0)
			{
				characterAnimPlayer.Play("AttackUp");
				interactionAnimPlayer.Play("Up");
			}
			else if (lastDirection.Y > 0)
			{
				characterAnimPlayer.Play("AttackDown");
				interactionAnimPlayer.Play("Down");
			}
			actionActive = true;
			direction = Vector2.Zero;
			actionType = ActionType.Attack;
		}

		private void Move()
		{
			direction = Input.GetVector(LEFT, RIGHT, UP, DOWN).Round();
					//Walks
			if(direction.X > 0)
			{
				characterAnimPlayer.Play("WalkRight");
				interactionAnimPlayer.Play("Right");
			}
			else if (direction.X < 0)
			{
				characterAnimPlayer.Play("WalkLeft");
				interactionAnimPlayer.Play("Left");
			}
			else if (direction.Y < 0)
			{
				characterAnimPlayer.Play("WalkUp");
				interactionAnimPlayer.Play("Up");
			}
			else if (direction.Y > 0)
			{
				characterAnimPlayer.Play("WalkDown");
				interactionAnimPlayer.Play("Down");
			}//Idles
			else if(lastDirection.X > 0)
			{
				characterAnimPlayer.Play("IdleRight");
				interactionAnimPlayer.Play("Right");
			}
			else if (lastDirection.X < 0)
			{
				characterAnimPlayer.Play("IdleLeft");
				interactionAnimPlayer.Play("Left");
			}
			else if (lastDirection.Y < 0)
			{
				characterAnimPlayer.Play("IdleUp");
				interactionAnimPlayer.Play("Up");
			}
			else if (lastDirection.Y > 0)
			{
				characterAnimPlayer.Play("IdleDown");
				interactionAnimPlayer.Play("Down");
			}
			else
			{
				characterAnimPlayer.Play("IdleDown");
			}
		}


		public override void _PhysicsProcess(double delta)
		{
			
			Vector2 velocity = Velocity;
			// Get the input direction and handle the movement/deceleration.
			// As good practice, you should replace UI actions with custom gameplay actions.
			if (direction != Vector2.Zero)
			{
				velocity.X = direction.X * SPEED;
				velocity.Y = direction.Y * SPEED;
			}
			else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, SPEED);
				velocity.Y = Mathf.MoveToward(Velocity.Y, 0, SPEED);
			}

			Velocity = velocity;
			MoveAndSlide();
			//Position.MoveToward(Position,50);
			
		}
		



		private void OnActionBodyEntered(Node2D body)
		{
			if(body.IsInGroup("enemy") && actionType == ActionType.Attack)
			{
				IDamagable enemy = (IDamagable)body;
				enemy.hit(damage);
			}
			
		}
		private void OnActionAreaEntered(Area2D area)
		{
			if(area.GetParent().IsInGroup("interactable") && !actionActive)
			{
				actionActive = true;
				IInteractable interactable = (IInteractable)area.GetParent();
				if(interactable is WorldItem worldItem)
				{
					Sprite2D spr = (Sprite2D)GetNode("PickSprite");
					spr.Texture = ((Sprite2D)worldItem.GetNode("Sprite2D")).Texture;
				}
				interactable.interact(this);
				if(lastDirection.X > 0)
				{
					characterAnimPlayer.Play("GrabRight");
					interactionAnimPlayer.Play("Right");
				}
				else if (lastDirection.X < 0)
				{
					characterAnimPlayer.Play("GrabLeft");
					interactionAnimPlayer.Play("Left");
				}
				else if (lastDirection.Y < 0)
				{
					characterAnimPlayer.Play("GrabUp");
					interactionAnimPlayer.Play("Up");
				}
				else if (lastDirection.Y > 0)
				{
					characterAnimPlayer.Play("GrabDown");
					interactionAnimPlayer.Play("Down");
				}
			}
		}
	}

}

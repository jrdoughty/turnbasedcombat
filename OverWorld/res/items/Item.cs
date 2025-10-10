using Godot;
using System;

//using MonoCustomResourceRegistry;
namespace TwoDGame
{
	//[RegisteredType(nameof(Item),"", nameof(Resource))]

	public partial class Item : Resource
	{
		[Export] public string name {get;set;}
		[Export] public string description {get;set;}
		[Export] public int value {get;set;}
		[Export] public int rarity {get;set;}
		[Export] public bool usable {get;set;}
		[Export] public bool equipable {get;set;}
		[Export] public int dropRate {get;set;}
		[Export] public PackedScene icon {get;set;}
		[Export] public bool questItem {get;set;} 
		/*
		public Item()
		{
			name = "";
			description = "";
			value = 0;
			rarity = 0;
			usable = false;
			equipable = false;
			dropRate = 0;
			icon = null;
			sprite = null;
			questItem = false;
		}*/
	}
}
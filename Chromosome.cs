
// This C# script is designed to run on the Unity3D Game Engine
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chromosome : MonoBehaviour {
	// This script resides on each entity and controls the following:
	// - Detecting when the player has clicked on the entity
	// - Recording the time of death
	// - 'Killing' the entity, basically turning it off so it is no longer visible

	// Gene for color (RGB)
	public float r;
	public float g;
	public float b;
	bool dead = false;
	// Record the time til death of the entity
	public float lengthOfLifespan = 0;
	// Make the sprite and collider accessible in this script
	SpriteRenderer sRenderer;
	Collider2D sCollider;

	// OnMouseDown detects when the player has clicked on this particular entity
	void OnMouseDown()
	{
		// Kill the entity
		dead = true;
		lengthOfLifespan = PopulationManager.elapsed;
		//Debug.Log("Dead at: " + timeToDie);
		// Hide the entity
		sRenderer.enabled = false;
		sCollider.enabled = false;
	}

	void Start ()
	{
		// Access the instance of the sprite and collider on the game object this script resides on
		sRenderer = GetComponent<SpriteRenderer>();
		sCollider = GetComponent<Collider2D>();
		sRenderer.color = new Color (r, g, b);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// System.Linq adds useful techniques for sorting lists
using System.Linq;

// It's important to note that the greater the population, the better the results
public class PopulationManager : MonoBehaviour {
	// Manages the population overall
	// - Assigns random values to RGB per entity spawned
	// - Instantiates each entity
	// - Breeds the 50% most fit individuals to generate the next generation


	// The 'Template' that entities will be based off of
	public GameObject entityPrefab;
	// The size of the population
	public int populationSize = 10;
	List<GameObject> population = new List<GameObject>();
	public static float elapsed = 0;
	int trialTime = 10;
	// The first population generated is generation 1
	int generation = 1;

	GUIStyle guiStyle = new GUIStyle();
	void OnGUI()
	{
		guiStyle.fontSize = 50;
		guiStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (10, 10, 100, 20), "Generation: " + generation, guiStyle);
		GUI.Label (new Rect (10, 65, 100, 20), "Trial Time: " + (int)elapsed, guiStyle);
	}

	void Start () {
		for(int i = 0; i < populationSize; i++)
		{
			// Pick a random point on the screen within a boundary (x,y) coordinates
			Vector3 pos = new Vector3(Random.Range(-9,9), Random.Range(-4.5f, 4.5f),0);
			// Instantiate an Entity (Quaternion.identity just gives it zero rotational value)
			GameObject go = Instantiate(entityPrefab, pos, Quaternion.identity);

			// Access the the entity's DNA and set it randomly to start out
			go.GetComponent<Chromosome>().r = Random.Range(0.0f,1.0f);
			go.GetComponent<Chromosome>().g = Random.Range(0.0f,1.0f);
			go.GetComponent<Chromosome>().b = Random.Range(0.0f,1.0f);
			population.Add(go);
		}

	}
		
	GameObject Breed(GameObject parent1, GameObject parent2)
	// Returns a gameObject with DNA mixed between parent1 and parent2 when called
	{
		// Pick a random position in accordance to the bounds of the screen
		Vector3 pos = new Vector3 (Random.Range (-9, 9), Random.Range (-4.5f, 4.5f), 0);
		// Instantiate the offspring
		GameObject offspring = Instantiate (entityPrefab, pos, Quaternion.identity);
		// Obtain parent1 and parent2's DNA
		Chromosome dna1 = parent1.GetComponent<Chromosome> ();
		Chromosome dna2 = parent2.GetComponent<Chromosome> ();

		// ADDING MUTATION: Mutations sometimes happen in nature as well, and can lead to beneficial or detrimental effects
		// Most of the time we get our information from the parent:
		if (Random.Range (0, 6) < 5) {
			// This is the fundamental part of genetic algorithms; go through each gene and randomly swap between parents
			// 50% of the time the child gets parent1's red channel
			// 50% of the time the child gets parent2's red channel
			// etc.
			offspring.GetComponent<Chromosome> ().r = Random.Range (0, 10) < 5 ? dna1.r : dna2.r;
			offspring.GetComponent<Chromosome> ().g = Random.Range (0, 10) < 5 ? dna1.g : dna2.g;
			offspring.GetComponent<Chromosome> ().b = Random.Range (0, 10) < 5 ? dna1.b : dna2.b;
		}
		// Otherwise.. Child is a total mutation (all values are random)
		else {
			offspring.GetComponent<Chromosome> ().r = Random.Range (0.0f, 1.0f);
			offspring.GetComponent<Chromosome> ().g = Random.Range (0.0f, 1.0f);
			offspring.GetComponent<Chromosome> ().b = Random.Range (0.0f, 1.0f);

		}
		return offspring;
	}

	void BreedNewPopulation()
	{
		List<GameObject> newPopulation = new List<GameObject> ();
		// Remove unfit individuals and order the list according to the lengthOfLifespan parameter
		// This means that the fittest individuals are at the bottom of sortedList
		List<GameObject> sortedList = population.OrderBy(o => o.GetComponent<Chromosome>().lengthOfLifespan).ToList();

		population.Clear ();
		// Loop through the list starting halfway (the 50% most fit entities to be bred together)
		for (int i = (int)(sortedList.Count / 2.0f) - 1; i < sortedList.Count - 1; i++) {
			// Breed the individual halfway through the list, with the next individual in the list, and continue through the rest of the list
			// The strategy is to breed i with i+1, then turn around and breed i+1, with i, to meet the population requirements
			Debug.Log(
				"Parent_1 DNA: " + 
				"\nRed Value = " + sortedList[i].GetComponent<Chromosome>().r + 
				"\nGreen Value = " + sortedList[i].GetComponent<Chromosome>().g +
				"\nBlue Value = " + sortedList[i].GetComponent<Chromosome>().b +
				"\nParent_2: " + sortedList[i + 1] +
				"\nRed Value = " + sortedList[i + 1].GetComponent<Chromosome>().r + 
				"\nGreen Value = " + sortedList[i + 1].GetComponent<Chromosome>().g +
				"\nBlue Value = " + sortedList[i + 1].GetComponent<Chromosome>().b		
			);
			population.Add (Breed (sortedList [i], sortedList [i + 1]));
			population.Add (Breed (sortedList [i + 1], sortedList [i]));
		}

		// Destroy all parents and previous population
		for (int i = 0; i < sortedList.Count; i++) {
			Destroy (sortedList [i]);
		}
		generation++;
	}

	void Update () {
		elapsed += Time.deltaTime;
		if (elapsed > trialTime) {
			BreedNewPopulation ();
			elapsed = 0;
		}

	}


}

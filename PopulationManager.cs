using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// It's important to note that the greater the population, the better the results

public class PopulationManager : MonoBehaviour {
    // The 'Template' that entities will be based off of
    public GameObject entityPrefab;
    // The size of the population
    public int populationSize = 10;
    List<GameObjects> population = new List<GameObject>();
    public static float elapsed = 0;
    
    void Start () {
        for(int i = 0; i < populationSize; i++)
        {
            // Pick a random point on the screen within a boundary (x,y) coordinates
            Vector3 pos = new Vector3(Random.Range(-9,9), Random.Range(-4.5f, 4.5f),0);
            // Instantiate an Entity (Quaternion.identity just gives it zero rotational value)
            GameObject go = Instantiate(personPrefab, pos, Quaternion.identity);
            
            // Access the the entity's DNA and set it randomly to start out
            go.GetComponent<EntityDNA>().r = Random.Range(0.0f,1.0f);
            go.GetComponent<EntityDNA>().g = Random.Range(0.0f,1.0f);
            go.GetComponent<EntityDNA>().b = Random.Range(0.0f,1.0f);
            population.Add(go);
        }
    
    } 


}

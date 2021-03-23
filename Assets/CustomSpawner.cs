using UnityEngine;
using System.Collections;

[AddComponentMenu("Playground/Gameplay/Object Creator Area")]
[RequireComponent(typeof(BoxCollider2D))]
public class CustomSpawner : MonoBehaviour
{
	[Header("Object creation")]

	// The object to spawn
	// WARNING: take if from the Project panel, NOT the Scene/Hierarchy!
	[SerializeField]
	public GameObject prefabToSpawn1;
	[SerializeField]
	public GameObject prefabToSpawn2;
	[SerializeField]
	public GameObject prefabToSpawn3;
	[SerializeField]
	public GameObject prefabToSpawn4;

	[Header("Other options")]

	// Configure the spawning pattern
	public float spawnInterval = 1;

	private BoxCollider2D boxCollider2D;

	void Start()
	{
		boxCollider2D = GetComponent<BoxCollider2D>();

		StartCoroutine(SpawnObject());
	}

	// This will spawn an object, and then wait some time, then spawn another...
	IEnumerator SpawnObject()
	{
		while (true)
		{
			// Create some random numbers
			float randomX = Random.Range(-boxCollider2D.size.x, boxCollider2D.size.x) * .5f;
			float randomY = Random.Range(-boxCollider2D.size.y, boxCollider2D.size.y) * .5f;

			float rnd = Random.value * 4;
			int rndInt = Mathf.FloorToInt(rnd);

			GameObject newObject = null;

			switch (rndInt)
			{
				case 0:
					newObject = Instantiate<GameObject>(prefabToSpawn1);
					break;
				case 1:
					newObject = Instantiate<GameObject>(prefabToSpawn2);
					break;
				case 2:
					newObject = Instantiate<GameObject>(prefabToSpawn3);
					break;
				case 3:
					newObject = Instantiate<GameObject>(prefabToSpawn4);
					break;
				default:
					break;
			}

			// Generate the new object
			//GameObject newObject = Instantiate<GameObject>(prefabToSpawn1);
			newObject.transform.position = new Vector2(randomX + this.transform.position.x, randomY + this.transform.position.y);

			// Wait for some time before spawning another object
			yield return new WaitForSeconds(spawnInterval);
		}
	}
}

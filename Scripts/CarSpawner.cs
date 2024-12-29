using System.Collections;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] GameObject[] carPrefabs;// üretilecek olan arabalar.
    [SerializeField] private GameObject[] cheeses;//Üretilecek peynirler.
    
    //Minimum ve maksimum üretme aralığı
    [SerializeField] private float minSpawnTime = 1f;
    [SerializeField] float maxSpawnTime = 3f;
    [SerializeField]float cheesesSpawnMin = 3f;
    [SerializeField] float cheesesSpawnMax = 10f;

    [SerializeField] CharacterController _characterController;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnCars());
        StartCoroutine(SpawnCheeses());
    }

    IEnumerator SpawnCars()
    {
        while (_characterController.isAlive)
        {
            // Rast gele bir süre bekletmeliyiz
            float randomTime = Random.Range(minSpawnTime, maxSpawnTime);
            yield return new WaitForSeconds(randomTime);
            

            //Rast gele bir referans noktası seçelim
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            //Arabayı üretmek 
            Instantiate(carPrefabs[Random.Range(0, carPrefabs.Length)], spawnPoint.position, spawnPoint.rotation);
            
        }
    }

    IEnumerator SpawnCheeses()
    {
        while (_characterController.isAlive)
        {
            // Rast gele bir süre bekletmeliyiz
            float randomTime = Random.Range(cheesesSpawnMin, cheesesSpawnMax);
            yield return new WaitForSeconds(randomTime);
            

            //Rast gele bir referans noktası seçelim
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Transform spawnPoint = spawnPoints[randomIndex];

            //Peyniri üretmek 
            Instantiate(cheeses[0], spawnPoint.position +new Vector3(0,0.01f,0) , spawnPoint.rotation);
            
        }
    }
}

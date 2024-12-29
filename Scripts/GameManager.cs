using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] List<GameObject> Roads = new List<GameObject>();

    [SerializeField] private Transform playerPrefab;
    [SerializeField] Transform carSpawn;
    
    private float previousPlayerZ;
    private float roadLenght = 3.2f;

    private int count = 5;
    // Start is called before the first frame update
    void Start()
    {
        //İlk zemini üret
        Instantiate(Roads[0],transform.position,transform.rotation);

        for (int i = 0; i < count; i++)
        {
            CreateRoad();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerPrefab.position.z > roadLenght - 3.2f*count)
        {
            CreateRoad();
        }
    }

    private void FixedUpdate()
    {
        //Oyuncumun z posiyonundaki değişimini hesapla
        float deltaZ = playerPrefab.position.z - previousPlayerZ;
        
        //Car spawner Z eksenini yukarıdaki değişim kadar arttır
        carSpawn.position += new Vector3(0,0,deltaZ);
        
        //Bir sonraki kare için önceki z posizyonunu güncellemeliyim
        previousPlayerZ = playerPrefab.position.z;
    }


    void CreateRoad()
    {
        Instantiate(Roads[Random.Range(0, Roads.Count)],transform.forward * roadLenght,transform.rotation);
        roadLenght += 3.2f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

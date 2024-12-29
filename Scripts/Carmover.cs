using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmover : MonoBehaviour
{
    [SerializeField] private float carSpeed;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        characterController = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (characterController.isAlive)
        {
            transform.Translate(0,0,carSpeed);
        }
       
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharterSpawner : MonoBehaviour
{
    [SerializeField] GameObject CharterPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(CharterPrefab);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

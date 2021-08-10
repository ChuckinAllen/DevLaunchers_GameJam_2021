using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharterSpawner : MonoBehaviour
{
    [SerializeField] GameObject CharterPrefab;
    void Start()
    {
        Instantiate(CharterPrefab);
    }
}

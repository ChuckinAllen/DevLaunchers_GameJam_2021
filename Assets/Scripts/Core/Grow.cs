using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour
{
    [SerializeField] float growAmount = 10f;
    [SerializeField] GameObject playerLight;
    [SerializeField] float lightGrowSize = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GrowUp()
    {
        gameObject.transform.localScale += new Vector3(transform.localScale.x + growAmount, transform.localScale.y + growAmount, transform.localScale.z + growAmount);
        playerLight.transform.localScale += new Vector3(lightGrowSize, lightGrowSize, lightGrowSize);
    }
}

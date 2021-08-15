using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] Image ShowHide;

    private void Start()
    {
        ShowHide = GameObject.FindWithTag("UI").GetComponent<Image>();
        ShowHide.gameObject.SetActive(false);
    }

    public void ShowDeathScreen()
    {
        ShowHide.gameObject.SetActive(true);
    }
}

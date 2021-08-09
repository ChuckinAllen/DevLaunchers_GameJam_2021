using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Attrbutes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] Health health = null;
        [SerializeField] RectTransform rectTransform = null;
        [SerializeField] Canvas canvas = null;
        void Update()
        {
            if(Mathf.Approximately(health.GetFraction(),0) || Mathf.Approximately(health.GetFraction(), 1))
            {
                canvas.enabled = false;
                return;
            }
            canvas.enabled = true;

            rectTransform.localScale = new Vector3(health.GetFraction(), 1, 1);
        }
    }
}

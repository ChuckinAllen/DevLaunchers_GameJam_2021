using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;

        public void Spawn(float damageAmount)
        {
            DamageText SpawnedText = Instantiate<DamageText>(damageTextPrefab, transform);
            SpawnedText.SetValue(damageAmount);
            //SpawnedText.name = gameObject.transform.parent.name + " Spawned Text";
        }
    }
}


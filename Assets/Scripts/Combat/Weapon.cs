using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Audio;

namespace RPG.Combat
{
    public class Weapon : MonoBehaviour
    {
        //[SerializeField] AudioSource audioSource;
        //[SerializeField] AudioClip[] audioClip;
        [SerializeField] UnityEvent onHit;
        public void OnHit()
        {
            onHit.Invoke();
            /*if(audioClip.Length != 0)
            {
                audioSource.PlayOneShot(audioClip[Random.Range(0, audioClip.Length)]);
            }*/

        }
    }

}
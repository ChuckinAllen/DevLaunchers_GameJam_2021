using BeardedManStudios.Forge.Networking.Generated;
using BeardedManStudios.Forge.Networking;
using UnityEngine;
using System;

namespace RPG.Stats
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] float experiencePoints = 0;

        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public float GetExperiencePoints()
        {
            return experiencePoints;
        }

        internal float GetMaxExperiencePoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.ExperienceToLevelUp);
        }
    }
}
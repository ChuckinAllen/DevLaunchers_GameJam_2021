using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProGressionCharacterClass[] characterClasses = null;

        Dictionary<CharacterClass, Dictionary<Stat, float[]>> LookupTable = null;

        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = LookupTable[characterClass][stat];

            if(levels.Length < level)
            {
                return 0;
            }
            return levels[level - 1];
        }

        public int GetLevels(Stat stat, CharacterClass characterClass)
        {
            BuildLookup();

            float[] levels = LookupTable[characterClass][stat];
            return levels.Length;
        }

        private void BuildLookup()
        {
            if (LookupTable != null) return;

            LookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (ProGressionCharacterClass progressionclass in characterClasses)
            {
                var statLookupTable = new Dictionary<Stat, float[]>();

                foreach(ProgressionStat progressionStat in progressionclass.stats)
                {
                    statLookupTable[progressionStat.stat] = progressionStat.levels;
                }

                LookupTable[progressionclass.characterClass] = statLookupTable;
            }
        }

        [System.Serializable]
        class ProGressionCharacterClass
        {
            public CharacterClass characterClass = default;
            public ProgressionStat[] stats;

        }
        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;
        }

    }
}
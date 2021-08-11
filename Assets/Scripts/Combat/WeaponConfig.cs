using UnityEngine;
using RPG.Attrbutes;
using GameDevTV.Inventories;
using RPG.Stats;
using System.Collections.Generic;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "RPG/Weapons/New Weapon", order = 0)]
	public class WeaponConfig : EquipableItem, IModifierProvider
	{
		[SerializeField] AnimatorOverrideController animatorOverride = null;
		[SerializeField] Weapon equippedPrefab = null;
		[SerializeField] float weaponDamage = 5f;
		[SerializeField] float percentageBonus = 0f;
		[SerializeField] float weaponRange = 2f;
		[SerializeField] bool isRightHanded = true;
		[SerializeField] Projectile projectile = null;

		const string weaponName = "Weapon";

		public Weapon Spawn(Transform rightHand, Transform LeftHand, Animator animator)
        {
			DestroyOldWeapon(rightHand, LeftHand);

            Weapon weapon = null;
			if(equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, LeftHand);
                weapon = Instantiate(equippedPrefab, handTransform);
				weapon.name = weaponName;
			}

			var overideController = animator.runtimeAnimatorController as AnimatorOverrideController;
			if (animatorOverride != null)
            {
				animator.runtimeAnimatorController = animatorOverride;
			}
		    else if (overideController != null)
			{
				animator.runtimeAnimatorController = overideController.runtimeAnimatorController;
			}
			return weapon;
		}

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
			Transform oldWeapon = rightHand.Find(weaponName);
			if(oldWeapon == null)
            {
				oldWeapon = leftHand.Find(weaponName);
            }
			if (oldWeapon == null) return;

			oldWeapon.name = "DESTROYING";
			Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform LeftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = LeftHand;
            return handTransform;
        }

        public bool HasProjectile()
        {
			return projectile != null;
        }

		public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
			Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
			projectileInstance.SetTarget(target, instigator, calculatedDamage);

        }

		public float GetDamage()
        {
			return weaponDamage;
        }
		public float GetPercentageBonus()
        {
			return percentageBonus;
        }
		public float GetRange()
		{
			return weaponRange;
		}

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
			if(stat == Stat.Damage)
			{
				yield return weaponDamage;
			}
        }

        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if(stat == Stat.Damage)
			{
				yield return percentageBonus;
			}
        }
    }
}



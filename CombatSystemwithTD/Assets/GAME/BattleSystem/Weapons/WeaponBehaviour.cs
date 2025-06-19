using System;
using UnityEngine;
namespace CS
{
    public class WeaponBehaviour : MonoBehaviour
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private GameObject _sparkGameObject;
        private Weapon _weapon;


        public void Init(Weapon weapon)
        {
            _weapon = weapon;
            weapon.DamageActivated += () => {
                _sparkGameObject.SetActive(true);
                _collider.enabled = true; };
            weapon.DamageDeactivated += () => { 
                _sparkGameObject.SetActive(false);
                _collider.enabled = false; };
        }

        
        

        private void OnTriggerEnter(Collider collider)
        {
            _weapon.Hit(collider); 
        }
    }
}
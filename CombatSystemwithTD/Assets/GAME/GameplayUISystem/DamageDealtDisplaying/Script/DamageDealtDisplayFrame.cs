using UnityEngine;
namespace CS
{
    public class DamageDealtDisplayFrame   
    {
        private DamageDealtDisplayBehavior _damageDealtDisplayBehaviorPrefab;
        private DamageCalculator _damageCalculator ;
        private Transform _parentTransform;
        private Camera _mainCamera;


        public DamageDealtDisplayFrame(DamageDealtDisplayBehavior damageDealtDisplayBehaviorPrefab ,Transform parent)
        {
            _mainCamera = Camera.main;
            _damageDealtDisplayBehaviorPrefab = damageDealtDisplayBehaviorPrefab;
            _damageCalculator = new DamageCalculator();
            _parentTransform = parent;
        }


        public void Display(IWeapon weapon, IDamagable damageble, IWeaponUser user)
        {
            float damage =_damageCalculator.CalculateDamage(weapon,user);
           GameObject.Instantiate( _damageDealtDisplayBehaviorPrefab.gameObject , CalculateScreenPosition(damageble.Position),Quaternion.identity,_parentTransform).GetComponent<DamageDealtDisplayBehavior>().Play((int)damage);
        }


        private Vector3 CalculateScreenPosition(Vector3 worldPosition)
        {
            return _mainCamera.WorldToScreenPoint(worldPosition);
        }
    }
}
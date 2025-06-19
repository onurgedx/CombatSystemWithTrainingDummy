namespace CS
{
    public class DamageCalculator
    {

        public float CalculateDamage(IWeapon weapon , IWeaponUser weaponUser)
        {
            float weaponPower = weapon ==null ? 1:weapon.Power;
            float userPower = weaponUser ==null ? 1:weaponUser.Power;           
            return weaponPower * userPower;
        }

    }
}
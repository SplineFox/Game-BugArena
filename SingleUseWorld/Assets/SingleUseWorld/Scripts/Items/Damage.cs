using UnityEngine;

namespace SingleUseWorld
{
    public struct Damage
    {
        #region Fields
        public int amount;
        public Vector2 direction;
        public Vector2 horizontalKnockback;
        public float verticalKnockback;
        public float spinKnockback;
        #endregion

        #region Constructors
        public Damage(int amount, Vector2 direction, Vector2 horizontalKnockback, float verticalKnockback, float spinKnockback)
        {
            this.amount = amount;
            this.direction = direction;
            this.horizontalKnockback = horizontalKnockback;
            this.verticalKnockback = verticalKnockback;
            this.spinKnockback = spinKnockback;
        }
        #endregion
    }
}
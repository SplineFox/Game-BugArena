using UnityEngine;

namespace SingleUseWorld
{
    public class Bottle : Item
    {
        public override void Use(Vector2 direction)
        {
            Detach();
            //_elevator.height = 1.8f;
            _projectile.SetVelocity(direction * 5f, 0f);
        }
    }
}
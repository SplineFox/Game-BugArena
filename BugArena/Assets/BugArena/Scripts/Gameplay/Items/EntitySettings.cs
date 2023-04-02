using UnityEngine;
using UnityEditor;

namespace BugArena
{
    public abstract class EntitySettings : ScriptableObject
    {
        #region Fields
        [ColoredHeader("Entity parameters:", "#7FD6FD")]
        [Range(0f, 1.8f)]
        public float LaunchHeight = 1f;
        [Min(0.25f)]
        public float LaunchOffset = 0.34375f;

        [Space]
        public float DamageAmount = 100;
        public RangeFloat KnockbackHorizontalSpeed = new RangeFloat(1f, 3f);
        public RangeFloat KnockbackVerticalSpeed = new RangeFloat(1f, 3f);
        public RangeFloat KnockbackSpinSpeed = new RangeFloat(90, 180);
        #endregion
    }
}
using UnityEngine;

namespace Extensions
{
    public static class RandomExtensions
    {
        public static Vector3 GenerateRandomDirectionXZ()
        {
            var twoDDirection = Random.insideUnitCircle;
            return new Vector3(twoDDirection.x, 0f, twoDDirection.y);
        }

        public static bool CanProc(this float decimalPercentage)
        {
            return decimalPercentage >= Random.Range(0.01f, 1f);
        }
    }
}

using UnityEngine;

namespace Helpers
{
    public static class Math
    {
        public static int RoundNearestNonZeroInt(float val)
        {
            if (val > 0)
            {
                return Mathf.CeilToInt(val);
            }
            else if (val < 0)
            {
                return Mathf.FloorToInt(val);
            }
            else
            {
                return 0;
            }
        }
    }
}
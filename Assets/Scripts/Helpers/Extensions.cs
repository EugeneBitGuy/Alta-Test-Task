using UnityEngine;

public static class Extensions
{
    public static bool Includes(this LayerMask mask, int layer)
    {
        return (mask.value & 1 << layer) > 0;
    }

    public static bool Excludes(this LayerMask mask, int layer)
    {
        return !mask.Includes(layer);
    }
}

#nullable enable

using UnityEngine;

namespace Utils
{
    /// <summary>
    /// The utilitary class for raycasts
    /// </summary>
    public static class RaycastUtils
    {
        /// <summary>
        /// Cast the specified raycast on the specified range
        /// </summary>
        /// <param name="ray">The ray to launch</param>
        /// <param name="range">The range</param>
        /// <returns>The hit object, NULL if no one</returns>
        public static RaycastHit? Cast(Ray ray, float range)
        {
            return Physics.Raycast(ray, out var raycastHit, range) ? raycastHit : null;
        }
    }
}
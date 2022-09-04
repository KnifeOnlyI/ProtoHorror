using UnityEngine;

namespace Utils
{
    public static class SerializationUtils
    {
        public static float[] Serialize(Vector3 value)
        {
            return new float[]
            {
                value.x,
                value.y,
                value.x
            };
        }

        public static float[] Serialize(Color value)
        {
            return new float[]
            {
                value.r,
                value.g,
                value.b,
                value.a
            };
        }

        public static Vector3 UnserializeVector3(float[] value)
        {
            return new Vector3(value[0], value[1], value[2]);
        }

        public static Color UnserializeColor(float[] value)
        {
            return new Color(value[0], value[1], value[2], value[3]);
        }
    }
}
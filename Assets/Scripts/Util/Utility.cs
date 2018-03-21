using UnityEngine;


namespace Maskirovka.Utility
{
    public class Utility
    {
        public static float GetCatagoryValue( Vector3 vector, Catagorie catagorie )
        {
            float result = 0;
            switch( catagorie )
            {
                case Catagorie.A:
                    result = vector.x;
                    break;
                case Catagorie.B:
                    result = vector.y;
                    break;
                case Catagorie.C:
                    result = vector.z;
                    break;
            }
            return result;
        }

        public static Vector3 SetCatagoryValue( Vector3 vector, Catagorie catagorie, float value )
        {
            switch( catagorie )
            {
                case Catagorie.A:
                    vector.x = value;
                    break;
                case Catagorie.B:
                    vector.y = value;
                    break;
                case Catagorie.C:
                    vector.z = value;
                    break;
            }
            return vector;
        }
    }
}
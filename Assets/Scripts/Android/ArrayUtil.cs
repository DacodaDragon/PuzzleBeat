using System.Collections.Generic;

namespace Android.Touchmanager
{
    public class ArrayUtil
    {
        public static List<t> FilterOut<t>(List<t> elements, List<t> from)
        {
            List<t> E = new List<t>(from);
            for (int i = 0; i < elements.Count; i++)
            {
                E.Remove(elements[i]);
            }
            return E;
        }
    }
}
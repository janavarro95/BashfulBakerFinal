using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public class Enums
    {
        public enum CookingStation
        {
            MixingBowl,
            Oven
        }

        /// <summary>
        /// Gets all of the values stored in an enum.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T[] GetValues<T>()
        {
            return (T[])Enum.GetValues(typeof(T));
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utilities
{
    public static class CSExtensions
    {
        public static string PathCombine(this List<string> paths)
        {
            if (paths.Count == 0) return "";
            if (paths.Count == 1) return paths[0];
            string final = paths[0];
            for(int i = 1; i < paths.Count; i++)
            {
                final = Path.Combine(final, paths[i]);
            }
            return final;

        }


    }
}

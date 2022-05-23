using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSTAADWrapperDemo
{
    public class StaadFileDirectory
    {
        private static string[] GetFileExtensionsCreatedOnCreateNewModel()
        {
            return new string[]
            {
                ".App.log",
                ".cod",
                ".emf",
                ".png",
                ".REI_Saved_Picture",
                ".sbk",
                ".std",
                ".std.metadata",
                ".uid"
            };
        }

        public static List<string> GetFilePathsCreatedOnCreateNewModel(string filePath)
        {
            List<string> filePaths = new List<string>();
            foreach (var extension in GetFileExtensionsCreatedOnCreateNewModel())
            {
                filePaths.Add(Path.ChangeExtension(filePath, extension));
            }
            return filePaths;
        }
    }
}

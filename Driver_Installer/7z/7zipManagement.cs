using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Common;
using SharpCompress.Factories;
using SharpCompress.Readers;
using SharpCompress.Writers;


namespace Driver_Installer._7z
{
    public partial class _7zipManagement
    {

        public static void Extract7z(string sourcePath, string extractPath = null, string password = null)
        {
            SharpCompress.Factories.SevenZipFactory fac = new SharpCompress.Factories.SevenZipFactory();
            FileInfo fi = new FileInfo(sourcePath);
            using (var archive = fac.Open(fi.OpenRead()))
            {
                if (fac.IsArchive(fi.OpenRead(), password))
                    foreach (var entry in archive.Entries)
                    {
                        if (!entry.IsDirectory)
                        {
                            archive.ExtractToDirectory(extractPath);
                        }
                    }
            }
        }

        public void CompressFilesTo7z(string sourcePath, string destinationPath = null, string password = null)
        {
            FileInfo fi = new FileInfo(sourcePath);
            DirectoryInfo directory = new DirectoryInfo(destinationPath);
            SharpCompress.Factories.SevenZipFactory fac = new SharpCompress.Factories.SevenZipFactory();
            using (var archive = fac.Open(fi.OpenRead()))
            {
                if (fac.IsArchive(fi.OpenRead(), password))
                {
                    if (fi.Exists)
                    {

                        string pa = (destinationPath != null ? directory.FullName : directory.FullName + "\\" + fi.Name);
                        archive.WriteToDirectory(pa, new SharpCompress.Common.ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                    //else if (directory.Exists)
                    //{
                    //    foreach (var item in archive.Entries)
                    //    {
                    //        archive.ExtractAllEntries().WriteEntryToFile(destinationPath!=null? destinationPath: directory.FullName+ directory.Name+".7z");
                    //    }

                    //}
                }
            }

        }

    }
}

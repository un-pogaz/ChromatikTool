using System;
using System.Collections.Generic;
using System.IO;

namespace Chromatik.Fichier
{

   /// <summary>
   /// Tri de fichiers
   /// </summary>
   public static class clsSortFiles
	{
		/// <summary>
		/// Tri une liste de fichiers par NOM
		/// </summary>
		/// <param name="path">Dossier des fichiers</param>
		/// <param name="filter">Filtre</param>
		/// <returns>Liste fichiers</returns>
		public static List<FileInfo> SortByName(string path, string filter)
		{
			FileInfo[] fi = new DirectoryInfo(path).GetFiles(filter);
			List<FileInfo> fis = new List<FileInfo>();
			foreach (FileInfo file in fi)
            fis.Add(file);
			fis.Sort(new CreationNameComparer()); // tri par nom			
			return fis;
		}

      /// <summary>
      /// Tri une liste de fichiers par DATE
      /// </summary>
      /// <param name="path">Dossier des fichiers</param>
      /// <param name="filter">Filtre</param>
      /// <returns>Liste fichiers</returns>
      public static List<FileInfo> SortByDate(string path, string filter)
      {
         FileInfo[] fi = new DirectoryInfo(path).GetFiles(filter);
         List<FileInfo> fis = new List<FileInfo>();
         foreach (FileInfo file in fi)
            fis.Add(file);
         fis.Sort(new CreationDateComparer()); // tri par date
         return fis;
      }



		// Tri des fichiers par DATE
      private class CreationDateComparer : IComparer<FileInfo>
      {
         public int Compare(FileInfo f1, FileInfo f2)
         {
            return DateTime.Compare(f1.LastWriteTime, f2.LastWriteTime);
         }
      }

      // Tri des fichiers par NOM
		private class CreationNameComparer : IComparer<FileInfo>
		{
			public int Compare(FileInfo f1, FileInfo f2)
			{
				return String.CompareOrdinal(f1.Name, f2.Name);
			}
		}
	}

  
}

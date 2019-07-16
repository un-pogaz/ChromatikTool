using System;
using System.IO;
using Chromatik.Data;
using Chromatik.Tools;

namespace Chromatik.Fichier
{
   public static class clsOutilsFichier
   {
      /// <summary>
      /// Compare 2 fichiers
      /// </summary>
      /// <param name="fic1">Fichier 1</param>
      /// <param name="fic2">Fichier 2</param>
      /// <param name="date">Comparer la date</param>
      /// <param name="taille">Comparer la taille</param>
      /// <param name="MD5">Comparer le MD5</param>
      /// <returns></returns>
      public static bool ComparerFichiers(string fic1, string fic2, bool date, bool taille, bool MD5)
      {
         bool identique = true;

         if (File.Exists(fic1) && File.Exists(fic2))
         {
            FileInfo infoF1 =new FileInfo(fic1); 
                        FileInfo infoF2 =new FileInfo(fic2); 

            //FileAttributes attributesF1 = File.GetAttributes(fic1);
            //FileAttributes attributesF2 = File.GetAttributes(fic2);

            if (date)
            {
               if (infoF1.LastWriteTime != infoF2.LastWriteTime)
                  identique = false;
            }

            if (identique &&  taille)
            {
               if (infoF1.Length != infoF2.Length)
                  identique = false;
            }

            if (identique && MD5)
            {
               clsErr Err = new clsErr();
               string md5_F1;
               string md5_F2;
               clsMD5File.CalculMD5fichier(fic1, out md5_F1, out Err);
               clsMD5File.CalculMD5fichier(fic2, out md5_F2, out Err);
               if (md5_F1 != md5_F2)
                  identique = false;
            }

         }
         else
         {
            identique = false;
         }


         return identique;

      }
   }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Chromatik.Fichier
{
   /// <summary>
   /// Gestion effacement de fichiers
   /// </summary>
   public static class clsEffacerFichiers
   {

      /// <summary>
      /// Effacement de tous les fichiers dans un dossier, avec une extension donnée
      /// </summary>
      /// <param name="Dossier">Dossier</param>
      /// <param name="Filtre">Filtre ( Ex : "*.txt")</param>
      /// <returns>Nombre de fichier effacés</returns>
      public static int EffacerFichier_Filtre(string Dossier, string Filtre)
      {
         int ret = 0;
         
         string[] files = Directory.GetFiles(Dossier, Filtre);
         foreach (string file in files)
         {
            try
            {
               File.Delete(file);
               ret++;
            }
            catch
            {
            }
         }

         return ret;

      }
   }
}

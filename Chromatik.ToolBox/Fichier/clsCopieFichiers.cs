using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Chromatik.Data;

namespace Chromatik.Fichier
{
   /// <summary>
   /// Gestion copie de fichiers
   /// </summary>
   public static class clsCopieFichiers
   {
      /// <summary>
      /// Purge les fichiers d'un dossier
      /// </summary>
      /// <param name="repSource">Dossier source</param>
      /// <param name="repDest">Dossier de sauvegarde. Chaine vide si pas de sauvegarde</param>
      /// <param name="filtre">Filtre des fichiers a traiter. Exemple : *.txt </param>
      /// <param name="nbFichierAconserver">Nombre de fichier à conserver</param>
      /// <param name="nbJoursAconserver">Période en nombre de jours de fichiers à conserver</param>
      /// <param name="ecraserSiExiste">VRAI : Ecraser le fichier si il existe déjà dans le dossier de destination. *
      /// Sinon, création d'un nouveau fichier avec un suffixe composé de la date et l'heure de copie. </param>
      /// <param name="prefixe">Préfixe ajouté en debut du fichier copié</param>
      /// <param name="Err">Retour d'erreur</param>
      /// <returns>"true"si OK</returns>
      public static bool Purge(string repSource, string repDest, string filtre, int nbFichierAconserver,
                               int nbJoursAconserver, bool ecraserSiExiste, string prefixe, out clsErr Err)
      {
         Err = new clsErr();

         //  Vérifier que les 2 répertoires sont accessibles
         if ((Directory.Exists(repSource)) && (Directory.Exists(repDest) || string.IsNullOrEmpty(repSource)))
         {
            // Liste des fichiers
            List<FileInfo> fis = clsSortFiles.SortByDate(repSource, filtre);
            fis.Reverse(); // Pour remettre dans l'ordre de date décroissant


            DateTime dateLimite = DateTime.MaxValue;

            if (nbJoursAconserver > 0)
               dateLimite = DateTime.Now.AddDays(-nbJoursAconserver);


            string suffixeSidoublon = "_" + DateTime.Now.Year.ToString()
                                      + "_" + DateTime.Now.Month.ToString("00")
                                      + "_" + DateTime.Now.Day.ToString("00")
                                      + "_" + DateTime.Now.Hour.ToString("00")
                                      + "_" + DateTime.Now.Minute.ToString("00")
                                      + "_" + DateTime.Now.Second.ToString("00");


            int cpt = 0;
            foreach (FileInfo fi in fis)
            {
               cpt++;
               // Filtr nb fichiers ou nb de jours
               if (((nbFichierAconserver > 0) && (cpt > nbFichierAconserver))
                   || (((nbJoursAconserver > 0) && (fi.LastWriteTime < dateLimite))
                       || ((nbFichierAconserver == 0) && nbJoursAconserver == 0)))
               {
                  // Déplacer le fichier
                  if (!string.IsNullOrEmpty(repDest))
                  {
                     string ficDest;
                     if (string.IsNullOrEmpty(prefixe))
                        ficDest = repDest + "\\" + fi.Name;
                     else
                        ficDest = repDest + "\\" + prefixe + fi.Name;
                     if ((File.Exists(ficDest)) && (!ecraserSiExiste))
                     {
                        string extension = Path.GetExtension(ficDest);
                        if (extension != null)
                           ficDest = ficDest.Replace(extension, suffixeSidoublon + extension);
                        else
                           ficDest = ficDest + suffixeSidoublon;
                     }
                     try
                     {
                        File.Move(fi.FullName, ficDest);
                     }
                     catch (Exception e)
                     {
                        Err.msgErr += fi.FullName + "\n" + ficDest + "\n" + e.Message + "\n\n";
                        Err.e = e; // Nota : ne conservera que la dernière exception
                     }
                  }
                  else
                  {
                     // Suppression sans sauvegarde
                     try
                     {
                        File.Delete(fi.FullName);
                     }
                     catch (Exception e)
                     {
                        Err.msgErr += fi.FullName + "\n" + e.Message + "\n\n";
                        Err.e = e; // Nota : ne conservera que la dernière exception
                     }
                  }
               }
            }

         }
         else
         {
             Err.msgErr =  "clsFichier_errRepertoireInaccessible"
                     + "\n" + repSource
                     + "\n" + repDest;

         }

         return (string.IsNullOrEmpty( Err.msgErr ));
      }

        

   }
}
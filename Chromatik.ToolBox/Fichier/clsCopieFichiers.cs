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
                        Err.msgErr += fi.FullName + clsTxt.NL() + ficDest + clsTxt.NL() + e.Message + clsTxt.NL(2);
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
                        Err.msgErr += fi.FullName + clsTxt.NL() + e.Message + clsTxt.NL(2);
                        Err.e = e; // Nota : ne conservera que la dernière exception
                     }
                  }
               }
            }

         }
         else
         {
             Err.msgErr =  clsMultilangue.Msg("clsFichier_errRepertoireInaccessible")
                     + clsTxt.NL() + repSource
                     + clsTxt.NL() + repDest;

         }

         return (string.IsNullOrEmpty( Err.msgErr ));
      }



      // A TERMINER      TESTER   (copié de l'appli OPHELY)

      /// <summary>
      /// Copie d'un répertoire et sous répertoires
      /// </summary>
      /// <param name="SourceDir">Repertoire source</param>
      /// <param name="DestDir">Repertoire destination</param>
      public static void CopyDir(string SourceDir, string DestDir)
      {
         DirectoryInfo dir = new DirectoryInfo(SourceDir);
         if (dir.Exists)
         {
            string realDestDir;
            if (dir.Root.Name != dir.Name)
            {
               realDestDir = Path.Combine(DestDir, dir.Name);
               if (!Directory.Exists(realDestDir))
                  Directory.CreateDirectory(realDestDir);
            }
            else realDestDir = DestDir;

            foreach (string d in Directory.GetDirectories(SourceDir))
               CopyDir(d, realDestDir);

            foreach (string file in Directory.GetFiles(SourceDir))
            {
               try
               {
                  string fileNameDest = Path.Combine(realDestDir, Path.GetFileName(file));
                  File.Copy(file, fileNameDest, true);
               }
               catch
               {
               }
            }
         }
      }


      // A TERMINER      TESTER   (copié de l'appli OPHELY)
      /// <summary>
      /// Copie d'un répertoire et sous répertoires
      /// </summary>
      /// <param name="SourcePath">Repertoire source</param>
      /// <param name="DestPath">Repertoire destination</param>
      /// <param name="lblSuiviCopie">Label utilisé pour suivi de la copie</param>
      /// <param name="Overwrite"> "true" : un fichier est écrasé si il est déjà existant </param>      
      public static void CopyDir(string SourcePath, string DestPath, Label lblSuiviCopie,
                                 bool Overwrite)
      {
         DirectoryInfo SourceDir = new DirectoryInfo(SourcePath);
         DirectoryInfo DestDir = new DirectoryInfo(DestPath);

         // the source directory must exist, otherwise throw an exception
         if (SourceDir.Exists)
         {
            //if destination SubDir's parent SubDir does not exist throw an exception
            if (DestDir.Parent != null && !DestDir.Parent.Exists)
               throw new DirectoryNotFoundException
                  (@"Le répertoire racine de destination n'existe pas : \n" + DestDir.Parent.FullName);

            if (!DestDir.Exists)
               DestDir.Create();

            // copy all the files of the current directory
            foreach (FileInfo ChildFile in SourceDir.GetFiles())
            {
               if (lblSuiviCopie != null)
               {
                  lblSuiviCopie.Text = clsMultilangue.Msg("Copie_en_cours") + ChildFile.Name;
                  lblSuiviCopie.Refresh();
               }
               if (Overwrite)
               {
                  ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), true);
               }
               else
               {
                  // if Overwrite = false, copy the file only if it does not exist
                  // this is done to avoid an IOException if a file already exists
                  // this way the other files can be copied anyway...
                  if (!File.Exists(Path.Combine(DestDir.FullName, ChildFile.Name)))
                     ChildFile.CopyTo(Path.Combine(DestDir.FullName, ChildFile.Name), false);
               }
            }

            // copy all the sub-directories by recursively calling this same routine
            foreach (DirectoryInfo SubDir in SourceDir.GetDirectories())
               CopyDir(SubDir.FullName, Path.Combine(DestDir.FullName, SubDir.Name), lblSuiviCopie, Overwrite);
         }
         else
         {
            throw new DirectoryNotFoundException("Le répertoire source n'existe pas : \n" + SourceDir.FullName);
         }

         if (lblSuiviCopie != null)
         {
            lblSuiviCopie.Text = "";
         }
      }



      // A TERMINER     TESTER   (copié de l'appli OPHELY)
      /// <summary>
      /// Copie des fichiers d'un répertoire vers un autre
      /// </summary>
      /// <param name="sourceDir">Repertoire source</param>
      /// <param name="destDir">Repertoire destination</param>
      public static void CopyFiles(string sourceDir, string destDir)
      {

         if (Directory.Exists(sourceDir))
         {
            if (!(Directory.Exists(destDir)))
            {
               try
               {
                  Directory.CreateDirectory(destDir);
               }
               catch
               {
               }
            }
            try
            {
               String[] ListeFichier = Directory.GetFiles(sourceDir);
               for (int i = 0; i < ListeFichier.Length; i++)
                  File.Copy(ListeFichier[i], destDir + '\\' + Path.GetFileName(ListeFichier[i]), true);
            }
            catch
            {
            }
         }
      }



      // A TERMINER      TESTER   (copié de l'appli OPHELY)
      /// <summary>
      /// Suppression d'un répertoire
      /// </summary>
      /// <param name="Dir">Repertoire à supprimer</param>
      public static void RazDir(string Dir)
      {
         DirectoryInfo dir = new DirectoryInfo(Dir);
         try
         {
            dir.Delete(true);
         }
         catch
         {
         }
      }


      /// <summary>
      /// Copie un fichier dans un dossier d'archives, an ajoutant un indice pour conserver plusieurs versions de fichiers
      /// </summary>
      /// <param name="fichier">Nom complet du fichier à archiver </param>
      /// <param name="dirArchive">Dossier d'archivage</param>
      public static void ArchiverFichierAvecIndice(string fichier, string dirArchive)
      {

         if (!Directory.Exists(dirArchive))
            Directory.CreateDirectory(dirArchive);

         string nomFicDest1 = dirArchive + "\\" + Path.GetFileName(fichier);
         string nomFicDest = nomFicDest1;

         bool fin = false;
         int i = 0;
         string dernierFicArchive = "";
         while (!fin)
         {
            dernierFicArchive = nomFicDest;
            nomFicDest = nomFicDest1.Replace(".", "_" + i.ToString("000") + ".");
            if (!File.Exists(nomFicDest))
            {
               // Vérifier si modif depuis dernière archive
               bool copier = true;
               if (i > 0)
               {
                  string MD5fichier;
                  string MD5Archive;
                  clsErr err;
                  Tools.clsMD5File.CalculMD5fichier(fichier, out MD5fichier, out err);
                  Tools.clsMD5File.CalculMD5fichier(dernierFicArchive, out MD5Archive, out err);
                  copier = (MD5Archive != MD5fichier);
               }
               if (copier)
                  File.Copy(fichier, nomFicDest, true);
               fin = true;
            }
            i++;
         }

      }


   }
}
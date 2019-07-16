using System;
using System.Diagnostics;

namespace Chromatik.Env
{

   /// <summary>
   /// Gestion process
   /// </summary>
   public static class clsProcess
   {
      /// <summary>
      /// Lance un exécutable et récupère le code retour de l'exe
      /// </summary>
      /// <param name="exe">Executable (chemin et nom)</param>
      /// <param name="param">Paramètres </param>
      /// <param name="timeOut">Time Out en secondes</param>
      /// <returns>Core retour de l'exécutable ou -1 si timeout ou erreur lancement   </returns>
      public static int LanceExeEtAttenteRetour(string exe, string param, int timeOut)
      {
         int ret = -1;
         Process p = Process.Start(exe, param);

         if (p != null)
         {
            bool exited = p.WaitForExit(timeOut*1000);
            if (exited)
               ret = p.ExitCode;
            else
               ret = -1;
         }
         return ret;
      }


      /// <summary>
      /// Recherche si un processus exist, et retourne la liste des processus.      
      /// </summary>
      /// <param name="nomProcess">Nom du process.  Exemple "Notepad"</param>
      /// <param name="ID_aIgnorer">Id  d'un process a ignorer (par exemple celui qui appel) </param>
      /// <param name="titreMain">Optionnel Titre de la fenêtre principale. Exemple "fic.txt - Bloc notes"</param>
      /// <param name="processList">Retourne la liste des procesus trouvés </param>
      /// <returns>"true" si au moins 1 process trouvé</returns>
      public static bool FindProcess(string nomProcess, int ID_aIgnorer, string titreMain, out Process[] processList)
      {
         processList = new Process[0];

         nomProcess = nomProcess.Trim();
         titreMain = titreMain.Trim();

         Process[] pr = Process.GetProcessesByName(nomProcess);

         // Si recherche aussi avec "titreMain"
         if ((pr.Length > 0) && (!string.IsNullOrEmpty(titreMain)))
         {
            foreach (Process p in pr)
            {
               if ((ID_aIgnorer != 0) && (p.Id == ID_aIgnorer))
               {
                  // Si ID connu, ne pas prendre en compte le process
               }
               else
               {
                  if (p.MainWindowTitle.Trim() == titreMain)
                  {
                     Array.Resize(ref processList, processList.Length + 1);
                     processList[processList.Length - 1] = p;
                  }
               }
            }
         }
         else
         {
            processList = pr;
         }
         return (processList.Length > 0);
      }


      /// <summary>
      /// Recherche si un processus existe, et retourne le nb de processus.
      /// 
      /// Exemple d'utilisation pour vérouiller un lancement multiple d'application
      ///    private void frmImpression_Load(object sender, EventArgs e)
      ///    {      
      ///    int nb;
      ///    if (clsProcess.FindProcess(Process.GetCurrentProcess().ProcessName, Process.GetCurrentProcess().Id, this.Text, out nb))
      ///      {
      ///        Close();
      ///        return;
      ///      }
      ///       ... suite traitement Load ....
      ///    }
      /// 
      /// </summary>
      /// <param name="nomProcess">Nom du process.  Exemple "Notepad"</param>
      ///  /// <param name="ID_aIgnorer">Id  d'un process a ignorer (par exemple celui qui appel) </param>
      /// <param name="titreMain">Optionnel Titre de la fenêtre principale. Exemple "fic.txt - Bloc notes"</param>
      /// <param name="nbProcess">Retourne le nombre de processus qui répondent aux critères nom et titre"</param>
      /// <returns>"true"si au moins 1 process trouvé</returns>
      public static bool FindProcess(string nomProcess, int ID_aIgnorer, string titreMain, out int nbProcess)
      {
         Process[] processList;
         bool Trouve = FindProcess(nomProcess, ID_aIgnorer, titreMain, out processList);
         nbProcess = processList.Length;
         return Trouve;
      }

      /// <summary>
      ///  "Kill" le ou les process spécifiés.
      /// </summary>
      /// <param name="nomProcess">Nom du process.  Exemple "Notepad"</param>
      /// <param name="ID_aIgnorer">ID de process à ignorer. Valeur 0 si non utilisé"</param>
      /// <param name="titreMain">Optionnel Titre de la fenêtre principale. Exemple "fic.txt - Bloc notes"</param>   
      /// <param name="timeOutExitProcess_ms">Time-out attente exit process en ms. Exemple : 1000"</param>       
      /// <returns>"true"si tous les process ont été supprimés</returns>
      public static bool KillProcess(string nomProcess, int ID_aIgnorer, string titreMain, int timeOutExitProcess_ms)
      {
         bool killOK = true;
         Process[] processList;

         // Recherche des process
         bool Trouve = FindProcess(nomProcess, ID_aIgnorer, titreMain, out processList);
         if (Trouve)
         {
            foreach (Process p in processList)
            {
               p.Kill();
               p.WaitForExit(1000);
            }
            // OK si plus aucun process
            killOK = !FindProcess(nomProcess, ID_aIgnorer, titreMain, out processList);
         }
         return killOK;
      }
   }
}

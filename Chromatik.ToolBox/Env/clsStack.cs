using System;
using System.Diagnostics;
using System.Reflection;
using Chromatik.Data;

namespace Chromatik.Env
{
   /// <summary>
   /// Recuparation d'informations de la pile pour débuggage
   /// </summary>
   public static class clsStack
   {
      /// <summary>
      /// Analyse de la pile des appels de méthodes pour aide au diagnostique des erreurs
      /// </summary>
      /// <param name="e">Exception à analyser (ou null)</param>
      /// <param name="NiveauDeb">Indice du premier élément a prendre en compta dans la pile des appels application</param>
      /// <param name="NiveauMax">Nombre d'lément pris en compte dans la pile des appels application</param>
      /// <param name="infoLight">Information sur la methode qui a appelé la méthode qui a généré une erreur(</param>
      /// <param name="methodeEnCours">Information sur la methode en cours (celle qui génère l'erreur)</param>
      /// <returns>Information de la pile</returns>
      public static string LireStack(Exception e, int NiveauDeb, int NiveauMax , out string infoLight, out string methodeEnCours )
      {
         string infoStack = "";
         infoLight = "";
         methodeEnCours = "";
         try
         {
            // *** Dans le cas d'une exception, récupération info origine de l'exception
            StackTrace stException;
            if (e != null)
            {
               stException = new StackTrace(e, true); // Pile de l'exception
               int i = stException.FrameCount - 1;
               StackFrame sfException = stException.GetFrame(i);
               MethodBase mException = sfException.GetMethod();

               string strLigne = "";
               int l = sfException.GetFileLineNumber();
               if (l > 0) // Si fichier BPD dispo (debugage)
                  strLigne = "   " + l.ToString();

               string version = mException.ReflectedType.Assembly.GetName().Version.ToString();
               infoStack ="Exception : "+ mException.ReflectedType.FullName + " (" + version + ")" + " / " + mException.Name + strLigne +
                           clsTxt.NL();

            }


            // **** Traitement pile des appel de l'appplication
            StackTrace stApplication = new StackTrace(true); // Pile des appels
           
            int iMax = stApplication.FrameCount;
            if (iMax > NiveauDeb + NiveauMax)
               iMax = NiveauDeb + NiveauMax;
            
            // On ne prend en compte que les éléments dans la pile antérieurs a l'appel  "LireStack" et autres intermédiaires
            for (int i = NiveauDeb; i < iMax; i++)
            {
               StackFrame sfApplication = stApplication.GetFrame(i);
               MethodBase mApplication = sfApplication.GetMethod();

               string strLigne = "";
               int l = sfApplication.GetFileLineNumber();
               if (l > 0) // Si fichier BPD dispo (debugage)
                  strLigne = "   " + l.ToString();

               string version = mApplication.ReflectedType.Assembly.GetName().Version.ToString();
               string ligne = mApplication.ReflectedType.FullName + " (" + version + ")" + " / " + mApplication.Name + strLigne +
                              clsTxt.NL();
               if (i == NiveauDeb)
                  methodeEnCours = mApplication.ReflectedType.FullName + "." + mApplication.Name;

               if (i == NiveauDeb+1)
                  infoLight = ligne;

               infoStack += ligne;
            }
         }
         catch
         {
         }
         return infoStack;
      }
   }
}

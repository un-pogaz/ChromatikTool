using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Chromatik.Data;

namespace Chromatik.Fichier
{
    /// <summary>
    /// Couleur pour affichage des traces dans un "RichTextBox"
    /// </summary>
    public enum CouleurTrace
    {
       /// <summary> </summary>
        Black, // --> Black = 0
        /// <summary> </summary>
        Green,
        /// <summary> </summary>
        Red,
        /// <summary> </summary>
        Blue
    };




    /// <summary>
    ///<para> Gestion de fichier trace / log</para>
    ///<para> Notes pour utilisation dans application</para>
    ///<para>  -  Mettre a jour les propriétés</para>
    ///<para>          dossierLog</para>
    ///<para>         fichierLog </para>
    ///<para>          logInformations  ("true" par défaut)</para>
    ///<para>          logNavigations   ("true" par défaut)</para>
    ///<para>          logErreurs       ("true" par défaut)</para>
    ///<para>          logAdmins        ("true" par défaut)</para>
    ///<para>          logParametrages  ("true" par défaut)</para>
    /// </summary>
    public static class clsFichierTrace
    {
       /// <summary>
        /// Préfixe du nom du fichier trace
        /// Exemple : "TRACE_"  donnera un fichier de la forme "TRACE_2016_01_01.log"
        /// </summary>
        public static string prefixeFichierLog;


        private static string _dossierLog = "";

        /// <summary>
        /// Dossier d'enregistrement des fichiers trace
        /// </summary>
        public static string dossierLog
        {
            get { return _dossierLog; }
            set
            {
                _dossierLog = value;
                // Traitement "\" en fin de chaine
                if ((!string.IsNullOrEmpty(_dossierLog)) && (_dossierLog.Length > 1))
                {
                    if (_dossierLog[_dossierLog.Length - 1] != '\\')
                        _dossierLog += "\\";

                }
            }
        }

        /// <summary>
        /// Enregistrement des trace de type "Information"
        /// </summary>
        public static bool logInformations = true;

        /// <summary>
        /// Enregistrement des trace de type "Navigation"
        /// </summary>
        public static bool logNavigations = true;

        /// <summary>
        /// Enregistrement des trace de type "Erreur"
        /// </summary>
        public static bool logErreurs = true;

        /// <summary>
        /// Enregistrement des trace de type "Administration"
        /// </summary>
        public static bool logAdmins = true;

        /// <summary>
        /// Enregistrement des trace de type "Paramétrage"
        /// </summary>
        public static bool logParametrages = true;



       /// <summary>
       /// Enregistrement d'une trace dans le fichier à la date du jour (ex:2005_07_06) en .log
       /// </summary>
       /// <param name="Rubrique">Identification de la rubrique</param>
       /// <param name="Evenement">Identification de l'événement</param>
       /// <param name="Message">Message complémentaire</param>
       private static void EnregistreTrace(string Rubrique, string Evenement, string Message)
       {
          //  dossierLog = "D:\\";
          //  prefixeFichierLog = "Essai";

          if (!string.IsNullOrEmpty(_dossierLog))
          {

             // Utilisation d'un MUTEX pour vérouiller l'écriture dans le fichier par plusieurs applications
             // Exemple dans Ophely plusieurs phases peuvent ecrire des traces dans le même fichier)

             // Init Mutex
             Mutex _m; 
             try
             {
                // Try to open existing mutex.
                _m = Mutex.OpenExisting(prefixeFichierLog);
                   // FichierLog contient l'entête du nom de fichier (ex : "TRACE_"  ,  "Ophely_V6_")
             }
             catch
             {
                // If exception occurred, there is no such mutex.
                _m = new Mutex(false, prefixeFichierLog);
             }


             // Verouillage
             _m.WaitOne();

             try
             {

                if (!Directory.Exists(_dossierLog))
                   Directory.CreateDirectory(_dossierLog);

                string leFichierLog = _dossierLog + prefixeFichierLog
                                      + "-" + DateTime.Now.Year.ToString()
                                      + "_" + DateTime.Now.Month.ToString("00")
                                      + "_" + DateTime.Now.Day.ToString("00") + ".log";

                if (!File.Exists(leFichierLog))
                {
                   FileStream f = File.Create(leFichierLog);
                   f.Close();
                }

                string txt = clsTxt.NL() + DateTime.Now.ToShortDateString() + '\t' + DateTime.Now.ToLongTimeString();
                if (!string.IsNullOrEmpty(Rubrique)) // Uniquement si rubrique renseignée
                   txt += '\t' + Rubrique;
                txt += '\t' + Evenement + '\t' + Message;

                File.AppendAllText(leFichierLog, txt, Encoding.Unicode);

             }
             finally
             {
                _m.ReleaseMutex();
             }
          }
       }


       /// <summary>
        /// Enregitre un évenement de rubrique "Information"
        /// </summary>
        /// <param name="Evenement"> Identification de l'événement </param>
        /// <param name="Message"> Message complémentaire</param>
        public static void TraceInformation(string Evenement, string Message)
        {
            if (logInformations)
                EnregistreTrace("Informations", Evenement, Message);
        }

        /// <summary>
        /// Enregitre un évenement sans rubrique
        /// </summary>
        /// <param name="Evenement"> Identification de l'événement </param>
        /// <param name="Message"> Message complémentaire</param>
        public static void TraceInfoSansRubrique(string Evenement, string Message)
        {
            if (logInformations) // nota : vrai par défaut
                EnregistreTrace("", Evenement, Message);
        }


        /// <summary>
        /// Enregitre un évenement de rubrique "Navigation"
        /// </summary>
        /// <param name="Evenement"> Identification de l'événement </param>
        /// <param name="Message"> Message complémentaire</param>
        public static void TraceNavigation(string Evenement, string Message)
        {
            if (logNavigations)
                EnregistreTrace("Navigation", Evenement, Message);
        }

        /// <summary>
        /// TraceErreur (surcharge 1)
        /// Enregitre un évenement de rubrique "Erreur", avec information d'exception.
        /// </summary>
        /// <param name="Evenement"> Identification de l'événement </param>
        /// <param name="Message"> Message complémentaire</param>
        /// <param name="e"> Exception </param>
        public static void TraceErreur(string Evenement, string Message, Exception e)
        {
           TraceErreur(Evenement,Message,"",e);
        }

        /// <summary>
        /// TraceErreur (surcharge 2)
        /// Enregitre un évenement de rubrique "Erreur", sans information d"exception.
        /// </summary>
        /// <param name="Evenement"> Identification de l'événement </param>
        /// <param name="Message"> Message complémentaire</param>
        public static void TraceErreur(string Evenement, string Message)
        {
            if (logErreurs)
                EnregistreTrace("Erreur", Evenement, Message);
        }


       /// <summary>
       /// TraceErreur
       /// Enregitre un évenement de rubrique "Erreur", avec information d'exception.
       /// </summary>
       /// <param name="Evenement"> Identification de l'événement </param>
       /// <param name="Message"> Message d'erreur</param>
       /// <param name="MessagePlus">Informations complémentaires</param>
       /// <param name="e"> Exception </param>
       public static void TraceErreur( string Evenement, string Message,string MessagePlus, Exception e)
       {
          if (logErreurs)
          {
             const string separateur = "------------------------------------------------------------------------";
             string MessageSuite = "";

             // Construction du message de la trace
             if (!string.IsNullOrEmpty(MessagePlus))
                MessageSuite +=  MessagePlus+clsTxt.NL();
             if (e!= null)
                MessageSuite += "[Source = ] " + e.Source
                             +clsTxt.NL() + "     [Message = ] " + e.Message
                             + clsTxt.NL() +"     [StackTrace = ] " + e.StackTrace+clsTxt.NL();
             if (!string.IsNullOrEmpty(MessageSuite))
             {
                MessageSuite = clsTxt.NL() +separateur + clsTxt.NL() + MessageSuite + separateur + clsTxt.NL();
             }

             Message = Message + MessageSuite;
             // sup des doubles sauts de ligne meilleur lisibilité

             while (Message.IndexOf(clsTxt.NL(2))>=0)
             {
                Message = Message.Replace(clsTxt.NL(2), clsTxt.NL());               
             }
             EnregistreTrace("Erreur", Evenement, Message);
          }
       }

       /// <summary>
        /// Enregitre un évenement de rubrique "Admin"
        /// </summary>
        /// <param name="Evenement"> Identification de l'événement </param>
        /// <param name="Message"> Message complémentaire</param>
        public static void TraceAdmin(string Evenement, string Message)
        {
            if (logAdmins)
                EnregistreTrace("Admin", Evenement, Message);
        }

        /// <summary>
        /// Enregitre un évenement de rubrique "Parametrage"
        /// </summary>
        /// <param name="Evenement"> Identification de l'événement </param>
        /// <param name="Message"> Message complémentaire</param>
        public static void TraceParametrage(string Evenement, string Message)
        {
            if (logParametrages)
                EnregistreTrace("Parametrage", Evenement, Message);
        }

      
        /// <summary>
        /// Enregistrement d'information dans un fichier spécifique
        /// </summary>
        /// <param name="leFichier">Fichier (avec le chemin) d'enregistrement</param>
        /// <param name="action">Information 1</param>
        /// <param name="detail">Information 2</param>
        public static void TraceJournal(string leFichier, string action, string detail)
        {
            if (!(File.Exists(leFichier)))
            {
                File.Create(leFichier).Close();
            }
            StreamWriter sw = new StreamWriter(leFichier, true);
            sw.Write(DateTime.Now.ToString() + "\t" + action + "\t" + detail + clsTxt.NL());
            sw.Close();
        }
    }


    /// <summary>
    /// Outils pour RichTextBox
    /// </summary>
    public static class clsRichTextBox
    {
        /// <summary>
        /// Copie le contenu d'un RichTextBox dans le presse papier
        /// </summary>
        /// <param name="rtb">Le RichTextBox</param>
        public static void CopyRTBtoClipBoard(RichTextBox rtb)
        {
            DataObject m_data = new DataObject();
            m_data.SetData(DataFormats.Rtf, true, rtb.Rtf);
            // NOTE: '\line' converts to a linefeed only.  Replace linefeed only with \r\n
            // However '\par' converts to \r\n so you may want to use a regex to make sure you
            // are only replacing \n that are not preceeded by \r
            m_data.SetData(DataFormats.Text, true, rtb.Text.Replace("\n", "\r\n"));
            Clipboard.SetDataObject(m_data, true);
        }



        /// <summary>
        /// Ajout d'une ligne dans un RichTextBox utilisé pour afficher de la trace
        /// </summary>
        /// <param name="rtbX">Le RichTextBox</param>
        /// <param name="ligne">Ligne de texte à ajouter</param>
        /// <param name="c">Couleur</param> 
        public static void AjouterLigneRichTextBox(ref RichTextBox rtbX, string ligne, CouleurTrace c)
        {
            AjouterLigneRichTextBox(ref rtbX, ligne, c, null);
        }

        /// <summary>
        /// Ajout d'une ligne dans un RichTextBox utilisé pour afficher de la trace
        /// </summary>
        /// <param name="rtbX">Le RichTextBox</param>
        /// <param name="ligne">Ligne de texte à ajouter</param>
        /// <param name="c">Couleur</param>
        /// <param name="legende">Legende des 4 couleurs (Noir/Vert/Rouge/Bleu) ou null
        /// Nota : la légende n'est prise en compte qu'a la première écriture dans le RichTextBox (</param>
        public static void AjouterLigneRichTextBox(ref RichTextBox rtbX, string ligne, CouleurTrace c, string[] legende)
        {
           AjouterLigneRichTextBox(ref rtbX, ligne, c, legende, true,0,0);
        }


       /// <summary>
       /// Ajout d'une ligne dans un RichTextBox utilisé pour afficher de la trace
       /// </summary>
       /// <param name="rtbX">Le RichTextBox</param>
       /// <param name="ligne">Ligne de texte à ajouter</param>
       /// <param name="c">Couleur</param>
       /// <param name="legende">Legende des 4 couleurs (Noir/Vert/Rouge/Bleu) ou null
       /// Nota : la légende n'est prise en compte qu'a la première écriture dans le RichTextBox (</param>
       /// <param name="avecDateHeure"></param>
       public static void AjouterLigneRichTextBox(ref RichTextBox rtbX, string ligne, CouleurTrace c, string[] legende,
                                                  bool avecDateHeure)
       {
          AjouterLigneRichTextBox(ref rtbX, ligne, c, legende, avecDateHeure, 0, 0);
       }



       /// <summary>
       /// Ajout d'une ligne dans un RichTextBox utilisé pour afficher de la trace
       /// </summary>
       /// <param name="rtbX">Le RichTextBox</param>
       /// <param name="ligne">Ligne de texte à ajouter</param>
       /// <param name="c">Couleur</param>
       /// <param name="legende">Legende des 4 couleurs (Noir/Vert/Rouge/Bleu) ou null
       /// Nota : la légende n'est prise en compte qu'a la première écriture dans le RichTextBox (</param>
       /// <param name="avecDateHeure"></param>
       /// <param name="Seuil1">Seuil nombre de lignes pour déclenchement de purge</param>
       /// <param name="Seuil2">Seuil nombre de lignes à conserver lors de la purge</param>
       public static void AjouterLigneRichTextBox(ref RichTextBox rtbX, string ligne, CouleurTrace c, string[] legende, bool avecDateHeure, int Seuil1, int Seuil2)
       {
          if (avecDateHeure)  //sm14/03/2017   Dans cas comOphely par exemple , la date-heure est enregistrée dans le message
          //                               (important car in y a un décallage entre l'enregistrement d'une trace est l'appel de l'affichage)
          {
             // Si la ligne est vide  (pour séparation groupe de messages)  : pas de date
             if (!String.IsNullOrEmpty(ligne))
                ligne = DateTime.Now.ToString() + " :" + ligne;
          }
          if (ligne != null)
             ligne = ligne.Replace("\\", "\\\\"); // Pour prise en compte "\" dans le message (ex chemin de fichier)

          string[] enTeteCodeCouleurLigneRTF = { "\\cf2 ", "\\cf3 ", "\\cf4 ", "\\cf5 " };

          const string finLigneRTF = "\\par";
          const string identEntete = " ---- ";

          // Séparation des lignes existantes
          string[] tabS = rtbX.Rtf.Split('\r');

          if (tabS.Length <= 5)
          {
             // Legende par défaut
             if ((legende == null) || (legende.Length != 4))
                legende = new[] { "Info", "Traitement OK", "Erreur", "Info" };

             tabS = new string[5];
             tabS[0] = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1036{\\fonttbl{\\f0\\fnil\\fcharset0 Arial}}";
             tabS[1] =
                 "{\\colortbl ;\\red1\\green0\\blue0;\\red0\\green0\\blue0;\\red0\\green120\\blue0;\\red255\\green0\\blue0;\\red0\\green0\\blue255;}";
             // "normal"
             tabS[2] =
                 "\\viewkind4\\uc1\\pard\\cf1\\f0\\fs20\\ "+identEntete+"   \\cf2[" + legende[0]
                 + "]   \\cf3[" + legende[1]
                 + "]   \\cf4[" + legende[2]
                 + "]   \\cf5[" + legende[3] + "]  \\cf1   " + identEntete + "\\par";

             tabS[3] = "}"; // Fin fichier
             tabS[4] = "";
          }

          // AJout ligne       sm15/11/2018 : correctifs
          // Nopta : la chaine "\\cf1  "  parmet d'ajouter un espace avec la couleur cf1 en début de ligne, pour éviter que le RichTextBox ne réaffecte les couleurs lors d'une suppression de lignes
          string LigneRTF = "\\cf1  " + enTeteCodeCouleurLigneRTF[(int)c] + @ligne + finLigneRTF;
          tabS[tabS.Length - 2] = '\n' + LigneRTF;
          tabS[tabS.Length - 1] = "}"; // Fin fichier                

          
          if ((Seuil1 > Seuil2) && (Seuil1 > 5) && (tabS.Length - 5 > Seuil1))
          {
             // Recherche fin entête
             int offset = 0;
             for (int i = 0; i < tabS.Length; i++)
             {
                if (tabS[i].Contains(identEntete))
                {
                   offset = i;
                   break; 
                }
             }

             // Sup lignes
             List<string> tabSlist = new List<string>(tabS);
             while (tabSlist.Count > Seuil2 + offset+2)
             {
                tabSlist.RemoveAt(offset + 1);
             }
             tabS = tabSlist.ToArray();
          }


          // Reconstruction RTF
          string s = "";
          for (int i = 0; i < tabS.Length; i++)
          {
             s = s + tabS[i] + '\r';
          }
          rtbX.Rtf = s;

          rtbX.SelectionStart = rtbX.Text.Length;
          rtbX.ScrollToCaret();
          rtbX.Refresh();
       }







    }

}

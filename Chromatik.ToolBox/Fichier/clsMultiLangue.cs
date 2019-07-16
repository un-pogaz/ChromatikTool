using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Chromatik.Data;
using Chromatik.Env;

namespace Chromatik.Fichier
{
   

   /// <summary>
   /// Getsion multilangue
   ///  Notes pour utilisation dans application
   ///    - Mettre a jour la propriétés    LangueEnCours 
   ///    - Mettre a jour la liste des fichiers de messages à lire  dans  ListeFichiersMessages   
   ///    -->  Les messages seront lus dans les fichiers definis dans ListeFichiersMessages  
   /// </summary>
   public static class clsMultilangue
   {
      /// <summary>
      /// Code langue en cours (par défaut "FR-fr" )
      /// </summary>
      public static string LangueEnCours = "FR-fr";

      /// <summary>
      /// Liste des fichiers de messages
      /// Exemples "C:\ProgramData\..\Ophely V6\_Messages.ini"  "C:\....\application\_messages2.ini"
      /// </summary>
      public static string[] ListeFichiersMessages;
    
      private static List<string> ident;
      private static List<string> fr_FR;
      private static List<string> en_GB;
      private static List<string> pl_PL;
      private static List<string> es_ES;


      private static void iniListetMsg()
      {
         ident = new List<string>();
         fr_FR = new List<string>();
         en_GB = new List<string>();
         pl_PL = new List<string>();
         es_ES = new List<string>();

         // Fichiers messages par défaut :
         //  si la  liste des fichiers message n'a pas été initialisée, on prend le fichier par défaut dans le dossier de l'application.
         string ficMsgParDefaut = clsDossier.Appli + "\\_Messages_SEEBtoolbox.ini";
         //if ((ListeFichiersMessages == null) && File.Exists(ficMsgParDefaut))
         //sm 03/12/2017
         if (       ((ListeFichiersMessages == null) || ((ListeFichiersMessages != null)
                 && (ListeFichiersMessages.Length == 0))) 
              && File.Exists(ficMsgParDefaut))
         {
            ListeFichiersMessages = new string[1];
            ListeFichiersMessages[0] =ficMsgParDefaut;
         }
    
          if (ListeFichiersMessages != null)
          {
             for (int iFicMessages = 0; iFicMessages < ListeFichiersMessages.Length; iFicMessages++)
             {
                string fic = ListeFichiersMessages[iFicMessages];

                if (File.Exists(fic))
                {
                   try
                   {
                      string[] l = File.ReadAllLines(fic);
                      int iTableau = -1;
                      for (int i = 0; i < l.Length; i++)
                      {
                         string ligne = l[i].Trim();
                         if (ligne.Length > 0)
                         {
                            if (ligne[0] == '[')
                            {
                               ident.Add(ligne.TrimStart('[').TrimEnd(']'));
                               fr_FR.Add("");
                               en_GB.Add("");
                               pl_PL.Add("");
                               es_ES.Add("");
                               iTableau = ident.Count - 1;
                            }
                            else
                            {
                               int posEgal = ligne.IndexOf('=');
                               if ((posEgal > 0) && (iTableau >= 0))
                               {
                                  string cle = ligne.Substring(0, posEgal).ToUpper().Trim();
                                  string val = ligne.Substring(posEgal + 1);
                                  if (cle == "FR-FR")
                                     fr_FR[iTableau] = val.TrimStart();
                                  if (cle == "EN-GB")
                                     en_GB[iTableau] = val.TrimStart();
                                  if (cle == "PL-PL")
                                     pl_PL[iTableau] = val.TrimStart();
                                  if (cle == "ES-ES")
                                     es_ES[iTableau] = val.TrimStart();
                               }
                            }
                         }
                      }

                   }
                   catch (Exception e)
                   {
                      //ATTENTION : ne pas utiliser clsBox.MsgBoxErr, sinon appel en boucle
                      MessageBox.Show(@"Messages file read error." +
                                     clsTxt.NL()  + fic +
                                     clsTxt.NL() + e.Message
                                      , @"Messages.ini", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   }
                }
                else
                {
                   //ATTENTION : ne pas utiliser clsBox.MsgBoxErr, sinon appel en boucle
                   MessageBox.Show(@"Unknown messages file : " +clsTxt.NL() + fic
                                   , @"Messages.ini", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
             }
          }
          else
          {
             //ATTENTION : ne pas utiliser clsBox.MsgBoxErr, sinon appel en boucle
             MessageBox.Show(@"No messages file found." 
                              + clsTxt.NL() + ficMsgParDefaut  //sm03/12/2016
                             , @"Messages.ini", MessageBoxButtons.OK, MessageBoxIcon.Error);
          }
      }



      /// <summary>
      /// Lecture d'un message dans la langue en cours (définie dans la propriété "LangueEnCours")
      /// </summary>
      /// <param name="idMsg">Identification du message</param>
      /// <returns>Le message dans la langue choisie</returns>
      public static string Msg(string idMsg)
      {
         string m = idMsg;

         if ((ident == null) || (ident.Count == 0))
            iniListetMsg();

         if (ident != null)
         {
            try
            {
               int i = ident.IndexOf(idMsg);
               if (i >= 0)
               {
                  string langue = LangueEnCours.ToUpper();
                  if (langue == "FR-FR")
                     m = fr_FR[i];
                  if (langue == "EN-GB")
                     m = en_GB[i];
                  if (langue == "PL-PL")
                     m = pl_PL[i];
                  if (langue == "ES-ES")
                     m = es_ES[i];
               }
               else
               {
                  //ajout dans fichier : par convention ajout dans le premier fichier de la liste des fichiers de messages
                  if (ListeFichiersMessages != null && ListeFichiersMessages.Length >= 1)
                  {
                     
                     string fic = ListeFichiersMessages[0];
                     if (File.Exists(fic))
                     {
                        string[] l = File.ReadAllLines(fic);
                        int ind = l.Length;
                        Array.Resize(ref l, l.Length + 6);
                        l[ind] = "";
                        l[ind + 1] = "[" + idMsg + "]";
                        l[ind + 2] = "fr-FR = " + idMsg;
                        l[ind + 3] = "en-GB = " + idMsg;
                        l[ind + 4] = "pl-PL = " + idMsg;
                        l[ind + 5] = "es-ES = " + idMsg;
                        File.WriteAllLines(fic, l);
                        iniListetMsg();
                     }

                  }
               }
               // Si caractère '_' en fin de message, remplacement par un espace.
               if (!String.IsNullOrEmpty(m))
               {

                  if (m.Length > 0)
                     if (m[m.Length - 1] == '_')
                     {
                        m = m.Remove(m.Length - 1, 1);
                        m += ' ';
                     }

               }
            }
            catch (Exception e)
            {
               //ATTENTION : ne pas utiliser clsBox.MsgBoxErr, sinon appel en boucle
               MessageBox.Show(@"Messages read error : " + idMsg + clsTxt.NL() + e.Message
                               , @"Messages.ini", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
         }
         return m;
      }
   }




}

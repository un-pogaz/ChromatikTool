using System;
using Microsoft.Win32;
using Chromatik.Data;

namespace Chromatik.Fichier
{

   /// <summary>
   /// Acces base de registre
   /// </summary>
   public static class clsRegistre
   { 
      /// <summary>
      /// Lecture d'une valeur ("object") dans la base de registre, avec valeur par défaut
      /// </summary>
      /// <param name="RegistryProjet"> Nœud de niveau clé dans le Registre Windows.</param>
      /// <param name="IdValeur">Nom de la valeur</param>
      /// <param name="Err">Retour erreur</param>
      /// <param name="ValeurParDefaut">Valeur par défaut si erreur de lecture</param>
      /// <returns>Valeur lue</returns>
      public static object LectReg(RegistryKey RegistryProjet, string IdValeur,out clsErr Err,
                                   object ValeurParDefaut)
      {
         object obj = null;
         Err = new clsErr();
         try
         {
            obj = RegistryProjet.GetValue(IdValeur);
         }
         catch
         {
            if (ValeurParDefaut != null)
               obj = ValeurParDefaut;
            else
               Err.msgErr= IdValeur;
         }

         if (obj == null)
         {
            if (ValeurParDefaut != null)
               obj = ValeurParDefaut;
            else
               Err.msgErr = IdValeur;
         }
         return obj;
      }



      /// <summary>
      /// Lecture d'une valeur ("object") dans la base de registre, sans valeur par défaut
      /// </summary>
      /// <param name="RegistryProjet"> Nœud de niveau clé dans le Registre Windows.</param>
      /// <param name="IdValeur">Nom de la valeur</param>
      /// <param name="Err">Retour erreur</param>   
      /// <returns>Valeur lue</returns>

      public static object LectReg(RegistryKey RegistryProjet, string IdValeur, out clsErr Err)
      {
         return LectReg(RegistryProjet, IdValeur, out Err, null);
      }


 
      /// <summary>
      /// Lecture d'une valeur ("object") dans la base de registre LocalMachine\\SOFTWARE\\CPIT\\
      /// </summary>
      /// <param name="IdentificationLogiciel">Nom du logiciel CPIT</param>
      /// <param name="IdValeur">Identification valeur à lire</param>
      /// <returns>Valeur, ou null si erreur</returns>
      public static object LectReg(string IdentificationLogiciel, string IdValeur)
      {
         clsErr Err;
         object obj = null;

         try
         {
            RegistryKey RegProj = Registry.LocalMachine;
            RegProj = RegProj.OpenSubKey("SOFTWARE\\CPIT\\" + IdentificationLogiciel);
            if (RegProj != null)
            {
               string ProjetEnCours = (string) LectReg(RegProj, "ProjetEnCours", out Err, null);
               RegProj = RegProj.OpenSubKey(ProjetEnCours);
               obj = LectReg(RegProj, IdValeur, out Err, null);
            }
         }
         catch
         {
            obj = null;
         }
         return obj;
      }



      /// <summary>
      /// Ecriture d'une valeur ("object") dans la base de registre LocalMachine\\SOFTWARE\\CPIT\\
      /// </summary>
      /// <param name="IdentificationLogiciel">Nom du logiciel CPIT</param>
      /// <param name="IdValeur">Identification valeur à ecrire</param>
       /// <param name="Valeur">Valeur à ecrire</param>
      /// <param name="Err">Retour erreur</param>
      /// <returns>"true"si pas d'erreur</returns>
      public static bool EcritureRegistre(string IdentificationLogiciel, string IdValeur, object Valeur, out clsErr Err)
      {
         bool OK = false;
         Err = new clsErr();

         RegistryKey RegProj = Registry.LocalMachine;
         try
         {
            RegProj = RegProj.OpenSubKey("SOFTWARE\\CPIT\\" + IdentificationLogiciel);

            if (RegProj != null)
            {
               string m_projetencours = (string) LectReg(RegProj, "ProjetEnCours", out Err, null);
               RegProj = RegProj.OpenSubKey(m_projetencours, true);
               if (RegProj != null)
               {
                  RegProj.SetValue(IdValeur, Valeur);
                  clsErr ErrVerif;
                  object verif = LectReg(RegProj, IdValeur, out ErrVerif, null);
                  if (Valeur.ToString() == verif.ToString())
                     OK = true;
                  else
                     Err.msgErr = IdValeur + " = " + Valeur + clsTxt.NL()+ "Registry write error.";
               }
            }
         }
         catch (Exception e)
         {
            Err.msgErr = IdValeur + " = " + Valeur + clsTxt.NL()+ e.Message;
         }
         return OK;
      }


      /// <summary>
      /// Ecriture d'une valeur ("object") dans la base de registre LocalMachine
      /// </summary>
      /// <param name="subKeyLocalMachine"></param>
      /// <param name="IdValeur">Identification valeur à ecrire</param>
      /// <param name="Valeur">Valeur à ecrire</param>
      /// <param name="Err">Retour erreur</param>
      /// <returns>"true"si pas d'erreur</returns>
      public static bool EcritureRegistre2(string subKeyLocalMachine, string IdValeur, object Valeur, out clsErr Err)
      {
         bool OK = false;
         Err = new clsErr();

         RegistryKey RegProj = Registry.LocalMachine;
         try
         {
            RegProj = RegProj.OpenSubKey(subKeyLocalMachine, true);

            if (RegProj != null)
            {
               RegProj.SetValue(IdValeur, Valeur);
               clsErr ErrVerif;
               object verif = LectReg(RegProj, IdValeur, out ErrVerif, null);
               if (Valeur.ToString() == verif.ToString())
                  OK = true;
               else
                  Err.msgErr = IdValeur + " = " + Valeur + clsTxt.NL()
                        + "Registry write error.";
            }
         }
         catch (Exception e)
         {
            Err.msgErr = IdValeur + " = " + Valeur + clsTxt.NL()
                  + e.Message;
         }
         return OK;
      }
   }

}


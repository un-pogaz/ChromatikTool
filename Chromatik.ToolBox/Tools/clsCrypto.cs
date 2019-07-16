using System;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Chromatik.Data;

namespace Chromatik.Tools
{
   /// <summary>
   /// Gestion MD5
   /// </summary>
   public static class clsMD5File
	{
	
      /// <summary>
      /// Calcul MD5 d'un fichier et conversion en chaine de caractères hexadecimales.
      /// </summary>
      /// <param name="fichier">Nom du fichier avec le chemin</param>
      /// <param name="valeurMD5">Retourne la valeur MD5 en chaine hexa</param>
      /// <param name="Err">Retour d'erreur</param>
      /// <returns>True si OK</returns>
      public static bool CalculMD5fichier(string fichier, out string valeurMD5, out clsErr Err)
      {
         Err = new clsErr();
     
         valeurMD5 = "";
         try  // Si le fichier est vérouillé (cas d'un fichier MDB par exemple)
         {
            // MD5
            MD5 md5 = MD5.Create();
            byte[] bMD5 = md5.ComputeHash(File.ReadAllBytes(fichier));

            // Conversion MD5 en chaine de caractères hexa
            StringBuilder sb = new StringBuilder(); // Plus rapide que la concaténation de chaine (?)
            for (int i = 0; i < bMD5.Length; i++)
               sb.Append(bMD5[i].ToString("X2"));
            valeurMD5 = sb.ToString();
            return true;
         }
         catch (Exception e)
         {
            Err.msgErr = e.Message;
            Err.e = e;
            return false;
         }
      }


      /// <summary>
      ///Calcul MD5 d'une chaine de caractères et conversion en chaine de caractères hexadecimales.
      /// </summary>
      /// <param name="str">Chaine</param>
      /// <returns>MD5 en chaine hexa</returns>
		public static string GetMD5Hash(string str)
		{
			// MD5
			MD5 md5 = MD5.Create();

			byte[] buffer = Encoding.ASCII.GetBytes(str);

			byte[] bMD5 = md5.ComputeHash(buffer);

			// Conversion MD5 en chaine de caractères hexa
			StringBuilder sb = new StringBuilder(); // Plus rapide que la concaténation de chaine (?)
			for (int i = 0; i < bMD5.Length; i++)
				sb.Append(bMD5[i].ToString("X2"));
			return sb.ToString();

		}
	}
}

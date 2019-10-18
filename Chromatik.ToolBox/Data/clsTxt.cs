using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chromatik.Data
{
    
    

   /// <summary>
   /// Gestion chaines de caractères / char / bytes
   /// </summary>
   public static class clsStr
   {

      /// <summary>
      /// Formate une chaine à une longeur fixe (en complétant avec des espaces si nécessaire)
      /// </summary>
      /// <param name="s"></param>
      /// <param name="NbCar"></param>
      /// <returns></returns>
      public static string FormaterChaine(string s, int NbCar)
      {
         if (s.Length > NbCar)
            s = s.Substring(NbCar);
         while (s.Length < NbCar)
            s = s + ' ';
         return s;
      }


      /// <summary>
      /// Converti un tableau "List string"  en "string[]"
      /// </summary>
      /// <param name="Liste">La tableau "List" à convertir</param>
      /// <returns>Le tableau string[] </returns>
      public static string[] ConversionListStringEnTabString(List<string> Liste)
      {
         string[] s = new string[Liste.Count];
         for (int i = 0; i < Liste.Count; i++)
         {
            s[i] = Liste[i];
         }
         return s;
      }

      
      /// <summary>
      /// Effectue le nettoyage d'une chaine de caractères
      /// </summary>
      /// <param name="sIn">Chaine en entrée</param>
      /// <param name="Trim">Supprimer les espaces en début et fin de chaine</param>
      /// <param name="RemplaceEspacePar_">Ramplacer les espaces par un caractère "_"</param>
      /// <param name="SupEspaces">Supprimer les espaces</param>
      /// <param name="SupCarSpeciaux">Supprimer les caractères spéciaux : \b  \t  \r  \v  \f  \0  \n</param>
      /// <returns>Chaine modifiée</returns>
      public static string NettoyerChaine(string sIn, bool Trim, bool RemplaceEspacePar_, bool SupEspaces,
                                          bool SupCarSpeciaux)
      {
         string sOut = sIn;

         if (Trim)
            sOut = sOut.Trim();

         if (RemplaceEspacePar_)
            sOut = sOut.Replace(' ', '_');

         if (SupEspaces)
            sOut = sOut.Replace(" ", "");

         /*
          http://lgmorand.developpez.com/dotnet/regex/
            \s Caractère d'espacement (espace, tabulation, saut de page, etc) 
          https://msdn.microsoft.com/fr-fr/library/4edbef7e(v=vs.110).aspx
            \b Dans une classe de caractères [groupe_caractères], correspond à un retour arrière, \u0008. (Voir Classes de caractères.) En dehors d'une classe de caractères, \b est une ancre qui correspond à une limite de mot. (Voir Ancres.) 
            \t Correspond à une tabulation, \u0009. 
            \r Correspond à un retour chariot, \u000D. Notez que \r n'est pas équivalent au caractère de nouvelle ligne, \n. 
            \v Correspond à une tabulation verticale, \u000B. 
            \f Correspond à un saut de page, \u000C. 
            \n Correspond à une nouvelle ligne, \u000A. 
            \0 for a null character.
          */

         if (SupCarSpeciaux)
            sOut = Regex.Replace(sOut, "\b|\t|\r|\v|\f|\0|\n", "");

         return sOut;
      }


   }
}

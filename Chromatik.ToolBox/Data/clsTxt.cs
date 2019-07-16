using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Chromatik.Data
{

   /// <summary>
   /// Chaines de caractères
   /// </summary>
   public static class clsTxt
   {
      // Caractères spéciaux
      /// <summary> </summary>
      public static char STX = (char)(0x02);
      /// <summary> </summary>
      public static char ETX = (char)(0x03);
      /// <summary> </summary>
      public static char EOT = (char)(0x04);
      /// <summary> </summary>
      public static char ENQ = (char)(0x05);
      /// <summary> </summary>
      public static char ACK = (char)(0x06);
      /// <summary> </summary>
      public static char BEL = (char)(0x07);
      /// <summary> </summary>
      public static char NAK = (char)(0x15);
      /// <summary> </summary>
      public static char LF = (char)(0x0A);
      /// <summary> </summary>
      public static char CR = (char)(0x0D);
      /// <summary> </summary>
      public static char Xon = (char)(0x11);
      /// <summary> </summary>
      public static char Xoff = (char)(0x13);


      /// <summary>
      ///  Un saut de ligne
      /// </summary>
      /// <returns>Chaine composée d'un saut de ligne</returns>
      public static string NL()
      {
         return NL(1);
      }

      /// <summary>
      /// n sauts de lignes 
      /// </summary>
      /// <param name="nb_SautDeLignes">Nombre de sauts de ligne</param>
      /// <returns>Chaine composée de n saut de lignes</returns>

      public static string NL(int n)
      {
         string s = string.Empty;
         if (n <= 0)
            return Environment.NewLine;
         else
            for (int i = 0; i < n; i++)
               s += Environment.NewLine;

         return s;
      }

   }

    

   /// <summary>
   /// Gestion chaines de caractères / char / bytes
   /// </summary>
   public static class clsStr
   {
      /// <summary>
      /// Conversion d'une chaine de caractères en chaine de valeurs hexa
      /// Exemple d'utilisation : affichage trame de communication
      /// </summary>
      /// <param name="avecIndice">Ajout indice de caractère devant chaque valeur</param>
      /// <param name="chaine">La chaine à convertir</param>
      /// <returns>Chaine composée des codes hexa</returns>
      public static string ConversionChaineEnHexa(bool avecIndice, string chaine)
      {
         string ValHexa;
         ushort valInt;
         string strHexa = "";

         for (int i = 0; i < chaine.Length; i++)
         {
            valInt = chaine[i];
            ValHexa = String.Format("{0:x2}", valInt).ToUpper();
            if (avecIndice)
               strHexa = strHexa + i.ToString() + ':' + ValHexa + ' ';
            else
               strHexa = strHexa + ValHexa + ' ';
         }
         return strHexa;
      }


      /// <summary>
      /// Conversion d'un tableau de bytes en chaine de valeurs hexa
      /// Exemple d'utilisation : affichage trame de communication
      /// </summary>
      /// <param name="avecIndice">Ajout indice de caractère devant chaque valeur</param>
      /// <param name="tabBytes">Le tableau de bytes à convertir</param>
      /// <param name="nb">Nb de caractères à traiter, ou 0 pour traiter tout le tableau </param>
      /// <returns>Chaine composée des codes hexa</returns>
      public static string ConversionTabBytesEnChaineEnHexa(bool avecIndice, byte[] tabBytes, int nb)
      {
         string ValHexa;
         ushort valInt;
         string strHexa = "";

         int iFin;
         if (nb == 0)
            iFin = tabBytes.Length;
         else
            iFin = Math.Min(tabBytes.Length, nb);

         for (int i = 0; i < iFin; i++)
         {
            valInt = tabBytes[i];
            ValHexa = String.Format("{0:x2}", valInt).ToUpper();
            if (avecIndice)
               strHexa = strHexa + i.ToString() + ':' + ValHexa + ' ';
            else
               strHexa = strHexa + ValHexa + ' ';
         }
         return strHexa;
      }



      /// <summary>
      /// Conversion chaine en tableau de bytes
      /// </summary>
      /// <param name="chaine">La chaine à convertir</param>
      /// <returns>Tableau de bytes</returns>
      public static byte[] StrToBytes(string chaine)
      {
         byte[] tab = new byte[chaine.Length];
         for (int i = 0; i < chaine.Length; i++)
            tab[i] = (byte) chaine[i];

         return tab;
      }

      /// <summary>
      /// Copie une chaine dans un tableau de bytes à une position donnée
      /// </summary>
      /// <param name="tabByte"></param>
      /// <param name="rang"></param>
      /// <param name="Valeur"></param>
      /// <returns></returns>
      public static bool StrInBytesArray(ref byte[] tabByte, int rang, string Valeur)
      {
         bool Ret = false;
         try
         {

            if (rang + Valeur.Length <= tabByte.Length)
            {
               for (int i = 0; i < Valeur.Length; i++)
               {
                  tabByte[rang + i] = Convert.ToByte(Valeur[i]);
               }

            }
            Ret = true;
         }
         catch
         {

         }

         return Ret;
      }



      /// <summary>
      ///  Conversion d'un tableau de bytes en chaine
      /// </summary>
      /// <param name="tabBytes">Tableu de bytes à convertir</param>
      /// <returns>Chaine composé des n caractères du tableau de bytes</returns>
      public static string BytesToStr(byte[] tabBytes)
      {
         string str = "";
         for (int i = 0; i < tabBytes.Length; i++)
            str = str + (char) (tabBytes[i]);

         return str;
      }


      /// <summary>
      /// Initialisation d'un tableau de bytes
      /// </summary>
      /// <param name="TabBytes">Le tableau à initialiser</param>
      /// <param name="val">La valeur d'initialisation</param>
      public static void InitValBytes(ref byte[] TabBytes, byte val)
      {
         for (int i = 0; i < TabBytes.Length; i++)
            TabBytes[0] = val;
      }



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

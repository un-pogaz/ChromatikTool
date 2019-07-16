using System;

namespace Chromatik.Data
{
   /// <summary>
   /// Conversions numériques diverses :
   ///   Bytes / Mots / Bool
   ///   Chaines / Bytes
   ///   Hexa
   /// 
   /// </summary>
   public static class clsNum
   {

      #region Conversions : Bytes / Mots / Bool
    
      /// <summary>
      /// Initialisation d'un tableau de bytes
      /// </summary>
      /// <param name="TabBytes">Tableau</param>
      /// <param name="Val">Valeur à charger</param>
      public static void InitValBytes(ref byte[] TabBytes, byte Val)
      {
         for (int i = 0; i < TabBytes.Length; i++)
            TabBytes[0] = Val;
      }


      /// <summary>
      /// Converion d'un "ushort" dans 16 bits
      /// </summary>
      /// <param name="u">ushort a convertir</param>
      /// <returns>Tableau de 16 booléens</returns>
      public static bool[] UNushortDansBool(ushort u)
      {
         bool[] tabB = new bool[16];

         long division = u;
         for (int i = 0; division > 0; i++)
         {
            tabB[i] = (division%2 == 1);
            division = division/2;
         }
         return tabB;
      }

      /// <summary>
      /// Converion d'un "byte" dans 8 bits
      /// </summary>
      /// <param name="b">byte a convertir</param>
      /// <returns>Tableau de 8 booléens</returns>
      public static bool[] UNbyteDansBool(byte b)
      {
         bool[] tabB = new bool[8];

         long division = b;
         for (int i = 0; division > 0; i++)
         {
            tabB[i] = (division % 2 == 1);
            division = division / 2;
         }
         return tabB;
      }

      /// <summary>
      /// Converion d'un tableau de 16 bool dans UN "ushort"
      /// </summary>
      /// <param name="TabBool">Tableau de 16 booléens</param>
      /// <returns>Resultat conversion</returns>
      public static ushort BoolDansUNushort(bool[] TabBool)
      {
         int temp = 0;
         try
         {
            for (int i = 0; i < 16; i++)
            {
               if (TabBool[i])
                  temp = temp + Convert.ToInt32(Math.Pow(2, i));
            }
         }
         catch
         {
         }
         return Convert.ToUInt16(temp);
      }


      /// <summary>
      /// Inverse les bytes 2 par 2 dans le tableau
      /// </summary>
      /// <param name="TabBytes">Tableu de byte à convertir</param>
      /// <returns>Resultat conversion</returns>
      public static byte[] InversionBytes2par2(byte[] TabBytes)
      {
         // Nb de bytes pair ?
         bool pair = ((TabBytes.Length%2) == 0);
         int iMax = TabBytes.Length - 1;
         if (!pair)
            iMax--;

         // Inversion bytes 2 par 2
         for (int i = 0; i < iMax; i = i + 2)
         {
            byte temp = TabBytes[i];
            TabBytes[i] = TabBytes[i + 1];
            TabBytes[i + 1] = temp;
         }

         return TabBytes;
      }

      
      /// <summary>
      /// Conversion d'un tableau de "ushort" en tableau de bool
      /// </summary>
      /// <param name="d">Tableau de "ushort" à convertir, passé dans un tableau de "double" </param>
      /// <returns>Tableau de booléens (longueur : 16 X Nb de mots</returns>
      public static bool[] TabUshortDansBool(double[] d)
      {
         bool[] tabB = new bool[16*d.Length];
         for (int i = 0; i < d.Length; i++)
         {
            ushort us = Convert.ToUInt16(d[i]); 
            bool[] tab16B = UNushortDansBool(us);
            for (int j = 0; j < 16; j++)
            {
               tabB[(i*16) + j] = tab16B[j];
            }
         }
         return tabB;
      }


      /// <summary>
      ///  Converion d'un tableau de bool dans un byte
      /// </summary>
      /// <param name="TabBool">Tableu de 8 booleens</param>
      /// <returns>Resultat conversion</returns>
      public static byte ConversionTabBoolDansByte(bool[] TabBool)
      {
         byte unByte = 0;
         byte poids = 1;

         for (int i = 0; i < TabBool.Length; i++)
         {
            if (TabBool[i])
               unByte = (byte) (unByte + poids);
            poids = (byte) (poids*2);
         }
         return unByte;
      }



      /// <summary>
      /// Converion d'un byte dans tableau de bool
      /// </summary>
      /// <param name="b">Byte a convertir</param>
      /// <returns>Resultat conversion</returns>
      public static bool[] ConversionByteDansTabBool(byte b)
      {
         bool[] myBits = new bool[8]; // 8 bits

         for (byte x = 0; x < myBits.Length; x++)
         {
            myBits[x] = (((b >> x) & 0x01) == 0x01);
         }
         return myBits;
      }


      /// <summary>
      ///  Converion d'un tableau de bool dans un "ushort"
      /// </summary>
      /// <param name="TabBool">Tableu de 16 booleens</param>
      /// <returns>Resultat conversion</returns>
      public static ushort ConversionTabBoolDansUshort(bool[] TabBool)
      {
         ushort uShortValue = 0;
         for (int i = 0; i < TabBool.Length; i++)
         {
            if (TabBool[i])
               uShortValue += (ushort) (1 << i);
         }
         return uShortValue;
      }


      /// <summary>
      /// Recupération signe, mantisse et exposant d'un double
      /// </summary>
      /// <param name="d">Valeur double</param>
      /// <param name="PrecisionMantisse">Précision de la mantisse</param>
      /// <param name="Mantisse">Retourne la mantisse </param>
      /// <param name="Exposant">Retourne l'exposant</param>
      public static void RecupMantisseExposant(double d, int PrecisionMantisse, out double Mantisse, out int Exposant)
      {
         Mantisse = 0;
         Exposant = 0;

         string s = d.ToString("E" + PrecisionMantisse.ToString());
         string[] splitS = s.Split('E');

         if (splitS.Length == 2)
         {
            Mantisse = Convert.ToDouble(splitS[0]);
            Exposant = Convert.ToInt32(splitS[1]);
         }
      }


      /// <summary>
      ///  Extrait deux "ushort" d'un tableau, et converti en tableau de 4 bytes
      /// </summary>
      /// <param name="TabUshort">Tableau de "ushort"</param>
      /// <param name="IndexDeb">Indicedans le tableau</param>
      /// <param name="swapMots">Si VRAI : inversion des 2 mots à traiter</param>
      /// <returns></returns>
      public static byte[] DeuxUshortTableau_dans_TabBytes(ushort[] TabUshort, int IndexDeb, bool swapMots)
      {
         byte[] b1234 = new byte[4];
         if (TabUshort.Length >= IndexDeb + 2) // Il faut accéder à IndexDeb + 1
         {
            byte[] b12;
            byte[] b34;
            if (swapMots)
            {
               b12 = (BitConverter.GetBytes(TabUshort[IndexDeb]));
               b34 = (BitConverter.GetBytes(TabUshort[IndexDeb + 1]));
            }
            else
            {
               b12 = (BitConverter.GetBytes(TabUshort[IndexDeb + 1]));
               b34 = (BitConverter.GetBytes(TabUshort[IndexDeb]));
            }
            b1234 = new byte[4];
            Array.Copy(b34, 0, b1234, 0, 2);
            Array.Copy(b12, 0, b1234, 2, 2);
         }
         return b1234;
      }


      /// <summary>
      /// Calcul du MSB d'un mot 16 bits
      /// </summary>
      /// <param name="mot">Mot d'entrée</param>
      /// <returns>Byte MSB</returns>
      public static byte MSB(UInt16 mot)
      {
         return (byte)(mot / 256);
      }


      /// <summary>
      /// Calcul du LSB d'un mot 16 bits
      /// </summary>
      /// <param name="mot">Mot d'entrée</param>
      /// <returns>Byte LSB</returns>
      public static byte LSB(UInt16 mot)
      {
         return (byte)(mot % 256);
      }

   
  


      #endregion



      #region Conversions : Chaines - Bytes

      /// <summary>
      /// Converion d'une chaine en tableau de bytes
      /// </summary>
      /// <param name="s">Chaine à convertir</param>
      /// <returns>Resultat conversion</returns>
      public static byte[] StrToBytes(string s)
      {
         byte[] tab = new byte[s.Length];
         for (int i = 0; i < s.Length; i++)
         {
            tab[i] = (byte) s[i];
         }
         return tab;
      }

      /// <summary>
      /// Converion d'un tableau de bytes en chaine
      /// </summary>    
      /// <param name="TabBytes">Tableau à convertir</param>
      /// <returns>Resultat conversion</returns>   
      public static string BytesToStr(byte[] TabBytes)
      {
         string str = "";
         for (int i = 0; i < TabBytes.Length; i++)
            str = str + (char) (TabBytes[i]);

         return str;
      }

      #endregion



      #region Hexa



      /// <summary>
      /// Converion d'un entier en hexa
      /// </summary>
      /// <param name="i">Entier à convertir</param>
      /// <returns>Resultat conversion</returns>   
      public static string ConversionIntToHexa(int i)
      {
         string s = "";
         try
         {
            s = i.ToString("X");
         }
         catch
         {
         }
         return s;
      }

      /// <summary>
      ///Converion d'un entier contenu dans une chaine en hexa
      /// </summary>
      /// <returns></returns>
      public static string ConversionIntToHexa(string str_i)
      {
         string s = "";
         try
         {
            int i = Convert.ToInt32(str_i);
            s = i.ToString("X");
         }
         catch
         {
         }
         return s;
      }



      /// <summary>
      /// Converion d'un hexa (chaine) en entier
      /// </summary>
      /// <param name="s">Chaine à convertir</param>
      /// <returns>Resultat conversion</returns>   
      public static int ConversionHexaToInt32(string s)
      {
         int i = 0;
         try
         {
            i = int.Parse(s, System.Globalization.NumberStyles.HexNumber);
         }
         catch
         {
         }
         return i;
      }

      #endregion


   }
}

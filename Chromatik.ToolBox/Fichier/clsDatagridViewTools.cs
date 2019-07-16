using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Chromatik.Data;
using Chromatik.Env;

namespace Chromatik.Fichier
{
   /// <summary>
   /// Outils pour DataGridView
   /// </summary>
   public static class clsDatagridViewTools
   {
      /// <summary>
      /// Export contenu DataGridView dans un fichier texte
      /// </summary>
      /// <param name="Dtg">Le DataGridView</param>
      /// <param name="FicTexte">Fichier à créer </param>
      /// <param name="EnteteCol">Avec/Sans entêtes de colonnes</param>
      /// <param name="EnteteLig">Avec/Sans entêtes de lignes </param>
      /// <param name="SeparateurColonnes">Caractère opu chaines sépérateur de colonnes (tabulation par défaut) </param>
      /// <param name="NomColonnes">Enêtes colonnes. Si Null : headers de colonnes </param>
      public static void ExportDtgvDansFichier(DataGridView Dtg, string FicTexte, bool EnteteCol, bool EnteteLig,
                                               string SeparateurColonnes, string[] NomColonnes)
      {
         ExportDtgvDansFichier(Dtg, FicTexte, EnteteCol, EnteteLig,SeparateurColonnes, NomColonnes, false,null,null);
      }



      //sm21/03/2018 : "ajouterSiFichierExiste" 
      //sm21/06/2018 : "lignesAmontTableau"  et "LignesAvalTableau"       
      /// <summary>
      /// Export contenu DataGridView dans un fichier texte
      /// </summary>
      /// <param name="Dtg">Le DataGridView</param>
      /// <param name="FicTexte">Fichier à créer </param>
      /// <param name="EnteteCol">Avec/Sans entêtes de colonnes</param>
      /// <param name="EnteteLig">Avec/Sans entêtes de lignes </param>
      /// <param name="SeparateurColonnes">Caractère opu chaines sépérateur de colonnes (tabulation par défaut) </param>
      /// <param name="NomColonnes">Enêtes colonnes. Si Null : headers de colonnes </param>
      /// <param name="ajouterSiFichierExiste">Si vrai : ajout en fin de fichier si déjà existant </param>
      public static void ExportDtgvDansFichier(DataGridView Dtg, string FicTexte, bool EnteteCol, bool EnteteLig,
                                               string SeparateurColonnes, string[] NomColonnes, bool ajouterSiFichierExiste,
                                               List<string> LignesAmontTableau, List <string> LignesAvalTableau)
      {
         List<string> l = new List<string>();
         string s = "";
         if (string.IsNullOrEmpty(SeparateurColonnes))
            SeparateurColonnes = "\t";


         // Lignes amont tableau
         if ((LignesAmontTableau != null) && (LignesAmontTableau.Count > 0))
            foreach (string lAmont in LignesAmontTableau)            
               l.Add(lAmont);

         // Ligne entête colonnes
         if (EnteteCol)
         {
            for (int iCol = 0; iCol < Dtg.ColumnCount; iCol++)
            {
               if ((NomColonnes != null) && (NomColonnes.Length > 0))
               {
                  // Noms de colonnes passé en paramètre
                  if (iCol < NomColonnes.Length)
                     s += NomColonnes[iCol];
                  else
                     s += (iCol + 1).ToString();
               }
               else
               {
                  // Nomse de colonnes header grille
                  if (Dtg.Columns[iCol].HeaderCell.Value != null)
                     s += Dtg.Columns[iCol].HeaderCell.Value.ToString();
                  else
                     s += (iCol + 1).ToString();
               }
               // Ajout tabulation entre colonnes
               if (iCol < Dtg.ColumnCount - 1)
                  s += SeparateurColonnes;
            }
            if (EnteteLig)
               s = SeparateurColonnes + s;
            l.Add(s.Replace('\n', ' '));  // Sup des sauts de lignes éventuels dans entête de colonnes
         }

         // Lignes tableau
         for (int iLig = 0; iLig < Dtg.RowCount; iLig++)
         {
            if (!Dtg.Rows[iLig].IsNewRow) // Si la ligne n'est pas la ligne de création du tdataGridView
            {
               if (Dtg.Rows[iLig].Visible) // Uniquement si la ligne est visible
               {
                  s = "";
                  if (EnteteLig)
                  {
                     if (Dtg.Rows[iLig].HeaderCell.Value != null)
                        s += Dtg.Rows[iLig].HeaderCell.Value.ToString() + SeparateurColonnes;
                     else
                        s += (iLig + 1).ToString() + SeparateurColonnes;
                  }


                  for (int iCol = 0; iCol < Dtg.ColumnCount; iCol++)
                  {
                     if (Dtg[iCol, iLig].Value != null)
                        s += Dtg[iCol, iLig].Value.ToString();
                     else
                        s += "(null)";

                     // Ajout tabulation entre colonnes
                     if (iCol < Dtg.ColumnCount - 1)
                        s += SeparateurColonnes;
                  }
                  l.Add(s);
               }
            }
         }


         // Lignes aval tableau
         if ((LignesAvalTableau != null) && (LignesAvalTableau.Count > 0))
            foreach (string lAval in LignesAvalTableau)
               l.Add(lAval);


         // Fichier
         // Encodage pour éviter problèmes d'accents selon programme d'appel.
         string enc = "iso-8859-1";

         string[] lFic;
         int offsetLfic = 0;
         if (File.Exists(FicTexte) && ajouterSiFichierExiste)
         {
            lFic = File.ReadAllLines(FicTexte , Encoding.GetEncoding(enc));
            offsetLfic = lFic.Length;
            Array.Resize(ref lFic, lFic.Length + l.Count);
         }
         else
         {
            lFic = new string[l.Count];
         }

         for (int i = 0; i < l.Count; i++)
            lFic[offsetLfic+i] = l[i];

       
         File.WriteAllLines(FicTexte, lFic, Encoding.GetEncoding(enc));
      }


      /// <summary>
      /// Copie les données d'une grille dans le presse papier et dans une chaine de caractères
      /// </summary>
      /// <param name="dtg">Grille</param>
      /// <returns>Données de la grille</returns>
      public static string CopierDonneesGrille(DataGridView dtg)
      {
         string s = "";
         // Entêtes colonnes
         s += '\t'; // Pour emplacement header lignes
         foreach (DataGridViewColumn c in dtg.Columns)
         {
            if (c.HeaderCell.Value != null)
               s += c.HeaderCell.Value.ToString() + '\t';
            else
               s += '\t';
         }
         s = s.Replace('\n', ' '); // Sup des saut de lignes éventuels dans les noms de colonnes

         // Contenu grille
         foreach (DataGridViewRow r in dtg.Rows)
         {
            if (r.HeaderCell.Value != null)
               s += clsTxt.NL() + r.HeaderCell.Value + '\t';
            else
               s += clsTxt.NL() + '\t';
            for (int i = 0; i < r.Cells.Count; i++)
            {
               if (r.Cells[i].Value != null)  //sm18/06/2018
                  s += r.Cells[i].Value.ToString() + '\t';
               else s+=  '\t';
            }
         }

         // Recopie dans presse papier
         Clipboard.SetText(s);

         return s;
      }


      /// <summary>
      /// Copie une ligne de la grille dans le presse papier et dans une chaine de caractères
      /// </summary>
      /// <param name="dtg">Grille</param>
      /// <param name="iLigne">Numero de ligne</param>
      /// <returns>Données de la ligne</returns>
      public static string CopierUneLigneGrille(DataGridView dtg, int iLigne)
      {
         string s = "";      

         // Contenu grille
         if (iLigne < dtg.RowCount)
         {
            DataGridViewRow r = dtg.Rows[iLigne];
            for (int i = 0; i < r.Cells.Count; i++)
            {
               if (r.Cells[i].Value != null) //sm18/06/2018
                  s += r.Cells[i].Value.ToString() + '\t';
               else s += '\t';
            }
         }
         // Recopie dans presse papier
         Clipboard.SetText(s);

         return s;
      }


   }
}

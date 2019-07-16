using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Chromatik.Env
{
    /// <summary>
    /// Dossiers environnement Windows
    /// </summary>
    public static class clsDossier
    {
        /// <summary>
        /// Dossier windows ProgramData
        /// </summary>
        public static string ProgramData = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        /// <summary>
        /// Dossier de l'application
        /// </summary>
        public static string Appli = Path.GetDirectoryName(Application.ExecutablePath);
    }



    /// <summary>
    /// Informations sur l'application
    /// </summary>
    public static class clsAppli
    {
        /// <summary>
        /// Version de l'application
        /// </summary>
        /// <param name="nbIndices">Nombre d'indices dans la version (Exemple : Valeur 2 donne  "V1.2" , Valeur 3 donne "V.1.2.3")  </param>
        /// <param name="avecDate">Ajoute la date </param>
        /// <param name="avecHeure">Ajoute l'heure</param>
        /// <param name="separateur">Séparateur entre les sous chaines versions / date / heure</param>
        /// <returns>Version</returns>
        public static string Version(int nbIndices, bool avecDate, bool avecHeure, string separateur)
        {

            string[] splitV = Application.ProductVersion.Split('.');
            // Nota : Assembly.GetExecutingAssembly : Assembly actuel (donc cette DLL)
            //        Assembly.GetCallingAssembly:   Assembly qui a appelé le code
            DateTime buildDate = new FileInfo(Assembly.GetCallingAssembly().Location).LastWriteTime;

            string sVersion = "";
            // Indices (ex "V.1.2")
            if (nbIndices > 0)
            {
                sVersion = "V";
                if (nbIndices > splitV.Length)
                    nbIndices = splitV.Length;
                for (int i = 0; i < nbIndices; i++)
                {
                    sVersion += splitV[i] + '.';
                }
                sVersion = sVersion.TrimEnd('.');
            }

            if (string.IsNullOrEmpty(separateur))
                separateur = "  ";

            // Date et Heure
            if (avecDate)
                if (!string.IsNullOrEmpty(sVersion))
                    sVersion += separateur + buildDate.Date.ToString("dd/MM/yyyy");

            if (avecHeure)
                if (!string.IsNullOrEmpty(sVersion))
                    sVersion += separateur + buildDate.ToLocalTime().ToString("hh:mm");

            return sVersion;
        }
    }

}

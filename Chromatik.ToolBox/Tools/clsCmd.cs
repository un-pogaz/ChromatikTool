using System;
using System.Diagnostics;
using System.Text;

namespace Chromatik.Tools
{
    /// <summary>
    /// Windows CMD
    /// </summary>
    public static class clsCmd
    {
        /// <summary>
        /// Envoi d'une commande cmd
        /// </summary>
        /// <param name="arg">Arguments de la commande</param>
        /// <param name="ShellExecute">Resultat dans fenêtre de commande</param>
        /// <returns></returns>
        public static string Cmd(string arg, bool ShellExecute)
        {
            string ret = "";

            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            if (ShellExecute)
                process.StartInfo.Arguments = "/T:17 /K " + arg;
            else
                process.StartInfo.Arguments = "/T:17 /C " + arg;

            process.StartInfo.UseShellExecute = ShellExecute;
            process.StartInfo.WorkingDirectory = "C:\\";

            if (ShellExecute)
            {
                process.Start();
            }
            else
            {
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                // Conversion du texte reçu :   Faut-il convertir ?  Selon la commande, la chaine de retour n'a pas le même encodage (! ?)
                // MS-DOS code pages CP437 (DOSLatinUS) 
                //   Exemple : é = 0x82 (130) 
                // MS-Windows code pages CP1252 (WinLatin1) 
                //   Exemple : é = 0xE9 (233)
                //
                // Methode non infaillible : Recherche de la valeur du byte le plus élevé
                // Code page CP437 (DOS) : caractère max = "A3" au dela : caractères semi-graphiques sauf "FF"

                string s = process.StandardOutput.ReadToEnd();
                byte[] b = process.StandardOutput.CurrentEncoding.GetBytes(s);

                byte bMax = 0;
                for (int i = 0; i < b.Length; i++)
                {
                    if ((b[i] > bMax) && (b[i] < 255))
                        bMax = b[i];
                }
                bool CP437 = (bMax < Convert.ToInt32("A3", 16));

                if (CP437)
                {
                    b = Encoding.Convert(Encoding.GetEncoding("CP437"), Encoding.GetEncoding(1252), b);
                    s = Encoding.Default.GetString(b, 0, b.Length);
                }

                // sup   du C:\  à la fin
                if (s.Substring(s.Length - 4, 4) == "C:\\>")
                    s = s.Remove(s.Length - 4, 4);

                s = "================= [" + DateTime.Now.ToString() + "]  " + arg + "  =================\r\n"
                    + s
                    + "===================================================================\r\n\r\n";

                ret = s;
            }
            return ret;
        }

    }
}

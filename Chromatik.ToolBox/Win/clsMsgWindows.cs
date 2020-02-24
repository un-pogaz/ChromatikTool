using System;
using System.Text;
using System.Windows.Forms;
using Chromatik.Data;

namespace Chromatik.ToolBox.Win
{
    /// <summary>
    /// Utilisation de la messagerie windows pour communiquer entre applications
    /// </summary>
    public static class clsMsgWindows
    {
        /// <summary>
        /// Envoi d'un message windows de type WM_COPYDATA
        /// </summary>
        /// <param name="fenetre">Titre (dans le bandeau) de la fenêtre de destination (Exemple : "Simulateur")</param>
        /// <param name="message">Message composé d'un identifiant expediteur , d'un message ou commande, et eventuellements de paramètres
        /// <para>  Syntaxe :  IdApplication|Commande|param1|param2.... </para>
        /// <para>  Les données sont séparées par la caractère "|" (pipe) </para>
        /// <para>  Exemple : "CPIT_OPHELY|TEST_SYNO|syno1.xml"</para>
        ///  </param>
        /// <returns></returns>
        public static bool EnvoyerMessage(string fenetre, string message)
        {
            bool OK = false;
            clsMessageHelper msg = new clsMessageHelper();

            //First param can be null
            int hWnd = msg.getWindowId(null, fenetre);
            if (hWnd != 0)
            {
                OK = (msg.sendWindowsStringMessage(hWnd, 0, message) == 0);
            }
            return OK;
        }



        /// <summary>
        /// Traitement de la réception d'un message. Cette methode doit être appellée depuis chaque fenêtre qui peut recevoir des messages :
        /// <code>
        /// protected override void WndProc(ref Message m)
        /// {
        ///   string IDappli = "ID";
        ///   string MsgRecu = "";
        ///   string[] Param = new string[0];
        ///   if (clsMsgWindows.TraitementWndProc(IDappli,m, out MsgRecu,out Param))
        ///   {
        ///     if (MsgRecu == "MessageAttendu")
        ///     {
        ///          //  Traitement du message
        ///      }
        ///   }
        ///   else
        ///   {
        ///     base.WndProc(ref m);
        ///   }
        ///  }  
        /// </code>
        /// </summary>
        /// <param name="IDappli">Identification application qui permet de vérifier si le message doit être trait. Ex :"CPIT_OPHELY"</param>
        /// <param name="m">Message windows</param>
        /// <param name="Msg">Message ou commande reçue</param>
        /// <param name="Param">Liste des paramètres recus </param>
        /// <returns>True si l'identification application du message est identique à IDappli (donc, le message doit être traité)</returns>
        public static bool TraitementWndProc(string IDappli, Message m, out string Msg, out string[] Param)
        {
            bool msgTraite = false;
            Msg = "";
            Param = new string[0];

            if (m.Msg == clsUser32.WM_COPYDATA)
            {
                try
                {
                    object o = m.GetLParam(typeof(clsUser32.COPYDATASTRUCT));
                    clsUser32.COPYDATASTRUCT data = (clsUser32.COPYDATASTRUCT)o;

                    // Traitement message
                    string[] message = data.lpData.Split('|');
                    for (int i = 0; i < message.Length; i++)
                        message[i] = message[i].Trim();

                    if ((message.Length >= 2) && (message[0].ToUpper() == IDappli)) //Exemple : "CPIT_OPHELY"))
                    {
                        // Message / Commande
                        Msg = message[1].ToUpper();
                        // Autres données éventuelles après le message : paramètres
                        Param = new string[message.Length - 2];
                        for (int i = 0; i < Param.Length; i++)
                        {
                            Param[i] = message[i + 2];
                        }

                        msgTraite = true;
                    }
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Oops", MessageBoxButtons.OK);
                }
            }
            return msgTraite;
        }

    }

    internal class clsMessageHelper
    {
        //http://boycook.wordpress.com/2008/07/29/c-win32-messaging-with-sendmessage-and-wm_copydata/

        public int sendWindowsStringMessage(int hWnd, int wParam, string msg)
        {
            int result = 0;
            if (hWnd > 0)
            {
                byte[] sarr = Encoding.Default.GetBytes(msg);
                int len = sarr.Length;
                clsUser32.COPYDATASTRUCT cds;
                cds.dwData = (IntPtr)100;
                cds.lpData = msg;
                cds.cbData = len + 1;

                result = clsUser32.SendMessage(hWnd, clsUser32.WM_COPYDATA, wParam, ref cds);
                // IMPORTANT PostMessage ne fonctionne pas avec "WM_COPYDATA"  (?)
                //  result = PostMessage(hWnd, WM_COPYDATA, wParam, ref cds);
            }
            return result;
        }

        public int sendWindowsMessage(int hWnd, int Msg, int wParam, int lParam)
        {
            int result = 0;
            if (hWnd > 0)
            {
                result = clsUser32.SendMessage(hWnd, Msg, wParam, lParam);
            }
            return result;
        }

        public int getWindowId(string className, string windowName)
        {
            return clsUser32.FindWindow(className, windowName);
        }
    }

}

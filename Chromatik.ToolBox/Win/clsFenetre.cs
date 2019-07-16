using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Chromatik.Env;

namespace Chromatik.Win
{
   //sm05/09/2017
   /// <summary>
   /// Gestion fenêtres windows
   /// </summary>
   public static class clsFenetre
   {

      /// <summary>
      /// Permet à une application de rappeler la fenêtre principale si l'application est déjà en cours d'exécution
      ///
      /// Exemple d'utilisation : 
      ///     private bool fermerApplication = false;
      /// 
      ///    --> dans constructeur ou load d'une "form" :
      ///    if (clsFenetre.RappelerAppliSiDejaOuverte(Process.GetCurrentProcess(), this.Text))
      ///    {
      ///       fermerApplication = true; 
      ///       return;
      ///    }
      /// 
      ///    --> dans shown :
      ///    if (fermerApplication)
      ///    {
      ///      Close();
      ///      return;
      ///    }
      ///  
      /// 
      /// </summary>
      /// <param name="pAppelant">process appelant</param>
      /// <param name="titreMain">titre fenêtre </param>
      /// <returns>"true" si rappele effectif</returns>
      public static bool RappelerAppliSiDejaOuverte(Process pAppelant, string titreMain)
      {
         bool ret = false;
         Process[] p;
         clsProcess.FindProcess(pAppelant.ProcessName, pAppelant.Id, titreMain, out p);
         if (p.Length > 0)
         {
            ActiverFenetre_PID(p[0].Id);
            ret = true;
         }
         return ret;
      }


      /// <summary>
      /// Rappelle une fenêtre au premier plan connaissant le PID
      /// </summary>
      /// <param name="PID">PID du processus</param>
      public static void ActiverFenetre_PID(int PID)
      {
         if (PID != 0)
         {
            try
            {
               Process p = Process.GetProcessById(PID);
               clsUser32.SetForegroundWindow((int) p.MainWindowHandle);
               clsUser32.ShowWindowAsync(new HandleRef(null, p.MainWindowHandle), clsUser32.Restore);
               clsUser32.SetForegroundWindow((int) p.MainWindowHandle);
            }
            catch
            {
            }
         }
      }
   }
}

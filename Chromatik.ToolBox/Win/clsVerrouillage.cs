using System.Diagnostics;
using Microsoft.Win32;

namespace Chromatik.Win
{
   // A TERMINER
   //  voir  http://www.tamas.io/c-disable-ctrl-alt-del-alt-tab-alt-f4-start-menu-and-so-on/

   /// <summary>
   /// Cette classe contient les fonction qui permettent le verrouillage du PC
   /// </summary>
   public static class clsVerrouillagePC
   {
      /// <summary>
      /// Verouille le PC
      /// </summary>
      public static void VerrouillePC()
      {
         //Activation_Ctrl_Alt_Supp(false);
         //Process[] pExplorer = Process.GetProcessesByName("Explorer");
         //foreach (Process p in pExplorer)
         //{
         //   p.Kill();
         //   p.WaitForExit();
         //}
      }


      /// <summary>
      /// Déverrouille le PC
      /// </summary>
      public static void DeverrouillePC(bool shuttdown)
      {
         //Activation_Ctrl_Alt_Supp(true);
         //if (shuttdown)
         //   Process.Start("shutdown", " -l");
         //else
         //   Process.Start("explorer.exe");
      }

      /// <summary>
      /// Verrouillage/Déverrouillage "Ctrl Alt Suppr" 
      /// </summary>
      /// <param name="actif"></param>
      /// <returns></returns>
      public static void Activation_GestionnaireTaches(bool actif)
      {
         try
         {
            if (actif)
            {
               string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";
               RegistryKey rk = Registry.CurrentUser;
               RegistryKey sk1 = rk.OpenSubKey(subKey);
               if (sk1 != null)
                  rk.DeleteSubKeyTree(subKey);
            }
            else
            {
               RegistryKey regkey;
               string keyValueInt = "1";
               string subKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

               regkey = Registry.CurrentUser.CreateSubKey(subKey);
               regkey.SetValue("DisableTaskMgr", keyValueInt);
               regkey.Close();
            }
         }
         catch
         {

         }

         /*
         RegistryKey RegistryProjet;


         // *** Ajout de l'info de verrouillage du gestionnaire des taches
         RegistryProjet =
            Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System", true);
         if (RegistryProjet == null)
         {
            RegistryProjet = Registry.CurrentUser;
            RegistryProjet.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System");
         }
         RegistryProjet =
            Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Policies\\System\\", true);
         if (RegistryProjet != null)
         {
            if (actif)
               RegistryProjet.DeleteValue("DisableTaskMgr");
            else
               RegistryProjet.SetValue("DisableTaskMgr", "1");
         }

         

         // *** Redemmarrage auto de explorer.exe

         RegistryProjet = Registry.LocalMachine.OpenSubKey(
            @"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon", true);
         if (RegistryProjet == null)
         {
            RegistryProjet = Registry.LocalMachine;
            RegistryProjet.CreateSubKey("@\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon",
                                        RegistryKeyPermissionCheck.ReadWriteSubTree);
         }
         RegistryProjet =
            Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\Winlogon\\",
                                             RegistryKeyPermissionCheck.ReadWriteSubTree);
         if (RegistryProjet != null)
            RegistryProjet.SetValue("AutoRestartShell", actif ? 1.ToString() : 0.ToString(), RegistryValueKind.DWord);
         
        

         if (RegistryProjet != null)
            RegistryProjet.Close();
          *  */
      }
  
   }

}

using System;
using System.Runtime.InteropServices;

namespace Chromatik.Win
{
   /// <summary> </summary>
   [StructLayout(LayoutKind.Sequential)]
   public class RECTbureau : ICloneable
   {
      /// <summary></summary>
      public int Left;
      /// <summary></summary>
      public int Top;
      /// <summary> </summary>
      public int Right;
      /// <summary> </summary>
      public int Bottom;


      /// <summary> </summary>
      public object Clone()
      {
         RECTbureau oRECT = new RECTbureau();
         oRECT.Top = this.Top;
         oRECT.Bottom = this.Bottom;
         oRECT.Left = this.Left;
         oRECT.Right = this.Right;
         return oRECT;
      }

   }

   /// <summary>
   /// Gestion de la dimension du bureau Windows
   /// </summary>
   public class clsBureau
   {

      private static RECTbureau m_RECTInit = new RECTbureau();


      private static void getWorkspace(RECTbureau oRECT)
      {
         clsUser32.SystemParametersInfo(clsUser32.SPI_GETWORKAREA, 0, oRECT, 0);
      }


      private static void setWorkspace(RECTbureau oRect)
      {
         clsUser32.SystemParametersInfo(clsUser32.SPI_SETWORKAREA, 0, oRect, clsUser32.SPIF_SENDCHANGE);
      }


      /// <summary>
      /// Sauvegarde la taille du bureau initial
      /// </summary>
      public static void SauvegardeBureauInitial()
      {
         getWorkspace(m_RECTInit);
         m_RECTInit.Left = 0;
      }

      /// <summary>
      /// Restaure le bureau à sa taille initiale
      /// </summary>
      public static void RestaureBureauInitial()
      {
         setWorkspace(m_RECTInit);
      }

      /// <summary>
      /// Modifie la taille du bureau
      /// </summary>
      /// <param name="Marge"></param>
      public static void RedimendionnementBureau(int Marge)
      {
         RECTbureau oRECT = (RECTbureau) m_RECTInit.Clone();
         oRECT.Left = Marge;
         setWorkspace(oRECT);

      }
   }
}

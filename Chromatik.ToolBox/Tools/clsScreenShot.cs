using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Chromatik.Tools
{
   /// <summary>
   /// Copie d'écran
   /// Exemple d'utilisation :
   /// <code>
   /// 	private void btnPrint_Click(object sender, EventArgs e)
   /// 	{
   /// 		var PrintDial = new PrintDialog();
   /// 		var PrintDoc = new PrintDocument();
   /// 	   PrintDoc.PrintPage += clsScreenShot.PrintImage;
   /// 	
   /// 	   clsScreenShot.CaptureScreen(this);
   /// 		PrintDial.Document = PrintDoc;
   /// 	
   ///    	// Sélection imprimante
   /// 		PrintDoc.PrinterSettings.PrinterName = "";   
   /// 		PrintDial.ShowDialog();
   /// 	
   ///  		//Forçage en paysage.
   /// 		PrintDoc.DefaultPageSettings.Landscape = true;
   /// 	
   /// 		// Impression
   /// 		if (PrintDoc.PrinterSettings.PrinterName != "")
   ///   	 	PrintDoc.Print();
   /// 	}
   /// </code>
   /// </summary>
   public static class clsScreenShot
	{	
		private static Bitmap memoryImage;
      /// <summary>
      /// Capture d'ecran pour impression.
      /// </summary>
      /// <param name="frm">La fenêtre</param>
		public static void CaptureScreen(Form frm)
		{
			Graphics myGraphics = frm.CreateGraphics();
			Size s = frm.Size;
			memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
			Graphics memoryGraphics = Graphics.FromImage(memoryImage);
			memoryGraphics.CopyFromScreen(frm.Location.X, frm.Location.Y, 0, 0, s);
		}

		
      /// <summary>
      /// Dessine la capture d'écran pour impression.
      /// (Abonnement à l'événement : PrintDoc.PrintPage += clsScreenShot.PrintImage; )
      /// </summary>
      /// <param name="o">na</param>
      /// <param name="e">na</param>
		public static void PrintImage(object o, PrintPageEventArgs e)
		{
			e.Graphics.DrawImage(memoryImage, 0, 0);
		}   
	}
}

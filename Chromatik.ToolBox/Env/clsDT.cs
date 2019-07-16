using System;
using System.Globalization;

namespace Chromatik.Env
{
   /// <summary>
   /// Gestion Date Time
   /// </summary>
   public static class clsDT
   {
      /// <summary>
      /// Date du premier jour de la semaine d'une date donnée
      /// </summary>
      /// <param name="dt">Date </param>
      /// <returns></returns>
      public static DateTime PremierJourDeLaSemaine(DateTime dt)
      {
         int JourDsSemaine = NumeroJourDeLaSemaine(dt);
            //(int) dt.DayOfWeek;  ATTENTION DayOfWeek donne 0 pour le dimanche         
         DateTime premJourSemaine = dt.AddDays((-1)*JourDsSemaine + 1);
         return premJourSemaine;
      }

      /// <summary>
      /// Date du dernier jour de la semaine d'une date donnée
      /// </summary>
      /// <param name="dt">Date </param>
      /// <returns></returns>
      public static DateTime DernierJourDeLaSemaine(DateTime dt)
      {
         DateTime dernierJourSemaine = PremierJourDeLaSemaine(dt).AddDays(6);
         return dernierJourSemaine;
      }

      /// <summary>
      /// Date du premier jour du mois d'une date donnée
      /// </summary>
      /// <param name="dt">Date </param>
      /// <returns></returns>
      public static DateTime PremierJourDuMois(DateTime dt)
      {
         DateTime premJourMois = new DateTime(dt.Year, dt.Month, 1);
         return premJourMois;
      }

      /// <summary>
      ///  Date du dernier jour du mois
      /// </summary>
      /// <param name="dt">Date </param>
      /// <returns></returns>
      public static DateTime DernierJourDuMois(DateTime dt)
      {
         DateTime dernierJourMois = new DateTime(dt.Year, dt.Month, 1).AddMonths(1).AddDays(-1);
         return dernierJourMois;

      }

      /// <summary>
      /// Date du premier jour d'une semaine donnée
      /// </summary>
      /// <param name="year">Année</param>
      /// <param name="weekOfYear">Numéro de sameine</param>
      /// <param name="culture">Culture (Ex : "CultureInfo.CurrentCulture") </param>
      /// <returns></returns>
      public static DateTime DatePremierJourSemaine(int year, int weekOfYear, CultureInfo culture)
      {
         DateTime jan1 = new DateTime(year, 1, 1);
         int daysOffset = (int)culture.DateTimeFormat.FirstDayOfWeek - (int)jan1.DayOfWeek;
         DateTime firstWeekDay = jan1.AddDays(daysOffset);
         int firstWeek = culture.Calendar.GetWeekOfYear(jan1, culture.DateTimeFormat.CalendarWeekRule,
                                                   culture.DateTimeFormat.FirstDayOfWeek);
         if (firstWeek <= 1 || firstWeek > 50)
         {
            weekOfYear -= 1;
         }
         return firstWeekDay.AddDays(weekOfYear * 7);
      }

      /// <summary>
      /// Numero de semaine d'une date donnée
      /// </summary>
      /// <param name="dt">Date </param>
      /// <returns></returns>
      public static int NumeroSemaine(DateTime dt)
      {
         //  https://msdn.microsoft.com/fr-fr/library/system.globalization.calendar.getweekofyear(v=vs.80).aspx

         CultureInfo myCI = CultureInfo.CurrentCulture;
         Calendar myCal = myCI.Calendar;
         CalendarWeekRule myCWR = CalendarWeekRule.FirstFourDayWeek;
         //ISO : Semaine 1 = 1ere semaine d'au moins 4 jours       
         DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;
         return myCal.GetWeekOfYear(dt, myCWR, myFirstDOW);
      }


      /// <summary>
      /// Numero de jour de la semaine d'une date donnée
      /// </summary>
      /// <param name="dt">Date </param>
      /// <returns></returns>
      public static int NumeroJourDeLaSemaine(DateTime dt)
      {
         int d = (int) dt.DayOfWeek;
         if (d == 0)
            d = 7; // Dimanche
         return d;
      }

      /// <summary>
      /// Numero de jour de l'année d'une date donnée
      /// </summary>
      /// <param name="dt">Date </param>
      /// <returns></returns>
      public static int NumeroJourDeAnnee(DateTime dt)
      {
         return dt.DayOfYear;
      }


   }
}

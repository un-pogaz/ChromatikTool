using System;


namespace Chromatik.ToolBox.Env
{
    //    https://codes-sources.commentcamarche.net/source/48564-calcul-dates-jours-feries

    /// <summary>
    /// Calcul de la liste des jours feriées d'une année donnée.
    /// </summary>
    public static class Jours_Feries
    {
        public static DateTime[] J_Feries = new DateTime[13];
        private static int AN;

        public static void CalculJourFerie(int annee)
        {
            J_Feries[0] = DateTime.Parse("01/01/" + annee.ToString());
            J_Feries[1] = DateTime.Parse(J_Paques.Jour_Pacques(annee));
            J_Feries[2] = J_Feries[1].AddDays(1); // lundi de pacques
            J_Feries[3] = DateTime.Parse("01/05/" + annee.ToString());
            J_Feries[4] = DateTime.Parse("08/05/" + annee.ToString());
            J_Feries[5] = J_Feries[1].AddDays(39); // ascension
            J_Feries[6] = J_Feries[1].AddDays(49); // Pentecote
            J_Feries[7] = J_Feries[1].AddDays(50); // Lundi Pentecote
            J_Feries[8] = DateTime.Parse("14/07/" + annee.ToString());
            J_Feries[9] = DateTime.Parse("15/08/" + annee.ToString());
            J_Feries[10] = DateTime.Parse("01/11/" + annee.ToString());
            J_Feries[11] = DateTime.Parse("11/11/" + annee.ToString());
            J_Feries[12] = DateTime.Parse("25/12/" + annee.ToString());
        }

        internal static int Annee // Année 
        {
            set
            {
                AN = value;
                CalculJourFerie(AN);
            }
        }

        internal static string I_Janv // Année 
        {
            get { return J_Feries[0].ToLongDateString().Substring(0, J_Feries[0].ToLongDateString().Length - 4); }
        }

        internal static string Pacques
        {
            get { return J_Feries[1].ToLongDateString().Substring(0, J_Feries[1].ToLongDateString().Length - 4); }
        }

        internal static string I_Mai // Année 
        {
            get { return J_Feries[3].ToLongDateString().Substring(0, J_Feries[3].ToLongDateString().Length - 4); }
        }

        internal static string Huit_Mai
        {
            get { return J_Feries[4].ToLongDateString().Substring(0, J_Feries[4].ToLongDateString().Length - 4); }
        }

        internal static string Quatorze_Juillet
        {
            get { return J_Feries[8].ToLongDateString().Substring(0, J_Feries[8].ToLongDateString().Length - 4); }
        }

        internal static string Assomption // 15 aout
        {
            get { return J_Feries[9].ToLongDateString().Substring(0, J_Feries[9].ToLongDateString().Length - 4); }
        }

        internal static string Ascenscion
        {
            get { return J_Feries[5].ToLongDateString().Substring(0, J_Feries[5].ToLongDateString().Length - 4); }
        }

        internal static string Pentecote
        {
            get { return J_Feries[6].ToLongDateString().Substring(0, J_Feries[6].ToLongDateString().Length - 4); }
        }

        internal static string Toussaint
        {
            get { return J_Feries[10].ToLongDateString().Substring(0, J_Feries[10].ToLongDateString().Length - 4); }
        }

        internal static string Onze_Novembre
        {
            get { return J_Feries[11].ToLongDateString().Substring(0, J_Feries[11].ToLongDateString().Length - 4); }
        }

        internal static string Noel
        {
            get { return J_Feries[12].ToLongDateString().Substring(0, J_Feries[12].ToLongDateString().Length - 4); }
        }

        internal static string Careme
        {
            get
            {
                DateTime D = J_Feries[1].AddDays(-42);
                return D.ToLongDateString().Substring(0, D.ToLongDateString().Length - 4);
            }
        }

        internal static string Mardi_Gras
        {
            get
            {
                DateTime D = J_Feries[1].AddDays(-47);
                return D.ToLongDateString().Substring(0, D.ToLongDateString().Length - 4);
            }
        }

        internal static string Cendres
        {
            get
            {
                DateTime D = J_Feries[1].AddDays(-46);
                return D.ToLongDateString().Substring(0, D.ToLongDateString().Length - 4);
            }
        }

        internal static string Rameaux
        {
            get
            {
                DateTime D = J_Feries[1].AddDays(-7);
                return D.ToLongDateString().Substring(0, D.ToLongDateString().Length - 4);
            }
        }

        internal static string Fete_Dieu
        {
            get
            {
                DateTime D = J_Feries[1].AddDays(+63);
                return D.ToLongDateString().Substring(0, D.ToLongDateString().Length - 4);
            }
        }

        internal static string Vendredi_Saint
        {
            get
            {
                DateTime D = J_Feries[1].AddDays(-2);
                return D.ToLongDateString().Substring(0, D.ToLongDateString().Length - 4);
            }
        }


        internal static class J_Paques
        {

            internal static string Jour_Pacques(int Annee)
            {
                int a = Annee % 19;
                int b = Annee / 100;
                int c = Annee % 100;
                int d = (19 * a + b - (b / 4) - ((b - ((b + 8) / 25) + 1) / 3) + 15) % 30;
                int e = (32 + 2 * ((b % 4) + (c / 4)) - d - (c % 4)) % 7;
                int f = (d + e - 7 * ((a + 11 * d + 22 * e) / 451) + 114);
                int g = (f % 31) + 1;
                // retourne la date en string 
                return (g.ToString().PadLeft(2, '0') + "/" + (f / 31).ToString().PadLeft(2, '0') + "/" + Annee.ToString());
            }
        }


    }

    /// <summary>
    /// Calcul du nombre de jours, nombre de jours ouvrés,... sur une période donnée.
    /// </summary>
    public static class Jours_Ouvrables_Ouvres
    {
        private static DateTime Date_D;
        private static DateTime Date_F;
        private static int Total_J;
        private static int Ouvrables;
        private static int Ouvre;
        private static int annee;

        public static DateTime Date_Debut
        {
            set
            {
                Date_D = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
            }
        }

        public static DateTime Date_Fin
        {
            set
            {
                Date_F = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
            }
        }

        public static int Total_Jours
        {
            get { return Total_J; }
        }

        public static int Jours_Ouvrables
        {
            get { return Ouvrables; }
        }

        public static int Jours_Ouvres
        {
            get { return Ouvre; }
        }


        public static void CalculJoursOuvrablesOuvres()
        {
            if (Date_D.Date <= Date_F.Date)
            {
                DateTime Date_Inter;
                Ouvrables = 0;
                Ouvre = 0;
                Date_Inter = Date_D;
                TimeSpan ts = Date_F - Date_D;
                Total_J = ts.Days;
                Jours_Feries.Annee = annee;
                while (Date_Inter < Date_F)
                {
                    if (annee != Date_Inter.Year) // changement d'année
                    {
                        annee = Date_Inter.Year;
                        Jours_Feries.Annee = annee;
                    }

                    if ((int)Date_Inter.Date.DayOfWeek != 0)
                        Ouvrables++;

                    if ((int)Date_Inter.Date.DayOfWeek != 0 & (int)Date_Inter.Date.DayOfWeek != 6)
                    {
                        foreach (DateTime DT in Jours_Feries.J_Feries)
                        {
                            if (DT.Date == Date_Inter.Date)
                            {
                                Ouvre--;
                                break;
                            }
                        }
                        Ouvre++;
                    }
                    Date_Inter = Date_Inter.AddDays(1);
                }
            }
        }
    }
}

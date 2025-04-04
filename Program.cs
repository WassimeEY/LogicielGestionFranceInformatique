using FranceInformatiqueInventaire.Controlleur;

namespace FranceInformatiqueInventaire
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            FormGestion frmGestionRef = new FormGestion();
            Application.Run(frmGestionRef);
        }
    }
}
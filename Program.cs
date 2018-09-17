using GuessingGameReproduction.Core;
using GuessingGameReproduction.GUI;
using System;
using System.Windows.Forms;

namespace GuessingGameReproduction
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var game = new Game(new DialogService());
            game.Start();
        }
    }
}

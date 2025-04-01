using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentRegistrationProgramWithDBConnection.Utilities
{
    internal static class MenuHelper
    {
        public static string LoginMenuTitle = "Logga in";
        public static string MainMenuTitle = "Huvudmeny";
        public static string MainMenuOptionRegister = "Registrera ny student";
        public static string MainMenuOptionEditOne = "Ändra student";
        public static string MainMenuOptionListAll = "Lista alla studenter";
        public static string MainMenuOptionQuit = "Avsluta programmet";
        public static string MainMenuPrompt = "Ditt val:";
        public static string RegisterMenuTitle = "Registrera ny student";
        public static string EditMenuTitle = "Ändra student";
        public static string EditMenuPromptStudentId = "Student att ändra (ange student id-nummer):";
        public static string ListAllMenuTitle = "Lista alla studenter";
        public static string QuitMenuTitle = "Avsluta programmet";
        public static string PromptUsername = "Användarnamn:";
        public static string PromptPassword = "Lösenord:";
        public static string PromptFirstName = "Förnamn:";
        public static string PromptLastName = "Efternamn:";
        public static string PromptCity = "Stad:";
        public static string SuccessStudentRegistered = "Ny student registerad.";
        public static string SuccessStudentEdited = "Student uppdaterad.";
        public static string SuccessGoodbye = "Tack och hej då!";
        public static string WarningInvalidUsernameOrPassword = "Lyckades inte hitta användare med detta användarnamn och lösenord.";
        public static string WarningStudentIdNotFound = "Lyckades inte hitta student med detta id-nummer.";
        public static string WarningUnexpectedInput = "Oväntad inmatning. Försök igen.";
        public static string WarningStudentIsNull = "Student är null.";
    }
}

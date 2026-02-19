using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Buisness;
using Microsoft.Win32;
using System.Security.Cryptography;

namespace DVLD.Classes
{
    internal static  class clsGlobal
    {
        public static clsUser CurrentUser;

        public static bool RememberUsernameAndPassword(string Username, string Password)
        {

            string path = @"HKEY_CURRENT_USER\Software\DVLD\LoginInfo";

            try
            {
                Registry.SetValue(path, "UserName" , Username , RegistryValueKind.String);
                Registry.SetValue(path, "Password" , Password , RegistryValueKind.String);
                return true;    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
                return false;
            }

        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string path = @"HKEY_CURRENT_USER\Software\DVLD\LoginInfo";
            try
            {
               Username =   Registry.GetValue(path, "UserName", null) as string;
               Password = Registry.GetValue(path, "Password",null ) as string;

                if (Username != null && Password != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ($"An error occurred: {ex.Message}");
                return false;   
            }

        }

        public static string HashingPassword(string password)
        {
            using (SHA256 sha  = SHA256 .Create())
            {
                byte[] output = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(output).Replace("-","").ToLower();
            }
        }
    }
}

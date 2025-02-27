using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TravelRecSW
{
    internal class shareInfo
    {
        public static void showWarningMSG(string msg)
        {
           MessageBox.Show(msg, "คำเตือน", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public static string connStr = "Server =AdisornMew\\SQLEXPRESS; Database=travel_db;Trusted_connection=true";

        public static int travellerId;
        public static string travellerFullname;
        public static string travellerEmail;
        public static string travellerPassword;
        public static byte[] travellerImage;
    }
}

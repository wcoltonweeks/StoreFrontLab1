using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoreFrontLab1.UI.MVC.Utilities
{
    public class CustomMethods
    {
        public static string GenerateFileName()
        {
            string fileName = "";

            List<int> nbrForName = new List<int>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = nbrForName[0]; i < nbrForName.Count(); i++)
            {
                int randomNbr = new Random().Next(10);
                System.Threading.Thread.Sleep(30);
                i = randomNbr;
            }

            return fileName;
        }
    }
}


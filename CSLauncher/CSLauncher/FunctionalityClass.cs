using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace WindowsFormsApplication3
{
    class FunctionalityClass
    {
        private StreamWriter serversDBWriter;
        private StreamReader serversDBReader;
        private String server;

        public Boolean isIPValid(String IPValidation)
        {
            Boolean hasThreeDots = false;
            Boolean isLegitSymbol = false;
            Boolean hasColon = false;
            Boolean isCorrectSize = false;
            Boolean isValid = false;
            int dotCounter = 0;
            for (int i = 0; i < IPValidation.Length; i++)
            {
                if ((IPValidation[i] > 45 && IPValidation[i] < 47) || (IPValidation[i] > 47 && IPValidation[i] < 58))
                    isLegitSymbol = true;
                else
                    isLegitSymbol = false;

                if (IPValidation[i] == '.')
                    dotCounter++;

            }

            if (IPValidation.Length > 13 && IPValidation.Length < 22)
                isCorrectSize = true;
            else
                isCorrectSize = false;

            if (IPValidation.Contains(":"))
                hasColon = true;
            else
                hasColon = false;

            if (dotCounter == 3)
                hasThreeDots = true;
            else
                hasThreeDots = false;

            if (hasThreeDots && isLegitSymbol && hasColon && isCorrectSize)
                isValid = true;

            return isValid;
        }

        public List<String> LoadDatabase()
        {
            List<String> ipList = new List<String>();
            try
            {
                serversDBReader = new StreamReader("servers");
                while (serversDBReader.Peek() >= 0)
                {
                    server = serversDBReader.ReadLine();
                    ipList.Add(server);
                } 
                serversDBReader.Close();
                return ipList;
            }
            catch (IOException)
            {
                FileStream fs = File.Create("servers");
                fs.Close();
                return ipList;
            }
            catch (ArgumentNullException)
            {
                MessageBox.Show("The file with the servers' IP is empty!");
                serversDBReader.Close();
                return ipList;
            }
        }

        public void AddServerToTheFile(String currentServer)
        {
            serversDBWriter = File.AppendText("servers");
            serversDBWriter.WriteLine(currentServer);
            serversDBWriter.Close();
        }

    }
}

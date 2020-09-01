using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;

namespace WebApplication2
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public List<String> readBooks(String file)
        {
            List<String> books = new List<String>();
            String line;

            StreamReader sr = new StreamReader(file);
            line = sr.ReadLine();

            while (line != null)
            {
                books.Add(line);
                line = sr.ReadLine();

            }
            sr.Close();
            return books;
        }

        [WebMethod]
        public void writeBooks(String line, String file)
        {
                System.IO.StreamWriter writer = new System.IO.StreamWriter(file, true);
                writer.WriteLine(line);
                writer.Close();
                writer.Dispose();
         
        }
        [WebMethod]
        public void deleteBooks(int bookID, String file)
        {
            string tempFile = Path.GetTempFileName();
            string lineVar = null;

            using (var sr = new StreamReader(file))
            using (var sw = new StreamWriter(tempFile))
            {

                string line;
                line = sr.ReadLine();
                while (line != null)
                {
                    String[] tokens = line.Split(',');
                    int id = Convert.ToInt32(tokens[0]);

                    if (id == bookID)
                        sw.WriteLine(lineVar);
                }
            }

            File.Delete(file);
            File.Move(tempFile, file);
        }
        [WebMethod]
        public Boolean isExist(int bookID, String file)
        {
            String line;

            StreamReader sr = new StreamReader(file);
            line = sr.ReadLine();
            while (line != null)
            {
                String[] tokens = line.Split(',');
                int id = Convert.ToInt32(tokens[0]);
                if (id == bookID)
                {
                    return true;
                }
                line = sr.ReadLine();

            }
            //close the file
            sr.Close();
            sr.Dispose();

            return false;

        }
    }
}

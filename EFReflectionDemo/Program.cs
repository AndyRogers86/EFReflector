using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EFReflectionDemo.Model;

namespace EFReflectionDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            string outputFormat = "ID:{0}, Name:{1}, DOB:{2}";

            using (ReflectDemoEntities ent = new Model.ReflectDemoEntities())
            {
                foreach (T_Person p in ent.T_Person)
                {
                    Console.WriteLine(string.Format(outputFormat, p.ID, p.Name, p.DOB.ToShortDateString()));
                }
            }

            DataTable dt = EFReflection.ReflectData.GetData("EFReflectionDemo.Model.ReflectDemoEntities", "T_Person", new List<string>() { "ID", "Name", "DOB" });

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(string.Format(outputFormat, dr["ID"], dr["Name"], dr["DOB"]));
            }
        }
    }
}

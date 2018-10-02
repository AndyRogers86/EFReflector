using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EFReflection
{
    public static class ReflectData
    {
        public static DataTable GetData(string entitiesNamespace, string dataEntity, IEnumerable<string> columns)
        {
            DataTable returnDT = new DataTable();

            Assembly assembly = Assembly.GetCallingAssembly();
            Type entType = assembly.GetType(entitiesNamespace);

            object ent = Activator.CreateInstance(entType);

            PropertyInfo tablePropInfo = entType.GetProperty(dataEntity);

            dynamic dataEntityValue = tablePropInfo.GetValue(ent, null);

            IEnumerable<dynamic> dataEntityValues = ((IEnumerable)dataEntityValue).Cast<dynamic>();

            foreach (string col in columns)
            {
                returnDT.Columns.Add(col);
            }

            foreach(dynamic row in dataEntityValues)
            {
                DataRow dr = returnDT.NewRow();

                foreach (string col in columns)
                {
                    dr[col] = GetDynamicMember(row, col);
                }

                returnDT.Rows.Add(dr);
            }

            return returnDT;
        }

        private static string GetDynamicMember(object obj, string memberName)
        {
            var binder = Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None,
                                                                         memberName,
                                                                         obj.GetType(),
                                                                         new[] {
                                                                             CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
                                                                         });

            var callsite = CallSite<Func<CallSite, object, object>>.Create(binder);

            return callsite.Target(callsite, obj).ToString();
        }
    }
}

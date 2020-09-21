using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using Library;
using Newtonsoft.Json;

namespace MyOwnJsonConverter
{
    class ReflectionWorker
    {
        public void Start()
        {
            Object Clerk1 = new Clerk("Clerk1", 2000, new List<string>() {"Madlavning", "Fægtning", "Svømme"});
            Manager m1 = new Manager("Manager1", 1990);
            Clerk c1 = new Clerk("Anders", 1997, new List<string>() { "Programmering", "Gaming", "Hockey", "Fitness" });
            Clerk c2 = new Clerk("Jacob", 1997, new List<string>() { "Programmering", "Oprydning", "Sove"});
            m1.Employees.Add(c1);
            m1.Employees.Add(c2);
            object Manager1 = m1;

            TryReflection(Clerk1);
            //TryReflection(Manager1);

            //MethodInfo mInfo = Clerk1.GetType().GetMethod("set_Name");
            //Console.WriteLine(mInfo.Invoke(Clerk1, new object?[]{"Anders"}));
            //Console.WriteLine(((Person)Clerk1).Name);

            Console.WriteLine(JsonConvert.SerializeObject(Manager1));
            Console.WriteLine(Serialize(Manager1));
        }

        public void TryReflection(object obj)
        {
            Console.WriteLine("");
            Console.WriteLine(obj);
            Console.WriteLine("------------------------------");
            Type t = obj.GetType();

            //Name of object
            Console.WriteLine($"Name of object: {t.Name}");
            //Type of object (class, abstract, interface)
            Console.WriteLine($"Object type: Abstract = {(t.IsAbstract ? "True" : "False")}, Interface = {(t.IsInterface ? "True" : "False")}, Class = {(t.IsClass ? "True" : "False")}");
            //Properties
            Console.WriteLine("Properties");
            foreach (PropertyInfo p in t.GetProperties())
            {
                Console.WriteLine(p.Name);
            }
            //methods
            Console.WriteLine("Methods:");
            foreach (MethodInfo m in t.GetMethods())
            {
                Console.WriteLine(m.Name);
            }
        }

        public String Serialize<T>(T obj)
        {
            StringBuilder s = new StringBuilder();
            Type t = obj.GetType();
            s.Append("{");

            foreach (PropertyInfo p in t.GetProperties())
            {
                //Console.WriteLine(p.PropertyType);
                if (p.PropertyType == typeof(String))
                {
                    s.Append($"\"{p.Name}\":\"{p.GetValue(obj, null)}\"");
                }
                else if (p.PropertyType == typeof(int))
                {
                    s.Append($"\"{p.Name}\":{p.GetValue(obj, null)}");
                }
                else if (p.PropertyType.GetInterfaces().Contains(typeof(IEnumerable)))
                {
                    s.Append($"\"{p.Name}\":");
                    s.Append("[");
                    foreach (object v in ((IEnumerable)p.GetValue(obj)))
                    {
                        //Nu er vi inde i den IEnumarable (liste, etc) som vores object har
                        Type t1 = v.GetType();
                        if (t1 == typeof(String))
                        {
                            s.Append($"\"{v}\"");
                        }
                        else if (t1 == typeof(int))
                        {
                            s.Append($"{v}");
                        }
                        else
                        {
                            s.Append(Serialize(v));
                        }

                        s.Append(",");

                    }
                    s.Remove(s.Length - 1, 1);
                    s.Append("]");
                }

                s.Append(",");
            }

            s.Remove(s.Length-1, 1);
            s.Append("}");
            return s.ToString();
        }
    }
}

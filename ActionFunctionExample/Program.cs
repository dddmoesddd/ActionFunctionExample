using ActionFunctionExample;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;

namespace ActionFunctionExample
{
    public static class CachManager
    {
        public static void CachInsert(this HttpContext httpcontext, string key, object data, int expierTime)
        {

            var d = 90;
var ss=670;
            if (data == null)
                return;
            httpcontext.Cache.Insert(key,
                data,
                null,
                DateTime.Now.AddMinutes(expierTime),
                TimeSpan.Zero,
                CacheItemPriority.AboveNormal,
                null
                );

        }



        public static T CacheRead<T>(this HttpContext httpcontext, string key, object data, int expierTime, Func<T> ifNullRetrivalMethod)
        {

            var item = httpcontext.Cache[key];
            if (item == null)
            {
                item = ifNullRetrivalMethod();
                if (item == null)
                    return default(T);
                CachInsert(httpcontext, key, data, expierTime);

            }

            return (T)item;

        }
    }

    class Program
    {

        public delegate int AddMethodDelegate(int a);
        public class DelagateSample
        {
            public void UseDeleGate(AddMethodDelegate addMethod)
            {


                Console.WriteLine(addMethod(5));
                Console.ReadKey();

            }
        }

        public class Helper
        {


            public int CustomAdd(int a)
            {

                return a++;

            }


            public void Run(Action<string> fun)
            {
                fun("hi");

            }

            public void printName(string name)
            {

                Console.WriteLine(name);
                Console.ReadKey();

            }


        }


        public class UserManagement
        {


            List<Int16> user = new List<Int16>() { 1, 2, 3, 4 };

            public Int16 FindUser(int id)
            {

                return user.Where(x => x == id).FirstOrDefault();



            }
        }




        static void Main(string[] args)
        {
            //net 1
                var helper = new Helper();
            var deleMwthod = new AddMethodDelegate(helper.CustomAdd);
            new DelagateSample().UseDeleGate(deleMwthod);
           // net 2
               new DelagateSample().UseDeleGate(delegate (int a) { return helper.CustomAdd(a); });
           // net 3
             new DelagateSample().UseDeleGate(a => helper.CustomAdd(a));

            Action<int> exam = x =>
            {
                Console.WriteLine(x); Console.ReadKey();
            };

            exam(6);


            Func<string, string> example = x => { return x + "good"; };
            Console.WriteLine(example("boy"));
            Console.ReadKey();

            helper.Run(x => { Console.WriteLine(x); Console.ReadKey(); });
            //Func<object> exm=null;
            //var user = HttpContext.Current.CacheRead<Func<Object>> ("k1", 2,()=> new UserManagement().FindUser(2));

        }
    }
}

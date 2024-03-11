using EF_Demo;
using System;
using System.Linq;

namespace DBFirstApproach
{
    class Program
    {
        static void Main(string[] args)
        {
            using (EF_Demo_DBEntities DBEntities = new EF_Demo_DBEntities())
            {
                DBEntities.Database.Log = Console.Write;

                // 1 query: fetch all items from Item table
                foreach (var standard in DBEntities.Standards.Include("Students").ToList())
                {
                    // 1 query again for each item to fetch invoice because of lazy loading
                    foreach (var student in standard.Students)
                    {
                        Console.WriteLine(student.FirstName);
                    }
                }
            }
        }
    }
}
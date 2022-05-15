using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OW21BB;

namespace TESTER
{
    [TestFixture]
    public class TesterClass
    {
        static Random rnd;

        static TesterClass()
        {
            rnd = new Random();
        }


        List<BuildingProject<Company>> buildingProjects;



        void ListingDisplay(Company item, int db)
        {
            Console.WriteLine(item.ToString());
        }

        void BBListDisplay(Company item, int number, int space)
        {
            Console.WriteLine($"[{number}] {item} --> Offices allocated: {item.SpaceNeeded} --> Money paid: {item.AmountPayed}\n\tThere are {space} offices left in the project to allocate.\n");
        }

        [SetUp]
        public void Init()
        {
            buildingProjects = new List<BuildingProject<Company>>();
            for (int i = 0; i < 3; i++)
            {
                buildingProjects.Add(new BuildingProject<Company>($"TesterProjectNO.{i + 1}", 100));
            }



            buildingProjects[0].Upload(new GovernmentCompany("Jani vagyok Bt.", 5, 100000));
            buildingProjects[0].Upload(new PrivateCompany("b", 25, 25000));
            buildingProjects[0].Upload(new PrivateCompany("c", 8, 300000));
            buildingProjects[0].Upload(new PrivatePerson("d", 5000));
            buildingProjects[0].Upload(new PrivatePerson("e", 100000));
            buildingProjects[0].Upload(new PrivateCompany("f", 10, 10000));
        }

        [TestCase]
        public void UploadCustomerTest()
        {
            buildingProjects[0].Upload(new GovernmentCompany("a", 10, 10000));
        }

        [TestCase]
        public void ListCustomerTester()
        {
            buildingProjects[0].List(ListingDisplay);
        }

        [TestCase]
        public void DeleteCustomerTest()
        {
            buildingProjects[0].Delete("c");
        }

        [TestCase]
        public void AllocationTest()
        {
            buildingProjects[0].BranchandBound(BBListDisplay, buildingProjects[0].Companies());
        }

        [TestCase]
        public void AddProject()
        {
            buildingProjects.Add(new BuildingProject<Company>("AddTestProject", 100));
        }

        [TestCase]
        public void DeleteProject()
        {
            buildingProjects.RemoveAt(2);
        }

        [TestCase]
        public void ListProject()
        {
            buildingProjects.ForEach(x => Console.WriteLine(x.Name));
        }
    }
}

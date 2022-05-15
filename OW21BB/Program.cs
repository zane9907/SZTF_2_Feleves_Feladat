using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OW21BB
{
    public class Program
    {
        static void ListingDisplay(Company item, int number)
        {
            Console.WriteLine($"[{number}] {item}");
        }

        static void BBListDisplay(Company item, int number, int space)
        {
            Console.WriteLine($"[{number}] {item} --> Offices allocated: {item.SpaceNeeded} --> Money paid: {item.AmountPayed}\n\tThere are {space} offices left in the project to allocate.\n");
        }

        static void Main(string[] args)
        {
            #region NOTES

            /* Classes:
             * 
             * BuildingProject:
             * - 1 db irodaház építésére vonatkozik
             * - Has name
             * - minden irodaháznak van max mérete (hány irodát lehet benne építeni)
             * - Ezen belül kell a bináris keresőfa, amiben a cégek szerepelnek név szerint rendezve
             * 
             * - Functions:
             *      - Megrendelő felvétel/törlés
             *      - Listázni a megrendelőket
             *      - hány megrendelő van
             *      - hány férőhely van még
             *      - Irodák kiosztása kapacitás szerint a lehető legjobban, másodlagosan ajánlott ár szerint
             *      - ha egy cég megkapha az irodáit akkor annyit levonna az elérhető irodákból
             * 
             * - Exception Handling:
             *      - Nincs BuildingProject létrehozva/rendelő felvéve 
             *      - azonos nevű céget akarunk bevinni a rendszerbe
             *      - ha megtelt az épület ( ilyenkor új project létrehozása )
             * 
             * - Event handling:
             *      - Létrehoztunk/töröltünk egy Building projectet
             *      - Felvettünk vagy töröltünk megrendelőket
             *      - cég előrendelése sikerült/(el lett utasítva)
             *
             *
------------------------------------------------------------------------------------------------------------------------------------------
             * 
             * Different Companies:
             * - Names: Magáncég, ÁllamiCég, Magánszemély, Cég,
             * - Megvalósítja az ICustomer Interface-t
             * 
             * ICustomer interface:
             *      - hány férőhelyes iroda kell
             *      - mennyit fizetne érte
             * 
             *- Tetszőleges implementálás pl. magáncég többet fizet, mint egy állami és csak 1 személyes iroda kell neki
             * 
             */

            #endregion
            Random rnd = new Random();

            

            List<BuildingProject<Company>> buildingProjects = new List<BuildingProject<Company>>();

            int actualProjectCounter = 0;
            int menuChoice = 0;
            int y = 1;


            while (menuChoice != 9)
            {
                Console.Clear();
                Console.ResetColor();


                //Starter Project Creation
                if (buildingProjects.Count == 0)
                {
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("There are no projects in the database!\nPlease create a new project!");
                        Console.WriteLine();

                        Console.WriteLine("How would you like to create a project?\n1. Random generated project\t2. My choice");
                        int choice = int.Parse(Console.ReadLine()); Console.WriteLine();
                        if (choice == 1)
                        {
                            buildingProjects.Add(new BuildingProject<Company>(NameGenerator(), rnd.Next(50, 201)));
                        }
                        else if (choice == 2)
                        {
                            Console.Write("Please type in the name: ");
                            string name = Console.ReadLine(); Console.WriteLine();

                            Console.WriteLine("Please type in the capacity of the project: ");
                            int size = int.Parse(Console.ReadLine());

                            buildingProjects.Add(new BuildingProject<Company>(name, size));
                        }
                        else
                        {
                            throw new WrongChoiceException();
                        }
                    }
                    catch (WrongChoiceException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press ENTER to continue!");
                        Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Input was not in a correct format!");
                        Console.WriteLine("Press ENTER to continue!");
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press ENTER to continue!");
                        Console.ReadLine();
                    }

                }
                else
                {
                    Console.Clear();


                    Console.WriteLine(buildingProjects[actualProjectCounter].Name.ToUpper());
                    Console.WriteLine("===================================");
                    Console.WriteLine($"Available space: {buildingProjects[actualProjectCounter].RemainingSpace}");
                    Console.WriteLine("===================================");

                    MenuDisplay(); Console.WriteLine();

                    Console.Write("Choose one option: ");
                    menuChoice = int.Parse(Console.ReadLine());

                    switch (menuChoice)
                    {
                        //New project
                        case (1):
                            {
                                try
                                {
                                    Console.Clear();
                                    Console.WriteLine("How would you like to create a project? (Type -1 to cancel)\n1. Random generated project\t2. My choice");
                                    int choice = int.Parse(Console.ReadLine()); Console.WriteLine();
                                    if (choice != -1)
                                    {
                                        if (choice == 1)
                                        {
                                            buildingProjects.Add(new BuildingProject<Company>(NameGenerator(), rnd.Next(50, 201)));
                                        }
                                        else if (choice == 2)
                                        {
                                            Console.Write("Please type in the name: ");
                                            string name = Console.ReadLine(); Console.WriteLine();

                                            Console.WriteLine("Please type in the capacity of the project: ");
                                            int size = int.Parse(Console.ReadLine());

                                            buildingProjects.Add(new BuildingProject<Company>(name, size));
                                        }
                                        else
                                        {
                                            throw new WrongChoiceException();
                                        }
                                    }
                                }
                                catch (WrongChoiceException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input was not in a correct format!");
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                break;
                            }

                        //Delete Project
                        case (2):
                            {
                                try
                                {
                                    Console.Clear();
                                    Console.WriteLine("=========================================");


                                    if (buildingProjects.Count != 0)
                                    {
                                        buildingProjects.ForEach(x => Console.WriteLine($"[{y++}] {x.Name}"));
                                    }
                                    else
                                    {
                                        Console.WriteLine("There aren't any projects in this list!");
                                    }


                                    Console.WriteLine("========================================="); Console.WriteLine();
                                    Console.WriteLine("Which project do you want to delete? Please type in the index!\n(Type -1 to cancel)");
                                    Console.Write("Index: ");

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    int index = int.Parse(Console.ReadLine());

                                    if (index > buildingProjects.Count)
                                    {
                                        throw new WrongChoiceException();
                                    }
                                    else if (index != -1)
                                    {
                                        buildingProjects.RemoveAt(index - 1);
                                    }

                                    Console.ResetColor();
                                }
                                catch (WrongChoiceException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input was not in a correct format!");
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                break;
                            }

                        //List projects
                        case (3):
                            {
                                Console.Clear();
                                y = 1;
                                Console.WriteLine("=========================================");
                                if (buildingProjects.Count != 0)
                                {
                                    buildingProjects.ForEach(x => Console.WriteLine($"[{y++}] {x.Name}"));
                                }
                                else
                                {
                                    Console.WriteLine("There aren't any projects in this list!");
                                }
                                Console.WriteLine("=========================================");

                                Console.WriteLine("Press ENTER to continue!");
                                Console.ReadLine();
                                break;
                            }

                        //New Customer
                        case (4):
                            {
                                try
                                {
                                    Console.Clear();
                                    if (buildingProjects[actualProjectCounter].RemainingSpace > 0)
                                    {
                                        Console.WriteLine("How would you like to add a cutomer? (Type -1 to cancel)\n1. Manual\t2. Read from .txt file");
                                        int choice = int.Parse(Console.ReadLine()); Console.WriteLine();

                                        if (choice != -1)
                                        {
                                            if (choice == 1)
                                            {
                                                RegisterCustomer(buildingProjects[actualProjectCounter]);
                                            }
                                            else if (choice == 2)
                                            {
                                                ReadFromTXt(buildingProjects[actualProjectCounter]);
                                            }
                                            else
                                            {
                                                throw new WrongChoiceException();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("The project is full! You can't add more customers!"); Console.WriteLine();
                                        Console.WriteLine("Press ENTER to continue!");
                                        Console.ReadLine();
                                    }
                                }
                                catch (WrongChoiceException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input was not in a correct format!");
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (ArgumentNullException)
                                {
                                    Console.WriteLine("The given .txt file was empty!");
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                break;
                            }

                        //Delete Customer
                        case (5):
                            {
                                Console.Clear();

                                try
                                {
                                    List<string> keys = buildingProjects[actualProjectCounter].Keys();
                                    y = 1;
                                    Console.WriteLine("The list of all the customer companies:");
                                    Console.WriteLine("=========================================");

                                    keys.ForEach(x => Console.WriteLine($"[{y++}] {x}"));

                                    Console.WriteLine("=========================================");


                                    Console.WriteLine();
                                    Console.WriteLine("Which customer do you want to delete? Please type in the index!\n(Type -1 to cancel)");
                                    Console.Write("Index: ");

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    int index = int.Parse(Console.ReadLine());

                                    if (index > keys.Count)
                                    {
                                        throw new WrongChoiceException();
                                    }
                                    else if (index != -1)
                                    {
                                        DeleteCustomer(buildingProjects[actualProjectCounter], keys[index - 1]);
                                    }

                                    Console.ResetColor();
                                }
                                catch (WrongChoiceException e)
                                {
                                    Console.WriteLine(e.Message); Console.WriteLine();
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("Input was not in a correct format!"); Console.WriteLine();
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message); Console.WriteLine();
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }

                                break;
                            }

                        //List Customers
                        case (6):
                            {
                                try
                                {
                                    ListCustomer(buildingProjects[actualProjectCounter]);
                                }
                                catch (EmptyArrayOrListException e)
                                {
                                    Console.WriteLine(e.Message); Console.WriteLine();
                                }
                                Console.WriteLine("Press ENTER to continue!");
                                Console.ReadLine();
                                break;
                            }

                        //Change Building Project
                        case (7):
                            {
                                try
                                {
                                    int memory = actualProjectCounter;
                                    Console.Clear(); y = 1;
                                    Console.WriteLine("=========================================");
                                    if (buildingProjects.Count != 0)
                                    {
                                        for (int i = 0; i < buildingProjects.Count; i++)
                                        {
                                            if (buildingProjects[i].RemainingSpace == 0)
                                            {
                                                Console.WriteLine($"[{y++}] {buildingProjects[i].Name}\t-> This Project is Full");
                                            }
                                            else
                                            {
                                                Console.WriteLine($"[{y++}] {buildingProjects[i].Name}");
                                            }
                                        }
                                        //buildingProjects.ForEach(x => Console.WriteLine($"[{y++}] {x.Name}"));
                                    }
                                    else
                                    {
                                        Console.WriteLine("There aren't any projects in this list!");
                                    }
                                    Console.WriteLine("=========================================");

                                    Console.WriteLine();
                                    Console.WriteLine("Wich project do you want to work on? (Type in the index)");
                                    Console.Write("Index: ");

                                    Console.ForegroundColor = ConsoleColor.Red;
                                    int index = int.Parse(Console.ReadLine());
                                    Console.ResetColor();

                                    if (index > buildingProjects.Count)
                                    {
                                        throw new WrongChoiceException();
                                    }
                                    else if (index != -1)
                                    {
                                        actualProjectCounter = index - 1;
                                    }


                                    //if (buildingProjects[actualProjectCounter].Size <= 0)
                                    //{
                                    //    Console.WriteLine("You can't choose this project because it's full!");

                                    //    Console.WriteLine("Press ENTER to continue!");

                                    //    Console.ReadLine();
                                    //    actualProjectCounter = memory;
                                    //}
                                }
                                catch (WrongChoiceException e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                    Console.WriteLine("Press ENTER to continue!");
                                    Console.ReadLine();
                                }

                                break;
                            }

                        //Allocation of offices
                        case (8):
                            {
                                Console.Clear();
                                if (buildingProjects[actualProjectCounter].CustomerCount != 0 && buildingProjects[actualProjectCounter].RemainingSpace > 0)
                                {

                                    List<Company> companies = buildingProjects[actualProjectCounter].Companies();
                                    Console.WriteLine("===================================");
                                    Console.WriteLine("Allocated offices:");
                                    Console.WriteLine("===================================");
                                    buildingProjects[actualProjectCounter].BranchandBound(BBListDisplay, companies);
                                    Console.WriteLine("===================================");


                                    Console.WriteLine(); Console.WriteLine();
                                    Console.WriteLine("Press ENTER to continue!");

                                    Console.ReadLine();
                                }
                                else
                                {
                                    //Project capacity full
                                    if (buildingProjects[actualProjectCounter].RemainingSpace <= 0)
                                    {
                                        Console.Clear();
                                        try
                                        {
                                            if (buildingProjects.Count == 1)
                                            {
                                                #region New Project
                                                Console.WriteLine("The project's capacity is full!\nPlease create a new project!\n!AUTOMATICALLY CHANGING TO NEW PROJECT!");

                                                Console.WriteLine();

                                                Console.WriteLine("How would you like to create a project?\n1. Random generated project\t2. My choice");
                                                int choice = int.Parse(Console.ReadLine()); Console.WriteLine();
                                                if (choice == 1)
                                                {
                                                    buildingProjects.Add(new BuildingProject<Company>(NameGenerator(), rnd.Next(50, 201)));
                                                }
                                                else if (choice == 2)
                                                {
                                                    Console.Write("Please type in the name: ");
                                                    string name = Console.ReadLine(); Console.WriteLine();

                                                    Console.WriteLine("Please type in the capacity of the project: ");
                                                    int size = int.Parse(Console.ReadLine());

                                                    buildingProjects.Add(new BuildingProject<Company>(name, size));
                                                }
                                                else
                                                {
                                                    throw new WrongChoiceException();
                                                }
                                                actualProjectCounter++;
                                                #endregion
                                            }
                                            else
                                            {
                                                #region Choose Another Project

                                                Console.WriteLine("The project's capacity is full!\nPlease switch to another Project!");

                                                int memory = actualProjectCounter;
                                                y = 1;
                                                Console.WriteLine("=========================================");
                                                if (buildingProjects.Count != 0)
                                                {
                                                    for (int i = 0; i < buildingProjects.Count; i++)
                                                    {
                                                        if (buildingProjects[i].RemainingSpace == 0)
                                                        {
                                                            Console.WriteLine($"[{y++}] {buildingProjects[i].Name}\t-> This Project is Full");
                                                        }
                                                        else
                                                        {
                                                            Console.WriteLine($"[{y++}] {buildingProjects[i].Name}");
                                                        }
                                                    }
                                                    //buildingProjects.ForEach(x => Console.WriteLine($"[{y++}] {x.Name}"));
                                                }
                                                else
                                                {
                                                    Console.WriteLine("There aren't any projects in this list!");
                                                }
                                                Console.WriteLine("=========================================");

                                                Console.WriteLine();
                                                Console.WriteLine("Wich project do you want to work on? (Type in the index)");
                                                Console.Write("Index: ");

                                                Console.ForegroundColor = ConsoleColor.Red;
                                                int index = int.Parse(Console.ReadLine());
                                                Console.ResetColor();

                                                if (index > buildingProjects.Count)
                                                {
                                                    throw new WrongChoiceException();
                                                }
                                                else if (index != -1)
                                                {
                                                    actualProjectCounter = index - 1;
                                                }

                                                #endregion
                                            }
                                        }
                                        catch (WrongChoiceException e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Press ENTER to continue!");
                                            Console.ReadLine();
                                        }
                                        catch (FormatException)
                                        {
                                            Console.WriteLine("Input was not in a correct format!");
                                            Console.WriteLine("Press ENTER to continue!");
                                            Console.ReadLine();
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine(e.Message);
                                            Console.WriteLine("Press ENTER to continue!");
                                            Console.ReadLine();
                                        }
                                    }
                                    else
                                    {

                                        Console.WriteLine("===================================");
                                        Console.WriteLine("The list is empty!\nPlease register customers!");
                                        Console.WriteLine("===================================");

                                        Console.WriteLine();
                                        Console.WriteLine("Press ENTER to continue!");

                                        Console.ReadLine();
                                    }
                                }



                                break;
                            }
                        //Exit
                        case (9): break;
                    }
                }
            }

            //Console.ReadLine();
        }

        static void DeleteCustomer(BuildingProject<Company> buildingProject, string key)
        {
            buildingProject.Delete(key);
        }

        static void ListCustomer(BuildingProject<Company> buildingProject)
        {
            Console.Clear();
            Console.WriteLine("The list of all the customer companies:\n=====================================");

            buildingProject.List(ListingDisplay);

            Console.WriteLine("=====================================");

        }

        static void RegisterCustomer(BuildingProject<Company> buildingProject)
        {
            Console.Clear();


            Console.WriteLine("Which type of company do you want to register?");
            Console.WriteLine("Options:\n1. Government Company\n2. Private Person\n3. Private Company");
            Console.WriteLine();

            int choice = int.Parse(Console.ReadLine());
            Console.Clear();
            if (choice == 1)
            {
                Console.WriteLine("What's the name of the company?");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.WriteLine("How many offices do you want?");
                int office = int.Parse(Console.ReadLine()); Console.WriteLine();

                Console.WriteLine("How much do you want to pay?");
                int money = int.Parse(Console.ReadLine());

                buildingProject.Upload(new GovernmentCompany(name, office, money));
            }
            else if (choice == 2)
            {
                Console.WriteLine("DISCLAIMER: Private Person Companies only get one office!"); Console.WriteLine();

                Console.WriteLine("What's the name of the company?");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.WriteLine("How much do you want to pay?");
                int money = int.Parse(Console.ReadLine()); Console.WriteLine();

                buildingProject.Upload(new PrivatePerson(name, money));
            }
            else if (choice == 3)
            {
                Console.WriteLine("What's the name of the company?");
                string name = Console.ReadLine(); Console.WriteLine();

                Console.WriteLine("How many offices do you want?");
                int office = int.Parse(Console.ReadLine()); Console.WriteLine();

                Console.WriteLine("How much do you want to pay?");
                int money = int.Parse(Console.ReadLine());

                buildingProject.Upload(new PrivateCompany(name, office, money));
            }
            else
            {
                throw new WrongChoiceException();
            }
        }

        static void MenuDisplay()
        {
            Console.WriteLine("1. Register new Building Project");
            Console.WriteLine("2. Delete Building Project");
            Console.WriteLine("3. List all Building Projects");

            Console.WriteLine();

            Console.WriteLine("4. Register new Customer");
            Console.WriteLine("5. Delete Customer");
            Console.WriteLine("6. List all Customers");

            Console.WriteLine();

            Console.WriteLine("7. Change Building Project");
            Console.WriteLine("8. Allocation of Offices");
            Console.WriteLine("9. Exit Application");
        }

        static void ReadFromTXt(BuildingProject<Company> buildingProject)
        {
            //Random rnd = new Random();
            string[] s = File.ReadAllLines("Customers.txt");

            if (s.Length != 0)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    try
                    {
                        string companyType = s[i].Split(',')[s[i].Split(',').Length - 1];


                        if (companyType == "Private")
                        {
                            string name = s[i].Split(',')[0];
                            int spaceNeeded = int.Parse(s[i].Split(',')[1]);
                            int amountpayed = int.Parse(s[i].Split(',')[2]);

                            buildingProject.Upload(new PrivateCompany(name, spaceNeeded, amountpayed));
                        }
                        else if (companyType == "Person")
                        {
                            string name = s[i].Split(',')[0];
                            int amountpayed = int.Parse(s[i].Split(',')[1]);

                            buildingProject.Upload(new PrivatePerson(name, amountpayed));
                        }
                        else if (companyType == "Government")
                        {
                            string name = s[i].Split(',')[0];
                            int spaceNeeded = int.Parse(s[i].Split(',')[1]);
                            int amountpayed = int.Parse(s[i].Split(',')[2]);

                            buildingProject.Upload(new GovernmentCompany(name, spaceNeeded, amountpayed));
                        }
                        else
                        {
                            throw new NoTypeException(companyType);
                        }
                    }
                    catch (ItemExistsException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press ENTER to continue!");
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine("Press ENTER to continue!");
                        Console.ReadLine();
                    }
                }
            }
            else
            {
                throw new ArgumentNullException();
            }
        }

        static string NameGenerator()
        {
            Random rnd = new Random();
            if (File.Exists("words.txt"))
            {
                string s = "";
                string[] words = File.ReadAllLines("words.txt");

                int a = rnd.Next(1, 4);
                for (int i = 0; i < a; i++)
                {
                    s += words[rnd.Next(0, words.Length)] + " ";
                }

                return s.ToUpper();
            }
            else
            {
                return "JUST A TEST";
            }
        }
    }
}

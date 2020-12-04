using Roommates.Models;
using Roommates.Repository;
using System;
using System.Collections.Generic;

namespace Roommates
{
    class Program
    {
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";
        static void Main(string[] args)
        {
            RoomRepository roomRepo = new RoomRepository(CONNECTION_STRING);
            ChoreRepository choreRepo = new ChoreRepository(CONNECTION_STRING);
            RoommateRepository rmRepo = new RoommateRepository(CONNECTION_STRING);

            bool runProgram = true;

            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):
                        List<Room> rooms = roomRepo.GetAll();
                        foreach (Room r in rooms)
                        {
                            Console.WriteLine($"{r.Id} - {r.Name} Max Occupancy({r.MaxOccupancy})");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Show all chores"):
                        List<Chore> chores = choreRepo.GetAll();
                        foreach (Chore c in chores)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Search for room"):
                        Console.Write("Room Id: ");
                        int id = int.Parse(Console.ReadLine());
                        Room room = roomRepo.GetById(id);

                        Console.WriteLine($"{room.Id} - {room.Name} Max Occupancy({room.MaxOccupancy})");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for chore"):
                        Console.WriteLine("Chore Id: ");
                        int choreId = int.Parse(Console.ReadLine());
                        Chore chore = choreRepo.GetById(choreId);
                        Console.WriteLine($"{chore.Id} - {chore.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Search for a roommate"):
                        Console.WriteLine("Roommate Id: ");
                        int roommateId = int.Parse(Console.ReadLine());
                        Roommate roommate = rmRepo.GetById(roommateId);
                        Console.WriteLine($"{roommate.Id} - {roommate.FirstName} {roommate.LastName} - {roommate.Room.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a room"):
                        Console.Write("Room name: ");
                        string name = Console.ReadLine();

                        Console.Write("Max occupancy: ");
                        int max = int.Parse(Console.ReadLine());

                        Room roomToAdd = new Room()
                        {
                            Name = name,
                            MaxOccupancy = max
                        };

                        roomRepo.Insert(roomToAdd);

                        Console.WriteLine($"{roomToAdd.Name} has been added and assigned an Id of {roomToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Add a chore"):
                        Console.WriteLine("Chore Name: ");
                        string choreName = Console.ReadLine();
                        Chore choreToAdd = new Chore()
                        {
                            Name = choreName
                        };
                        choreRepo.Insert(choreToAdd);
                        Console.WriteLine($"{choreToAdd.Name} has been added and assigned an Id of {choreToAdd.Id}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Show all unassigned chores"):
                        List<Chore> unassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in unassignedChores)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;

                    case ("Assign chore to roommate"):
                        List<Chore> UnassignedChores = choreRepo.GetUnassignedChores();
                        foreach (Chore c in UnassignedChores)
                        {
                            Console.WriteLine($"{c.Id} - {c.Name}");
                        }
                        Console.Write("Select the id of the chore you want to assign.");
                        int selectedChoreId = int.Parse(Console.ReadLine());
                        List<Roommate> roommates = rmRepo.GetAll();
                        foreach (Roommate r in roommates)
                        {
                            Console.WriteLine($"{r.Id} - {r.FirstName} {r.LastName})");
                        }
                        Console.Write("Select the id of the roommate you want to assign the chore to.");
                        int selectedRmId = int.Parse(Console.ReadLine());
                        choreRepo.AssignChore(selectedRmId, selectedChoreId);
                        Console.Write("Chore assigned");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        break;
                    case ("Exit"):
                        runProgram = false;
                        break;


                }
            }
        }

        static string GetMenuSelection()
        {
            Console.Clear();
            List<string> options = new List<string>()
            {
                "Show all rooms",
                "Search for room",
                "Add a room",
                "Show all chores",
                "Search for a chore",
                "Search for a roommate",
                "Add a chore",
                "Show all unassigned chores",
                "Assign chore to roommate",
                "Exit"
            };
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.Write("Select an option > ");

                    string input = Console.ReadLine();
                    int index = int.Parse(input) - 1;
                    return options[index];

                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}


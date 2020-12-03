﻿using System;
using System.Collections.Generic;

namespace Roomates
{
    class Program
    {
        private const string CONNECTION_STRING = @"server=localhost\SQLExpress;database=Roommates;integrated security=true";
        static void Main(string[] args)
        {
            bool runProgram = true;

            while (runProgram)
            {
                string selection = GetMenuSelection();

                switch (selection)
                {
                    case ("Show all rooms"):

                        break;
                    case ("Search for room"):

                        break;
                    case ("Add a room"):

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


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    internal class Program
    {
        static ParkingLot parkingLot = new ParkingLot();
        static void Main(string[] args)
        {
            string[] commandArgs;
            while (true)
            {
                Console.Write("$ ");
                string command = Console.ReadLine();

                if (string.Equals(command, "exit", StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }

                commandArgs = command.Split(' ');
                command = commandArgs[0];
                commandArgs = commandArgs.Skip(1).ToArray();

                ProcessCommand(command, commandArgs);
            }
        }

        static void ProcessCommand(string command, string[] args)
        {
            switch (command.ToLower())
            {
                case "create_parking_lot":

                    if (args.Length != 1 || args.Contains(""))
                    {
                        Console.WriteLine($"Error: Need 1 argument");
                        break;
                    }

                    try
                    {
                        int slot = int.Parse(args[0]);
                        parkingLot = new ParkingLot(slot);
                        Console.WriteLine($"Created a parking lot with {parkingLot.Slot} slots");
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine($"Error: Slot should be a number");
                    }

                    break;
  
                case "park":
                    if (args.Length != 3 || args.Contains(""))
                    {
                        Console.WriteLine($"Error: Need 3 argument");
                        break;
                    }

                    try
                    {
                        if (parkingLot.Slot < 1)
                        {
                            Console.WriteLine("No Parking Lot Available!");
                            break;
                        }

                        Vehicle vehicle = new Vehicle(args[0], args[1], args[2]);

                        if (vehicle.Type != null)
                        {
                            parkingLot.InsertVehicle(vehicle);
                        }
                    }

                    catch (Exception e)
                    {
                        Console.WriteLine($"Error : {e.Message}");
                    }

                    break;

                case "leave":
                    if (args.Length != 1 || args.Contains(""))
                    {
                        Console.WriteLine($"Error: Need 1 argument");
                        break;
                    }

                    try
                    {
                        int slotNumber = int.Parse(args[0]);
                        parkingLot.RemoveVehicle(slotNumber);
                    }

                    catch (FormatException)
                    {
                        Console.WriteLine($"Error: Slot should be a number");
                    }

                    break;

                case "status":
                    {
                        List<Vehicle> slots = parkingLot.Slots;

                        if (slots != null)
                        {
                            int slotNumber = 0;

                            Console.WriteLine("Slot\tNo.\tType\tRegistration No Colour");

                            foreach (var item in slots)
                            {
                                slotNumber++;
                                if (item != null)
                                {
                                    Console.WriteLine($"{slotNumber}\t{item.plateNumber}\t{item.Type}\t{item.colour}");
                                }
                                else
                                {
                                    Console.WriteLine($"{slotNumber}\tSlot Empty");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Parking Lot");
                        }
                        break;
                    }

                case "type_of_vehicles":
                    {
                        if (args.Length != 1 || args.Contains(""))
                        {
                            Console.WriteLine($"Error: Need 1 argument");
                            break;
                        }

                        List<Vehicle> slots = parkingLot.Slots;

                        if (slots == null)
                        {
                            break;
                        }

                        int vehicleCount = slots.Count(item => item != null && item.Type.Equals(args[0], StringComparison.OrdinalIgnoreCase));

                        Console.WriteLine(vehicleCount);
                        break;
                    }

                case "registration_numbers_for_vehicles_with_odd_plate":
                    {
                        List<Vehicle> slots = parkingLot.Slots;

                        if (slots == null)
                        {
                            break;
                        }

                        var oddPlate = slots.Where(item => item != null && item.IsOddPlate(item.plateNumber)).Select(item => item.plateNumber).ToList();

                        if (oddPlate.Any())
                        {
                            Console.WriteLine(string.Join(",", oddPlate));
                        }

                    }
                    break;

                case "registration_numbers_for_vehicles_with_even_plate":
                    {
                        List<Vehicle> slots = parkingLot.Slots;

                        if (slots == null)
                        {
                            break;
                        }

                        var evenPlate = slots.Where(item => item != null && !item.IsOddPlate(item.plateNumber)).Select(item => item.plateNumber).ToList();

                        if (evenPlate.Any())
                        {
                            Console.WriteLine(string.Join(",", evenPlate));
                        }
                        else
                        {
                            Console.WriteLine("Not found");
                        }
                        break;
                    }

                case "registration_numbers_for_vehicles_with_colour":
                    {
                        if (args.Length != 1 || args.Contains(""))
                        {
                            Console.WriteLine($"Error: Need 1 argument");
                            break;
                        }

                        List<Vehicle> slots = parkingLot.Slots;

                        if (slots == null)
                        {
                            break;
                        }

                        var filteredPlate = slots.Where(item => item.plateNumber != null && item.colour.Equals(args[0], StringComparison.OrdinalIgnoreCase)).Select(item => item.plateNumber).ToList();

                        if (filteredPlate.Any())
                        {
                            Console.WriteLine(string.Join(",", filteredPlate));
                        }
                        else
                        {
                            Console.WriteLine("Not found");
                        }
                        break;
                    }

                case "slot_numbers_for_vehicles_with_colour":
                    {
                        if (args.Length != 1 || args.Contains(""))
                        {
                            Console.WriteLine($"Error: Need 1 argument");
                            break;
                        }

                        List<Vehicle> slots = parkingLot.Slots;

                        if (slots == null)
                        {
                            break;
                        }

                        var filteredSlot = slots.Select((item, index) => new {Vehicle = item, SlotNumber = index + 1}).Where(slot => slot.Vehicle != null && slot.Vehicle.colour.Equals(args[0], StringComparison.OrdinalIgnoreCase)).Select(slot => slot.SlotNumber);

                        if (filteredSlot.Any())
                        {
                            Console.WriteLine(string.Join(",", filteredSlot));
                        }
                        else
                        {
                            Console.WriteLine("Not found");
                        }
                        break;
                    }

                case "slot_numbers_for_registration_number":
                    {
                        if (args.Length != 1 || args.Contains(""))
                        {
                            Console.WriteLine($"Error: Need 1 argument");
                            break;
                        }

                        List<Vehicle> slots = parkingLot.Slots;

                        if (slots == null)
                        {
                            break;
                        }

                        var filteredSlot = slots.Select((item, index) => new { Vehicle = item, SlotNumber = index + 1 }).Where(slot => slot.Vehicle != null && slot.Vehicle.plateNumber.Equals(args[0])).Select(slot => slot.SlotNumber).FirstOrDefault();

                        if (filteredSlot != 0)
                        {
                            Console.WriteLine(filteredSlot);
                        } 
                        else
                        {
                            Console.WriteLine("Not found");
                        }

                        break;
                    }

                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }
    }
}

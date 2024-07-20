using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    internal class ParkingLot
    {
        private static int slot;
        private static List<Vehicle> slots;

        public ParkingLot() { }
        public ParkingLot(int aSlot)
        {
            Slot = aSlot;
        }
        public int Slot
        {
            get
            {
                return slot;
            }
            set { 
                slot = value;
                slots = new List<Vehicle>(new Vehicle[slot]);
            }
        }

        public void InsertVehicle(Vehicle aVehicle)
        {
            for (int i = 0; i < slot; i++)
            {
                if (slots[i] == null)
                {
                    slots[i] = aVehicle;
                    Console.WriteLine($"Allocated slot number: {i + 1}");
                    return;
                }
            }
            Console.WriteLine("Sorry, parking lot is full.");

        }

        public void RemoveVehicle(int slotNumber)
        {
            if (slotNumber < 1 || slotNumber > slot)
            {
                Console.WriteLine("Invalid slot number");
                return;
            }

            if (slots[slotNumber - 1] != null)
            {
                slots[slotNumber - 1] = null;
                Console.WriteLine($"Slot number {slotNumber} is free");
            } 
            else
            {
                Console.WriteLine($"Slot number {slotNumber} is already empty");
            }
        }

        public List<Vehicle> Slots
        {
            get { return slots; }
        }

    }
}

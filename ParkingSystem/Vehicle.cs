using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSystem
{
    internal class Vehicle
    {
        public string plateNumber;
        public string colour;
        private string type;
        public Vehicle(string aPlateNumber, string aColour, string aType) {
            plateNumber = aPlateNumber;
            colour = aColour;
            Type = aType;
        }
        
        public string Type
        {
            get { return type; }
            set 
            { 
                if (value.ToLower() == "mobil")
                {
                    type = "Mobil";
                }
                else if (value.ToLower() == "motor")
                {
                    type = "Motor";
                }
                else
                {
                    Console.WriteLine("Invalid Vehicle Type");
                    return;
                }
            }
        }

        public bool IsOddPlate(string aPlateNumber)
        {
            var numberPart = new string(aPlateNumber.Where(char.IsDigit).ToArray());

            char lastNumber = numberPart.Last();

            return (lastNumber - '0') % 2 !=0 ;
        }
    }
}

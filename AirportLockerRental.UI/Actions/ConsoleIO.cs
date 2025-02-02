using AirportLockerRental.UI.DataStorage;
using AirportLockerRental.UI.DTOs;
using AirportLockerRental.UI.Storage;

namespace AirportLockerRental.UI.Actions
{
    public static class ConsoleIO
    {
        public static string GetRequiredString(string prompt)
        {
            do
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();

                if(!string.IsNullOrEmpty(input))
                {
                    return input;
                }
                else
                {
                    Console.WriteLine("This data is required.");
                    AnyKey();
                }
            } while (true);
        }

        public static int GetCapacity()
        {
            int capacity;

            do
            {
                Console.Write("Enter storage capacity: ");
                if(int.TryParse(Console.ReadLine(), out capacity))
                {
                    if(capacity > 0)
                    {
                        return capacity;
                    }
                }

                Console.WriteLine("Capacity must be a positive number.");
                AnyKey();
            } while (true);
        }
        public static void DisplayLockerContents(LockerContents? dto, User currentUser)
        {

            if (dto != null)
            {
                Console.WriteLine("=====================================");
                Console.WriteLine($"Locker #: {dto.LockerNumber}");
                Console.WriteLine($"Renter Name: {currentUser.UserName}");
                Console.WriteLine($"Contents: {dto.Description}");
                Console.WriteLine("=====================================");
            }
        }

        public static int GetLockerNumber(int capacity)
        {
            int lockerNumber;

            do
            {
                Console.Write($"Enter locker number (1-{capacity}): ");
                if (int.TryParse(Console.ReadLine(), out lockerNumber))
                {
                    if (lockerNumber >= 1 && lockerNumber <= capacity)
                    {
                        return lockerNumber;
                    }

                    Console.WriteLine($"Invalid choice. Please enter a number between 1 and {capacity}.");
                }
            } while (true);
        }

        public static int GetMenuOption()
        {
            int userChoice;

            Console.Clear();

            do
            {
                Console.Clear();
                Console.WriteLine("Airport Locker Rental Menu");
                Console.WriteLine("=============================");
                Console.WriteLine("1. View a locker");
                Console.WriteLine("2. Rent a locker");
                Console.WriteLine("3. End a locker rental");
                Console.WriteLine("4. List all locker contents");
                Console.WriteLine("5. Quit");
                Console.Write("\nEnter your choice (1-5): ");

                if (int.TryParse(Console.ReadLine(), out userChoice))
                {
                    if (userChoice >= 1 && userChoice <= 5)
                    {
                        return userChoice;
                    }

                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 5.");
                    AnyKey();
                }
            } while (true);
        }

        public static void AnyKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        public static LockerContents GetLockerContentsFromUser()
        {
            LockerContents contents = new LockerContents();
            contents.Description = GetRequiredString("Enter the item you want to store in the locker: ");
            return contents;
        }

        public static string GetPassword()
        {
            
            var result = "";
            while (true)
            {
                Console.WriteLine("Please enter your password: ");
                result = Console.ReadLine();
                if (string.IsNullOrEmpty(result))
                {
                    Console.WriteLine("Please enter a valid username");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return result;
        }
        public static string GetUserName()
        {
            
            var result = "";
            while (true)
            {
                Console.WriteLine("Please enter your username: ");
                result = Console.ReadLine();
                if (string.IsNullOrEmpty(result)){
                    Console.WriteLine("Please enter a valid username");
                    continue;
                }
                else
                {
                    break;
                }
            }
            return result;
        }
        public static int GetLoginChoice()
        {
           Console.WriteLine("1. Login\r\n2. Create an Account\r\n3. Quit");
            int result;
            while (true)
            {
            if(int.TryParse(Console.ReadLine(), out result) && result == 1 || result == 2 || result == 3)
            {
                return result;
            }
            else
            {
                    Console.WriteLine("Please enter a valid option...");
            }
            }
        }

        internal static void DisplayLockerNumbers(List<int> lockerNumbers)
        {
            Console.Write($"Your Lockers: ");
            foreach (var locker in lockerNumbers)
            {
                Console.Write($"{locker},");
            }
            Console.WriteLine();
        }
    }
}

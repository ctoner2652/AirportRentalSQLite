using AirportLockerRental.UI.Actions;
using AirportLockerRental.UI.DataQueries;
using AirportLockerRental.UI.DataStorage;
using AirportLockerRental.UI.DTOs;
using AirportLockerRental.UI.Security;
using AirportLockerRental.UI.Storage;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace AirportLockerRental.UI.Workflows
{
    // we only need one App object, so making it static is appropriate
    public static class App
    {

        public static void Run(string key)
        {
            User connectedUser;
            Console.WriteLine("===== Welcome to the airport locker rental!!! =====");
            connectedUser = LoginValidation.GetLogin()!;
            ILockerRepository lockerStorage = new SQLLockerRepository(connectedUser);
            Encryption encrypt = new Encryption(key);
            while (true)
            {
                if(connectedUser == null)
                {
                    break;
                }
                int choice = ConsoleIO.GetMenuOption();

                if (choice == 5)
                {
                    break;
                }
                else if (choice == 4)
                {
                    var lockers = lockerStorage.List();
                    foreach(var locker in lockers)
                    {
                        locker.Description = encrypt.Decrypt(locker.Description);
                        ConsoleIO.DisplayLockerContents(locker, connectedUser);
                    }
                }
                else
                {
                    int lockerNumber;

                    if (choice == 1)
                    { 
                        List<int> lockerNumbers = lockerStorage.GetLockerNumbers();
                        ConsoleIO.DisplayLockerNumbers(lockerNumbers);
                        lockerNumber = ConsoleIO.GetLockerNumber(100);
                        if (!lockerStorage.IsAvailable(lockerNumber))
                        {
                            var rental = lockerStorage.Get(lockerNumber);
                            if (rental != null)
                            {
                                if (rental.UserID == connectedUser.UserID)
                                {
                                    var myLocker = lockerStorage.Get(lockerNumber);
                                    myLocker.Description = encrypt.Decrypt(myLocker.Description);
                                    ConsoleIO.DisplayLockerContents(myLocker, connectedUser);
                                }
                                else
                                {
                                    Console.WriteLine("Sorry you do not own this locker...");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("That locker is currently empty!");
                        }
                    }
                    else if (choice == 2)
                    {
                        lockerNumber = ConsoleIO.GetLockerNumber(100);
                        if (lockerStorage.IsAvailable(lockerNumber))
                        {
                            LockerContents contents = ConsoleIO.GetLockerContentsFromUser();
                            contents.LockerNumber = lockerNumber;
                            contents.Description = encrypt.Encrypt(contents.Description);
                            lockerStorage.Add(contents);
                            Console.WriteLine($"Locker {lockerNumber} is rented, stop by later to pick up your stuff!");
                        }
                        else
                        {
                            Console.WriteLine($"Sorry, but locker {lockerNumber} has already been rented!");
                        }
                    }
                    else
                    {
                        //Get All Locker Numbers For User
                        List<int> lockerNumbers = lockerStorage.GetLockerNumbers();
                        ConsoleIO.DisplayLockerNumbers(lockerNumbers);
                        lockerNumber = ConsoleIO.GetLockerNumber(100);
                        {
                            if (!lockerStorage.IsAvailable(lockerNumber))
                            {
                                var rental = lockerStorage.Get(lockerNumber);
                                if (rental != null)
                                {
                                    if (rental.UserID == connectedUser.UserID)
                                    {
                                        lockerStorage.Remove(lockerNumber);
                                        Console.WriteLine("Locker rental has been ended!");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Sorry you do not own this locker...");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("That locker is currently empty!");
                            }
                        }
                    }

                }
                    ConsoleIO.AnyKey();
            }
        }
    }
}

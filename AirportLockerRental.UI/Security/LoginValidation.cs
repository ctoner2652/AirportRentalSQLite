using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLockerRental.UI.Actions;
using AirportLockerRental.UI.DataQueries;
using AirportLockerRental.UI.DataStorage;

namespace AirportLockerRental.UI.Security
{
    public static class LoginValidation
    {
        
        public static User? GetLogin()
        {
            DataAccess _DataAccess = new();
            PasswordHashing ph = new PasswordHashing();
            User connectedUser = new();
            int loginChoice = 0;
            do
            {
                loginChoice = ConsoleIO.GetLoginChoice();
                if (loginChoice == 1)
                {
                    //Attempt to login!
                    string SignInUsername = ConsoleIO.GetUserName();
                    string SignInPassword = ConsoleIO.GetPassword();
                    User attemptingToLoginAs = _DataAccess.GetUser(SignInUsername)!;
                    string? hash;
                    if (attemptingToLoginAs != null)
                    {
                        hash = ph.VerifyPassword(SignInPassword, attemptingToLoginAs);
                    }
                    else
                    {
                        hash = null;
                    }
                    if (hash == null)
                    {
                        Console.WriteLine("Invalid credentials! Please try again");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine("Valid credentials accepted!");
                        Console.WriteLine($"You have logged in as {attemptingToLoginAs.UserName}");
                        ConsoleIO.AnyKey();
                        connectedUser = attemptingToLoginAs;
                    }
                    break;
                }
                else if (loginChoice == 2)
                {
                    string newUserName = "";
                    bool duplicate = false;
                    do
                    {
                        newUserName = ConsoleIO.GetUserName();
                        if (_DataAccess.GetUser(newUserName) != null)
                        {
                            duplicate = true;
                            Console.WriteLine("Sorry that username is already taken! Please try again: ");
                        }
                        else
                        {
                            duplicate = false;
                        }
                    } while (duplicate);
                    string newPassword = ConsoleIO.GetPassword();
                    //Create Account!
                    var salt = ph.CreateSalt();
                    string Newpasswordhash = ph.HashPassword(newPassword, salt);
                    User newUser = new User
                    {
                        UserName = newUserName,
                        PasswordHash = Newpasswordhash,
                        Salt = Convert.ToHexString(salt)
                    };
                    int userID = _DataAccess.CreateUser(newUser);
                    newUser.UserID = userID;
                    Console.WriteLine($"New user account created successfully! Welcome {newUser.UserName}");
                    ConsoleIO.AnyKey();
                    connectedUser = newUser;
                    break;
                }
                else if (loginChoice == 3)
                {
                    return null;
                }
                break;
            } while (true);

            return connectedUser;
        }
    }
}

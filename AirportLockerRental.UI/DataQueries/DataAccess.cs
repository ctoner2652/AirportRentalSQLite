using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AirportLockerRental.UI.DataStorage;
using AirportLockerRental.UI.DTOs;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.VisualBasic;

namespace AirportLockerRental.UI.DataQueries
{
    public class DataAccess
    {
        private readonly string connectionString = "";
        public DataAccess()
        {
            var configBuilder = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();
            connectionString = configBuilder["ConnectionString"]!;
        }

        public int CreateUser(User newUser)
        {
            int id = 0;
            using (var cn = new SqliteConnection(connectionString))
            {
                var sql = @"INSERT INTO Users (UserName, PasswordHash, Salt)
                            VALUES (@UserName, @PasswordHash, @Salt);
                            SELECT last_insert_rowid();";

                try
                {
                    cn.Open();
                    var cmd = new SqliteCommand(sql, cn);

                    cmd.Parameters.AddWithValue("@UserName", newUser.UserName);
                    cmd.Parameters.AddWithValue("@PasswordHash", newUser. PasswordHash);
                    cmd.Parameters.AddWithValue("@Salt", newUser.Salt);

                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }catch(Exception ex) { 
                    Console.WriteLine(ex.Message);
                }

                return id;
            }
        }

        public User? GetUser(string UserName)
        {
            User user = new User();
            using (var cn = new SqliteConnection(connectionString))
            {
                var sql = @"SELECT * FROM Users WHERE Username = @Username";

                try
                {
                    var cmd = new SqliteCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@Username", UserName);
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {

                        if (dr.Read())
                        {
                            user.UserName = (string)dr["Username"];
                            user.PasswordHash = (string)dr["PasswordHash"];
                            user.Salt = (string)dr["Salt"];
                            user.UserID = Convert.ToInt32(dr["UserID"]);
                            return user;

                        }
                        else
                        {
                            return null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public bool IsAvaliable(int lockerNumber)
        {
            using (var cn = new SqliteConnection(connectionString))
            {
                var sql = @"SELECT * FROM Rentals WHERE LockerNumber = @LockerNumber";

                try
                {
                    var cmd = new SqliteCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@LockerNumber", lockerNumber);
                    cn.Open();

                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
        }
        public void AddLocker(LockerContents newLocker, User owner)
        {
            using(var cn = new SqliteConnection(connectionString))
            {
                var sql = @"INSERT INTO Rentals (LockerNumber, UserID, Contents, StartDate)
                            VALUES (@LockerNumber, @UserID, @Contents, @StartDate);";

                try
                {
                    cn.Open();
                    var cmd = new SqliteCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@LockerNumber", newLocker.LockerNumber);
                    cmd.Parameters.AddWithValue("@UserID", owner.UserID);
                    cmd.Parameters.AddWithValue("@Contents", newLocker.Description);
                    cmd.Parameters.AddWithValue("@StartDate", DateTime.Now.ToString());

                    cmd.ExecuteNonQuery();
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public Rental? GetRental(int lockerNumber)
        {
            Rental result = new();
            using(var cn = new SqliteConnection(connectionString))
            {
                var sql = @"SELECT * FROM Rentals WHERE LockerNumber = @LockerNumber";
                
                try
                {
                    var cmd = new SqliteCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@LockerNumber", lockerNumber);
                    cn.Open();

                    using(var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            result.LockerNumber = Convert.ToInt32(dr["LockerNumber"]);
                            result.UserID = Convert.ToInt32(dr["UserID"]);
                            result.Contents = (string)dr["Contents"];
                            result.StartDate = (string)dr["StartDate"];
                            return result;
                        }
                        else
                        {
                            return null;
                        }

                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }
        public void EndLockerRental(int LockerNumber, User currentUser)
        {
            Rental deletedRental = new();
            using (var cn = new SqliteConnection(connectionString))
            {
                var getSql = @"SELECT * FROM Rentals WHERE lockerNumber = @LockerNumber AND UserID = @UserID";
                var sql = @"DELETE FROM Rentals WHERE lockerNumber = @LockerNumber AND UserID = @UserID";
                var sql2 = @"INSERT INTO RentalHistory (UserID, LockerNumber, Contents, StartDate, EndDate)
                             VALUES (@UserID, @LockerNumber, @Contents, @StartDate, @EndDate);";
                try
                {
                    cn.Open();
                    var cmdGet = new SqliteCommand(getSql, cn);
                    cmdGet.Parameters.AddWithValue("@LockerNumber", LockerNumber);
                    cmdGet.Parameters.AddWithValue("@UserID", currentUser.UserID);
                    using (var dr = cmdGet.ExecuteReader())
                    {
                        if (dr.Read()) {
                            deletedRental.LockerNumber = Convert.ToInt32(dr["LockerNumber"]);
                            deletedRental.UserID = Convert.ToInt32(dr["UserID"]);
                            deletedRental.Contents = (string)dr["Contents"];
                            deletedRental.StartDate = (string)dr["StartDate"];
                        }
                    };
                    var cmd1 = new SqliteCommand(sql, cn);
                    cmd1.Parameters.AddWithValue("@LockerNumber", LockerNumber);
                    cmd1.Parameters.AddWithValue("@UserID", currentUser.UserID);
                    cmd1.ExecuteNonQuery();
                    var cmd2 = new SqliteCommand(sql2, cn);
                    cmd2.Parameters.AddWithValue("@UserID", currentUser.UserID);
                    cmd2.Parameters.AddWithValue("@LockerNumber", LockerNumber);
                    cmd2.Parameters.AddWithValue("@Contents", deletedRental.Contents);
                    cmd2.Parameters.AddWithValue("@StartDate", deletedRental.StartDate);
                    cmd2.Parameters.AddWithValue("@EndDate", DateTime.Now.ToString());
                    cmd2.ExecuteNonQuery();

                } catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        public List<Rental> GetAllRentals(User currentUser)
        {
            List<Rental> rentals = new();
            using (var cn = new SqliteConnection(connectionString))
            {
                var sql = "SELECT * FROM RentalHistory WHERE UserID = @UserID";
                var sql2 = "SELECT * FROM Rentals WHERE UserID = @UserID";

                try
                {
                    cn.Open();
                    var cmd1 = new SqliteCommand(sql, cn);
                    cmd1.Parameters.AddWithValue("@UserID", currentUser.UserID);

                    using (var dr = cmd1.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            rentals.Add(new Rental
                            {
                                LockerNumber = Convert.ToInt32(dr["LockerNumber"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                Contents = (string)dr["Contents"],
                                StartDate = (string)dr["StartDate"]
                            });
                        }
                    }

                    cn.Open();
                    var cmd2 = new SqliteCommand(sql2, cn);
                    cmd2.Parameters.AddWithValue("@UserID", currentUser.UserID);

                    using (var dr = cmd2.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            rentals.Add(new Rental
                            {
                                LockerNumber = Convert.ToInt32(dr["LockerNumber"]),
                                UserID = Convert.ToInt32(dr["UserID"]),
                                Contents = (string)dr["Contents"],
                                StartDate = (string)dr["StartDate"]
                            });
                        }
                    }

                }
                catch(Exception ex) { Console.WriteLine(ex.Message) ; }

                return rentals;
            };
        }

        public List<int> GetCurrentLockerNumbers(User currentUser)
        {
            List<int> lockerNumbers = new();
            using (var cn = new SqliteConnection(connectionString))
            {
                var sql = @"SELECT LockerNumber FROM Rentals WHERE UserID = @UserID";

                try
                {
                    cn.Open();
                    var cmd = new SqliteCommand(sql, cn);
                    cmd.Parameters.AddWithValue("@UserID", currentUser.UserID);

                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read()) 
                        {
                            lockerNumbers.Add(Convert.ToInt32(dr["LockerNumber"]));
                        }

                    }
                }catch(Exception ex)
                {
                   Console.WriteLine(ex.Message);
                   
                }
                return lockerNumbers;
            }
        }
    }
}

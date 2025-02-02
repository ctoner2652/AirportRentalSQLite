using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportLockerRental.UI.DataQueries;
using AirportLockerRental.UI.DataStorage;
using AirportLockerRental.UI.DTOs;

namespace AirportLockerRental.UI.Storage
{
    public class SQLLockerRepository : ILockerRepository
    {
        private User _ActiveUser;
        private DataAccess _DataAccess = new();
        public SQLLockerRepository(User activeUser)
        {
            _ActiveUser = activeUser;
        }

        public void Add(LockerContents contents)
        {
            _DataAccess.AddLocker(contents, _ActiveUser);
        }

        public LockerContents? Get(int number)
        {
            var result = _DataAccess.GetRental(number);
            LockerContents myLocker = new LockerContents
            {
                LockerNumber = number,
                Description = result.Contents,
                UserName = _ActiveUser.UserName,
                UserID = result.UserID
            };
            return myLocker;
        }

        public List<int> GetLockerNumbers()
        {
            return _DataAccess.GetCurrentLockerNumbers(_ActiveUser);
        }

        public bool IsAvailable(int number)
        {
            return _DataAccess.IsAvaliable(number);
        }

        public List<LockerContents> List()
        {
            List<Rental> rentals = _DataAccess.GetAllRentals(_ActiveUser);
            List<LockerContents> lockers = new();

            foreach (var rental in rentals)
            {
                lockers.Add(new LockerContents
                {
                    UserName = _ActiveUser.UserName,
                    LockerNumber = rental.LockerNumber,
                    Description = rental.Contents

                });
            }
            return lockers;
        }

        public void Remove(int number)
        {
            _DataAccess.EndLockerRental(number, _ActiveUser);
        }
    }
}

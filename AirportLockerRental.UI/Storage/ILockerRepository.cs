using AirportLockerRental.UI.DTOs;

namespace AirportLockerRental.UI.Storage
{
    public interface ILockerRepository
    {
        List<LockerContents> List();
        LockerContents? Get(int number);
        bool IsAvailable(int number);
        void Add(LockerContents contents);
        void Remove(int number);
        List<int> GetLockerNumbers();
    }
}


using BookingManager.Models;

namespace BookingManager.Services.Interfaces
{
    public interface IApartmentService
    {
        //GetApartment
        ApartmentDTO GetApartment(int id);
        IList<ApartmentDTO> GetApartments();

        //AddApartment
        BaseResponse AddApartment(ApartmentDTO request);

        //UpdateApartment
        BaseResponse UpdateApartment(int id, ApartmentDTO request);
        public IList<ApartmentDTO> SearchApartment(SearchDTO request);

        ApartmentDTO BookApartment(int id);
        ApartmentDTO CheckOut(int id);


    }
}

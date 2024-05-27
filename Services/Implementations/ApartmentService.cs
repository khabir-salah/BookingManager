﻿
using BookingManager.Entities;
using BookingManager.Exceptions;
using BookingManager.Models;
using BookingManager.Repositories.Interfaces;
using BookingManager.Services.Interfaces;
using Mapster;
using System.Net;

namespace BookingManager.Services.Implementations
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IUnitOfWork _unitOfWork;


        public ApartmentService(IApartmentRepository apartmentRepository, IUnitOfWork unitOfWork)
        {
            _apartmentRepository = apartmentRepository;
            _unitOfWork = unitOfWork;
        }

        public BaseResponse AddApartment(ApartmentDTO request)
        {
            //check existing apartment
            var apartmentExist = _apartmentRepository.Exist(x => x.CompanyName == request.CompanyName && x.Name == request.Name);
            if (apartmentExist)
            {
                throw new ApartmentDuplicateException($"Apartment {request.Name} already registered for company: {request.CompanyName}");
            }

            var apartment = new Apartment(request.CompanyName, request.Name, request.Description, request.Guests, request.BedRooms, request.NumberOfRooms, request.Price, request.City, request.State, request.Country, request.PrimaryImageUrl, request.Images.ToArray(), request.Facilities.ToArray());

            _apartmentRepository.AddApartment(apartment);
            _unitOfWork.SaveChanges();
            return new BaseResponse
            {
                Status = true,
                Message = "Apart succesfully added"
            };
        }

        public ApartmentDTO GetApartment(int id)
        {
            var apartment = _apartmentRepository.FindById(id);
            if (apartment == null)
            {
                throw new ApartmentNotFoundException("Apartment can not be found");
            }
            
            return apartment.Adapt<ApartmentDTO>();
        }

        public IList<ApartmentDTO> GetApartments()
        {
            var apartments = _apartmentRepository.GetApartments();
            return apartments.Adapt<IList<ApartmentDTO>>();
        }

        public BaseResponse UpdateApartment(int id, ApartmentDTO request)
        {
            return null;
        }

        public IList<ApartmentDTO>  SearchApartments(SearchDTO request)
        {
            var apartment = _apartmentRepository.GetApartments().Where(s => s.NumberOfRooms == request.NoOfRooms && (s.State == request.State || s.Country == request.Country) && s.Status == Enums.ApartmentStatus.Available);
            if(apartment != null)
            {
                return apartment.Adapt<IList<ApartmentDTO>>();
            }
            throw new ApartmentNotFoundException("Apartment can not be found");

        }
        public IList<ApartmentDTO>  SearchApartment(SearchDTO request)
        {
            var apartment = _apartmentRepository.GetApartments().Where(s => s.NumberOfRooms == request.NoOfRooms && (s.State == request.State || s.Country == request.Country) && s.Status == Enums.ApartmentStatus.Available).ToList();
            if(apartment.Any())
            {
                return apartment.Select(apartment => new ApartmentDTO
                {
                    Name = apartment.Name,
                    BedRooms = apartment.BedRooms,
                    City = apartment.City,
                    CompanyName = apartment.CompanyName,
                    Country = apartment.Country,
                    Description  = apartment.Description,
                    Facilities = apartment.Facilities,
                    Price = apartment.Price,
                    Guests = apartment.Guests,
                    Images = apartment.Images,
                    NumberOfRooms = apartment.NumberOfRooms,
                    State = apartment.State,
                    Status = apartment.Status,
                    PrimaryImageUrl = apartment.PrimaryImageUrl,
                }).ToList();
                
            }
            throw new ApartmentNotFoundException("Apartment can not be found");

        }
    }
}

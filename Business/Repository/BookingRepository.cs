using AutoMapper;
using Business.Repository.IRepository;
using Common;
using DataAccess;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public BookingRepository(IMapper mapper, ApplicationDbContext db)
        {
            _mapper = mapper;
            _db = db;
        }

        public async Task ChangeStatus(int id, string status)
        {
            var bookingHeader = _db.BookingHeaders.FirstOrDefault(b => b.Id == id);
            
            if (bookingHeader != null)
            {
                if (status.Equals(SD.Status_Refunded))
                {
                    var options = new Stripe.RefundCreateOptions
                    {
                        Reason = Stripe.RefundReasons.RequestedByCustomer,
                        PaymentIntent = bookingHeader.PaymentIntentId
                    };

                    var service = new Stripe.RefundService();
                    Stripe.Refund refund = service.Create(options);

                    bookingHeader.Status = SD.Status_Refunded;
                    await _db.SaveChangesAsync();
                }
                else
                {
                    bookingHeader.Status = status;
                    _db.BookingHeaders.Update(bookingHeader);
                    await _db.SaveChangesAsync();
                }
            }
        }

        public async Task<BookingDTO> Create(BookingDTO dto)
        {
            try
            {
                var bookingHeader = _mapper.Map<BookingHeaderDTO, BookingHeader>(dto.BookingHeaderDTO);
                IEnumerable<BookingDetail> bookingDetails = _mapper.Map<IEnumerable<BookingDetailDTO>, IEnumerable<BookingDetail>>(dto.BookingDetailDTOs);

                string code = SD.GetCode();
                int isUnique = 0;

                while (isUnique != 1)
                {
                    var booking = _db.BookingHeaders.FirstOrDefault(b => b.BookingCode.Equals(code));
                    if (booking == null || booking == new BookingHeader())
                    {
                        isUnique++;
                    }
                    else
                    {
                        code = SD.GetCode();
                    }
                }

                bookingHeader.BookingCode = code;

                _db.BookingHeaders.Add(bookingHeader);
                await _db.SaveChangesAsync();

                foreach (var detail in bookingDetails)
                {
                    detail.BookingHeaderId = bookingHeader.Id;
                }
                _db.BookingDetails.AddRange(bookingDetails);
                await _db.SaveChangesAsync();

                return new BookingDTO()
                {
                    BookingHeaderDTO = _mapper.Map<BookingHeader, BookingHeaderDTO>(bookingHeader),
                    BookingDetailDTOs = _mapper.Map<IEnumerable<BookingDetail>, IEnumerable<BookingDetailDTO>>(bookingDetails)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dto;
        }

        public async Task<int> Delete(int id)
        {
            var bookingHeader = _db.BookingHeaders.FirstOrDefault(b => b.Id == id);
            if (bookingHeader != null)
            {
                var bookingDetails = _db.BookingDetails.Where(b => b.BookingHeaderId == id);
                _db.BookingDetails.RemoveRange(bookingDetails);
                _db.BookingHeaders.Remove(bookingHeader);
                return await _db.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<IEnumerable<BookingDTO>> GetSpecific(int trainId)
        {
            var trainBookingsHeaders = _db.BookingHeaders.Where(b => b.TrainId == trainId).ToList();
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            foreach (var header in trainBookingsHeaders)
            {
                var booking = new BookingDTO()
                {
                    BookingHeaderDTO = _mapper.Map<BookingHeader, BookingHeaderDTO>(header),
                };

                var details = _db.BookingDetails.Where(d => d.BookingHeaderId == header.Id);
                booking.BookingDetailDTOs = _mapper.Map<IEnumerable<BookingDetail>, IEnumerable<BookingDetailDTO>>(details);

                bookingDTOs.Add(booking);
            }
            return bookingDTOs;
        }

        public async Task<IEnumerable<BookingDTO>> GetAll()
        {
            var bookingsHeaders = _db.BookingHeaders.ToList();

            //Delete that not paid
            foreach (var header in bookingsHeaders)
            {
                if ((DateTime.Now.Date.Equals(header.BookedDate.Date.AddDays(4)) && header.Status.Equals(SD.Status_Pending)) || DateTime.Now.Date.Equals(header.BookedDate.Date.AddDays(31)))
                {
                    await Delete(header.Id);
                }
            }

            bookingsHeaders = _db.BookingHeaders.ToList();
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();

            foreach (var header in bookingsHeaders)
            {
                var booking = new BookingDTO()
                {
                    BookingHeaderDTO = _mapper.Map<BookingHeader, BookingHeaderDTO>(header),
                };

                var details = _db.BookingDetails.Where(d => d.BookingHeaderId == header.Id);
                booking.BookingDetailDTOs = _mapper.Map<IEnumerable<BookingDetail>, IEnumerable<BookingDetailDTO>>(details);

                bookingDTOs.Add(booking);
            }
            return bookingDTOs;
        }

        public async Task<BookingDTO> Get(int id)
        {
            var bookingHeader = _db.BookingHeaders.FirstOrDefault(b => b.Id == id);
            
            if (bookingHeader != null)
            {
                var bookingDetails = _db.BookingDetails.Where(d => d.BookingHeaderId == id);
                return new BookingDTO()
                {
                    BookingHeaderDTO = _mapper.Map<BookingHeader, BookingHeaderDTO>(bookingHeader),
                    BookingDetailDTOs = _mapper.Map<IEnumerable<BookingDetail>, IEnumerable<BookingDetailDTO>>(bookingDetails)
                };
            }
            return new BookingDTO();
        }

        public async Task<IEnumerable<BookingDTO>> GetUser(string userId)
        {
            var trainBookingsHeaders = _db.BookingHeaders.Where(b => b.UserId.Equals(userId)).ToList();
            List<BookingDTO> bookingDTOs = new List<BookingDTO>();
            foreach (var header in trainBookingsHeaders)
            {
                var booking = new BookingDTO()
                {
                    BookingHeaderDTO = _mapper.Map<BookingHeader, BookingHeaderDTO>(header),
                };

                var details = _db.BookingDetails.Where(d => d.BookingHeaderId == header.Id);
                booking.BookingDetailDTOs = _mapper.Map<IEnumerable<BookingDetail>, IEnumerable<BookingDetailDTO>>(details);

                bookingDTOs.Add(booking);
            }
            return bookingDTOs;
        }

        public async Task<BookingDTO> Update(BookingDTO dto)
        {
            var bookingHeader = _db.BookingHeaders.FirstOrDefault(b => b.Id == dto.BookingHeaderDTO.Id);
            if (bookingHeader != null)
            {
                bookingHeader.SessionId = dto.BookingHeaderDTO?.SessionId;
                bookingHeader.PaymentIntentId = dto.BookingHeaderDTO?.PaymentIntentId;
                _db.BookingHeaders.Update(bookingHeader);
                await _db.SaveChangesAsync();
                dto.BookingHeaderDTO = _mapper.Map<BookingHeader, BookingHeaderDTO>(bookingHeader);
                return dto;
            }
            return dto;
        }
    }
}       

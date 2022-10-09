using Models;

namespace ClientSide.Service.IService
{
    public interface IPaymentService
    {
        public Task<SuccessModelDTO> Checkout(StripePaymentDTO paymentDTO);
    }
}

using DeKee.DomainContext.Base;

namespace DeKee.DomainContext.Services
{
    public interface ITransferToService : IApplicatonService 
    {
        Dto.CheckResponse TopUpCheck(string phoneNumber);
        Dto.TopUpResponse TopUpConfirm(Dto.TopUpRequest input);
        Dto.ReserveIdResponse TopUpPrepare();
    }
}

using Entities.VMs.User;

namespace Communication.EmailManager.Abstract
{
    public interface IEmailManager
    {
        void SendForgotPasswordEmail(ForgotPasswordVm mailModel);
    }
}
namespace Automated_Menu_Ordering_System.Contracts.Services;

public interface IActivationService
{
    Task ActivateAsync(object activationArgs);
}

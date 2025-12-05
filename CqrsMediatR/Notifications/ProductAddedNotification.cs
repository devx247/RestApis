using CqrsMediatR.Models;
using MediatR;

namespace CqrsMediatR.Notifications
{
    public record ProductAddedNotification(Product Product) : INotification;
}
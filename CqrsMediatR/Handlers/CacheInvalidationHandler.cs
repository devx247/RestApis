using CqrsMediatR.Contracts;
using CqrsMediatR.Notifications;
using MediatR;

namespace CqrsMediatR.Handlers;

public class CacheInvalidationHandler : INotificationHandler<ProductAddedNotification>
{
    private readonly IDataStore _dataStore;

    public CacheInvalidationHandler(IDataStore dataStore)
    {
        _dataStore = dataStore;
    }
    public async Task Handle(ProductAddedNotification notification, CancellationToken cancellationToken)
    {
        await _dataStore.UpdateEventOccured(notification.Product, "Cache Invalidated");
    }
}
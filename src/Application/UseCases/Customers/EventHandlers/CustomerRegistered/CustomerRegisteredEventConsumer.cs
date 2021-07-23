using System.Threading.Tasks;
using Application.Interfaces.Customers;
using Application.UseCases.Customers.Queries.GetCustomers;
using Domain.Entities.Customers;
using MassTransit;

namespace Application.UseCases.Customers.EventHandlers.CustomerRegistered
{
    public class CustomerRegisteredEventConsumer : IConsumer<Events.CustomerRegistered>
    {
        private readonly ICustomerEventStoreService _eventStoreService;
        private readonly ICustomerProjectionsService _projectionsService;

        public CustomerRegisteredEventConsumer(
            ICustomerEventStoreService eventStoreService,
            ICustomerProjectionsService projectionsService)
        {
            _eventStoreService = eventStoreService;
            _projectionsService = projectionsService;
        }

        public async Task Consume(ConsumeContext<Events.CustomerRegistered> context)
        {
            var (aggregateId, _, _) = context.Message;
            
            var customer = await _eventStoreService.LoadAggregateFromStreamAsync(aggregateId, context.CancellationToken);

            var customerModel = new Models.CustomerModel
            {
                Age = customer.Age,
                Name = customer.Name
            };

            await _projectionsService.ProjectNewCustomerAsync(customerModel, context.CancellationToken);
            await _projectionsService.ProjectCustomerListAsync(customerModel, context.CancellationToken);
        }
    }
}
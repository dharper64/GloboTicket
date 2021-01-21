using AutoMapper;
using GloboTicket.TicketManagement.Application.Contracts.Persistence;
using GloboTicket.TicketManagement.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GloboTicket.TicketManagement.Application.Features.Events.Queries.GetEventsList
{
    public class GetEventsListQueryHandler : IRequestHandler<GetEventsListQuery, List<EventListVm>>
    {
        private readonly IAsyncRepository<Event> _eventRepository;
        private readonly IMapper _mapper;

        public GetEventsListQueryHandler(IMapper mapper, IAsyncRepository<Event> eventRepository)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
        }

        public async Task<List<EventListVm>> Handle(GetEventsListQuery request, CancellationToken cancellationToken)
        {
            // Get list of event entities and order by date
            var allEvents = (await _eventRepository.ListAllAsync()).OrderBy(x => x.Date);
            // Use autoMapper to map allEvents entitry list to a list of Events as defined by the view model
            // autoMapper will automatically map properties if they have the same name, if not, a profile can ge used such as MappingPofile.cs
            return _mapper.Map<List<EventListVm>>(allEvents);
        }
    }
}

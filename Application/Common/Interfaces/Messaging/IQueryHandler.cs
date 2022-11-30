﻿using Domain.Shared;
using MediatR;

namespace Application.Common.Interfaces.Messaging
{
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> where TQuery : IQuery<TResponse>
    {
    }
}

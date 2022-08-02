﻿using E_Commerce.Application.Exceptions;
using E_Commerce.Application.Interfaces.Repositories;
using E_Commerce.Application.Wrappers;
using E_Commerce.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace E_Commerce.Application.Features.OrderItem.Queries.GetOrderItemById
{
    public class GetOrderItemByIdQuery : IRequest<Response<GetOrderItemViewModel>>
    {
        public int Id { get; set; }
        public class GetProductByIdQueryHandler : IRequestHandler<GetOrderItemByIdQuery, Response<GetOrderItemViewModel>>
        {
            private readonly IOrderItemRepositoryAsync _orderItemRepository;
            public GetProductByIdQueryHandler(IOrderItemRepositoryAsync orderItemRepository)
            {
                _orderItemRepository = orderItemRepository;
            }
            public async Task<Response<GetOrderItemViewModel>> Handle(GetOrderItemByIdQuery query, CancellationToken cancellationToken)
            {
                var orderItem = await _orderItemRepository.GetOrderItem(query.Id);
                if (orderItem == null) throw new ApiException($"Order Item Not Found.");
                return new Response<GetOrderItemViewModel>(orderItem);
            }
        }
    }
}

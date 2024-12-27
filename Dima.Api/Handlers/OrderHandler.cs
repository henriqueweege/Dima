using Dima.Api.Data;
using Dima.Core.Handlers;
using Dima.Core.Models;
using Dima.Core.Requests.Orders;
using Dima.Core.Requests.Stripe;
using Dima.Core.Responses;
using Microsoft.EntityFrameworkCore;

namespace Dima.Api.Handlers
{
    public class OrderHandler(AppDbContext context, IStripeHandler stripeHandler) : IOrderHandler
    {
        public async Task<Response<Order?>> HandleAsync(CancelOrderRequest request)
        {
            Order? order;

            try
            {
                order = await context.Orders.Include(x => x.Product).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);

                if(order is null) 
                {
                    return new Response<Order?>(null, 404, "Pedido não encontrado");
                }
            }
            catch
            {

                return new Response<Order?>(null, 500, "Erro ao obter pedido");
            }

            switch (order.Status)
            {
                case Core.Enums.EOrderStatus.Calceled:
                    return new Response<Order?>(null, 400, "Pedido já foi cancelado.");
                case Core.Enums.EOrderStatus.Paid:
                    return new Response<Order?>(null, 400, "Pedido já foi pago e não pode ser cancelado.");
                case Core.Enums.EOrderStatus.Refunded:
                    return new Response<Order?>(null, 400, "Pedido já foi estornado e não pode ser cancelado.");
                default:
                    break;
            }

            order.Status = Core.Enums.EOrderStatus.Calceled;
            order.UpdatedAt = DateTime.Now;

            try
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
            catch
            {

                return new Response<Order?>(order, 500, "Erro ao obter pedido");

            }

            return new Response<Order?>(order, 200, $"Pedido {order.Number} cancelado com sucesso");

        }

        public async Task<Response<Order?>> HandleAsync(CreateOrderRequest request)
        {
            Product? product;

            try
            {
                product = await context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.ProductId && x.IsActive);

                if(product is null)
                {
                    return new Response<Order?>(null, 404, "Produto não encontrado");
                }
                
                context.Attach(product);

            }
            catch
            {
                return new Response<Order?>(null, 500, "Erro ao buscar produto");
            }

            Voucher? voucher = default!;
            try
            {
                if(request.VoucherId is not null)
                {
                    voucher = await context.Vouchers.AsNoTracking().FirstOrDefaultAsync(x => x.IsActive && x.Id == request.VoucherId);

                    if(voucher is null || !voucher.IsActive)
                    {
                        return new Response<Order?>(null, 404, "Voucher inválido ou não encontrado");
                    }

                    voucher.IsActive = false;

                    context.Vouchers.Update(voucher);
                }
            }
            catch
            {
                return new Response<Order?>(null, 500, "Erro ao buscar voucher");
            }

            var order = new Order()
            {
                UserId = request.UserId,
                Product = product,
                ProductId = product.Id,
                Voucher = voucher,
                VoucherId = voucher.Id
            };

            try
            {
                await context.Orders.AddAsync(order);
                await context.SaveChangesAsync();
            }
            catch
            {
                return new Response<Order?>(null, 500, "Erro ao criar pedido");

            }

            return new Response<Order?>(order, 200, $"Pedido {order.Number} criado com sucesso");

        }

        public async Task<Response<Order?>> HandleAsync(PayOrderRequest request)
        {
            Order? order;
            try
            {
                order = await context.Orders.Include(x => x.Product).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (order is null)
                {
                    return new Response<Order?>(null, 404, "Voucher inválido ou não encontrado");
                }
            }
            catch
            {
                return new Response<Order?>(null, 500, "Erro ao recuperar pedido");
            }

            switch (order.Status)
            {
                case Core.Enums.EOrderStatus.Calceled:
                    return new Response<Order?>(order , 400, "Pedido cancelado não pode ser pago");
                case Core.Enums.EOrderStatus.Paid:
                    return new Response<Order?>(order, 400, "Pedido já pago não pode ser pago");
                case Core.Enums.EOrderStatus.Refunded:
                    return new Response<Order?>(order, 400, "Pedido reenmbolsado não pode ser pago");
                default:
                    break;
            }

            try
            {
                var res = await stripeHandler.HandleAsync(new GetTransactionsByOrderNumberRequest { Number = order.Number });

                if (!res.IsSuccess || res.Data is null)
                {
                    return new Response<Order?>(null, 500, res.Message);
                }

                if (res.Data.Any(x => x.Refunded) || !res.Data.Any(x=>x.Paid))
                {
                    return new Response<Order?>(null, 500, "Não é possível processar pagamentos.");

                }

                request.ExternalReference = res.Data[0].Id;

            }
            catch
            {
                return new Response<Order?>(null, 500, "Não é possível dar baixa no pedido.");
            }

            order.Status = Core.Enums.EOrderStatus.Paid;
            order.ExternalReference = request.ExternalReference;
            order.UpdatedAt = DateTime.Now;

            try
            {
                context.Orders.Update(order);

                await context.SaveChangesAsync();
            }
            catch
            {
                return new Response<Order?>(order, 500, "Falha ao pagar pedido");
            }

            return new Response<Order?>(order, 200, $"Pedido {order.Number} pago com sucesso");

        }

        public async Task<Response<Order?>> HandleAsync(RefundOrderRequest request)
        {
            Order? order;

            try
            {
                order = await context.Orders.Include(x => x.Product).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Id == request.Id && x.UserId == request.UserId);
                if (order is null)
                {
                    return new Response<Order?>(null, 404, "Voucher inválido ou não encontrado");
                }
            }
            catch 
            {
                return new Response<Order?>(null, 500, "Erro ao recuperar pedido");
            }

            switch (order.Status)
            {
                case Core.Enums.EOrderStatus.Calceled:
                    return new Response<Order?>(order, 400, "Pedido cancelado não pode ser estornado");
                case Core.Enums.EOrderStatus.Refunded:
                    return new Response<Order?>(order, 400, "Pedido estornado não pode ser estornado");
                case Core.Enums.EOrderStatus.WaitingPayment:
                    return new Response<Order?>(order, 400, "Pedido não pago não pode ser estornado");
                default:
                    break;
            }

            order.Status = Core.Enums.EOrderStatus.Refunded;
            order.UpdatedAt = DateTime.Now;

            try
            {
                context.Orders.Update(order);
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return new Response<Order?>(order, 500, "Falha ao reembolsar pedido");
            }

            return new Response<Order?>(order, 200, $"Pedido {order.Number} reembolsado");

        }

        public async Task<PagedResponse<List<Order?>>> HandleAsync(GetAllOrdersRequest request)
        {
            try
            {
                var query = context.Orders.AsNoTracking().Include(x => x.Product).Include(x => x.Voucher).Where(x => x.UserId == request.UserId).OrderByDescending(x => x.CreatedAt);

                var orders = await query.Skip((request.PageNumber - 1) * request.PageSize).Take(request.PageSize).ToListAsync();

                var count = await query.CountAsync();

                return new PagedResponse<List<Order?>>(request.PageNumber, request.PageSize, count, orders, 200);
            }
            catch (Exception)
            {
                return new PagedResponse<List<Order?>>(null, 500, "Falha ao buscar pedidos");
            }
        }

        public async Task<Response<Order?>> HandleAsync(GetOrderByNumberRequest request)
        {
            try
            {
                var order = await context.Orders.AsNoTracking().Include(x => x.Product).Include(x => x.Voucher).FirstOrDefaultAsync(x => x.Number == request.Number && x.UserId == request.UserId);

                return order is null ? new Response<Order?>(null, 404, "pedido não encontrado") : new Response<Order?>(order, 200);
            }
            catch
            {
                return new Response<Order?>(null, 500, "Falha ao buscar pedido");
            }
        }
    }
}

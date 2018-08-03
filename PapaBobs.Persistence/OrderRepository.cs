﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PapaBobs.DTO;

namespace PapaBobs.Persistence
{
    public class OrderRepository
    {
        public static void CreateOrder(DTO.OrderDTO orderDTO)
        {
            var db = new PapaBobsDbEntities();

            var order = convertToEntity(orderDTO);

            db.Orders.Add(order);
            db.SaveChanges();
        }

        private static Order convertToEntity(OrderDTO orderDTO)
        {
            var order = new Order();

            order.OrderId = orderDTO.OrderId;
            order.Size = orderDTO.Size;
            order.Crust = orderDTO.Crust;
            order.Sausage = orderDTO.Sausage;
            order.Pepperoni = orderDTO.Pepperoni;
            order.Onions = orderDTO.Onions;
            order.GreenPeppers = orderDTO.GreenPeppers;
            order.Name = orderDTO.Name;
            order.Address = orderDTO.Address;
            order.ZipCode = orderDTO.ZipCode;
            order.Phone = orderDTO.Phone;
            order.TotalCost = orderDTO.TotalCost;
            order.PaymentType = orderDTO.PaymentType;
            order.Completed = orderDTO.Completed;

            return order;
        }

        public static void CompleteOrder(Guid orderId)
        {
            var db = new PapaBobsDbEntities();
            var order = db.Orders.FirstOrDefault(p => p.OrderId == orderId);
            order.Completed = true;
            db.SaveChanges();
        }

        public static object GetOrders()
        {
            var db = new PapaBobsDbEntities();
            var orders = db.Orders.Where(p => p.Completed == false).ToList();
            var ordersDTO = convertToDTO(orders);
            return ordersDTO;
        }

        private static List<DTO.OrderDTO> convertToDTO(List<Order> orders)
        {
            var ordersDTO = new List<DTO.OrderDTO>();

            foreach (var order in orders)
            {
                var orderDto = new DTO.OrderDTO();

                orderDto.OrderId = order.OrderId;
                orderDto.Size = order.Size;
                orderDto.Crust = order.Crust;
                orderDto.Sausage = order.Sausage;
                orderDto.Pepperoni = order.Pepperoni;
                orderDto.Onions = order.Onions;
                orderDto.GreenPeppers = order.GreenPeppers;
                orderDto.Name = order.Name;
                orderDto.Address = order.Address;
                orderDto.ZipCode = order.ZipCode;
                orderDto.Phone = order.Phone;
                orderDto.PaymentType = order.PaymentType;
                orderDto.TotalCost = order.TotalCost;

                ordersDTO.Add(orderDto);
            }

            return ordersDTO;
        }
    }
}

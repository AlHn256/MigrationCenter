using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLibrary.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLibrary.Repos
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MC_ContextDb db;

        public OrderRepository(MC_ContextDb dbContext)
        {
            db = dbContext;
        }

        public async Task Create(Order order)
        {
            List<OrderItem> orderItems = order.OrderItems;
            db.Order.Add(order);
            db.OrderItem.AddRange(orderItems);
            await db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Order order = await db.Order.FindAsync(id);
            if (order != null)
            {
                db.Order.Remove(order);
                await db.SaveChangesAsync();
            }
        }

        public async Task<Order> GetRecord(int id)
        {
            Order order = await db.Order.FindAsync(id);
            order.OrderItems = db.OrderItem.Where(p => p.OrderId == order.Id).ToList();

            return order;
        }

        public async Task Update(Order updatedorder)
        {
            var original = await db.Order.FindAsync(updatedorder.Id);

            if (original != null)
            {
                // db.Entry(original).CurrentValues.SetValues(updatedorder);
                original.Name = updatedorder.Name;
                original.IsDelete = updatedorder.IsDelete;
                original.Status = updatedorder.Status;

                await db.SaveChangesAsync();
            }
        }

        public IQueryable<Order> Orders
        {
            get
            {
                return db.Order.Where(c => c.OrderItems != null);
            }
        }

        //IEnumerable IOrderRepository.GetOrderList()
        //{
        //    List<Order> orders = db.Order.ToList();
        //    foreach (Order item in orders)
        //    {
        //        item.OrderItems = db.OrderItem.Where(p => p.OrderId == item.Id).ToList();
        //    }
        //    return orders;
        //}
    }
}

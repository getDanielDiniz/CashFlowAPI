﻿using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infraestructure.DataAccess;
internal class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }

    public CashFlowDbContext(DbContextOptions options) : base(options){ }
}

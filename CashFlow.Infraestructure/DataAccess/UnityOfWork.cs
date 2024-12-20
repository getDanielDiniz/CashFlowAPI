﻿using CashFlow.Domain.Repositories;

namespace CashFlow.Infraestructure.DataAccess;
internal class UnityOfWork : IUnitOfWork
{
    private readonly CashFlowDbContext _context;
    public UnityOfWork(CashFlowDbContext context)
    {
        _context = context;
    }

    public async Task Commit()
    {
       await _context.SaveChangesAsync();
    }
}
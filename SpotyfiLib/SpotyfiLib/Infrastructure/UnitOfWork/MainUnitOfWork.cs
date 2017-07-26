using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Text;

namespace SpotyfiLib.Infrastructure.UnitOfWork
{
    public class MainUnitOfWork : DbContext, IUnitOfWork
    {
        public FirstGenUnitOfWork _firstGenUoW {get; private set;}
        public SecondGenUnitOfWork _secondGenUoW {get; private set;}

        public MainUnitOfWork(FirstGenUnitOfWork firstUoW, SecondGenUnitOfWork secondUoW){
            _firstGenUoW = firstUoW;
            _secondGenUoW = secondUoW;
        }

        public void Commit()
        {
            SaveChanges();
        }

        public void Rollback()
        {
            Dispose();
        }

        public IDbContextTransaction BeginTransaction(int gen){
            IDbContextTransaction transaction  = null;

            switch(gen)
            {
                case 1:
                        Console.WriteLine("Goes to first gen. The break of sequence.");
                        transaction = _firstGenUoW.Database.BeginTransaction();
                        break;
                case 2:
                        Console.WriteLine("Begin transaction 2: accessing record and artist");
                        transaction = _firstGenUoW.Database.BeginTransaction();
                        _secondGenUoW.Database.UseTransaction(transaction.GetDbTransaction());
                        break;
            }
            Console.WriteLine($"Transaction: ", transaction);
            return transaction;
        }
    }
}
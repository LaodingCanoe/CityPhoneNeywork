﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CityPhoneNeywork
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SharpovEntities : DbContext
    {

        public static SharpovEntities _context;
        public static SharpovEntities GetContext()
        {
            if (_context == null)
                _context = new SharpovEntities();
            return _context;
        }
        public SharpovEntities()
            : base("name=SharpovEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ATS> ATS { get; set; }
        public virtual DbSet<Call_Log> Call_Log { get; set; }
        public virtual DbSet<City> City { get; set; }
        public virtual DbSet<Price_List> Price_List { get; set; }
        public virtual DbSet<Subscribers> Subscribers { get; set; }
        public virtual DbSet<Subscription_Billing> Subscription_Billing { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}
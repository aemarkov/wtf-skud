﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace skud
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Cards> Cards { get; set; }
        public virtual DbSet<Departments> Departments { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<Ranks> Ranks { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<WorkShifts> WorkShifts { get; set; }
    }
}
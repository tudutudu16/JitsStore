﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace JitsStore.Models;

public partial class JITS_STORE : DbContext
{
    public JITS_STORE()
    {
    }

    public JITS_STORE(DbContextOptions<JITS_STORE> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=JITS_STORE;User=sa;Password=161198;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("CATEGORIES");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(20)
                .HasColumnName("CATEGORY_NAME");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("DESCRIPTION");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("CUSTOMERS");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Age).HasColumnName("AGE");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(50)
                .HasColumnName("CUSTOMER_NAME");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONE");
            entity.Property(e => e.Sex).HasColumnName("SEX");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("EMPLOYEES");

            entity.Property(e => e.EmployeeId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("BIRTH_DATE");
            entity.Property(e => e.ContactType)
                .HasMaxLength(30)
                .HasColumnName("CONTACT_TYPE");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONE");
            entity.Property(e => e.Photo)
                .HasColumnType("image")
                .HasColumnName("PHOTO");
            entity.Property(e => e.Salary)
                .HasColumnType("money")
                .HasColumnName("SALARY");
            entity.Property(e => e.Status).HasColumnName("STATUS");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("ORDERS");

            entity.Property(e => e.OrderId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ORDER_ID");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CUSTOMER_ID");
            entity.Property(e => e.EmployeeId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("EMPLOYEE_ID");
            entity.Property(e => e.Note)
                .HasColumnType("ntext")
                .HasColumnName("NOTE");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATE");
            entity.Property(e => e.ShipAddress)
                .HasMaxLength(50)
                .HasColumnName("SHIP_ADDRESS");
            entity.Property(e => e.ShippedDate)
                .HasColumnType("datetime")
                .HasColumnName("SHIPPED_DATE");
            entity.Property(e => e.ShipperId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SHIPPER_ID");
            entity.Property(e => e.ShippingFee)
                .HasColumnType("money")
                .HasColumnName("SHIPPING_FEE");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_ORDERS_CUSTOMERS");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_ORDERS_EMPLOYEES");

            entity.HasOne(d => d.Shipper).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipperId)
                .HasConstraintName("FK_ORDERS_SHIPPER");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.ToTable("ORDER_DETAIL");

            entity.Property(e => e.OrderId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("ORDER_ID");
            entity.Property(e => e.ProductId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.Discount).HasColumnName("DISCOUNT");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_DETAIL_ORDERS");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ORDER_DETAIL_PRODUCTS");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.ToTable("PRODUCTS");

            entity.Property(e => e.ProductId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PRODUCT_ID");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("CATEGORY_ID");
            entity.Property(e => e.Picture)
                .HasColumnType("image")
                .HasColumnName("PICTURE");
            entity.Property(e => e.Price)
                .HasColumnType("money")
                .HasColumnName("PRICE");
            entity.Property(e => e.ProductName)
                .HasMaxLength(50)
                .HasColumnName("PRODUCT_NAME");
            entity.Property(e => e.QuantityOfOrder)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("QUANTITY_OF_ORDER");
            entity.Property(e => e.QuantityOfStock).HasColumnName("QUANTITY_OF_STOCK");
            entity.Property(e => e.Status).HasColumnName("STATUS");
            entity.Property(e => e.SupplierId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.Unit).HasColumnName("UNIT");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_PRODUCTS_CATEGORIES");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK_PRODUCTS_SUPPLIERS");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.ToTable("SHIPPER");

            entity.Property(e => e.ShipperId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SHIPPER_ID");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("NAME");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONE");
            entity.Property(e => e.Region)
                .HasMaxLength(50)
                .HasColumnName("REGION");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.ToTable("SUPPLIERS");

            entity.Property(e => e.SupplierId)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("SUPPLIER_ID");
            entity.Property(e => e.Address)
                .HasMaxLength(50)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.City)
                .HasMaxLength(30)
                .HasColumnName("CITY");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(50)
                .HasColumnName("COMPANY_NAME");
            entity.Property(e => e.Country)
                .HasMaxLength(50)
                .HasColumnName("COUNTRY");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("PHONE");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

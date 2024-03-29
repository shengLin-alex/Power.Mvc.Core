﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Power.Mvc.Helper;
using Power.Mvc.Helper.Extensions;

namespace Power.Repository.EFCore
{
    /// <summary>
    /// PostgreSQL DbContext 基底
    /// </summary>
    public class PostgreSqlDbContextBase : DbContext
    {
        /// <summary>
        /// 連線字串
        /// </summary>
        private readonly string ConnectionString;
        
        /// <summary>
        /// LoggerFactory
        /// </summary>
        private ILoggerFactory LoggerFactory => PackageDiResolver.Current.GetService<ILoggerFactory>();

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="connectionString">連線字串</param>
        public PostgreSqlDbContextBase(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        /// <summary>
        ///     <para>
        ///         Override this method to configure the database (and other options) to be used for this context.
        ///         This method is called for each instance of the context that is created.
        ///         The base implementation does nothing.
        ///     </para>
        ///     <para>
        ///         In situations where an instance of <see cref="T:Microsoft.EntityFrameworkCore.DbContextOptions" /> may or may not have been passed
        ///         to the constructor, you can use <see cref="P:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.IsConfigured" /> to determine if
        ///         the options have already been set, and skip some or all of the logic in
        ///         <see cref="M:Microsoft.EntityFrameworkCore.DbContext.OnConfiguring(Microsoft.EntityFrameworkCore.DbContextOptionsBuilder)" />.
        ///     </para>
        /// </summary>
        /// <param name="optionsBuilder">
        ///     A builder used to create or modify options for this context. Databases (and other extensions)
        ///     typically define extension methods on this object that allow you to configure the context.
        /// </param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(this.ConnectionString);
            optionsBuilder.UseLoggerFactory(this.LoggerFactory);
        }
    }
}
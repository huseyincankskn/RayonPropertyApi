using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.Concrete.EntityFramework.Configuration.Extensions
{
    public static class PropertyExtensions
    {
        public static void AccountNumber<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("varchar(26)");
        }

        public static void AccountSuffix<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("varchar(5)");
        }

        public static void Phone<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("char(10)");
        }

        public static void Iban<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("varchar(26)");
        }

        public static void Vkn<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("varchar(10)");
        }

        public static void Tckn<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("varchar(11)");
        }

        public static void BankCode<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("varchar(4)");
        }

        public static void BranchCode<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("varchar(5)");
        }

        public static void Date<TProperty>(this PropertyBuilder<TProperty> property)
        {
            property.HasColumnType("datetime").HasDefaultValueSql("getdate()");
        }

        public static void VarChar<TProperty>(this PropertyBuilder<TProperty> property, int size)
        {
            property.HasColumnType($"varchar({size})");
        }

        public static void NVarChar<TProperty>(this PropertyBuilder<TProperty> property, int size)
        {
            property.HasColumnType($"nvarchar({size})");
        }
    }
}
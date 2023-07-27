using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using XmTestTask.Core.Entities;

namespace XmTestTask.Infrastructure.Data.Config
{
    public class BTCPriceConfiguration : IEntityTypeConfiguration<BTCPrice>
    {
        public void Configure(EntityTypeBuilder<BTCPrice> builder)
        {
            builder.HasKey(p => p.Date);

            builder.Property(p => p.Date).ValueGeneratedNever();

            builder.Property(p => p.Price)
                .IsRequired();
        }
    }
}

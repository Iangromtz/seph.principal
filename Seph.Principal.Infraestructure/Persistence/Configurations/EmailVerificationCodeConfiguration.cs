using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Seph.Principal.Domain.Entities;

namespace Seph.Principal.Infraestructure.Persistence.Configurations
{
    public sealed class EmailVerificationCodeConfiguration : IEntityTypeConfiguration<EmailVerificationCode>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationCode> builder)
        {
            builder.ToTable("EmailVerificationCodes", "security");
            builder.HasKey(code => code.Id);

            builder.Property(code => code.CodeHash).HasMaxLength(128).IsRequired();
            builder.Property(code => code.Status).HasConversion<string>().HasMaxLength(32).IsRequired();

            builder.HasIndex(code => new { code.UserId, code.Status });
        }
    }
}
using BookWeb.Application.Interfaces.Services;
using System;

namespace BookWeb.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}
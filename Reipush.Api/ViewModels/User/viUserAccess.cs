using Reipush.Api.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reipush.Api.ViewModels.User
{
    public class viUserAccess
    {
        public viUserAccess()
        {
            refreshAccesToken = new RefreshAccessToken();
        }
        public int UserId { get; set; }
        public RefreshAccessToken refreshAccesToken { get; set; }
    }
}

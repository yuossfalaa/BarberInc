﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services.DomainServices.UserServices
{
    public interface IUserService : IDataService<User>
    {
        Task<User> GetByEmail(string Email);

    }
}

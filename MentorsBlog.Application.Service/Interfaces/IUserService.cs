using System;
using System.Collections.Generic;
using MentorsBlog.Application.Service.Models;

namespace MentorsBlog.Application.Service.Interfaces
{
    public interface IUserService
    {
        bool IsUserExist(string nickname);
        string Registration(User user);
        string Authorization(User user);
    }
}
using NaijaFarmers.Model.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaijaFarmers.Model.Repository
{
    public interface IUser
    {
        UserModel Creatuser(UserModel userModel);

        UserModel GetUser(UserModel userModel);
    }
}

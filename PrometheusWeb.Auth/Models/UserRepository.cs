using PrometheusWeb.Data;
using PrometheusWeb.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrometheusWeb.Auth.Models
{
    public class UserRepository : IDisposable
    {
        // UserEntities - your context class
        PrometheusEntities context = new PrometheusEntities();

        //To check and validate the user credentials
        public User ValidateUser(string username, string password)
        {
            return context.Users.FirstOrDefault(user =>
            user.UserID.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.Password == password);
        }
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
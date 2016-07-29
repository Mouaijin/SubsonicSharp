using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SubsonicSharp.SubTypes;

namespace SubsonicSharp.ActionGroups
{
    public class UserManagement
    {
        public SubsonicClient Client { get; set; }

        public UserManagement(SubsonicClient client)
        {
            Client = client;
        }

        public User GetUser(string username)
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getUser";
            command.AddParameter("username", username);
            return User.Create(Client.GetResponseXDocument(command).RealRoot());
        }

        public IEnumerable<User> GetUsers()
        {
            RestCommand command = new RestCommand();
            command.MethodName = "getUsers";
            foreach (XElement element in Client.GetResponseXDocument(command).RealRoot().Elements())
            {
                yield return User.Create(element);
            }
        }


    }
}

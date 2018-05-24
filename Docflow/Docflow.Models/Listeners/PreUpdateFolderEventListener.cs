using Docflow.Models.Repositories;
using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docflow.Models.Listeners
{
    [Listener(ListenerType = ListenerType.PreUpdate)]
    public class PreUpdateFolderEventListener : IPreUpdateEventListener
    {    

        public bool OnPreUpdate(PreUpdateEvent @event)
        {
            return SetProps(@event);
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            return new Task<bool>(() => {
                return SetProps(@event);
            });
        }

        private bool SetProps(PreUpdateEvent @event)
        {
            var folder = @event.Entity as Folder;
            if (folder != null)
            {
                folder.ChangeDate = DateTime.Now;
                folder.ChangeAuthor = DependencyResolver. UserRepository.GetCurrentUser();
            }
            return false;
        }
    }
}

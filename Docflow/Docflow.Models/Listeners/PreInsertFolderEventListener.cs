using NHibernate.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Docflow.Models.Listeners
{
    [Listener(ListenerType = ListenerType.PreInsert)]
    public class PreInsertFolderEventListener : IPreInsertEventListener
    {
        public bool OnPreInsert(PreInsertEvent @event)
        {
            return SetCreationProps(@event);
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {   
            return new Task<bool>(() => {
                return SetCreationProps(@event);                
            });
        }

        private bool SetCreationProps(PreInsertEvent @event)
        {            
            var folder = @event.Entity as Folder;
            if (folder != null)
            {
                folder.CreationDate = DateTime.Now;
            }
            return false;
        }
    }
}

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
    public class PreSaveFolderListener : IPreInsertEventListener
    {
        public bool OnPreInsert(PreInsertEvent @event)
        {
            var folder = @event.Entity as Folder;
            if (folder != null)
            {
                SetCreationProps(folder);
            }
            return false;
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {   
            return new Task<bool>(() => {
                var folder = @event.Entity as Folder;
                if (folder != null)
                {
                    SetCreationProps(folder);
                }
                return false;
            });
        }

        private void SetCreationProps(Folder folder)
        {
            folder.CreationDate = DateTime.Now;
        }
    }
}

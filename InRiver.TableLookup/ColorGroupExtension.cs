using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inRiver.Remoting.Extension;
using inRiver.Remoting.Extension.Interface;
using inRiver.Remoting.Objects;

namespace InRiver.TableLookupExtension
{
    public class ColorGroupExtension : IEntityListener
    {
        public inRiverContext Context { get; set; }

        public Dictionary<string, string> DefaultSettings { get; }

        public string Test()
        {
            throw new NotImplementedException();
        }

        public void EntityCreated(int entityId)
        {
            
        }

        public void EntityUpdated(int entityId, string[] fields)
        {
        }

        public void EntityDeleted(Entity deletedEntity)
        {
        }

        public void EntityLocked(int entityId)
        {
        }

        public void EntityUnlocked(int entityId)
        {
        }

        public void EntityFieldSetUpdated(int entityId, string fieldSetId)
        {
        }

        public void EntityCommentAdded(int entityId, int commentId)
        {
        }

        public void EntitySpecificationFieldAdded(int entityId, string fieldName)
        {
        }

        public void EntitySpecificationFieldUpdated(int entityId, string fieldName)
        {
        }
    }
}

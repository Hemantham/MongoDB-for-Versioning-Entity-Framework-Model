using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIG4.Framework.Entities.Attributes
{
   public class ParentAttribute : Attribute
{
   public readonly Type  Parent;

   public ParentAttribute(Type parent)
   {
       this.Parent = parent;
   }


}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MilestoneCST247.Models
{
    public class Error
    {

        /** Error model class **/

        private string content;

        public Error(string content)
        {
            this.content = content;
        }

        public string Content { get => content; set => content = value; }
    }
}
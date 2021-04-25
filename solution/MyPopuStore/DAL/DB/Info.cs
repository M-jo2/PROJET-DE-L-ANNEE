using System;
using System.Collections.Generic;

#nullable disable

namespace MyPopuStore.DAL.DB
{
    public partial class Info
    {
        public string PopupStoreName { get; set; }
        public DateTime CreationDate { get; set; }

        public Info(string popupStoreName, DateTime creationDate)
        {
            PopupStoreName = popupStoreName;
            CreationDate = creationDate;
        }
    }
}

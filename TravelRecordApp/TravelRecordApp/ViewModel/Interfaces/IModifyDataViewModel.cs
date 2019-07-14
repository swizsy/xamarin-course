using System;
using System.Collections.Generic;
using System.Text;

namespace TravelRecordApp.ViewModel.Interfaces
{
    public enum Modification
    {
        UPDATE,
        DELETE
    }

    public interface IModifyDataViewModel
    {
        void Modify(Modification modification);
    }
}

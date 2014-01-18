using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AppraiseUtah.Models;
using AppraiseUtah.ServiceModels;

namespace AppraiseUtah.Utilities
{
    public static class GetAppraiserForDisplay
    {

        #region Methods

        public static Appraiser GetAppraiser(int id)
        {
            AppraisalServiceModel appraisalServiceModel = new AppraisalServiceModel();
            return appraisalServiceModel.Get_Appraiser(id);
        }


        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMSCore.BAL
{
    public enum StoredProcedures
    {
        [Description("OMMS_CheckUserExistBasedOnUserNamePassword")]
        AuthenticateUser,





        [Description("OMMS_GetRegionDetails")]
        OMMS_GetRegionDetails,

        [Description("OMMS_GetAllCurrency")]
        OMMS_GetAllCurrency,
        [Description("OMMS_GetTimeZoneDetail")]
        OMMS_GetTimeZoneDetail,

        [Description("OMMS_ResetUserPassword")]
        OMMS_ResetUserPassword,

        [Description("OMMS_CheckUserExistBasedOnUserNamePassword")]
        OMMS_CheckUserExistBasedOnUserNamePassword,
        [Description("OMMS_ChangePassword")]
        OMMS_ChangePassword,
        [Description("OMMS_GetAllModule")]
        OMMS_GetAllModule,

        [Description("OMMS_CheckUserUsingEmailAndToken")]
        OMMS_CheckUserUsingEmailAndToken,

        //---------------------------------------------------

        [Description("OMMS_GetAllCountry")]
        OMMS_GetAllCountry,
        [Description("OMMS_SaveUpdateCountreyDetail")]
        OMMS_SaveUpdateCountreyDetail,
        [Description("OMMS_GetCountryBaseOnId")]
        OMMS_GetCountryBaseOnId,
        [Description("OMMS_DeleteCountryDetail")]
        OMMS_DeleteCountryDetail,

        //-------------------------Area-------------------------

        [Description("OMMS_GetAllArea")]
        OMMS_GetAllArea,
        [Description("OMMS_GetAllCity")]
        OMMS_GetAllCity,
        [Description("OMMS_GetAllCircle")]
        OMMS_GetAllCircle,
        [Description("OMMS_SaveUpdateAreaDetail")]
        OMMS_SaveUpdateAreaDetail,
        [Description("OMMS_DeleteAreaDetail")]
        OMMS_DeleteAreaDetail,
        [Description("OMMS_GetAreaBaseOnId")]
        OMMS_GetAreaBaseOnId,
        [Description("OMMS_GetAllCountryBaseOnId")]
        OMMS_GetAllCountryBaseOnId,
        [Description("OMMS_GetAllCircleBaseOnId")]
        OMMS_GetAllCircleBaseOnId,
        [Description("OMMS_GetAllCityBaseOnId")]
        OMMS_GetAllCityBaseOnId,

        //-----------Circle-----------Neha------

        //[Description("OMMS_GetAllCircleCountry")]
        [Description("OMMS_GetAllCircle")]
        GetAllCircleCountry,

        [Description("OMMS_SaveUpdateCircleDetail")]
        SaveUpdateCircleDetail,


        //[Description("OMMS_SaveUpdateCircleDetail")]
        //SaveUpdateCircleDetail,

        [Description("OMMS_GetAllCountry")]
        GetAllCountry,

        [Description("OMMS_GetCircleBaseOnId")]
        GetCircleBaseOnId,

        //OMMS_DeleteCicleDetail

        [Description("OMMS_DeleteCicleDetail")]
        DeleteCicleDetail,

        //-------State---------Neha-----


        [Description("[OMMS_GetAllState]")]
        GetAllStatelist,

        // save and update

        [Description("[OMMS_SaveUpdateStateDetail]")]
        SaveUpdateStateDetail,

        //edit OMMS_GetStateBaseOnId

        [Description("[OMMS_GetStateBaseOnId]")]
        GetStateBaseOnId,

        //OMMS_DeleteStateDetail

        [Description("[OMMS_DeleteStateDetail]")]
        DeleteStateDetail,


        //Operator  List 

        [Description("[OMMS_GetOperatordetailCountry_test]")]
        GetOperatordetailCountry,

        //save or upodate

        [Description("[OMMS_SaveUpdateOperaterDetail]")]
        SaveUpdateOperaterDetail,

        //edit

        [Description("[OMMS_GetOperatordetail]")]
        GetOperatordetail,

        //delete OMMS_DeleteOperatorDetail

        [Description("[OMMS_DeleteOperatorDetail]")]
        DeleteOperatorDetail,

        [Description("OMMS_GetAllCityType")]
        OMMS_GetAllCityType,
        [Description("OMMS_SaveUpdateCityDetail")]
        OMMS_SaveUpdateCityDetail,
        [Description("OMMS_GetCityBaseOnId")]
        OMMS_GetCityBaseOnId,
        [Description("OMMS_DeleteCityDetail_Kritika")]  //OMMS_DeleteCityDetail
        OMMS_DeleteCityDetail,
        [Description("OMMS_GetCustomerdetailBasedOnCountry")]
        OMMS_GetCustomerdetailBasedOnCountry,
        [Description("OMMS_GetAllCompanydetails")]
        OMMS_GetAllCompanydetails,
        [Description("OMMS_SaveUpdateCustomerDetail")]
        OMMS_SaveUpdateCustomerDetail,
        [Description("OMMS_GetCustomerdetail")]
        OMMS_GetCustomerdetail,
        [Description("OMMS_DeleteCustomerDetail")]
        OMMS_DeleteCustomerDetail,
        //[Description("CVMS_UploadLineItem")]
        CVMS_UploadLineItem,
        [Description("OMMS_GetAllUOM")]
        OMMS_GetAllUOM,
        [Description("OMMS_GetAllState")]
        OMMS_GetAllState,


        //----------------------

        [Description("OMMS_GetAllCircleCountry")]
        OMMS_GetAllCircleCountry,

        [Description("OMMS_GetNatures")]
        OMMS_GetNatures,

        [Description("OMMS_GetAllCountryBsedonComp")]
        OMMS_GetAllCountryBsedonComp,

        [Description("OMMS_GetVendordetail")]
        OMMS_GetVendordetail,

        [Description("OMMS_DeleteVendorDetail")]
        OMMS_DeleteVendorDetail,

        [Description("OMMS_GetVendordetailNew")]
        OMMS_GetVendordetailNew,

        [Description("OMMS_SaveUpdateVendorDetail")]
        OMMS_SaveUpdateVendorDetail,

        [Description("OMMS_SaveUpdateVendorSubDetail")]
        OMMS_SaveUpdateVendorSubDetail,

        [Description("OMMS_SaveUpdateVendorDetail_New1")]
        OMMS_SaveUpdateVendorDetail_New1,


    }
}

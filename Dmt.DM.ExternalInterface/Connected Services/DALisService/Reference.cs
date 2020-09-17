//------------------------------------------------------------------------------
// <自动生成>
//     此代码由工具生成。
//     //
//     对此文件的更改可能导致不正确的行为，并在以下条件下丢失:
//     代码重新生成。
// </自动生成>
//------------------------------------------------------------------------------

namespace DALisService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ArrayOfString", Namespace="http://report.dagene.net/", ItemName="string")]
    public class ArrayOfString : System.Collections.Generic.List<string>
    {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://report.dagene.net/", ConfigurationName="DALisService.RasClientDetailSoap")]
    public interface RasClientDetailSoap
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetIfReleasedAllByBarcode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetIfReleasedAllByBarcodeResponse> GetIfReleasedAllByBarcodeAsync(DALisService.GetIfReleasedAllByBarcodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportFileForXA", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportFileForXAResponse> GetReportFileForXAAsync(DALisService.GetReportFileForXARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetFileBase64ByName", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetFileBase64ByNameResponse> GetFileBase64ByNameAsync(DALisService.GetFileBase64ByNameRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByCodeDA2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByCodeDA2Response> GetDetailByCodeDA2Async(DALisService.GetDetailByCodeDA2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataTCTByBarcodes", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTByBarcodesResponse> GetDetailDataTCTByBarcodesAsync(DALisService.GetDetailDataTCTByBarcodesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetHPVDataByBarcodes", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetHPVDataByBarcodesResponse> GetHPVDataByBarcodesAsync(DALisService.GetHPVDataByBarcodesRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData6ByPage", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData6ByPageResponse> GetDetailData6ByPageAsync(DALisService.GetDetailData6ByPageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailResultForXK", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailResultForXKResponse> GetDetailResultForXKAsync(DALisService.GetDetailResultForXKRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetPathDetailData", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataResponse> GetPathDetailDataAsync(DALisService.GetPathDetailDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetPathDetailDataWithPic", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataWithPicResponse> GetPathDetailDataWithPicAsync(DALisService.GetPathDetailDataWithPicRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetPathDetailDataWithPicByPage", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataWithPicByPageResponse> GetPathDetailDataWithPicByPageAsync(DALisService.GetPathDetailDataWithPicByPageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportJpegByClinicid", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportJpegByClinicidResponse> GetReportJpegByClinicidAsync(DALisService.GetReportJpegByClinicidRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportJpegByClinicid2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportJpegByClinicid2Response> GetReportJpegByClinicid2Async(DALisService.GetReportJpegByClinicid2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetCheckPData", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetCheckPDataResponse> GetCheckPDataAsync(DALisService.GetCheckPDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetSept9PDFReport", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetSept9PDFReportResponse> GetSept9PDFReportAsync(DALisService.GetSept9PDFReportRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataByHospGroup", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospGroupResponse> GetDetailDataByHospGroupAsync(DALisService.GetDetailDataByHospGroupRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetSampleInfoFromDA", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDAResponse> GetSampleInfoFromDAAsync(DALisService.GetSampleInfoFromDARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/RunLimsFunction", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.RunLimsFunctionResponse> RunLimsFunctionAsync(DALisService.RunLimsFunctionRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataMic3", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMic3Response> GetDetailDataMic3Async(DALisService.GetDetailDataMic3Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportPathByDate", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportPathByDateResponse> GetReportPathByDateAsync(DALisService.GetReportPathByDateRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataTS2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTS2Response> GetDetailDataTS2Async(DALisService.GetDetailDataTS2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataGS", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataGSResponse> GetDetailDataGSAsync(DALisService.GetDetailDataGSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataRST", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataRSTResponse> GetDetailDataRSTAsync(DALisService.GetDetailDataRSTRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetSampleBackInfo", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetSampleBackInfoResponse> GetSampleBackInfoAsync(DALisService.GetSampleBackInfoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/SampleBackConfirm", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.SampleBackConfirmResponse> SampleBackConfirmAsync(DALisService.SampleBackConfirmRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetSampleInfoBySend", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoBySendResponse> GetSampleInfoBySendAsync(DALisService.GetSampleInfoBySendRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetNameJpg", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetNameJpgResponse> GetNameJpgAsync(DALisService.GetNameJpgRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByHospCode2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCode2Response> GetDetailByHospCode2Async(DALisService.GetDetailByHospCode2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetSampleInfoFromDA2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDA2Response> GetSampleInfoFromDA2Async(DALisService.GetSampleInfoFromDA2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData9", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData9Response> GetDetailData9Async(DALisService.GetDetailData9Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByHospCode3", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCode3Response> GetDetailByHospCode3Async(DALisService.GetDetailByHospCode3Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetSampleInfoFromDA3", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDA3Response> GetSampleInfoFromDA3Async(DALisService.GetSampleInfoFromDA3Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetPathDetailDataSR", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataSRResponse> GetPathDetailDataSRAsync(DALisService.GetPathDetailDataSRRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData10", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData10Response> GetDetailData10Async(DALisService.GetDetailData10Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByHospCode4", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCode4Response> GetDetailByHospCode4Async(DALisService.GetDetailByHospCode4Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataByHospBarcode3", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcode3Response> GetDetailDataByHospBarcode3Async(DALisService.GetDetailDataByHospBarcode3Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDABarcodeByHospCode2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospCode2Response> GetDABarcodeByHospCode2Async(DALisService.GetDABarcodeByHospCode2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataMicByHospBarcode2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMicByHospBarcode2Response> GetDetailDataMicByHospBarcode2Async(DALisService.GetDetailDataMicByHospBarcode2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByCode2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByCode2Response> GetDetailByCode2Async(DALisService.GetDetailByCode2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetResultPicByBarcode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetResultPicByBarcodeResponse> GetResultPicByBarcodeAsync(DALisService.GetResultPicByBarcodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/HelloWorld", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.HelloWorldResponse> HelloWorldAsync(DALisService.HelloWorldRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByCode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByCodeResponse> GetDetailByCodeAsync(DALisService.GetDetailByCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByCodeDA", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByCodeDAResponse> GetDetailByCodeDAAsync(DALisService.GetDetailByCodeDARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData4", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData4Response> GetDetailData4Async(DALisService.GetDetailData4Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData5", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData5Response> GetDetailData5Async(DALisService.GetDetailData5Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataResponse> GetDetailDataAsync(DALisService.GetDetailDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/ExportBySerialNumber", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.ExportBySerialNumberResponse> ExportBySerialNumberAsync(DALisService.ExportBySerialNumberRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData2Response> GetDetailData2Async(DALisService.GetDetailData2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData3", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData3Response> GetDetailData3Async(DALisService.GetDetailData3Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData6", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData6Response> GetDetailData6Async(DALisService.GetDetailData6Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData7", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData7Response> GetDetailData7Async(DALisService.GetDetailData7Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData8", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData8Response> GetDetailData8Async(DALisService.GetDetailData8Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataTS", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTSResponse> GetDetailDataTSAsync(DALisService.GetDetailDataTSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataTCT", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTResponse> GetDetailDataTCTAsync(DALisService.GetDetailDataTCTRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataTCTWithPic", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTWithPicResponse> GetDetailDataTCTWithPicAsync(DALisService.GetDetailDataTCTWithPicRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReporFile", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReporFileResponse> GetReporFileAsync(DALisService.GetReporFileRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportListDA", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportListDAResponse> GetReportListDAAsync(DALisService.GetReportListDARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportFileDA", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportFileDAResponse> GetReportFileDAAsync(DALisService.GetReportFileDARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportFileDA2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportFileDA2Response> GetReportFileDA2Async(DALisService.GetReportFileDA2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetPathWithPic", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetPathWithPicResponse> GetPathWithPicAsync(DALisService.GetPathWithPicRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataByIdNum", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByIdNumResponse> GetDetailDataByIdNumAsync(DALisService.GetDetailDataByIdNumRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataByHospBarcode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcodeResponse> GetDetailDataByHospBarcodeAsync(DALisService.GetDetailDataByHospBarcodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataByHospInfo", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospInfoResponse> GetDetailDataByHospInfoAsync(DALisService.GetDetailDataByHospInfoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataByBarCode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByBarCodeResponse> GetDetailDataByBarCodeAsync(DALisService.GetDetailDataByBarCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailByHospCode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCodeResponse> GetDetailByHospCodeAsync(DALisService.GetDetailByHospCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GenQRCbyOHBarcode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GenQRCbyOHBarcodeResponse> GenQRCbyOHBarcodeAsync(DALisService.GenQRCbyOHBarcodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataMic", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMicResponse> GetDetailDataMicAsync(DALisService.GetDetailDataMicRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataTCTByBarcode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTByBarcodeResponse> GetDetailDataTCTByBarcodeAsync(DALisService.GetDetailDataTCTByBarcodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetHPVData", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetHPVDataResponse> GetHPVDataAsync(DALisService.GetHPVDataRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataForHealth", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataForHealthResponse> GetDetailDataForHealthAsync(DALisService.GetDetailDataForHealthRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataByHospBarcode2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcode2Response> GetDetailDataByHospBarcode2Async(DALisService.GetDetailDataByHospBarcode2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportJpegDA", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportJpegDAResponse> GetReportJpegDAAsync(DALisService.GetReportJpegDARequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportJpegDA2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportJpegDA2Response> GetReportJpegDA2Async(DALisService.GetReportJpegDA2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDABarcodeByHospCode", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospCodeResponse> GetDABarcodeByHospCodeAsync(DALisService.GetDABarcodeByHospCodeRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDABarcodeByHospInfo", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospInfoResponse> GetDABarcodeByHospInfoAsync(DALisService.GetDABarcodeByHospInfoRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataMic2", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMic2Response> GetDetailDataMic2Async(DALisService.GetDetailDataMic2Request request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailData5ByPage", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailData5ByPageResponse> GetDetailData5ByPageAsync(DALisService.GetDetailData5ByPageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetPathWithPicByPage", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetPathWithPicByPageResponse> GetPathWithPicByPageAsync(DALisService.GetPathWithPicByPageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetDetailDataTCTWithPicByPage", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTWithPicByPageResponse> GetDetailDataTCTWithPicByPageAsync(DALisService.GetDetailDataTCTWithPicByPageRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetPathJpegZip", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetPathJpegZipResponse> GetPathJpegZipAsync(DALisService.GetPathJpegZipRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://report.dagene.net/GetReportJpegForBJ", ReplyAction="*")]
        System.Threading.Tasks.Task<DALisService.GetReportJpegForBJResponse> GetReportJpegForBJAsync(DALisService.GetReportJpegForBJRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetIfReleasedAllByBarcodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIfReleasedAllByBarcode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetIfReleasedAllByBarcodeRequestBody Body;
        
        public GetIfReleasedAllByBarcodeRequest()
        {
        }
        
        public GetIfReleasedAllByBarcodeRequest(DALisService.GetIfReleasedAllByBarcodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetIfReleasedAllByBarcodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        public GetIfReleasedAllByBarcodeRequestBody()
        {
        }
        
        public GetIfReleasedAllByBarcodeRequestBody(string ClientID, string ClientGUID, string BarCode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetIfReleasedAllByBarcodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetIfReleasedAllByBarcodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetIfReleasedAllByBarcodeResponseBody Body;
        
        public GetIfReleasedAllByBarcodeResponse()
        {
        }
        
        public GetIfReleasedAllByBarcodeResponse(DALisService.GetIfReleasedAllByBarcodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetIfReleasedAllByBarcodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetIfReleasedAllByBarcodeResult;
        
        public GetIfReleasedAllByBarcodeResponseBody()
        {
        }
        
        public GetIfReleasedAllByBarcodeResponseBody(string GetIfReleasedAllByBarcodeResult)
        {
            this.GetIfReleasedAllByBarcodeResult = GetIfReleasedAllByBarcodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportFileForXARequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportFileForXA", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportFileForXARequestBody Body;
        
        public GetReportFileForXARequest()
        {
        }
        
        public GetReportFileForXARequest(DALisService.GetReportFileForXARequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportFileForXARequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string type;
        
        public GetReportFileForXARequestBody()
        {
        }
        
        public GetReportFileForXARequestBody(string ClientID, string ClientGUID, string BarCode, string type)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportFileForXAResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportFileForXAResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportFileForXAResponseBody Body;
        
        public GetReportFileForXAResponse()
        {
        }
        
        public GetReportFileForXAResponse(DALisService.GetReportFileForXAResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportFileForXAResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportFileForXAResult;
        
        public GetReportFileForXAResponseBody()
        {
        }
        
        public GetReportFileForXAResponseBody(string GetReportFileForXAResult)
        {
            this.GetReportFileForXAResult = GetReportFileForXAResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetFileBase64ByNameRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetFileBase64ByName", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetFileBase64ByNameRequestBody Body;
        
        public GetFileBase64ByNameRequest()
        {
        }
        
        public GetFileBase64ByNameRequest(DALisService.GetFileBase64ByNameRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetFileBase64ByNameRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string fileName;
        
        public GetFileBase64ByNameRequestBody()
        {
        }
        
        public GetFileBase64ByNameRequestBody(string ClientID, string ClientGUID, string fileName)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.fileName = fileName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetFileBase64ByNameResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetFileBase64ByNameResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetFileBase64ByNameResponseBody Body;
        
        public GetFileBase64ByNameResponse()
        {
        }
        
        public GetFileBase64ByNameResponse(DALisService.GetFileBase64ByNameResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetFileBase64ByNameResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetFileBase64ByNameResult;
        
        public GetFileBase64ByNameResponseBody()
        {
        }
        
        public GetFileBase64ByNameResponseBody(string GetFileBase64ByNameResult)
        {
            this.GetFileBase64ByNameResult = GetFileBase64ByNameResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCodeDA2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCodeDA2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCodeDA2RequestBody Body;
        
        public GetDetailByCodeDA2Request()
        {
        }
        
        public GetDetailByCodeDA2Request(DALisService.GetDetailByCodeDA2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCodeDA2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        public GetDetailByCodeDA2RequestBody()
        {
        }
        
        public GetDetailByCodeDA2RequestBody(string ClientId, string ClientGUID, string BarCode)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCodeDA2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCodeDA2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCodeDA2ResponseBody Body;
        
        public GetDetailByCodeDA2Response()
        {
        }
        
        public GetDetailByCodeDA2Response(DALisService.GetDetailByCodeDA2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCodeDA2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByCodeDA2Result;
        
        public GetDetailByCodeDA2ResponseBody()
        {
        }
        
        public GetDetailByCodeDA2ResponseBody(string GetDetailByCodeDA2Result)
        {
            this.GetDetailByCodeDA2Result = GetDetailByCodeDA2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTByBarcodesRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTByBarcodes", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTByBarcodesRequestBody Body;
        
        public GetDetailDataTCTByBarcodesRequest()
        {
        }
        
        public GetDetailDataTCTByBarcodesRequest(DALisService.GetDetailDataTCTByBarcodesRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTByBarcodesRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string barcodes;
        
        public GetDetailDataTCTByBarcodesRequestBody()
        {
        }
        
        public GetDetailDataTCTByBarcodesRequestBody(string ClientID, string ClientGUID, string barcodes)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.barcodes = barcodes;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTByBarcodesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTByBarcodesResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTByBarcodesResponseBody Body;
        
        public GetDetailDataTCTByBarcodesResponse()
        {
        }
        
        public GetDetailDataTCTByBarcodesResponse(DALisService.GetDetailDataTCTByBarcodesResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTByBarcodesResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataTCTByBarcodesResult;
        
        public GetDetailDataTCTByBarcodesResponseBody()
        {
        }
        
        public GetDetailDataTCTByBarcodesResponseBody(string GetDetailDataTCTByBarcodesResult)
        {
            this.GetDetailDataTCTByBarcodesResult = GetDetailDataTCTByBarcodesResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetHPVDataByBarcodesRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetHPVDataByBarcodes", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetHPVDataByBarcodesRequestBody Body;
        
        public GetHPVDataByBarcodesRequest()
        {
        }
        
        public GetHPVDataByBarcodesRequest(DALisService.GetHPVDataByBarcodesRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetHPVDataByBarcodesRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string barcodes;
        
        public GetHPVDataByBarcodesRequestBody()
        {
        }
        
        public GetHPVDataByBarcodesRequestBody(string ClientID, string ClientGUID, string barcodes)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.barcodes = barcodes;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetHPVDataByBarcodesResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetHPVDataByBarcodesResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetHPVDataByBarcodesResponseBody Body;
        
        public GetHPVDataByBarcodesResponse()
        {
        }
        
        public GetHPVDataByBarcodesResponse(DALisService.GetHPVDataByBarcodesResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetHPVDataByBarcodesResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetHPVDataByBarcodesResult;
        
        public GetHPVDataByBarcodesResponseBody()
        {
        }
        
        public GetHPVDataByBarcodesResponseBody(string GetHPVDataByBarcodesResult)
        {
            this.GetHPVDataByBarcodesResult = GetHPVDataByBarcodesResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData6ByPageRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData6ByPage", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData6ByPageRequestBody Body;
        
        public GetDetailData6ByPageRequest()
        {
        }
        
        public GetDetailData6ByPageRequest(DALisService.GetDetailData6ByPageRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData6ByPageRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int num;
        
        public GetDetailData6ByPageRequestBody()
        {
        }
        
        public GetDetailData6ByPageRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, int num)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.num = num;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData6ByPageResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData6ByPageResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData6ByPageResponseBody Body;
        
        public GetDetailData6ByPageResponse()
        {
        }
        
        public GetDetailData6ByPageResponse(DALisService.GetDetailData6ByPageResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData6ByPageResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData6ByPageResult;
        
        public GetDetailData6ByPageResponseBody()
        {
        }
        
        public GetDetailData6ByPageResponseBody(string GetDetailData6ByPageResult)
        {
            this.GetDetailData6ByPageResult = GetDetailData6ByPageResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailResultForXKRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailResultForXK", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailResultForXKRequestBody Body;
        
        public GetDetailResultForXKRequest()
        {
        }
        
        public GetDetailResultForXKRequest(DALisService.GetDetailResultForXKRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailResultForXKRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DataType;
        
        public GetDetailResultForXKRequestBody()
        {
        }
        
        public GetDetailResultForXKRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DataType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DataType = DataType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailResultForXKResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailResultForXKResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailResultForXKResponseBody Body;
        
        public GetDetailResultForXKResponse()
        {
        }
        
        public GetDetailResultForXKResponse(DALisService.GetDetailResultForXKResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailResultForXKResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailResultForXKResult;
        
        public GetDetailResultForXKResponseBody()
        {
        }
        
        public GetDetailResultForXKResponseBody(string GetDetailResultForXKResult)
        {
            this.GetDetailResultForXKResult = GetDetailResultForXKResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailData", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataRequestBody Body;
        
        public GetPathDetailDataRequest()
        {
        }
        
        public GetPathDetailDataRequest(DALisService.GetPathDetailDataRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        public GetPathDetailDataRequestBody()
        {
        }
        
        public GetPathDetailDataRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailDataResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataResponseBody Body;
        
        public GetPathDetailDataResponse()
        {
        }
        
        public GetPathDetailDataResponse(DALisService.GetPathDetailDataResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetPathDetailDataResult;
        
        public GetPathDetailDataResponseBody()
        {
        }
        
        public GetPathDetailDataResponseBody(string GetPathDetailDataResult)
        {
            this.GetPathDetailDataResult = GetPathDetailDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataWithPicRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailDataWithPic", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataWithPicRequestBody Body;
        
        public GetPathDetailDataWithPicRequest()
        {
        }
        
        public GetPathDetailDataWithPicRequest(DALisService.GetPathDetailDataWithPicRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataWithPicRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        public GetPathDetailDataWithPicRequestBody()
        {
        }
        
        public GetPathDetailDataWithPicRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataWithPicResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailDataWithPicResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataWithPicResponseBody Body;
        
        public GetPathDetailDataWithPicResponse()
        {
        }
        
        public GetPathDetailDataWithPicResponse(DALisService.GetPathDetailDataWithPicResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataWithPicResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetPathDetailDataWithPicResult;
        
        public GetPathDetailDataWithPicResponseBody()
        {
        }
        
        public GetPathDetailDataWithPicResponseBody(string GetPathDetailDataWithPicResult)
        {
            this.GetPathDetailDataWithPicResult = GetPathDetailDataWithPicResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataWithPicByPageRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailDataWithPicByPage", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataWithPicByPageRequestBody Body;
        
        public GetPathDetailDataWithPicByPageRequest()
        {
        }
        
        public GetPathDetailDataWithPicByPageRequest(DALisService.GetPathDetailDataWithPicByPageRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataWithPicByPageRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int num;
        
        public GetPathDetailDataWithPicByPageRequestBody()
        {
        }
        
        public GetPathDetailDataWithPicByPageRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, int num)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
            this.num = num;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataWithPicByPageResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailDataWithPicByPageResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataWithPicByPageResponseBody Body;
        
        public GetPathDetailDataWithPicByPageResponse()
        {
        }
        
        public GetPathDetailDataWithPicByPageResponse(DALisService.GetPathDetailDataWithPicByPageResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataWithPicByPageResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetPathDetailDataWithPicByPageResult;
        
        public GetPathDetailDataWithPicByPageResponseBody()
        {
        }
        
        public GetPathDetailDataWithPicByPageResponseBody(string GetPathDetailDataWithPicByPageResult)
        {
            this.GetPathDetailDataWithPicByPageResult = GetPathDetailDataWithPicByPageResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegByClinicidRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegByClinicid", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegByClinicidRequestBody Body;
        
        public GetReportJpegByClinicidRequest()
        {
        }
        
        public GetReportJpegByClinicidRequest(DALisService.GetReportJpegByClinicidRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegByClinicidRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Clinicid;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte type;
        
        public GetReportJpegByClinicidRequestBody()
        {
        }
        
        public GetReportJpegByClinicidRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, string Clinicid, byte type)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Clinicid = Clinicid;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegByClinicidResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegByClinicidResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegByClinicidResponseBody Body;
        
        public GetReportJpegByClinicidResponse()
        {
        }
        
        public GetReportJpegByClinicidResponse(DALisService.GetReportJpegByClinicidResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegByClinicidResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportJpegByClinicidResult;
        
        public GetReportJpegByClinicidResponseBody()
        {
        }
        
        public GetReportJpegByClinicidResponseBody(string GetReportJpegByClinicidResult)
        {
            this.GetReportJpegByClinicidResult = GetReportJpegByClinicidResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegByClinicid2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegByClinicid2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegByClinicid2RequestBody Body;
        
        public GetReportJpegByClinicid2Request()
        {
        }
        
        public GetReportJpegByClinicid2Request(DALisService.GetReportJpegByClinicid2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegByClinicid2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string Clinicid;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte type;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public bool IsMicroPic;
        
        public GetReportJpegByClinicid2RequestBody()
        {
        }
        
        public GetReportJpegByClinicid2RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, string Clinicid, byte type, bool IsMicroPic)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.Clinicid = Clinicid;
            this.type = type;
            this.IsMicroPic = IsMicroPic;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegByClinicid2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegByClinicid2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegByClinicid2ResponseBody Body;
        
        public GetReportJpegByClinicid2Response()
        {
        }
        
        public GetReportJpegByClinicid2Response(DALisService.GetReportJpegByClinicid2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegByClinicid2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportJpegByClinicid2Result;
        
        public GetReportJpegByClinicid2ResponseBody()
        {
        }
        
        public GetReportJpegByClinicid2ResponseBody(string GetReportJpegByClinicid2Result)
        {
            this.GetReportJpegByClinicid2Result = GetReportJpegByClinicid2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCheckPDataRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCheckPData", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetCheckPDataRequestBody Body;
        
        public GetCheckPDataRequest()
        {
        }
        
        public GetCheckPDataRequest(DALisService.GetCheckPDataRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetCheckPDataRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Barcode;
        
        public GetCheckPDataRequestBody()
        {
        }
        
        public GetCheckPDataRequestBody(string ClientID, string ClientGUID, string Barcode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.Barcode = Barcode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetCheckPDataResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetCheckPDataResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetCheckPDataResponseBody Body;
        
        public GetCheckPDataResponse()
        {
        }
        
        public GetCheckPDataResponse(DALisService.GetCheckPDataResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetCheckPDataResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetCheckPDataResult;
        
        public GetCheckPDataResponseBody()
        {
        }
        
        public GetCheckPDataResponseBody(string GetCheckPDataResult)
        {
            this.GetCheckPDataResult = GetCheckPDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSept9PDFReportRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSept9PDFReport", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSept9PDFReportRequestBody Body;
        
        public GetSept9PDFReportRequest()
        {
        }
        
        public GetSept9PDFReportRequest(DALisService.GetSept9PDFReportRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSept9PDFReportRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        public GetSept9PDFReportRequestBody()
        {
        }
        
        public GetSept9PDFReportRequestBody(string ClientID, string ClientGUID, string BarCode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSept9PDFReportResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSept9PDFReportResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSept9PDFReportResponseBody Body;
        
        public GetSept9PDFReportResponse()
        {
        }
        
        public GetSept9PDFReportResponse(DALisService.GetSept9PDFReportResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSept9PDFReportResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetSept9PDFReportResult;
        
        public GetSept9PDFReportResponseBody()
        {
        }
        
        public GetSept9PDFReportResponseBody(string GetSept9PDFReportResult)
        {
            this.GetSept9PDFReportResult = GetSept9PDFReportResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospGroupRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospGroup", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospGroupRequestBody Body;
        
        public GetDetailDataByHospGroupRequest()
        {
        }
        
        public GetDetailDataByHospGroupRequest(DALisService.GetDetailDataByHospGroupRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospGroupRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int num;
        
        public GetDetailDataByHospGroupRequestBody()
        {
        }
        
        public GetDetailDataByHospGroupRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, int num)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.num = num;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospGroupResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospGroupResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospGroupResponseBody Body;
        
        public GetDetailDataByHospGroupResponse()
        {
        }
        
        public GetDetailDataByHospGroupResponse(DALisService.GetDetailDataByHospGroupResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospGroupResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataByHospGroupResult;
        
        public GetDetailDataByHospGroupResponseBody()
        {
        }
        
        public GetDetailDataByHospGroupResponseBody(string GetDetailDataByHospGroupResult)
        {
            this.GetDetailDataByHospGroupResult = GetDetailDataByHospGroupResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoFromDARequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoFromDA", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoFromDARequestBody Body;
        
        public GetSampleInfoFromDARequest()
        {
        }
        
        public GetSampleInfoFromDARequest(DALisService.GetSampleInfoFromDARequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoFromDARequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string User;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string PassWord;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Dept;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Barcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string DivideDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string StartTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string EndTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string page;
        
        public GetSampleInfoFromDARequestBody()
        {
        }
        
        public GetSampleInfoFromDARequestBody(string User, string PassWord, string Dept, string Barcode, string DivideDate, string StartTime, string EndTime, string page)
        {
            this.User = User;
            this.PassWord = PassWord;
            this.Dept = Dept;
            this.Barcode = Barcode;
            this.DivideDate = DivideDate;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.page = page;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoFromDAResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoFromDAResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoFromDAResponseBody Body;
        
        public GetSampleInfoFromDAResponse()
        {
        }
        
        public GetSampleInfoFromDAResponse(DALisService.GetSampleInfoFromDAResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoFromDAResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetSampleInfoFromDAResult;
        
        public GetSampleInfoFromDAResponseBody()
        {
        }
        
        public GetSampleInfoFromDAResponseBody(string GetSampleInfoFromDAResult)
        {
            this.GetSampleInfoFromDAResult = GetSampleInfoFromDAResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RunLimsFunctionRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RunLimsFunction", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.RunLimsFunctionRequestBody Body;
        
        public RunLimsFunctionRequest()
        {
        }
        
        public RunLimsFunctionRequest(DALisService.RunLimsFunctionRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class RunLimsFunctionRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string User;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string PassWord;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string FuncID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public DALisService.ArrayOfString ParamInfo;
        
        public RunLimsFunctionRequestBody()
        {
        }
        
        public RunLimsFunctionRequestBody(string User, string PassWord, string FuncID, DALisService.ArrayOfString ParamInfo)
        {
            this.User = User;
            this.PassWord = PassWord;
            this.FuncID = FuncID;
            this.ParamInfo = ParamInfo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class RunLimsFunctionResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="RunLimsFunctionResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.RunLimsFunctionResponseBody Body;
        
        public RunLimsFunctionResponse()
        {
        }
        
        public RunLimsFunctionResponse(DALisService.RunLimsFunctionResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class RunLimsFunctionResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string RunLimsFunctionResult;
        
        public RunLimsFunctionResponseBody()
        {
        }
        
        public RunLimsFunctionResponseBody(string RunLimsFunctionResult)
        {
            this.RunLimsFunctionResult = RunLimsFunctionResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMic3Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMic3", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMic3RequestBody Body;
        
        public GetDetailDataMic3Request()
        {
        }
        
        public GetDetailDataMic3Request(DALisService.GetDetailDataMic3RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMic3RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailDataMic3RequestBody()
        {
        }
        
        public GetDetailDataMic3RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMic3Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMic3Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMic3ResponseBody Body;
        
        public GetDetailDataMic3Response()
        {
        }
        
        public GetDetailDataMic3Response(DALisService.GetDetailDataMic3ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMic3ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataMic3Result;
        
        public GetDetailDataMic3ResponseBody()
        {
        }
        
        public GetDetailDataMic3ResponseBody(string GetDetailDataMic3Result)
        {
            this.GetDetailDataMic3Result = GetDetailDataMic3Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportPathByDateRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportPathByDate", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportPathByDateRequestBody Body;
        
        public GetReportPathByDateRequest()
        {
        }
        
        public GetReportPathByDateRequest(DALisService.GetReportPathByDateRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportPathByDateRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string SendDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string type;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int num;
        
        public GetReportPathByDateRequestBody()
        {
        }
        
        public GetReportPathByDateRequestBody(string ClientID, string ClientGUID, string SendDate, string type, int num)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.SendDate = SendDate;
            this.type = type;
            this.num = num;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportPathByDateResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportPathByDateResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportPathByDateResponseBody Body;
        
        public GetReportPathByDateResponse()
        {
        }
        
        public GetReportPathByDateResponse(DALisService.GetReportPathByDateResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportPathByDateResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportPathByDateResult;
        
        public GetReportPathByDateResponseBody()
        {
        }
        
        public GetReportPathByDateResponseBody(string GetReportPathByDateResult)
        {
            this.GetReportPathByDateResult = GetReportPathByDateResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTS2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTS2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTS2RequestBody Body;
        
        public GetDetailDataTS2Request()
        {
        }
        
        public GetDetailDataTS2Request(DALisService.GetDetailDataTS2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTS2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailDataTS2RequestBody()
        {
        }
        
        public GetDetailDataTS2RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTS2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTS2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTS2ResponseBody Body;
        
        public GetDetailDataTS2Response()
        {
        }
        
        public GetDetailDataTS2Response(DALisService.GetDetailDataTS2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTS2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataTS2Result;
        
        public GetDetailDataTS2ResponseBody()
        {
        }
        
        public GetDetailDataTS2ResponseBody(string GetDetailDataTS2Result)
        {
            this.GetDetailDataTS2Result = GetDetailDataTS2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataGSRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataGS", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataGSRequestBody Body;
        
        public GetDetailDataGSRequest()
        {
        }
        
        public GetDetailDataGSRequest(DALisService.GetDetailDataGSRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataGSRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailDataGSRequestBody()
        {
        }
        
        public GetDetailDataGSRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataGSResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataGSResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataGSResponseBody Body;
        
        public GetDetailDataGSResponse()
        {
        }
        
        public GetDetailDataGSResponse(DALisService.GetDetailDataGSResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataGSResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataGSResult;
        
        public GetDetailDataGSResponseBody()
        {
        }
        
        public GetDetailDataGSResponseBody(string GetDetailDataGSResult)
        {
            this.GetDetailDataGSResult = GetDetailDataGSResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataRSTRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataRST", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataRSTRequestBody Body;
        
        public GetDetailDataRSTRequest()
        {
        }
        
        public GetDetailDataRSTRequest(DALisService.GetDetailDataRSTRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataRSTRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailDataRSTRequestBody()
        {
        }
        
        public GetDetailDataRSTRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataRSTResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataRSTResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataRSTResponseBody Body;
        
        public GetDetailDataRSTResponse()
        {
        }
        
        public GetDetailDataRSTResponse(DALisService.GetDetailDataRSTResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataRSTResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataRSTResult;
        
        public GetDetailDataRSTResponseBody()
        {
        }
        
        public GetDetailDataRSTResponseBody(string GetDetailDataRSTResult)
        {
            this.GetDetailDataRSTResult = GetDetailDataRSTResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleBackInfoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleBackInfo", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleBackInfoRequestBody Body;
        
        public GetSampleBackInfoRequest()
        {
        }
        
        public GetSampleBackInfoRequest(DALisService.GetSampleBackInfoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleBackInfoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetSampleBackInfoRequestBody()
        {
        }
        
        public GetSampleBackInfoRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleBackInfoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleBackInfoResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleBackInfoResponseBody Body;
        
        public GetSampleBackInfoResponse()
        {
        }
        
        public GetSampleBackInfoResponse(DALisService.GetSampleBackInfoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleBackInfoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetSampleBackInfoResult;
        
        public GetSampleBackInfoResponseBody()
        {
        }
        
        public GetSampleBackInfoResponseBody(string GetSampleBackInfoResult)
        {
            this.GetSampleBackInfoResult = GetSampleBackInfoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SampleBackConfirmRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SampleBackConfirm", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.SampleBackConfirmRequestBody Body;
        
        public SampleBackConfirmRequest()
        {
        }
        
        public SampleBackConfirmRequest(DALisService.SampleBackConfirmRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class SampleBackConfirmRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string barcode;
        
        public SampleBackConfirmRequestBody()
        {
        }
        
        public SampleBackConfirmRequestBody(string ClientID, string ClientGUID, string barcode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.barcode = barcode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SampleBackConfirmResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SampleBackConfirmResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.SampleBackConfirmResponseBody Body;
        
        public SampleBackConfirmResponse()
        {
        }
        
        public SampleBackConfirmResponse(DALisService.SampleBackConfirmResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class SampleBackConfirmResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string SampleBackConfirmResult;
        
        public SampleBackConfirmResponseBody()
        {
        }
        
        public SampleBackConfirmResponseBody(string SampleBackConfirmResult)
        {
            this.SampleBackConfirmResult = SampleBackConfirmResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoBySendRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoBySend", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoBySendRequestBody Body;
        
        public GetSampleInfoBySendRequest()
        {
        }
        
        public GetSampleInfoBySendRequest(DALisService.GetSampleInfoBySendRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoBySendRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetSampleInfoBySendRequestBody()
        {
        }
        
        public GetSampleInfoBySendRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoBySendResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoBySendResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoBySendResponseBody Body;
        
        public GetSampleInfoBySendResponse()
        {
        }
        
        public GetSampleInfoBySendResponse(DALisService.GetSampleInfoBySendResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoBySendResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetSampleInfoBySendResult;
        
        public GetSampleInfoBySendResponseBody()
        {
        }
        
        public GetSampleInfoBySendResponseBody(string GetSampleInfoBySendResult)
        {
            this.GetSampleInfoBySendResult = GetSampleInfoBySendResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetNameJpgRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetNameJpg", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetNameJpgRequestBody Body;
        
        public GetNameJpgRequest()
        {
        }
        
        public GetNameJpgRequest(DALisService.GetNameJpgRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetNameJpgRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string userid;
        
        public GetNameJpgRequestBody()
        {
        }
        
        public GetNameJpgRequestBody(string ClientID, string ClientGUID, string userid)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.userid = userid;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetNameJpgResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetNameJpgResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetNameJpgResponseBody Body;
        
        public GetNameJpgResponse()
        {
        }
        
        public GetNameJpgResponse(DALisService.GetNameJpgResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetNameJpgResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetNameJpgResult;
        
        public GetNameJpgResponseBody()
        {
        }
        
        public GetNameJpgResponseBody(string GetNameJpgResult)
        {
            this.GetNameJpgResult = GetNameJpgResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCode2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCode2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCode2RequestBody Body;
        
        public GetDetailByHospCode2Request()
        {
        }
        
        public GetDetailByHospCode2Request(DALisService.GetDetailByHospCode2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCode2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string clinicid;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetDetailByHospCode2RequestBody()
        {
        }
        
        public GetDetailByHospCode2RequestBody(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.clinicid = clinicid;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCode2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCode2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCode2ResponseBody Body;
        
        public GetDetailByHospCode2Response()
        {
        }
        
        public GetDetailByHospCode2Response(DALisService.GetDetailByHospCode2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCode2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByHospCode2Result;
        
        public GetDetailByHospCode2ResponseBody()
        {
        }
        
        public GetDetailByHospCode2ResponseBody(string GetDetailByHospCode2Result)
        {
            this.GetDetailByHospCode2Result = GetDetailByHospCode2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoFromDA2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoFromDA2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoFromDA2RequestBody Body;
        
        public GetSampleInfoFromDA2Request()
        {
        }
        
        public GetSampleInfoFromDA2Request(DALisService.GetSampleInfoFromDA2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoFromDA2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string User;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string PassWord;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Dept;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string DivideDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string StartTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string EndTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public int PageSize;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public int PageIndex;
        
        public GetSampleInfoFromDA2RequestBody()
        {
        }
        
        public GetSampleInfoFromDA2RequestBody(string User, string PassWord, string Dept, string DivideDate, string StartTime, string EndTime, int PageSize, int PageIndex)
        {
            this.User = User;
            this.PassWord = PassWord;
            this.Dept = Dept;
            this.DivideDate = DivideDate;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.PageSize = PageSize;
            this.PageIndex = PageIndex;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoFromDA2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoFromDA2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoFromDA2ResponseBody Body;
        
        public GetSampleInfoFromDA2Response()
        {
        }
        
        public GetSampleInfoFromDA2Response(DALisService.GetSampleInfoFromDA2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoFromDA2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetSampleInfoFromDA2Result;
        
        public GetSampleInfoFromDA2ResponseBody()
        {
        }
        
        public GetSampleInfoFromDA2ResponseBody(string GetSampleInfoFromDA2Result)
        {
            this.GetSampleInfoFromDA2Result = GetSampleInfoFromDA2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData9Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData9", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData9RequestBody Body;
        
        public GetDetailData9Request()
        {
        }
        
        public GetDetailData9Request(DALisService.GetDetailData9RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData9RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailData9RequestBody()
        {
        }
        
        public GetDetailData9RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData9Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData9Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData9ResponseBody Body;
        
        public GetDetailData9Response()
        {
        }
        
        public GetDetailData9Response(DALisService.GetDetailData9ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData9ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData9Result;
        
        public GetDetailData9ResponseBody()
        {
        }
        
        public GetDetailData9ResponseBody(string GetDetailData9Result)
        {
            this.GetDetailData9Result = GetDetailData9Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCode3Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCode3", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCode3RequestBody Body;
        
        public GetDetailByHospCode3Request()
        {
        }
        
        public GetDetailByHospCode3Request(DALisService.GetDetailByHospCode3RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCode3RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string clinicid;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetDetailByHospCode3RequestBody()
        {
        }
        
        public GetDetailByHospCode3RequestBody(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.clinicid = clinicid;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCode3Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCode3Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCode3ResponseBody Body;
        
        public GetDetailByHospCode3Response()
        {
        }
        
        public GetDetailByHospCode3Response(DALisService.GetDetailByHospCode3ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCode3ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByHospCode3Result;
        
        public GetDetailByHospCode3ResponseBody()
        {
        }
        
        public GetDetailByHospCode3ResponseBody(string GetDetailByHospCode3Result)
        {
            this.GetDetailByHospCode3Result = GetDetailByHospCode3Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoFromDA3Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoFromDA3", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoFromDA3RequestBody Body;
        
        public GetSampleInfoFromDA3Request()
        {
        }
        
        public GetSampleInfoFromDA3Request(DALisService.GetSampleInfoFromDA3RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoFromDA3RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string User;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string PassWord;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string Dept;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string DivideDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string StartTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string EndTime;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public int PageSize;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public int PageIndex;
        
        public GetSampleInfoFromDA3RequestBody()
        {
        }
        
        public GetSampleInfoFromDA3RequestBody(string User, string PassWord, string Dept, string DivideDate, string StartTime, string EndTime, int PageSize, int PageIndex)
        {
            this.User = User;
            this.PassWord = PassWord;
            this.Dept = Dept;
            this.DivideDate = DivideDate;
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.PageSize = PageSize;
            this.PageIndex = PageIndex;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetSampleInfoFromDA3Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetSampleInfoFromDA3Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetSampleInfoFromDA3ResponseBody Body;
        
        public GetSampleInfoFromDA3Response()
        {
        }
        
        public GetSampleInfoFromDA3Response(DALisService.GetSampleInfoFromDA3ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetSampleInfoFromDA3ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetSampleInfoFromDA3Result;
        
        public GetSampleInfoFromDA3ResponseBody()
        {
        }
        
        public GetSampleInfoFromDA3ResponseBody(string GetSampleInfoFromDA3Result)
        {
            this.GetSampleInfoFromDA3Result = GetSampleInfoFromDA3Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataSRRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailDataSR", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataSRRequestBody Body;
        
        public GetPathDetailDataSRRequest()
        {
        }
        
        public GetPathDetailDataSRRequest(DALisService.GetPathDetailDataSRRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataSRRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        public GetPathDetailDataSRRequestBody()
        {
        }
        
        public GetPathDetailDataSRRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathDetailDataSRResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathDetailDataSRResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathDetailDataSRResponseBody Body;
        
        public GetPathDetailDataSRResponse()
        {
        }
        
        public GetPathDetailDataSRResponse(DALisService.GetPathDetailDataSRResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathDetailDataSRResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetPathDetailDataSRResult;
        
        public GetPathDetailDataSRResponseBody()
        {
        }
        
        public GetPathDetailDataSRResponseBody(string GetPathDetailDataSRResult)
        {
            this.GetPathDetailDataSRResult = GetPathDetailDataSRResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData10Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData10", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData10RequestBody Body;
        
        public GetDetailData10Request()
        {
        }
        
        public GetDetailData10Request(DALisService.GetDetailData10RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData10RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailData10RequestBody()
        {
        }
        
        public GetDetailData10RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData10Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData10Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData10ResponseBody Body;
        
        public GetDetailData10Response()
        {
        }
        
        public GetDetailData10Response(DALisService.GetDetailData10ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData10ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData10Result;
        
        public GetDetailData10ResponseBody()
        {
        }
        
        public GetDetailData10ResponseBody(string GetDetailData10Result)
        {
            this.GetDetailData10Result = GetDetailData10Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCode4Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCode4", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCode4RequestBody Body;
        
        public GetDetailByHospCode4Request()
        {
        }
        
        public GetDetailByHospCode4Request(DALisService.GetDetailByHospCode4RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCode4RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string clinicid;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetDetailByHospCode4RequestBody()
        {
        }
        
        public GetDetailByHospCode4RequestBody(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.clinicid = clinicid;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCode4Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCode4Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCode4ResponseBody Body;
        
        public GetDetailByHospCode4Response()
        {
        }
        
        public GetDetailByHospCode4Response(DALisService.GetDetailByHospCode4ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCode4ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByHospCode4Result;
        
        public GetDetailByHospCode4ResponseBody()
        {
        }
        
        public GetDetailByHospCode4ResponseBody(string GetDetailByHospCode4Result)
        {
            this.GetDetailByHospCode4Result = GetDetailByHospCode4Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospBarcode3Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospBarcode3", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospBarcode3RequestBody Body;
        
        public GetDetailDataByHospBarcode3Request()
        {
        }
        
        public GetDetailDataByHospBarcode3Request(DALisService.GetDetailDataByHospBarcode3RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospBarcode3RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospBarcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte SelectType;
        
        public GetDetailDataByHospBarcode3RequestBody()
        {
        }
        
        public GetDetailDataByHospBarcode3RequestBody(string ClientID, string ClientGUID, string hospBarcode, byte SelectType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospBarcode = hospBarcode;
            this.SelectType = SelectType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospBarcode3Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospBarcode3Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospBarcode3ResponseBody Body;
        
        public GetDetailDataByHospBarcode3Response()
        {
        }
        
        public GetDetailDataByHospBarcode3Response(DALisService.GetDetailDataByHospBarcode3ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospBarcode3ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataByHospBarcode3Result;
        
        public GetDetailDataByHospBarcode3ResponseBody()
        {
        }
        
        public GetDetailDataByHospBarcode3ResponseBody(string GetDetailDataByHospBarcode3Result)
        {
            this.GetDetailDataByHospBarcode3Result = GetDetailDataByHospBarcode3Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDABarcodeByHospCode2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDABarcodeByHospCode2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDABarcodeByHospCode2RequestBody Body;
        
        public GetDABarcodeByHospCode2Request()
        {
        }
        
        public GetDABarcodeByHospCode2Request(DALisService.GetDABarcodeByHospCode2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDABarcodeByHospCode2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospBarcode;
        
        public GetDABarcodeByHospCode2RequestBody()
        {
        }
        
        public GetDABarcodeByHospCode2RequestBody(string ClientID, string ClientGUID, string hospBarcode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospBarcode = hospBarcode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDABarcodeByHospCode2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDABarcodeByHospCode2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDABarcodeByHospCode2ResponseBody Body;
        
        public GetDABarcodeByHospCode2Response()
        {
        }
        
        public GetDABarcodeByHospCode2Response(DALisService.GetDABarcodeByHospCode2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDABarcodeByHospCode2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDABarcodeByHospCode2Result;
        
        public GetDABarcodeByHospCode2ResponseBody()
        {
        }
        
        public GetDABarcodeByHospCode2ResponseBody(string GetDABarcodeByHospCode2Result)
        {
            this.GetDABarcodeByHospCode2Result = GetDABarcodeByHospCode2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMicByHospBarcode2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMicByHospBarcode2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMicByHospBarcode2RequestBody Body;
        
        public GetDetailDataMicByHospBarcode2Request()
        {
        }
        
        public GetDetailDataMicByHospBarcode2Request(DALisService.GetDetailDataMicByHospBarcode2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMicByHospBarcode2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospBarcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string extend;
        
        public GetDetailDataMicByHospBarcode2RequestBody()
        {
        }
        
        public GetDetailDataMicByHospBarcode2RequestBody(string ClientID, string ClientGUID, string hospBarcode, string extend)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospBarcode = hospBarcode;
            this.extend = extend;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMicByHospBarcode2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMicByHospBarcode2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMicByHospBarcode2ResponseBody Body;
        
        public GetDetailDataMicByHospBarcode2Response()
        {
        }
        
        public GetDetailDataMicByHospBarcode2Response(DALisService.GetDetailDataMicByHospBarcode2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMicByHospBarcode2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataMicByHospBarcode2Result;
        
        public GetDetailDataMicByHospBarcode2ResponseBody()
        {
        }
        
        public GetDetailDataMicByHospBarcode2ResponseBody(string GetDetailDataMicByHospBarcode2Result)
        {
            this.GetDetailDataMicByHospBarcode2Result = GetDetailDataMicByHospBarcode2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCode2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCode2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCode2RequestBody Body;
        
        public GetDetailByCode2Request()
        {
        }
        
        public GetDetailByCode2Request(DALisService.GetDetailByCode2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCode2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetDetailByCode2RequestBody()
        {
        }
        
        public GetDetailByCode2RequestBody(string ClientId, string ClientGUID, string BarCode, byte type)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCode2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCode2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCode2ResponseBody Body;
        
        public GetDetailByCode2Response()
        {
        }
        
        public GetDetailByCode2Response(DALisService.GetDetailByCode2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCode2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByCode2Result;
        
        public GetDetailByCode2ResponseBody()
        {
        }
        
        public GetDetailByCode2ResponseBody(string GetDetailByCode2Result)
        {
            this.GetDetailByCode2Result = GetDetailByCode2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetResultPicByBarcodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetResultPicByBarcode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetResultPicByBarcodeRequestBody Body;
        
        public GetResultPicByBarcodeRequest()
        {
        }
        
        public GetResultPicByBarcodeRequest(DALisService.GetResultPicByBarcodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetResultPicByBarcodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string Extend;
        
        public GetResultPicByBarcodeRequestBody()
        {
        }
        
        public GetResultPicByBarcodeRequestBody(string ClientId, string ClientGUID, string BarCode, string Extend)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.Extend = Extend;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetResultPicByBarcodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetResultPicByBarcodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetResultPicByBarcodeResponseBody Body;
        
        public GetResultPicByBarcodeResponse()
        {
        }
        
        public GetResultPicByBarcodeResponse(DALisService.GetResultPicByBarcodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetResultPicByBarcodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetResultPicByBarcodeResult;
        
        public GetResultPicByBarcodeResponseBody()
        {
        }
        
        public GetResultPicByBarcodeResponseBody(string GetResultPicByBarcodeResult)
        {
            this.GetResultPicByBarcodeResult = GetResultPicByBarcodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorld", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.HelloWorldRequestBody Body;
        
        public HelloWorldRequest()
        {
        }
        
        public HelloWorldRequest(DALisService.HelloWorldRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute()]
    public partial class HelloWorldRequestBody
    {
        
        public HelloWorldRequestBody()
        {
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class HelloWorldResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="HelloWorldResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.HelloWorldResponseBody Body;
        
        public HelloWorldResponse()
        {
        }
        
        public HelloWorldResponse(DALisService.HelloWorldResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class HelloWorldResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string HelloWorldResult;
        
        public HelloWorldResponseBody()
        {
        }
        
        public HelloWorldResponseBody(string HelloWorldResult)
        {
            this.HelloWorldResult = HelloWorldResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCodeRequestBody Body;
        
        public GetDetailByCodeRequest()
        {
        }
        
        public GetDetailByCodeRequest(DALisService.GetDetailByCodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetDetailByCodeRequestBody()
        {
        }
        
        public GetDetailByCodeRequestBody(string ClientId, string ClientGUID, string BarCode, byte type)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCodeResponseBody Body;
        
        public GetDetailByCodeResponse()
        {
        }
        
        public GetDetailByCodeResponse(DALisService.GetDetailByCodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByCodeResult;
        
        public GetDetailByCodeResponseBody()
        {
        }
        
        public GetDetailByCodeResponseBody(string GetDetailByCodeResult)
        {
            this.GetDetailByCodeResult = GetDetailByCodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCodeDARequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCodeDA", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCodeDARequestBody Body;
        
        public GetDetailByCodeDARequest()
        {
        }
        
        public GetDetailByCodeDARequest(DALisService.GetDetailByCodeDARequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCodeDARequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        public GetDetailByCodeDARequestBody()
        {
        }
        
        public GetDetailByCodeDARequestBody(string ClientId, string ClientGUID, string BarCode)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByCodeDAResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByCodeDAResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByCodeDAResponseBody Body;
        
        public GetDetailByCodeDAResponse()
        {
        }
        
        public GetDetailByCodeDAResponse(DALisService.GetDetailByCodeDAResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByCodeDAResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByCodeDAResult;
        
        public GetDetailByCodeDAResponseBody()
        {
        }
        
        public GetDetailByCodeDAResponseBody(string GetDetailByCodeDAResult)
        {
            this.GetDetailByCodeDAResult = GetDetailByCodeDAResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData4Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData4", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData4RequestBody Body;
        
        public GetDetailData4Request()
        {
        }
        
        public GetDetailData4Request(DALisService.GetDetailData4RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData4RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailData4RequestBody()
        {
        }
        
        public GetDetailData4RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData4Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData4Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData4ResponseBody Body;
        
        public GetDetailData4Response()
        {
        }
        
        public GetDetailData4Response(DALisService.GetDetailData4ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData4ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData4Result;
        
        public GetDetailData4ResponseBody()
        {
        }
        
        public GetDetailData4ResponseBody(string GetDetailData4Result)
        {
            this.GetDetailData4Result = GetDetailData4Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData5Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData5", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData5RequestBody Body;
        
        public GetDetailData5Request()
        {
        }
        
        public GetDetailData5Request(DALisService.GetDetailData5RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData5RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailData5RequestBody()
        {
        }
        
        public GetDetailData5RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData5Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData5Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData5ResponseBody Body;
        
        public GetDetailData5Response()
        {
        }
        
        public GetDetailData5Response(DALisService.GetDetailData5ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData5ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData5Result;
        
        public GetDetailData5ResponseBody()
        {
        }
        
        public GetDetailData5ResponseBody(string GetDetailData5Result)
        {
            this.GetDetailData5Result = GetDetailData5Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataRequestBody Body;
        
        public GetDetailDataRequest()
        {
        }
        
        public GetDetailDataRequest(DALisService.GetDetailDataRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailDataRequestBody()
        {
        }
        
        public GetDetailDataRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataResponseBody Body;
        
        public GetDetailDataResponse()
        {
        }
        
        public GetDetailDataResponse(DALisService.GetDetailDataResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataResult;
        
        public GetDetailDataResponseBody()
        {
        }
        
        public GetDetailDataResponseBody(string GetDetailDataResult)
        {
            this.GetDetailDataResult = GetDetailDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ExportBySerialNumberRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ExportBySerialNumber", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.ExportBySerialNumberRequestBody Body;
        
        public ExportBySerialNumberRequest()
        {
        }
        
        public ExportBySerialNumberRequest(DALisService.ExportBySerialNumberRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class ExportBySerialNumberRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public int SerialNumber;
        
        public ExportBySerialNumberRequestBody()
        {
        }
        
        public ExportBySerialNumberRequestBody(string ClientID, string ClientGUID, int SerialNumber)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.SerialNumber = SerialNumber;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ExportBySerialNumberResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ExportBySerialNumberResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.ExportBySerialNumberResponseBody Body;
        
        public ExportBySerialNumberResponse()
        {
        }
        
        public ExportBySerialNumberResponse(DALisService.ExportBySerialNumberResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class ExportBySerialNumberResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ExportBySerialNumberResult;
        
        public ExportBySerialNumberResponseBody()
        {
        }
        
        public ExportBySerialNumberResponseBody(string ExportBySerialNumberResult)
        {
            this.ExportBySerialNumberResult = ExportBySerialNumberResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData2RequestBody Body;
        
        public GetDetailData2Request()
        {
        }
        
        public GetDetailData2Request(DALisService.GetDetailData2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte DataType;
        
        public GetDetailData2RequestBody()
        {
        }
        
        public GetDetailData2RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
            this.DataType = DataType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData2ResponseBody Body;
        
        public GetDetailData2Response()
        {
        }
        
        public GetDetailData2Response(DALisService.GetDetailData2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData2Result;
        
        public GetDetailData2ResponseBody()
        {
        }
        
        public GetDetailData2ResponseBody(string GetDetailData2Result)
        {
            this.GetDetailData2Result = GetDetailData2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData3Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData3", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData3RequestBody Body;
        
        public GetDetailData3Request()
        {
        }
        
        public GetDetailData3Request(DALisService.GetDetailData3RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData3RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte DataType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public bool All;
        
        public GetDetailData3RequestBody()
        {
        }
        
        public GetDetailData3RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType, bool All)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
            this.DataType = DataType;
            this.All = All;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData3Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData3Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData3ResponseBody Body;
        
        public GetDetailData3Response()
        {
        }
        
        public GetDetailData3Response(DALisService.GetDetailData3ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData3ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData3Result;
        
        public GetDetailData3ResponseBody()
        {
        }
        
        public GetDetailData3ResponseBody(string GetDetailData3Result)
        {
            this.GetDetailData3Result = GetDetailData3Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData6Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData6", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData6RequestBody Body;
        
        public GetDetailData6Request()
        {
        }
        
        public GetDetailData6Request(DALisService.GetDetailData6RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData6RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailData6RequestBody()
        {
        }
        
        public GetDetailData6RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData6Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData6Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData6ResponseBody Body;
        
        public GetDetailData6Response()
        {
        }
        
        public GetDetailData6Response(DALisService.GetDetailData6ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData6ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData6Result;
        
        public GetDetailData6ResponseBody()
        {
        }
        
        public GetDetailData6ResponseBody(string GetDetailData6Result)
        {
            this.GetDetailData6Result = GetDetailData6Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData7Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData7", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData7RequestBody Body;
        
        public GetDetailData7Request()
        {
        }
        
        public GetDetailData7Request(DALisService.GetDetailData7RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData7RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte DataType;
        
        public GetDetailData7RequestBody()
        {
        }
        
        public GetDetailData7RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
            this.DataType = DataType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData7Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData7Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData7ResponseBody Body;
        
        public GetDetailData7Response()
        {
        }
        
        public GetDetailData7Response(DALisService.GetDetailData7ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData7ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData7Result;
        
        public GetDetailData7ResponseBody()
        {
        }
        
        public GetDetailData7ResponseBody(string GetDetailData7Result)
        {
            this.GetDetailData7Result = GetDetailData7Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData8Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData8", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData8RequestBody Body;
        
        public GetDetailData8Request()
        {
        }
        
        public GetDetailData8Request(DALisService.GetDetailData8RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData8RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte DataType;
        
        public GetDetailData8RequestBody()
        {
        }
        
        public GetDetailData8RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
            this.DataType = DataType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData8Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData8Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData8ResponseBody Body;
        
        public GetDetailData8Response()
        {
        }
        
        public GetDetailData8Response(DALisService.GetDetailData8ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData8ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData8Result;
        
        public GetDetailData8ResponseBody()
        {
        }
        
        public GetDetailData8ResponseBody(string GetDetailData8Result)
        {
            this.GetDetailData8Result = GetDetailData8Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTSRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTS", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTSRequestBody Body;
        
        public GetDetailDataTSRequest()
        {
        }
        
        public GetDetailDataTSRequest(DALisService.GetDetailDataTSRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTSRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailDataTSRequestBody()
        {
        }
        
        public GetDetailDataTSRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTSResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTSResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTSResponseBody Body;
        
        public GetDetailDataTSResponse()
        {
        }
        
        public GetDetailDataTSResponse(DALisService.GetDetailDataTSResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTSResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataTSResult;
        
        public GetDetailDataTSResponseBody()
        {
        }
        
        public GetDetailDataTSResponseBody(string GetDetailDataTSResult)
        {
            this.GetDetailDataTSResult = GetDetailDataTSResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCT", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTRequestBody Body;
        
        public GetDetailDataTCTRequest()
        {
        }
        
        public GetDetailDataTCTRequest(DALisService.GetDetailDataTCTRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        public GetDetailDataTCTRequestBody()
        {
        }
        
        public GetDetailDataTCTRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTResponseBody Body;
        
        public GetDetailDataTCTResponse()
        {
        }
        
        public GetDetailDataTCTResponse(DALisService.GetDetailDataTCTResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataTCTResult;
        
        public GetDetailDataTCTResponseBody()
        {
        }
        
        public GetDetailDataTCTResponseBody(string GetDetailDataTCTResult)
        {
            this.GetDetailDataTCTResult = GetDetailDataTCTResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTWithPicRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTWithPic", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTWithPicRequestBody Body;
        
        public GetDetailDataTCTWithPicRequest()
        {
        }
        
        public GetDetailDataTCTWithPicRequest(DALisService.GetDetailDataTCTWithPicRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTWithPicRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        public GetDetailDataTCTWithPicRequestBody()
        {
        }
        
        public GetDetailDataTCTWithPicRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTWithPicResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTWithPicResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTWithPicResponseBody Body;
        
        public GetDetailDataTCTWithPicResponse()
        {
        }
        
        public GetDetailDataTCTWithPicResponse(DALisService.GetDetailDataTCTWithPicResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTWithPicResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataTCTWithPicResult;
        
        public GetDetailDataTCTWithPicResponseBody()
        {
        }
        
        public GetDetailDataTCTWithPicResponseBody(string GetDetailDataTCTWithPicResult)
        {
            this.GetDetailDataTCTWithPicResult = GetDetailDataTCTWithPicResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReporFileRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReporFile", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReporFileRequestBody Body;
        
        public GetReporFileRequest()
        {
        }
        
        public GetReporFileRequest(DALisService.GetReporFileRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReporFileRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        public GetReporFileRequestBody()
        {
        }
        
        public GetReporFileRequestBody(string ClientID, string ClientGUID, string BarCode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReporFileResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReporFileResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReporFileResponseBody Body;
        
        public GetReporFileResponse()
        {
        }
        
        public GetReporFileResponse(DALisService.GetReporFileResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReporFileResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReporFileResult;
        
        public GetReporFileResponseBody()
        {
        }
        
        public GetReporFileResponseBody(string GetReporFileResult)
        {
            this.GetReporFileResult = GetReporFileResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportListDARequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportListDA", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportListDARequestBody Body;
        
        public GetReportListDARequest()
        {
        }
        
        public GetReportListDARequest(DALisService.GetReportListDARequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportListDARequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        public GetReportListDARequestBody()
        {
        }
        
        public GetReportListDARequestBody(string ClientID, string ClientGUID, string BarCode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportListDAResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportListDAResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportListDAResponseBody Body;
        
        public GetReportListDAResponse()
        {
        }
        
        public GetReportListDAResponse(DALisService.GetReportListDAResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportListDAResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportListDAResult;
        
        public GetReportListDAResponseBody()
        {
        }
        
        public GetReportListDAResponseBody(string GetReportListDAResult)
        {
            this.GetReportListDAResult = GetReportListDAResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportFileDARequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportFileDA", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportFileDARequestBody Body;
        
        public GetReportFileDARequest()
        {
        }
        
        public GetReportFileDARequest(DALisService.GetReportFileDARequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportFileDARequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetReportFileDARequestBody()
        {
        }
        
        public GetReportFileDARequestBody(string ClientID, string ClientGUID, string BarCode, byte type)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportFileDAResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportFileDAResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportFileDAResponseBody Body;
        
        public GetReportFileDAResponse()
        {
        }
        
        public GetReportFileDAResponse(DALisService.GetReportFileDAResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportFileDAResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportFileDAResult;
        
        public GetReportFileDAResponseBody()
        {
        }
        
        public GetReportFileDAResponseBody(string GetReportFileDAResult)
        {
            this.GetReportFileDAResult = GetReportFileDAResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportFileDA2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportFileDA2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportFileDA2RequestBody Body;
        
        public GetReportFileDA2Request()
        {
        }
        
        public GetReportFileDA2Request(DALisService.GetReportFileDA2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportFileDA2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string TempNo;
        
        public GetReportFileDA2RequestBody()
        {
        }
        
        public GetReportFileDA2RequestBody(string ClientID, string ClientGUID, string BarCode, byte type, string TempNo)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
            this.TempNo = TempNo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportFileDA2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportFileDA2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportFileDA2ResponseBody Body;
        
        public GetReportFileDA2Response()
        {
        }
        
        public GetReportFileDA2Response(DALisService.GetReportFileDA2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportFileDA2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportFileDA2Result;
        
        public GetReportFileDA2ResponseBody()
        {
        }
        
        public GetReportFileDA2ResponseBody(string GetReportFileDA2Result)
        {
            this.GetReportFileDA2Result = GetReportFileDA2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathWithPicRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathWithPic", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathWithPicRequestBody Body;
        
        public GetPathWithPicRequest()
        {
        }
        
        public GetPathWithPicRequest(DALisService.GetPathWithPicRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathWithPicRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        public GetPathWithPicRequestBody()
        {
        }
        
        public GetPathWithPicRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathWithPicResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathWithPicResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathWithPicResponseBody Body;
        
        public GetPathWithPicResponse()
        {
        }
        
        public GetPathWithPicResponse(DALisService.GetPathWithPicResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathWithPicResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetPathWithPicResult;
        
        public GetPathWithPicResponseBody()
        {
        }
        
        public GetPathWithPicResponseBody(string GetPathWithPicResult)
        {
            this.GetPathWithPicResult = GetPathWithPicResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByIdNumRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByIdNum", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByIdNumRequestBody Body;
        
        public GetDetailDataByIdNumRequest()
        {
        }
        
        public GetDetailDataByIdNumRequest(DALisService.GetDetailDataByIdNumRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByIdNumRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string idNum;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte DataType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string begDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string endDate;
        
        public GetDetailDataByIdNumRequestBody()
        {
        }
        
        public GetDetailDataByIdNumRequestBody(string ClientID, string ClientGUID, string idNum, byte DataType, string begDate, string endDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.idNum = idNum;
            this.DataType = DataType;
            this.begDate = begDate;
            this.endDate = endDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByIdNumResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByIdNumResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByIdNumResponseBody Body;
        
        public GetDetailDataByIdNumResponse()
        {
        }
        
        public GetDetailDataByIdNumResponse(DALisService.GetDetailDataByIdNumResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByIdNumResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataByIdNumResult;
        
        public GetDetailDataByIdNumResponseBody()
        {
        }
        
        public GetDetailDataByIdNumResponseBody(string GetDetailDataByIdNumResult)
        {
            this.GetDetailDataByIdNumResult = GetDetailDataByIdNumResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospBarcodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospBarcode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospBarcodeRequestBody Body;
        
        public GetDetailDataByHospBarcodeRequest()
        {
        }
        
        public GetDetailDataByHospBarcodeRequest(DALisService.GetDetailDataByHospBarcodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospBarcodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string hospBarcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DataType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string begDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string endDate;
        
        public GetDetailDataByHospBarcodeRequestBody()
        {
        }
        
        public GetDetailDataByHospBarcodeRequestBody(string ClientID, string ClientGUID, string hospName, string hospBarcode, byte DataType, string begDate, string endDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospName = hospName;
            this.hospBarcode = hospBarcode;
            this.DataType = DataType;
            this.begDate = begDate;
            this.endDate = endDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospBarcodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospBarcodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospBarcodeResponseBody Body;
        
        public GetDetailDataByHospBarcodeResponse()
        {
        }
        
        public GetDetailDataByHospBarcodeResponse(DALisService.GetDetailDataByHospBarcodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospBarcodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataByHospBarcodeResult;
        
        public GetDetailDataByHospBarcodeResponseBody()
        {
        }
        
        public GetDetailDataByHospBarcodeResponseBody(string GetDetailDataByHospBarcodeResult)
        {
            this.GetDetailDataByHospBarcodeResult = GetDetailDataByHospBarcodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospInfoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospInfo", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospInfoRequestBody Body;
        
        public GetDetailDataByHospInfoRequest()
        {
        }
        
        public GetDetailDataByHospInfoRequest(DALisService.GetDetailDataByHospInfoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospInfoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string patName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string patSex;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string patAge;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public byte DataType;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string begDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=8)]
        public string endDate;
        
        public GetDetailDataByHospInfoRequestBody()
        {
        }
        
        public GetDetailDataByHospInfoRequestBody(string ClientID, string ClientGUID, string hospName, string patName, string patSex, string patAge, byte DataType, string begDate, string endDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospName = hospName;
            this.patName = patName;
            this.patSex = patSex;
            this.patAge = patAge;
            this.DataType = DataType;
            this.begDate = begDate;
            this.endDate = endDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospInfoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospInfoResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospInfoResponseBody Body;
        
        public GetDetailDataByHospInfoResponse()
        {
        }
        
        public GetDetailDataByHospInfoResponse(DALisService.GetDetailDataByHospInfoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospInfoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataByHospInfoResult;
        
        public GetDetailDataByHospInfoResponseBody()
        {
        }
        
        public GetDetailDataByHospInfoResponseBody(string GetDetailDataByHospInfoResult)
        {
            this.GetDetailDataByHospInfoResult = GetDetailDataByHospInfoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByBarCodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByBarCode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByBarCodeRequestBody Body;
        
        public GetDetailDataByBarCodeRequest()
        {
        }
        
        public GetDetailDataByBarCodeRequest(DALisService.GetDetailDataByBarCodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByBarCodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string barcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte DataType;
        
        public GetDetailDataByBarCodeRequestBody()
        {
        }
        
        public GetDetailDataByBarCodeRequestBody(string ClientID, string ClientGUID, string barcode, byte DataType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.barcode = barcode;
            this.DataType = DataType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByBarCodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByBarCodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByBarCodeResponseBody Body;
        
        public GetDetailDataByBarCodeResponse()
        {
        }
        
        public GetDetailDataByBarCodeResponse(DALisService.GetDetailDataByBarCodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByBarCodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataByBarCodeResult;
        
        public GetDetailDataByBarCodeResponseBody()
        {
        }
        
        public GetDetailDataByBarCodeResponseBody(string GetDetailDataByBarCodeResult)
        {
            this.GetDetailDataByBarCodeResult = GetDetailDataByBarCodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCodeRequestBody Body;
        
        public GetDetailByHospCodeRequest()
        {
        }
        
        public GetDetailByHospCodeRequest(DALisService.GetDetailByHospCodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string clinicid;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetDetailByHospCodeRequestBody()
        {
        }
        
        public GetDetailByHospCodeRequestBody(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            this.ClientId = ClientId;
            this.ClientGUID = ClientGUID;
            this.clinicid = clinicid;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailByHospCodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailByHospCodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailByHospCodeResponseBody Body;
        
        public GetDetailByHospCodeResponse()
        {
        }
        
        public GetDetailByHospCodeResponse(DALisService.GetDetailByHospCodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailByHospCodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailByHospCodeResult;
        
        public GetDetailByHospCodeResponseBody()
        {
        }
        
        public GetDetailByHospCodeResponseBody(string GetDetailByHospCodeResult)
        {
            this.GetDetailByHospCodeResult = GetDetailByHospCodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenQRCbyOHBarcodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenQRCbyOHBarcode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GenQRCbyOHBarcodeRequestBody Body;
        
        public GenQRCbyOHBarcodeRequest()
        {
        }
        
        public GenQRCbyOHBarcodeRequest(DALisService.GenQRCbyOHBarcodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GenQRCbyOHBarcodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string barcode;
        
        public GenQRCbyOHBarcodeRequestBody()
        {
        }
        
        public GenQRCbyOHBarcodeRequestBody(string ClientID, string ClientGUID, string barcode)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.barcode = barcode;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GenQRCbyOHBarcodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GenQRCbyOHBarcodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GenQRCbyOHBarcodeResponseBody Body;
        
        public GenQRCbyOHBarcodeResponse()
        {
        }
        
        public GenQRCbyOHBarcodeResponse(DALisService.GenQRCbyOHBarcodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GenQRCbyOHBarcodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GenQRCbyOHBarcodeResult;
        
        public GenQRCbyOHBarcodeResponseBody()
        {
        }
        
        public GenQRCbyOHBarcodeResponseBody(string GenQRCbyOHBarcodeResult)
        {
            this.GenQRCbyOHBarcodeResult = GenQRCbyOHBarcodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMicRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMic", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMicRequestBody Body;
        
        public GetDetailDataMicRequest()
        {
        }
        
        public GetDetailDataMicRequest(DALisService.GetDetailDataMicRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMicRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte DateType;
        
        public GetDetailDataMicRequestBody()
        {
        }
        
        public GetDetailDataMicRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, string BarCode, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.BarCode = BarCode;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMicResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMicResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMicResponseBody Body;
        
        public GetDetailDataMicResponse()
        {
        }
        
        public GetDetailDataMicResponse(DALisService.GetDetailDataMicResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMicResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataMicResult;
        
        public GetDetailDataMicResponseBody()
        {
        }
        
        public GetDetailDataMicResponseBody(string GetDetailDataMicResult)
        {
            this.GetDetailDataMicResult = GetDetailDataMicResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTByBarcodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTByBarcode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTByBarcodeRequestBody Body;
        
        public GetDetailDataTCTByBarcodeRequest()
        {
        }
        
        public GetDetailDataTCTByBarcodeRequest(DALisService.GetDetailDataTCTByBarcodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTByBarcodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string barcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string patName;
        
        public GetDetailDataTCTByBarcodeRequestBody()
        {
        }
        
        public GetDetailDataTCTByBarcodeRequestBody(string ClientID, string ClientGUID, string barcode, string patName)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.barcode = barcode;
            this.patName = patName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTByBarcodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTByBarcodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTByBarcodeResponseBody Body;
        
        public GetDetailDataTCTByBarcodeResponse()
        {
        }
        
        public GetDetailDataTCTByBarcodeResponse(DALisService.GetDetailDataTCTByBarcodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTByBarcodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataTCTByBarcodeResult;
        
        public GetDetailDataTCTByBarcodeResponseBody()
        {
        }
        
        public GetDetailDataTCTByBarcodeResponseBody(string GetDetailDataTCTByBarcodeResult)
        {
            this.GetDetailDataTCTByBarcodeResult = GetDetailDataTCTByBarcodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetHPVDataRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetHPVData", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetHPVDataRequestBody Body;
        
        public GetHPVDataRequest()
        {
        }
        
        public GetHPVDataRequest(DALisService.GetHPVDataRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetHPVDataRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string barcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string patName;
        
        public GetHPVDataRequestBody()
        {
        }
        
        public GetHPVDataRequestBody(string ClientID, string ClientGUID, string barcode, string patName)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.barcode = barcode;
            this.patName = patName;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetHPVDataResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetHPVDataResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetHPVDataResponseBody Body;
        
        public GetHPVDataResponse()
        {
        }
        
        public GetHPVDataResponse(DALisService.GetHPVDataResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetHPVDataResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetHPVDataResult;
        
        public GetHPVDataResponseBody()
        {
        }
        
        public GetHPVDataResponseBody(string GetHPVDataResult)
        {
            this.GetHPVDataResult = GetHPVDataResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataForHealthRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataForHealth", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataForHealthRequestBody Body;
        
        public GetDetailDataForHealthRequest()
        {
        }
        
        public GetDetailDataForHealthRequest(DALisService.GetDetailDataForHealthRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataForHealthRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        public GetDetailDataForHealthRequestBody()
        {
        }
        
        public GetDetailDataForHealthRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataForHealthResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataForHealthResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataForHealthResponseBody Body;
        
        public GetDetailDataForHealthResponse()
        {
        }
        
        public GetDetailDataForHealthResponse(DALisService.GetDetailDataForHealthResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataForHealthResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataForHealthResult;
        
        public GetDetailDataForHealthResponseBody()
        {
        }
        
        public GetDetailDataForHealthResponseBody(string GetDetailDataForHealthResult)
        {
            this.GetDetailDataForHealthResult = GetDetailDataForHealthResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospBarcode2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospBarcode2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospBarcode2RequestBody Body;
        
        public GetDetailDataByHospBarcode2Request()
        {
        }
        
        public GetDetailDataByHospBarcode2Request(DALisService.GetDetailDataByHospBarcode2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospBarcode2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospBarcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte SelectType;
        
        public GetDetailDataByHospBarcode2RequestBody()
        {
        }
        
        public GetDetailDataByHospBarcode2RequestBody(string ClientID, string ClientGUID, string hospBarcode, byte SelectType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospBarcode = hospBarcode;
            this.SelectType = SelectType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataByHospBarcode2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataByHospBarcode2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataByHospBarcode2ResponseBody Body;
        
        public GetDetailDataByHospBarcode2Response()
        {
        }
        
        public GetDetailDataByHospBarcode2Response(DALisService.GetDetailDataByHospBarcode2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataByHospBarcode2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataByHospBarcode2Result;
        
        public GetDetailDataByHospBarcode2ResponseBody()
        {
        }
        
        public GetDetailDataByHospBarcode2ResponseBody(string GetDetailDataByHospBarcode2Result)
        {
            this.GetDetailDataByHospBarcode2Result = GetDetailDataByHospBarcode2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegDARequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegDA", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegDARequestBody Body;
        
        public GetReportJpegDARequest()
        {
        }
        
        public GetReportJpegDARequest(DALisService.GetReportJpegDARequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegDARequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetReportJpegDARequestBody()
        {
        }
        
        public GetReportJpegDARequestBody(string ClientID, string ClientGUID, string BarCode, byte type)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegDAResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegDAResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegDAResponseBody Body;
        
        public GetReportJpegDAResponse()
        {
        }
        
        public GetReportJpegDAResponse(DALisService.GetReportJpegDAResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegDAResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportJpegDAResult;
        
        public GetReportJpegDAResponseBody()
        {
        }
        
        public GetReportJpegDAResponseBody(string GetReportJpegDAResult)
        {
            this.GetReportJpegDAResult = GetReportJpegDAResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegDA2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegDA2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegDA2RequestBody Body;
        
        public GetReportJpegDA2Request()
        {
        }
        
        public GetReportJpegDA2Request(DALisService.GetReportJpegDA2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegDA2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string TempNo;
        
        public GetReportJpegDA2RequestBody()
        {
        }
        
        public GetReportJpegDA2RequestBody(string ClientID, string ClientGUID, string BarCode, byte type, string TempNo)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
            this.TempNo = TempNo;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegDA2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegDA2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegDA2ResponseBody Body;
        
        public GetReportJpegDA2Response()
        {
        }
        
        public GetReportJpegDA2Response(DALisService.GetReportJpegDA2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegDA2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportJpegDA2Result;
        
        public GetReportJpegDA2ResponseBody()
        {
        }
        
        public GetReportJpegDA2ResponseBody(string GetReportJpegDA2Result)
        {
            this.GetReportJpegDA2Result = GetReportJpegDA2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDABarcodeByHospCodeRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDABarcodeByHospCode", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDABarcodeByHospCodeRequestBody Body;
        
        public GetDABarcodeByHospCodeRequest()
        {
        }
        
        public GetDABarcodeByHospCodeRequest(DALisService.GetDABarcodeByHospCodeRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDABarcodeByHospCodeRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string hospBarcode;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string begDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string endDate;
        
        public GetDABarcodeByHospCodeRequestBody()
        {
        }
        
        public GetDABarcodeByHospCodeRequestBody(string ClientID, string ClientGUID, string hospName, string hospBarcode, string begDate, string endDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospName = hospName;
            this.hospBarcode = hospBarcode;
            this.begDate = begDate;
            this.endDate = endDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDABarcodeByHospCodeResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDABarcodeByHospCodeResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDABarcodeByHospCodeResponseBody Body;
        
        public GetDABarcodeByHospCodeResponse()
        {
        }
        
        public GetDABarcodeByHospCodeResponse(DALisService.GetDABarcodeByHospCodeResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDABarcodeByHospCodeResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDABarcodeByHospCodeResult;
        
        public GetDABarcodeByHospCodeResponseBody()
        {
        }
        
        public GetDABarcodeByHospCodeResponseBody(string GetDABarcodeByHospCodeResult)
        {
            this.GetDABarcodeByHospCodeResult = GetDABarcodeByHospCodeResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDABarcodeByHospInfoRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDABarcodeByHospInfo", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDABarcodeByHospInfoRequestBody Body;
        
        public GetDABarcodeByHospInfoRequest()
        {
        }
        
        public GetDABarcodeByHospInfoRequest(DALisService.GetDABarcodeByHospInfoRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDABarcodeByHospInfoRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string hospName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string patName;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string patSex;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string patAge;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=6)]
        public string begDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=7)]
        public string endDate;
        
        public GetDABarcodeByHospInfoRequestBody()
        {
        }
        
        public GetDABarcodeByHospInfoRequestBody(string ClientID, string ClientGUID, string hospName, string patName, string patSex, string patAge, string begDate, string endDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.hospName = hospName;
            this.patName = patName;
            this.patSex = patSex;
            this.patAge = patAge;
            this.begDate = begDate;
            this.endDate = endDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDABarcodeByHospInfoResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDABarcodeByHospInfoResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDABarcodeByHospInfoResponseBody Body;
        
        public GetDABarcodeByHospInfoResponse()
        {
        }
        
        public GetDABarcodeByHospInfoResponse(DALisService.GetDABarcodeByHospInfoResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDABarcodeByHospInfoResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDABarcodeByHospInfoResult;
        
        public GetDABarcodeByHospInfoResponseBody()
        {
        }
        
        public GetDABarcodeByHospInfoResponseBody(string GetDABarcodeByHospInfoResult)
        {
            this.GetDABarcodeByHospInfoResult = GetDABarcodeByHospInfoResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMic2Request
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMic2", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMic2RequestBody Body;
        
        public GetDetailDataMic2Request()
        {
        }
        
        public GetDetailDataMic2Request(DALisService.GetDetailDataMic2RequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMic2RequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=4)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public byte DateType;
        
        public GetDetailDataMic2RequestBody()
        {
        }
        
        public GetDetailDataMic2RequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, string BarCode, byte DateType)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.BarCode = BarCode;
            this.DateType = DateType;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataMic2Response
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataMic2Response", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataMic2ResponseBody Body;
        
        public GetDetailDataMic2Response()
        {
        }
        
        public GetDetailDataMic2Response(DALisService.GetDetailDataMic2ResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataMic2ResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataMic2Result;
        
        public GetDetailDataMic2ResponseBody()
        {
        }
        
        public GetDetailDataMic2ResponseBody(string GetDetailDataMic2Result)
        {
            this.GetDetailDataMic2Result = GetDetailDataMic2Result;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData5ByPageRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData5ByPage", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData5ByPageRequestBody Body;
        
        public GetDetailData5ByPageRequest()
        {
        }
        
        public GetDetailData5ByPageRequest(DALisService.GetDetailData5ByPageRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData5ByPageRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public int num;
        
        public GetDetailData5ByPageRequestBody()
        {
        }
        
        public GetDetailData5ByPageRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, int num)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.num = num;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailData5ByPageResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailData5ByPageResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailData5ByPageResponseBody Body;
        
        public GetDetailData5ByPageResponse()
        {
        }
        
        public GetDetailData5ByPageResponse(DALisService.GetDetailData5ByPageResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailData5ByPageResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailData5ByPageResult;
        
        public GetDetailData5ByPageResponseBody()
        {
        }
        
        public GetDetailData5ByPageResponseBody(string GetDetailData5ByPageResult)
        {
            this.GetDetailData5ByPageResult = GetDetailData5ByPageResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathWithPicByPageRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathWithPicByPage", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathWithPicByPageRequestBody Body;
        
        public GetPathWithPicByPageRequest()
        {
        }
        
        public GetPathWithPicByPageRequest(DALisService.GetPathWithPicByPageRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathWithPicByPageRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int num;
        
        public GetPathWithPicByPageRequestBody()
        {
        }
        
        public GetPathWithPicByPageRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, int num)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
            this.num = num;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathWithPicByPageResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathWithPicByPageResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathWithPicByPageResponseBody Body;
        
        public GetPathWithPicByPageResponse()
        {
        }
        
        public GetPathWithPicByPageResponse(DALisService.GetPathWithPicByPageResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathWithPicByPageResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetPathWithPicByPageResult;
        
        public GetPathWithPicByPageResponseBody()
        {
        }
        
        public GetPathWithPicByPageResponseBody(string GetPathWithPicByPageResult)
        {
            this.GetPathWithPicByPageResult = GetPathWithPicByPageResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTWithPicByPageRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTWithPicByPage", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTWithPicByPageRequestBody Body;
        
        public GetDetailDataTCTWithPicByPageRequest()
        {
        }
        
        public GetDetailDataTCTWithPicByPageRequest(DALisService.GetDetailDataTCTWithPicByPageRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTWithPicByPageRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string StartDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=3)]
        public string EndDate;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public byte DateType;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public int num;
        
        public GetDetailDataTCTWithPicByPageRequestBody()
        {
        }
        
        public GetDetailDataTCTWithPicByPageRequestBody(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, int num)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.StartDate = StartDate;
            this.EndDate = EndDate;
            this.DateType = DateType;
            this.num = num;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetDetailDataTCTWithPicByPageResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetDetailDataTCTWithPicByPageResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetDetailDataTCTWithPicByPageResponseBody Body;
        
        public GetDetailDataTCTWithPicByPageResponse()
        {
        }
        
        public GetDetailDataTCTWithPicByPageResponse(DALisService.GetDetailDataTCTWithPicByPageResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetDetailDataTCTWithPicByPageResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetDetailDataTCTWithPicByPageResult;
        
        public GetDetailDataTCTWithPicByPageResponseBody()
        {
        }
        
        public GetDetailDataTCTWithPicByPageResponseBody(string GetDetailDataTCTWithPicByPageResult)
        {
            this.GetDetailDataTCTWithPicByPageResult = GetDetailDataTCTWithPicByPageResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathJpegZipRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathJpegZip", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathJpegZipRequestBody Body;
        
        public GetPathJpegZipRequest()
        {
        }
        
        public GetPathJpegZipRequest(DALisService.GetPathJpegZipRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathJpegZipRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string SendDate;
        
        public GetPathJpegZipRequestBody()
        {
        }
        
        public GetPathJpegZipRequestBody(string ClientID, string ClientGUID, string SendDate)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.SendDate = SendDate;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetPathJpegZipResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetPathJpegZipResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetPathJpegZipResponseBody Body;
        
        public GetPathJpegZipResponse()
        {
        }
        
        public GetPathJpegZipResponse(DALisService.GetPathJpegZipResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetPathJpegZipResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetPathJpegZipResult;
        
        public GetPathJpegZipResponseBody()
        {
        }
        
        public GetPathJpegZipResponseBody(string GetPathJpegZipResult)
        {
            this.GetPathJpegZipResult = GetPathJpegZipResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegForBJRequest
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegForBJ", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegForBJRequestBody Body;
        
        public GetReportJpegForBJRequest()
        {
        }
        
        public GetReportJpegForBJRequest(DALisService.GetReportJpegForBJRequestBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegForBJRequestBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ClientID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string ClientGUID;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=2)]
        public string BarCode;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public byte type;
        
        public GetReportJpegForBJRequestBody()
        {
        }
        
        public GetReportJpegForBJRequestBody(string ClientID, string ClientGUID, string BarCode, byte type)
        {
            this.ClientID = ClientID;
            this.ClientGUID = ClientGUID;
            this.BarCode = BarCode;
            this.type = type;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class GetReportJpegForBJResponse
    {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="GetReportJpegForBJResponse", Namespace="http://report.dagene.net/", Order=0)]
        public DALisService.GetReportJpegForBJResponseBody Body;
        
        public GetReportJpegForBJResponse()
        {
        }
        
        public GetReportJpegForBJResponse(DALisService.GetReportJpegForBJResponseBody Body)
        {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://report.dagene.net/")]
    public partial class GetReportJpegForBJResponseBody
    {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string GetReportJpegForBJResult;
        
        public GetReportJpegForBJResponseBody()
        {
        }
        
        public GetReportJpegForBJResponseBody(string GetReportJpegForBJResult)
        {
            this.GetReportJpegForBJResult = GetReportJpegForBJResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public interface RasClientDetailSoapChannel : DALisService.RasClientDetailSoap, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("dotnet-svcutil", "1.0.0.1")]
    public partial class RasClientDetailSoapClient : System.ServiceModel.ClientBase<DALisService.RasClientDetailSoap>, DALisService.RasClientDetailSoap
    {
        
    /// <summary>
    /// 实现此分部方法，配置服务终结点。
    /// </summary>
    /// <param name="serviceEndpoint">要配置的终结点</param>
    /// <param name="clientCredentials">客户端凭据</param>
    static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public RasClientDetailSoapClient(EndpointConfiguration endpointConfiguration) : 
                base(RasClientDetailSoapClient.GetBindingForEndpoint(endpointConfiguration), RasClientDetailSoapClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RasClientDetailSoapClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(RasClientDetailSoapClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RasClientDetailSoapClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(RasClientDetailSoapClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public RasClientDetailSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetIfReleasedAllByBarcodeResponse> DALisService.RasClientDetailSoap.GetIfReleasedAllByBarcodeAsync(DALisService.GetIfReleasedAllByBarcodeRequest request)
        {
            return base.Channel.GetIfReleasedAllByBarcodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetIfReleasedAllByBarcodeResponse> GetIfReleasedAllByBarcodeAsync(string ClientID, string ClientGUID, string BarCode)
        {
            DALisService.GetIfReleasedAllByBarcodeRequest inValue = new DALisService.GetIfReleasedAllByBarcodeRequest();
            inValue.Body = new DALisService.GetIfReleasedAllByBarcodeRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            return ((DALisService.RasClientDetailSoap)(this)).GetIfReleasedAllByBarcodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportFileForXAResponse> DALisService.RasClientDetailSoap.GetReportFileForXAAsync(DALisService.GetReportFileForXARequest request)
        {
            return base.Channel.GetReportFileForXAAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportFileForXAResponse> GetReportFileForXAAsync(string ClientID, string ClientGUID, string BarCode, string type)
        {
            DALisService.GetReportFileForXARequest inValue = new DALisService.GetReportFileForXARequest();
            inValue.Body = new DALisService.GetReportFileForXARequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportFileForXAAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetFileBase64ByNameResponse> DALisService.RasClientDetailSoap.GetFileBase64ByNameAsync(DALisService.GetFileBase64ByNameRequest request)
        {
            return base.Channel.GetFileBase64ByNameAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetFileBase64ByNameResponse> GetFileBase64ByNameAsync(string ClientID, string ClientGUID, string fileName)
        {
            DALisService.GetFileBase64ByNameRequest inValue = new DALisService.GetFileBase64ByNameRequest();
            inValue.Body = new DALisService.GetFileBase64ByNameRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.fileName = fileName;
            return ((DALisService.RasClientDetailSoap)(this)).GetFileBase64ByNameAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByCodeDA2Response> DALisService.RasClientDetailSoap.GetDetailByCodeDA2Async(DALisService.GetDetailByCodeDA2Request request)
        {
            return base.Channel.GetDetailByCodeDA2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByCodeDA2Response> GetDetailByCodeDA2Async(string ClientId, string ClientGUID, string BarCode)
        {
            DALisService.GetDetailByCodeDA2Request inValue = new DALisService.GetDetailByCodeDA2Request();
            inValue.Body = new DALisService.GetDetailByCodeDA2RequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByCodeDA2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTByBarcodesResponse> DALisService.RasClientDetailSoap.GetDetailDataTCTByBarcodesAsync(DALisService.GetDetailDataTCTByBarcodesRequest request)
        {
            return base.Channel.GetDetailDataTCTByBarcodesAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataTCTByBarcodesResponse> GetDetailDataTCTByBarcodesAsync(string ClientID, string ClientGUID, string barcodes)
        {
            DALisService.GetDetailDataTCTByBarcodesRequest inValue = new DALisService.GetDetailDataTCTByBarcodesRequest();
            inValue.Body = new DALisService.GetDetailDataTCTByBarcodesRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.barcodes = barcodes;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataTCTByBarcodesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetHPVDataByBarcodesResponse> DALisService.RasClientDetailSoap.GetHPVDataByBarcodesAsync(DALisService.GetHPVDataByBarcodesRequest request)
        {
            return base.Channel.GetHPVDataByBarcodesAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetHPVDataByBarcodesResponse> GetHPVDataByBarcodesAsync(string ClientID, string ClientGUID, string barcodes)
        {
            DALisService.GetHPVDataByBarcodesRequest inValue = new DALisService.GetHPVDataByBarcodesRequest();
            inValue.Body = new DALisService.GetHPVDataByBarcodesRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.barcodes = barcodes;
            return ((DALisService.RasClientDetailSoap)(this)).GetHPVDataByBarcodesAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData6ByPageResponse> DALisService.RasClientDetailSoap.GetDetailData6ByPageAsync(DALisService.GetDetailData6ByPageRequest request)
        {
            return base.Channel.GetDetailData6ByPageAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData6ByPageResponse> GetDetailData6ByPageAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, int num)
        {
            DALisService.GetDetailData6ByPageRequest inValue = new DALisService.GetDetailData6ByPageRequest();
            inValue.Body = new DALisService.GetDetailData6ByPageRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.num = num;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData6ByPageAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailResultForXKResponse> DALisService.RasClientDetailSoap.GetDetailResultForXKAsync(DALisService.GetDetailResultForXKRequest request)
        {
            return base.Channel.GetDetailResultForXKAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailResultForXKResponse> GetDetailResultForXKAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DataType)
        {
            DALisService.GetDetailResultForXKRequest inValue = new DALisService.GetDetailResultForXKRequest();
            inValue.Body = new DALisService.GetDetailResultForXKRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DataType = DataType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailResultForXKAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataResponse> DALisService.RasClientDetailSoap.GetPathDetailDataAsync(DALisService.GetPathDetailDataRequest request)
        {
            return base.Channel.GetPathDetailDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetPathDetailDataResponse> GetPathDetailDataAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            DALisService.GetPathDetailDataRequest inValue = new DALisService.GetPathDetailDataRequest();
            inValue.Body = new DALisService.GetPathDetailDataRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetPathDetailDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataWithPicResponse> DALisService.RasClientDetailSoap.GetPathDetailDataWithPicAsync(DALisService.GetPathDetailDataWithPicRequest request)
        {
            return base.Channel.GetPathDetailDataWithPicAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetPathDetailDataWithPicResponse> GetPathDetailDataWithPicAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            DALisService.GetPathDetailDataWithPicRequest inValue = new DALisService.GetPathDetailDataWithPicRequest();
            inValue.Body = new DALisService.GetPathDetailDataWithPicRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetPathDetailDataWithPicAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataWithPicByPageResponse> DALisService.RasClientDetailSoap.GetPathDetailDataWithPicByPageAsync(DALisService.GetPathDetailDataWithPicByPageRequest request)
        {
            return base.Channel.GetPathDetailDataWithPicByPageAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetPathDetailDataWithPicByPageResponse> GetPathDetailDataWithPicByPageAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, int num)
        {
            DALisService.GetPathDetailDataWithPicByPageRequest inValue = new DALisService.GetPathDetailDataWithPicByPageRequest();
            inValue.Body = new DALisService.GetPathDetailDataWithPicByPageRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            inValue.Body.num = num;
            return ((DALisService.RasClientDetailSoap)(this)).GetPathDetailDataWithPicByPageAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportJpegByClinicidResponse> DALisService.RasClientDetailSoap.GetReportJpegByClinicidAsync(DALisService.GetReportJpegByClinicidRequest request)
        {
            return base.Channel.GetReportJpegByClinicidAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportJpegByClinicidResponse> GetReportJpegByClinicidAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, string Clinicid, byte type)
        {
            DALisService.GetReportJpegByClinicidRequest inValue = new DALisService.GetReportJpegByClinicidRequest();
            inValue.Body = new DALisService.GetReportJpegByClinicidRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.Clinicid = Clinicid;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportJpegByClinicidAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportJpegByClinicid2Response> DALisService.RasClientDetailSoap.GetReportJpegByClinicid2Async(DALisService.GetReportJpegByClinicid2Request request)
        {
            return base.Channel.GetReportJpegByClinicid2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportJpegByClinicid2Response> GetReportJpegByClinicid2Async(string ClientID, string ClientGUID, string StartDate, string EndDate, string Clinicid, byte type, bool IsMicroPic)
        {
            DALisService.GetReportJpegByClinicid2Request inValue = new DALisService.GetReportJpegByClinicid2Request();
            inValue.Body = new DALisService.GetReportJpegByClinicid2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.Clinicid = Clinicid;
            inValue.Body.type = type;
            inValue.Body.IsMicroPic = IsMicroPic;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportJpegByClinicid2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetCheckPDataResponse> DALisService.RasClientDetailSoap.GetCheckPDataAsync(DALisService.GetCheckPDataRequest request)
        {
            return base.Channel.GetCheckPDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetCheckPDataResponse> GetCheckPDataAsync(string ClientID, string ClientGUID, string Barcode)
        {
            DALisService.GetCheckPDataRequest inValue = new DALisService.GetCheckPDataRequest();
            inValue.Body = new DALisService.GetCheckPDataRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.Barcode = Barcode;
            return ((DALisService.RasClientDetailSoap)(this)).GetCheckPDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetSept9PDFReportResponse> DALisService.RasClientDetailSoap.GetSept9PDFReportAsync(DALisService.GetSept9PDFReportRequest request)
        {
            return base.Channel.GetSept9PDFReportAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetSept9PDFReportResponse> GetSept9PDFReportAsync(string ClientID, string ClientGUID, string BarCode)
        {
            DALisService.GetSept9PDFReportRequest inValue = new DALisService.GetSept9PDFReportRequest();
            inValue.Body = new DALisService.GetSept9PDFReportRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            return ((DALisService.RasClientDetailSoap)(this)).GetSept9PDFReportAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospGroupResponse> DALisService.RasClientDetailSoap.GetDetailDataByHospGroupAsync(DALisService.GetDetailDataByHospGroupRequest request)
        {
            return base.Channel.GetDetailDataByHospGroupAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataByHospGroupResponse> GetDetailDataByHospGroupAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, int num)
        {
            DALisService.GetDetailDataByHospGroupRequest inValue = new DALisService.GetDetailDataByHospGroupRequest();
            inValue.Body = new DALisService.GetDetailDataByHospGroupRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.num = num;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataByHospGroupAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDAResponse> DALisService.RasClientDetailSoap.GetSampleInfoFromDAAsync(DALisService.GetSampleInfoFromDARequest request)
        {
            return base.Channel.GetSampleInfoFromDAAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDAResponse> GetSampleInfoFromDAAsync(string User, string PassWord, string Dept, string Barcode, string DivideDate, string StartTime, string EndTime, string page)
        {
            DALisService.GetSampleInfoFromDARequest inValue = new DALisService.GetSampleInfoFromDARequest();
            inValue.Body = new DALisService.GetSampleInfoFromDARequestBody();
            inValue.Body.User = User;
            inValue.Body.PassWord = PassWord;
            inValue.Body.Dept = Dept;
            inValue.Body.Barcode = Barcode;
            inValue.Body.DivideDate = DivideDate;
            inValue.Body.StartTime = StartTime;
            inValue.Body.EndTime = EndTime;
            inValue.Body.page = page;
            return ((DALisService.RasClientDetailSoap)(this)).GetSampleInfoFromDAAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.RunLimsFunctionResponse> DALisService.RasClientDetailSoap.RunLimsFunctionAsync(DALisService.RunLimsFunctionRequest request)
        {
            return base.Channel.RunLimsFunctionAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.RunLimsFunctionResponse> RunLimsFunctionAsync(string User, string PassWord, string FuncID, DALisService.ArrayOfString ParamInfo)
        {
            DALisService.RunLimsFunctionRequest inValue = new DALisService.RunLimsFunctionRequest();
            inValue.Body = new DALisService.RunLimsFunctionRequestBody();
            inValue.Body.User = User;
            inValue.Body.PassWord = PassWord;
            inValue.Body.FuncID = FuncID;
            inValue.Body.ParamInfo = ParamInfo;
            return ((DALisService.RasClientDetailSoap)(this)).RunLimsFunctionAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMic3Response> DALisService.RasClientDetailSoap.GetDetailDataMic3Async(DALisService.GetDetailDataMic3Request request)
        {
            return base.Channel.GetDetailDataMic3Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataMic3Response> GetDetailDataMic3Async(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailDataMic3Request inValue = new DALisService.GetDetailDataMic3Request();
            inValue.Body = new DALisService.GetDetailDataMic3RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataMic3Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportPathByDateResponse> DALisService.RasClientDetailSoap.GetReportPathByDateAsync(DALisService.GetReportPathByDateRequest request)
        {
            return base.Channel.GetReportPathByDateAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportPathByDateResponse> GetReportPathByDateAsync(string ClientID, string ClientGUID, string SendDate, string type, int num)
        {
            DALisService.GetReportPathByDateRequest inValue = new DALisService.GetReportPathByDateRequest();
            inValue.Body = new DALisService.GetReportPathByDateRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.SendDate = SendDate;
            inValue.Body.type = type;
            inValue.Body.num = num;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportPathByDateAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTS2Response> DALisService.RasClientDetailSoap.GetDetailDataTS2Async(DALisService.GetDetailDataTS2Request request)
        {
            return base.Channel.GetDetailDataTS2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataTS2Response> GetDetailDataTS2Async(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailDataTS2Request inValue = new DALisService.GetDetailDataTS2Request();
            inValue.Body = new DALisService.GetDetailDataTS2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataTS2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataGSResponse> DALisService.RasClientDetailSoap.GetDetailDataGSAsync(DALisService.GetDetailDataGSRequest request)
        {
            return base.Channel.GetDetailDataGSAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataGSResponse> GetDetailDataGSAsync(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailDataGSRequest inValue = new DALisService.GetDetailDataGSRequest();
            inValue.Body = new DALisService.GetDetailDataGSRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataGSAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataRSTResponse> DALisService.RasClientDetailSoap.GetDetailDataRSTAsync(DALisService.GetDetailDataRSTRequest request)
        {
            return base.Channel.GetDetailDataRSTAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataRSTResponse> GetDetailDataRSTAsync(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailDataRSTRequest inValue = new DALisService.GetDetailDataRSTRequest();
            inValue.Body = new DALisService.GetDetailDataRSTRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataRSTAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetSampleBackInfoResponse> DALisService.RasClientDetailSoap.GetSampleBackInfoAsync(DALisService.GetSampleBackInfoRequest request)
        {
            return base.Channel.GetSampleBackInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetSampleBackInfoResponse> GetSampleBackInfoAsync(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetSampleBackInfoRequest inValue = new DALisService.GetSampleBackInfoRequest();
            inValue.Body = new DALisService.GetSampleBackInfoRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetSampleBackInfoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.SampleBackConfirmResponse> DALisService.RasClientDetailSoap.SampleBackConfirmAsync(DALisService.SampleBackConfirmRequest request)
        {
            return base.Channel.SampleBackConfirmAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.SampleBackConfirmResponse> SampleBackConfirmAsync(string ClientID, string ClientGUID, string barcode)
        {
            DALisService.SampleBackConfirmRequest inValue = new DALisService.SampleBackConfirmRequest();
            inValue.Body = new DALisService.SampleBackConfirmRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.barcode = barcode;
            return ((DALisService.RasClientDetailSoap)(this)).SampleBackConfirmAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoBySendResponse> DALisService.RasClientDetailSoap.GetSampleInfoBySendAsync(DALisService.GetSampleInfoBySendRequest request)
        {
            return base.Channel.GetSampleInfoBySendAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetSampleInfoBySendResponse> GetSampleInfoBySendAsync(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetSampleInfoBySendRequest inValue = new DALisService.GetSampleInfoBySendRequest();
            inValue.Body = new DALisService.GetSampleInfoBySendRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetSampleInfoBySendAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetNameJpgResponse> DALisService.RasClientDetailSoap.GetNameJpgAsync(DALisService.GetNameJpgRequest request)
        {
            return base.Channel.GetNameJpgAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetNameJpgResponse> GetNameJpgAsync(string ClientID, string ClientGUID, string userid)
        {
            DALisService.GetNameJpgRequest inValue = new DALisService.GetNameJpgRequest();
            inValue.Body = new DALisService.GetNameJpgRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.userid = userid;
            return ((DALisService.RasClientDetailSoap)(this)).GetNameJpgAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCode2Response> DALisService.RasClientDetailSoap.GetDetailByHospCode2Async(DALisService.GetDetailByHospCode2Request request)
        {
            return base.Channel.GetDetailByHospCode2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByHospCode2Response> GetDetailByHospCode2Async(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            DALisService.GetDetailByHospCode2Request inValue = new DALisService.GetDetailByHospCode2Request();
            inValue.Body = new DALisService.GetDetailByHospCode2RequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.clinicid = clinicid;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByHospCode2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDA2Response> DALisService.RasClientDetailSoap.GetSampleInfoFromDA2Async(DALisService.GetSampleInfoFromDA2Request request)
        {
            return base.Channel.GetSampleInfoFromDA2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDA2Response> GetSampleInfoFromDA2Async(string User, string PassWord, string Dept, string DivideDate, string StartTime, string EndTime, int PageSize, int PageIndex)
        {
            DALisService.GetSampleInfoFromDA2Request inValue = new DALisService.GetSampleInfoFromDA2Request();
            inValue.Body = new DALisService.GetSampleInfoFromDA2RequestBody();
            inValue.Body.User = User;
            inValue.Body.PassWord = PassWord;
            inValue.Body.Dept = Dept;
            inValue.Body.DivideDate = DivideDate;
            inValue.Body.StartTime = StartTime;
            inValue.Body.EndTime = EndTime;
            inValue.Body.PageSize = PageSize;
            inValue.Body.PageIndex = PageIndex;
            return ((DALisService.RasClientDetailSoap)(this)).GetSampleInfoFromDA2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData9Response> DALisService.RasClientDetailSoap.GetDetailData9Async(DALisService.GetDetailData9Request request)
        {
            return base.Channel.GetDetailData9Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData9Response> GetDetailData9Async(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailData9Request inValue = new DALisService.GetDetailData9Request();
            inValue.Body = new DALisService.GetDetailData9RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData9Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCode3Response> DALisService.RasClientDetailSoap.GetDetailByHospCode3Async(DALisService.GetDetailByHospCode3Request request)
        {
            return base.Channel.GetDetailByHospCode3Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByHospCode3Response> GetDetailByHospCode3Async(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            DALisService.GetDetailByHospCode3Request inValue = new DALisService.GetDetailByHospCode3Request();
            inValue.Body = new DALisService.GetDetailByHospCode3RequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.clinicid = clinicid;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByHospCode3Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDA3Response> DALisService.RasClientDetailSoap.GetSampleInfoFromDA3Async(DALisService.GetSampleInfoFromDA3Request request)
        {
            return base.Channel.GetSampleInfoFromDA3Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetSampleInfoFromDA3Response> GetSampleInfoFromDA3Async(string User, string PassWord, string Dept, string DivideDate, string StartTime, string EndTime, int PageSize, int PageIndex)
        {
            DALisService.GetSampleInfoFromDA3Request inValue = new DALisService.GetSampleInfoFromDA3Request();
            inValue.Body = new DALisService.GetSampleInfoFromDA3RequestBody();
            inValue.Body.User = User;
            inValue.Body.PassWord = PassWord;
            inValue.Body.Dept = Dept;
            inValue.Body.DivideDate = DivideDate;
            inValue.Body.StartTime = StartTime;
            inValue.Body.EndTime = EndTime;
            inValue.Body.PageSize = PageSize;
            inValue.Body.PageIndex = PageIndex;
            return ((DALisService.RasClientDetailSoap)(this)).GetSampleInfoFromDA3Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetPathDetailDataSRResponse> DALisService.RasClientDetailSoap.GetPathDetailDataSRAsync(DALisService.GetPathDetailDataSRRequest request)
        {
            return base.Channel.GetPathDetailDataSRAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetPathDetailDataSRResponse> GetPathDetailDataSRAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            DALisService.GetPathDetailDataSRRequest inValue = new DALisService.GetPathDetailDataSRRequest();
            inValue.Body = new DALisService.GetPathDetailDataSRRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetPathDetailDataSRAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData10Response> DALisService.RasClientDetailSoap.GetDetailData10Async(DALisService.GetDetailData10Request request)
        {
            return base.Channel.GetDetailData10Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData10Response> GetDetailData10Async(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailData10Request inValue = new DALisService.GetDetailData10Request();
            inValue.Body = new DALisService.GetDetailData10RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData10Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCode4Response> DALisService.RasClientDetailSoap.GetDetailByHospCode4Async(DALisService.GetDetailByHospCode4Request request)
        {
            return base.Channel.GetDetailByHospCode4Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByHospCode4Response> GetDetailByHospCode4Async(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            DALisService.GetDetailByHospCode4Request inValue = new DALisService.GetDetailByHospCode4Request();
            inValue.Body = new DALisService.GetDetailByHospCode4RequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.clinicid = clinicid;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByHospCode4Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcode3Response> DALisService.RasClientDetailSoap.GetDetailDataByHospBarcode3Async(DALisService.GetDetailDataByHospBarcode3Request request)
        {
            return base.Channel.GetDetailDataByHospBarcode3Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcode3Response> GetDetailDataByHospBarcode3Async(string ClientID, string ClientGUID, string hospBarcode, byte SelectType)
        {
            DALisService.GetDetailDataByHospBarcode3Request inValue = new DALisService.GetDetailDataByHospBarcode3Request();
            inValue.Body = new DALisService.GetDetailDataByHospBarcode3RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospBarcode = hospBarcode;
            inValue.Body.SelectType = SelectType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataByHospBarcode3Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospCode2Response> DALisService.RasClientDetailSoap.GetDABarcodeByHospCode2Async(DALisService.GetDABarcodeByHospCode2Request request)
        {
            return base.Channel.GetDABarcodeByHospCode2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospCode2Response> GetDABarcodeByHospCode2Async(string ClientID, string ClientGUID, string hospBarcode)
        {
            DALisService.GetDABarcodeByHospCode2Request inValue = new DALisService.GetDABarcodeByHospCode2Request();
            inValue.Body = new DALisService.GetDABarcodeByHospCode2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospBarcode = hospBarcode;
            return ((DALisService.RasClientDetailSoap)(this)).GetDABarcodeByHospCode2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMicByHospBarcode2Response> DALisService.RasClientDetailSoap.GetDetailDataMicByHospBarcode2Async(DALisService.GetDetailDataMicByHospBarcode2Request request)
        {
            return base.Channel.GetDetailDataMicByHospBarcode2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataMicByHospBarcode2Response> GetDetailDataMicByHospBarcode2Async(string ClientID, string ClientGUID, string hospBarcode, string extend)
        {
            DALisService.GetDetailDataMicByHospBarcode2Request inValue = new DALisService.GetDetailDataMicByHospBarcode2Request();
            inValue.Body = new DALisService.GetDetailDataMicByHospBarcode2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospBarcode = hospBarcode;
            inValue.Body.extend = extend;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataMicByHospBarcode2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByCode2Response> DALisService.RasClientDetailSoap.GetDetailByCode2Async(DALisService.GetDetailByCode2Request request)
        {
            return base.Channel.GetDetailByCode2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByCode2Response> GetDetailByCode2Async(string ClientId, string ClientGUID, string BarCode, byte type)
        {
            DALisService.GetDetailByCode2Request inValue = new DALisService.GetDetailByCode2Request();
            inValue.Body = new DALisService.GetDetailByCode2RequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByCode2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetResultPicByBarcodeResponse> DALisService.RasClientDetailSoap.GetResultPicByBarcodeAsync(DALisService.GetResultPicByBarcodeRequest request)
        {
            return base.Channel.GetResultPicByBarcodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetResultPicByBarcodeResponse> GetResultPicByBarcodeAsync(string ClientId, string ClientGUID, string BarCode, string Extend)
        {
            DALisService.GetResultPicByBarcodeRequest inValue = new DALisService.GetResultPicByBarcodeRequest();
            inValue.Body = new DALisService.GetResultPicByBarcodeRequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.Extend = Extend;
            return ((DALisService.RasClientDetailSoap)(this)).GetResultPicByBarcodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.HelloWorldResponse> DALisService.RasClientDetailSoap.HelloWorldAsync(DALisService.HelloWorldRequest request)
        {
            return base.Channel.HelloWorldAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.HelloWorldResponse> HelloWorldAsync()
        {
            DALisService.HelloWorldRequest inValue = new DALisService.HelloWorldRequest();
            inValue.Body = new DALisService.HelloWorldRequestBody();
            return ((DALisService.RasClientDetailSoap)(this)).HelloWorldAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByCodeResponse> DALisService.RasClientDetailSoap.GetDetailByCodeAsync(DALisService.GetDetailByCodeRequest request)
        {
            return base.Channel.GetDetailByCodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByCodeResponse> GetDetailByCodeAsync(string ClientId, string ClientGUID, string BarCode, byte type)
        {
            DALisService.GetDetailByCodeRequest inValue = new DALisService.GetDetailByCodeRequest();
            inValue.Body = new DALisService.GetDetailByCodeRequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByCodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByCodeDAResponse> DALisService.RasClientDetailSoap.GetDetailByCodeDAAsync(DALisService.GetDetailByCodeDARequest request)
        {
            return base.Channel.GetDetailByCodeDAAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByCodeDAResponse> GetDetailByCodeDAAsync(string ClientId, string ClientGUID, string BarCode)
        {
            DALisService.GetDetailByCodeDARequest inValue = new DALisService.GetDetailByCodeDARequest();
            inValue.Body = new DALisService.GetDetailByCodeDARequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByCodeDAAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData4Response> DALisService.RasClientDetailSoap.GetDetailData4Async(DALisService.GetDetailData4Request request)
        {
            return base.Channel.GetDetailData4Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData4Response> GetDetailData4Async(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailData4Request inValue = new DALisService.GetDetailData4Request();
            inValue.Body = new DALisService.GetDetailData4RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData4Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData5Response> DALisService.RasClientDetailSoap.GetDetailData5Async(DALisService.GetDetailData5Request request)
        {
            return base.Channel.GetDetailData5Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData5Response> GetDetailData5Async(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailData5Request inValue = new DALisService.GetDetailData5Request();
            inValue.Body = new DALisService.GetDetailData5RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData5Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataResponse> DALisService.RasClientDetailSoap.GetDetailDataAsync(DALisService.GetDetailDataRequest request)
        {
            return base.Channel.GetDetailDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataResponse> GetDetailDataAsync(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailDataRequest inValue = new DALisService.GetDetailDataRequest();
            inValue.Body = new DALisService.GetDetailDataRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.ExportBySerialNumberResponse> DALisService.RasClientDetailSoap.ExportBySerialNumberAsync(DALisService.ExportBySerialNumberRequest request)
        {
            return base.Channel.ExportBySerialNumberAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.ExportBySerialNumberResponse> ExportBySerialNumberAsync(string ClientID, string ClientGUID, int SerialNumber)
        {
            DALisService.ExportBySerialNumberRequest inValue = new DALisService.ExportBySerialNumberRequest();
            inValue.Body = new DALisService.ExportBySerialNumberRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.SerialNumber = SerialNumber;
            return ((DALisService.RasClientDetailSoap)(this)).ExportBySerialNumberAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData2Response> DALisService.RasClientDetailSoap.GetDetailData2Async(DALisService.GetDetailData2Request request)
        {
            return base.Channel.GetDetailData2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData2Response> GetDetailData2Async(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType)
        {
            DALisService.GetDetailData2Request inValue = new DALisService.GetDetailData2Request();
            inValue.Body = new DALisService.GetDetailData2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            inValue.Body.DataType = DataType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData3Response> DALisService.RasClientDetailSoap.GetDetailData3Async(DALisService.GetDetailData3Request request)
        {
            return base.Channel.GetDetailData3Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData3Response> GetDetailData3Async(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType, bool All)
        {
            DALisService.GetDetailData3Request inValue = new DALisService.GetDetailData3Request();
            inValue.Body = new DALisService.GetDetailData3RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            inValue.Body.DataType = DataType;
            inValue.Body.All = All;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData3Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData6Response> DALisService.RasClientDetailSoap.GetDetailData6Async(DALisService.GetDetailData6Request request)
        {
            return base.Channel.GetDetailData6Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData6Response> GetDetailData6Async(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailData6Request inValue = new DALisService.GetDetailData6Request();
            inValue.Body = new DALisService.GetDetailData6RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData6Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData7Response> DALisService.RasClientDetailSoap.GetDetailData7Async(DALisService.GetDetailData7Request request)
        {
            return base.Channel.GetDetailData7Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData7Response> GetDetailData7Async(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType)
        {
            DALisService.GetDetailData7Request inValue = new DALisService.GetDetailData7Request();
            inValue.Body = new DALisService.GetDetailData7RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            inValue.Body.DataType = DataType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData7Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData8Response> DALisService.RasClientDetailSoap.GetDetailData8Async(DALisService.GetDetailData8Request request)
        {
            return base.Channel.GetDetailData8Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData8Response> GetDetailData8Async(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, byte DataType)
        {
            DALisService.GetDetailData8Request inValue = new DALisService.GetDetailData8Request();
            inValue.Body = new DALisService.GetDetailData8RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            inValue.Body.DataType = DataType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData8Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTSResponse> DALisService.RasClientDetailSoap.GetDetailDataTSAsync(DALisService.GetDetailDataTSRequest request)
        {
            return base.Channel.GetDetailDataTSAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataTSResponse> GetDetailDataTSAsync(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailDataTSRequest inValue = new DALisService.GetDetailDataTSRequest();
            inValue.Body = new DALisService.GetDetailDataTSRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataTSAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTResponse> DALisService.RasClientDetailSoap.GetDetailDataTCTAsync(DALisService.GetDetailDataTCTRequest request)
        {
            return base.Channel.GetDetailDataTCTAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataTCTResponse> GetDetailDataTCTAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            DALisService.GetDetailDataTCTRequest inValue = new DALisService.GetDetailDataTCTRequest();
            inValue.Body = new DALisService.GetDetailDataTCTRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataTCTAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTWithPicResponse> DALisService.RasClientDetailSoap.GetDetailDataTCTWithPicAsync(DALisService.GetDetailDataTCTWithPicRequest request)
        {
            return base.Channel.GetDetailDataTCTWithPicAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataTCTWithPicResponse> GetDetailDataTCTWithPicAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            DALisService.GetDetailDataTCTWithPicRequest inValue = new DALisService.GetDetailDataTCTWithPicRequest();
            inValue.Body = new DALisService.GetDetailDataTCTWithPicRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataTCTWithPicAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReporFileResponse> DALisService.RasClientDetailSoap.GetReporFileAsync(DALisService.GetReporFileRequest request)
        {
            return base.Channel.GetReporFileAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReporFileResponse> GetReporFileAsync(string ClientID, string ClientGUID, string BarCode)
        {
            DALisService.GetReporFileRequest inValue = new DALisService.GetReporFileRequest();
            inValue.Body = new DALisService.GetReporFileRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            return ((DALisService.RasClientDetailSoap)(this)).GetReporFileAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportListDAResponse> DALisService.RasClientDetailSoap.GetReportListDAAsync(DALisService.GetReportListDARequest request)
        {
            return base.Channel.GetReportListDAAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportListDAResponse> GetReportListDAAsync(string ClientID, string ClientGUID, string BarCode)
        {
            DALisService.GetReportListDARequest inValue = new DALisService.GetReportListDARequest();
            inValue.Body = new DALisService.GetReportListDARequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportListDAAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportFileDAResponse> DALisService.RasClientDetailSoap.GetReportFileDAAsync(DALisService.GetReportFileDARequest request)
        {
            return base.Channel.GetReportFileDAAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportFileDAResponse> GetReportFileDAAsync(string ClientID, string ClientGUID, string BarCode, byte type)
        {
            DALisService.GetReportFileDARequest inValue = new DALisService.GetReportFileDARequest();
            inValue.Body = new DALisService.GetReportFileDARequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportFileDAAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportFileDA2Response> DALisService.RasClientDetailSoap.GetReportFileDA2Async(DALisService.GetReportFileDA2Request request)
        {
            return base.Channel.GetReportFileDA2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportFileDA2Response> GetReportFileDA2Async(string ClientID, string ClientGUID, string BarCode, byte type, string TempNo)
        {
            DALisService.GetReportFileDA2Request inValue = new DALisService.GetReportFileDA2Request();
            inValue.Body = new DALisService.GetReportFileDA2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            inValue.Body.TempNo = TempNo;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportFileDA2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetPathWithPicResponse> DALisService.RasClientDetailSoap.GetPathWithPicAsync(DALisService.GetPathWithPicRequest request)
        {
            return base.Channel.GetPathWithPicAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetPathWithPicResponse> GetPathWithPicAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType)
        {
            DALisService.GetPathWithPicRequest inValue = new DALisService.GetPathWithPicRequest();
            inValue.Body = new DALisService.GetPathWithPicRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetPathWithPicAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByIdNumResponse> DALisService.RasClientDetailSoap.GetDetailDataByIdNumAsync(DALisService.GetDetailDataByIdNumRequest request)
        {
            return base.Channel.GetDetailDataByIdNumAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataByIdNumResponse> GetDetailDataByIdNumAsync(string ClientID, string ClientGUID, string idNum, byte DataType, string begDate, string endDate)
        {
            DALisService.GetDetailDataByIdNumRequest inValue = new DALisService.GetDetailDataByIdNumRequest();
            inValue.Body = new DALisService.GetDetailDataByIdNumRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.idNum = idNum;
            inValue.Body.DataType = DataType;
            inValue.Body.begDate = begDate;
            inValue.Body.endDate = endDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataByIdNumAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcodeResponse> DALisService.RasClientDetailSoap.GetDetailDataByHospBarcodeAsync(DALisService.GetDetailDataByHospBarcodeRequest request)
        {
            return base.Channel.GetDetailDataByHospBarcodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcodeResponse> GetDetailDataByHospBarcodeAsync(string ClientID, string ClientGUID, string hospName, string hospBarcode, byte DataType, string begDate, string endDate)
        {
            DALisService.GetDetailDataByHospBarcodeRequest inValue = new DALisService.GetDetailDataByHospBarcodeRequest();
            inValue.Body = new DALisService.GetDetailDataByHospBarcodeRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospName = hospName;
            inValue.Body.hospBarcode = hospBarcode;
            inValue.Body.DataType = DataType;
            inValue.Body.begDate = begDate;
            inValue.Body.endDate = endDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataByHospBarcodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospInfoResponse> DALisService.RasClientDetailSoap.GetDetailDataByHospInfoAsync(DALisService.GetDetailDataByHospInfoRequest request)
        {
            return base.Channel.GetDetailDataByHospInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataByHospInfoResponse> GetDetailDataByHospInfoAsync(string ClientID, string ClientGUID, string hospName, string patName, string patSex, string patAge, byte DataType, string begDate, string endDate)
        {
            DALisService.GetDetailDataByHospInfoRequest inValue = new DALisService.GetDetailDataByHospInfoRequest();
            inValue.Body = new DALisService.GetDetailDataByHospInfoRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospName = hospName;
            inValue.Body.patName = patName;
            inValue.Body.patSex = patSex;
            inValue.Body.patAge = patAge;
            inValue.Body.DataType = DataType;
            inValue.Body.begDate = begDate;
            inValue.Body.endDate = endDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataByHospInfoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByBarCodeResponse> DALisService.RasClientDetailSoap.GetDetailDataByBarCodeAsync(DALisService.GetDetailDataByBarCodeRequest request)
        {
            return base.Channel.GetDetailDataByBarCodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataByBarCodeResponse> GetDetailDataByBarCodeAsync(string ClientID, string ClientGUID, string barcode, byte DataType)
        {
            DALisService.GetDetailDataByBarCodeRequest inValue = new DALisService.GetDetailDataByBarCodeRequest();
            inValue.Body = new DALisService.GetDetailDataByBarCodeRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.barcode = barcode;
            inValue.Body.DataType = DataType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataByBarCodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailByHospCodeResponse> DALisService.RasClientDetailSoap.GetDetailByHospCodeAsync(DALisService.GetDetailByHospCodeRequest request)
        {
            return base.Channel.GetDetailByHospCodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailByHospCodeResponse> GetDetailByHospCodeAsync(string ClientId, string ClientGUID, string clinicid, byte type)
        {
            DALisService.GetDetailByHospCodeRequest inValue = new DALisService.GetDetailByHospCodeRequest();
            inValue.Body = new DALisService.GetDetailByHospCodeRequestBody();
            inValue.Body.ClientId = ClientId;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.clinicid = clinicid;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailByHospCodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GenQRCbyOHBarcodeResponse> DALisService.RasClientDetailSoap.GenQRCbyOHBarcodeAsync(DALisService.GenQRCbyOHBarcodeRequest request)
        {
            return base.Channel.GenQRCbyOHBarcodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GenQRCbyOHBarcodeResponse> GenQRCbyOHBarcodeAsync(string ClientID, string ClientGUID, string barcode)
        {
            DALisService.GenQRCbyOHBarcodeRequest inValue = new DALisService.GenQRCbyOHBarcodeRequest();
            inValue.Body = new DALisService.GenQRCbyOHBarcodeRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.barcode = barcode;
            return ((DALisService.RasClientDetailSoap)(this)).GenQRCbyOHBarcodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMicResponse> DALisService.RasClientDetailSoap.GetDetailDataMicAsync(DALisService.GetDetailDataMicRequest request)
        {
            return base.Channel.GetDetailDataMicAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataMicResponse> GetDetailDataMicAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, string BarCode, byte DateType)
        {
            DALisService.GetDetailDataMicRequest inValue = new DALisService.GetDetailDataMicRequest();
            inValue.Body = new DALisService.GetDetailDataMicRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.BarCode = BarCode;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataMicAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTByBarcodeResponse> DALisService.RasClientDetailSoap.GetDetailDataTCTByBarcodeAsync(DALisService.GetDetailDataTCTByBarcodeRequest request)
        {
            return base.Channel.GetDetailDataTCTByBarcodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataTCTByBarcodeResponse> GetDetailDataTCTByBarcodeAsync(string ClientID, string ClientGUID, string barcode, string patName)
        {
            DALisService.GetDetailDataTCTByBarcodeRequest inValue = new DALisService.GetDetailDataTCTByBarcodeRequest();
            inValue.Body = new DALisService.GetDetailDataTCTByBarcodeRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.barcode = barcode;
            inValue.Body.patName = patName;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataTCTByBarcodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetHPVDataResponse> DALisService.RasClientDetailSoap.GetHPVDataAsync(DALisService.GetHPVDataRequest request)
        {
            return base.Channel.GetHPVDataAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetHPVDataResponse> GetHPVDataAsync(string ClientID, string ClientGUID, string barcode, string patName)
        {
            DALisService.GetHPVDataRequest inValue = new DALisService.GetHPVDataRequest();
            inValue.Body = new DALisService.GetHPVDataRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.barcode = barcode;
            inValue.Body.patName = patName;
            return ((DALisService.RasClientDetailSoap)(this)).GetHPVDataAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataForHealthResponse> DALisService.RasClientDetailSoap.GetDetailDataForHealthAsync(DALisService.GetDetailDataForHealthRequest request)
        {
            return base.Channel.GetDetailDataForHealthAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataForHealthResponse> GetDetailDataForHealthAsync(string ClientID, string ClientGUID, string StartDate, string EndDate)
        {
            DALisService.GetDetailDataForHealthRequest inValue = new DALisService.GetDetailDataForHealthRequest();
            inValue.Body = new DALisService.GetDetailDataForHealthRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataForHealthAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcode2Response> DALisService.RasClientDetailSoap.GetDetailDataByHospBarcode2Async(DALisService.GetDetailDataByHospBarcode2Request request)
        {
            return base.Channel.GetDetailDataByHospBarcode2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataByHospBarcode2Response> GetDetailDataByHospBarcode2Async(string ClientID, string ClientGUID, string hospBarcode, byte SelectType)
        {
            DALisService.GetDetailDataByHospBarcode2Request inValue = new DALisService.GetDetailDataByHospBarcode2Request();
            inValue.Body = new DALisService.GetDetailDataByHospBarcode2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospBarcode = hospBarcode;
            inValue.Body.SelectType = SelectType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataByHospBarcode2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportJpegDAResponse> DALisService.RasClientDetailSoap.GetReportJpegDAAsync(DALisService.GetReportJpegDARequest request)
        {
            return base.Channel.GetReportJpegDAAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportJpegDAResponse> GetReportJpegDAAsync(string ClientID, string ClientGUID, string BarCode, byte type)
        {
            DALisService.GetReportJpegDARequest inValue = new DALisService.GetReportJpegDARequest();
            inValue.Body = new DALisService.GetReportJpegDARequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportJpegDAAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportJpegDA2Response> DALisService.RasClientDetailSoap.GetReportJpegDA2Async(DALisService.GetReportJpegDA2Request request)
        {
            return base.Channel.GetReportJpegDA2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportJpegDA2Response> GetReportJpegDA2Async(string ClientID, string ClientGUID, string BarCode, byte type, string TempNo)
        {
            DALisService.GetReportJpegDA2Request inValue = new DALisService.GetReportJpegDA2Request();
            inValue.Body = new DALisService.GetReportJpegDA2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            inValue.Body.TempNo = TempNo;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportJpegDA2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospCodeResponse> DALisService.RasClientDetailSoap.GetDABarcodeByHospCodeAsync(DALisService.GetDABarcodeByHospCodeRequest request)
        {
            return base.Channel.GetDABarcodeByHospCodeAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospCodeResponse> GetDABarcodeByHospCodeAsync(string ClientID, string ClientGUID, string hospName, string hospBarcode, string begDate, string endDate)
        {
            DALisService.GetDABarcodeByHospCodeRequest inValue = new DALisService.GetDABarcodeByHospCodeRequest();
            inValue.Body = new DALisService.GetDABarcodeByHospCodeRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospName = hospName;
            inValue.Body.hospBarcode = hospBarcode;
            inValue.Body.begDate = begDate;
            inValue.Body.endDate = endDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDABarcodeByHospCodeAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospInfoResponse> DALisService.RasClientDetailSoap.GetDABarcodeByHospInfoAsync(DALisService.GetDABarcodeByHospInfoRequest request)
        {
            return base.Channel.GetDABarcodeByHospInfoAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDABarcodeByHospInfoResponse> GetDABarcodeByHospInfoAsync(string ClientID, string ClientGUID, string hospName, string patName, string patSex, string patAge, string begDate, string endDate)
        {
            DALisService.GetDABarcodeByHospInfoRequest inValue = new DALisService.GetDABarcodeByHospInfoRequest();
            inValue.Body = new DALisService.GetDABarcodeByHospInfoRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.hospName = hospName;
            inValue.Body.patName = patName;
            inValue.Body.patSex = patSex;
            inValue.Body.patAge = patAge;
            inValue.Body.begDate = begDate;
            inValue.Body.endDate = endDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetDABarcodeByHospInfoAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataMic2Response> DALisService.RasClientDetailSoap.GetDetailDataMic2Async(DALisService.GetDetailDataMic2Request request)
        {
            return base.Channel.GetDetailDataMic2Async(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataMic2Response> GetDetailDataMic2Async(string ClientID, string ClientGUID, string StartDate, string EndDate, string BarCode, byte DateType)
        {
            DALisService.GetDetailDataMic2Request inValue = new DALisService.GetDetailDataMic2Request();
            inValue.Body = new DALisService.GetDetailDataMic2RequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.BarCode = BarCode;
            inValue.Body.DateType = DateType;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataMic2Async(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailData5ByPageResponse> DALisService.RasClientDetailSoap.GetDetailData5ByPageAsync(DALisService.GetDetailData5ByPageRequest request)
        {
            return base.Channel.GetDetailData5ByPageAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailData5ByPageResponse> GetDetailData5ByPageAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, int num)
        {
            DALisService.GetDetailData5ByPageRequest inValue = new DALisService.GetDetailData5ByPageRequest();
            inValue.Body = new DALisService.GetDetailData5ByPageRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.num = num;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailData5ByPageAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetPathWithPicByPageResponse> DALisService.RasClientDetailSoap.GetPathWithPicByPageAsync(DALisService.GetPathWithPicByPageRequest request)
        {
            return base.Channel.GetPathWithPicByPageAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetPathWithPicByPageResponse> GetPathWithPicByPageAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, int num)
        {
            DALisService.GetPathWithPicByPageRequest inValue = new DALisService.GetPathWithPicByPageRequest();
            inValue.Body = new DALisService.GetPathWithPicByPageRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            inValue.Body.num = num;
            return ((DALisService.RasClientDetailSoap)(this)).GetPathWithPicByPageAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetDetailDataTCTWithPicByPageResponse> DALisService.RasClientDetailSoap.GetDetailDataTCTWithPicByPageAsync(DALisService.GetDetailDataTCTWithPicByPageRequest request)
        {
            return base.Channel.GetDetailDataTCTWithPicByPageAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetDetailDataTCTWithPicByPageResponse> GetDetailDataTCTWithPicByPageAsync(string ClientID, string ClientGUID, string StartDate, string EndDate, byte DateType, int num)
        {
            DALisService.GetDetailDataTCTWithPicByPageRequest inValue = new DALisService.GetDetailDataTCTWithPicByPageRequest();
            inValue.Body = new DALisService.GetDetailDataTCTWithPicByPageRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.StartDate = StartDate;
            inValue.Body.EndDate = EndDate;
            inValue.Body.DateType = DateType;
            inValue.Body.num = num;
            return ((DALisService.RasClientDetailSoap)(this)).GetDetailDataTCTWithPicByPageAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetPathJpegZipResponse> DALisService.RasClientDetailSoap.GetPathJpegZipAsync(DALisService.GetPathJpegZipRequest request)
        {
            return base.Channel.GetPathJpegZipAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetPathJpegZipResponse> GetPathJpegZipAsync(string ClientID, string ClientGUID, string SendDate)
        {
            DALisService.GetPathJpegZipRequest inValue = new DALisService.GetPathJpegZipRequest();
            inValue.Body = new DALisService.GetPathJpegZipRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.SendDate = SendDate;
            return ((DALisService.RasClientDetailSoap)(this)).GetPathJpegZipAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<DALisService.GetReportJpegForBJResponse> DALisService.RasClientDetailSoap.GetReportJpegForBJAsync(DALisService.GetReportJpegForBJRequest request)
        {
            return base.Channel.GetReportJpegForBJAsync(request);
        }
        
        public System.Threading.Tasks.Task<DALisService.GetReportJpegForBJResponse> GetReportJpegForBJAsync(string ClientID, string ClientGUID, string BarCode, byte type)
        {
            DALisService.GetReportJpegForBJRequest inValue = new DALisService.GetReportJpegForBJRequest();
            inValue.Body = new DALisService.GetReportJpegForBJRequestBody();
            inValue.Body.ClientID = ClientID;
            inValue.Body.ClientGUID = ClientGUID;
            inValue.Body.BarCode = BarCode;
            inValue.Body.type = type;
            return ((DALisService.RasClientDetailSoap)(this)).GetReportJpegForBJAsync(inValue);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.RasClientDetailSoap))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            if ((endpointConfiguration == EndpointConfiguration.RasClientDetailSoap12))
            {
                System.ServiceModel.Channels.CustomBinding result = new System.ServiceModel.Channels.CustomBinding();
                System.ServiceModel.Channels.TextMessageEncodingBindingElement textBindingElement = new System.ServiceModel.Channels.TextMessageEncodingBindingElement();
                textBindingElement.MessageVersion = System.ServiceModel.Channels.MessageVersion.CreateVersion(System.ServiceModel.EnvelopeVersion.Soap12, System.ServiceModel.Channels.AddressingVersion.None);
                result.Elements.Add(textBindingElement);
                System.ServiceModel.Channels.HttpTransportBindingElement httpBindingElement = new System.ServiceModel.Channels.HttpTransportBindingElement();
                httpBindingElement.AllowCookies = true;
                httpBindingElement.MaxBufferSize = int.MaxValue;
                httpBindingElement.MaxReceivedMessageSize = int.MaxValue;
                result.Elements.Add(httpBindingElement);
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.RasClientDetailSoap))
            {
                return new System.ServiceModel.EndpointAddress("http://report.dagene.net/RasClientDetail.asmx");
            }
            if ((endpointConfiguration == EndpointConfiguration.RasClientDetailSoap12))
            {
                return new System.ServiceModel.EndpointAddress("http://report.dagene.net/RasClientDetail.asmx");
            }
            throw new System.InvalidOperationException(string.Format("找不到名称为“{0}”的终结点。", endpointConfiguration));
        }
        
        public enum EndpointConfiguration
        {
            
            RasClientDetailSoap,
            
            RasClientDetailSoap12,
        }
    }
}

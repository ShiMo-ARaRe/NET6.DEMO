using Microsoft.AspNetCore.Mvc;
using NET6.DEMO.WebApi.Utility.Swagger;

namespace NET6.DEMO.WebApi.Controllers
{
    /// <summary>
    /// �ļ���Դ
    /// </summary>
    [ApiController]
    [Route("[controller]")]

    //����һ�����Ա�ǣ�����ָ�������������� API Explorer ���á�
    //[ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersions.V2))] // ApiVersions�ǰ汾ö��
    //������Ը��� API Explorer ��Ҫ�������������������
    //�����������ָ������������������ API �������ơ�����������У�ʹ���� ApiVersions.V2��
    //����һ��������������ʾ API �İ汾��Ϊ V2��ͨ��ָ���������ƣ�
    //���Խ��������������ղ�ͬ�ķ�������ĵ�������ʾ��


#region ����nameof
    //nameof �� C# �е�һ������������ڻ�ȡָ�����ŵ����ƣ�������Ϊ�ַ�������
    //���ڱ���ʱ������ֵ��������Ϊ�������ݵı�ʶ�������ơ�
    //string propertyName = "Name";
    //string name = nameof(propertyName);
    //������Ĵ����У�nameof(propertyName) �������ַ��� "propertyName"��
    //���������ǿ���ȷ���ڽ��������������ַ���ʱ������ʵ�����Ե����Ʊ���һ�£��Ӷ��������ֶ������ַ����Ĵ���
#endregion


    public class FileController : ControllerBase
    {
        private readonly ILogger<FileController> _logger;

        /// <summary>
        /// ���캯��
        /// </summary>
        /// <param name="logger"></param>
        public FileController(ILogger<FileController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// �ļ��ϴ�
        /// </summary>
        /// <returns></returns>
        [HttpPost(Name = "File")]
        public JsonResult UploadFile(IFormCollection form)
        //IFormCollection ��һ���ӿڣ����ڱ�ʾ���������ݵļ��ϡ�
        //JsonResult ��һ���࣬���ڱ�ʾ���������л�Ϊ JSON ��ʽ�� HTTP ��Ӧ��

        {
            //ͨ�� return new JsonResult(...) ������һ�� JsonResult ���󣬲�������Ϊ�����ķ���ֵ��
            //������󽫱�ת��Ϊ JSON ��ʽ������Ϊ HTTP ��Ӧ���͸��ͻ��ˡ�
            //JsonResult �������������Ϊ���ݽ������л������ṩ��һЩ���Ժͷ����������� JSON ���л�����Ϊ��
            return new JsonResult(new
            {
                Success = true,
                Message = "�ϴ��ɹ�",
                FileName = form.Files.FirstOrDefault()?.FileName // ?��ʾ��ʾΪnull
                //��ȡ�ϴ��ļ����ļ�����form.Files ��һ�����ϣ��������ϴ����ļ�����
                //FirstOrDefault() �������ڻ�ȡ��һ���ļ�����Ȼ��ͨ��.FileName ���Ի�ȡ�ļ�����
            });
            //����һ�� JSON ��Ӧ����� JSON �������������ԣ�Success ���Ա�ʾ�ϴ��Ƿ�ɹ���
            //Message ������һ����Ϣ�ַ�������ʾ�ϴ������FileName ���԰������ϴ��ļ����ļ�����
        }
        //��� UploadFile �������ṩ��һ�� JSON ��Ӧ�󣬲�û�н��ϴ����ļ��洢�����ء�
        //������������������ϴ��ļ��������Ϣ��Ϊ��Ӧ��

    }
}
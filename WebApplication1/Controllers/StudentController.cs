using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApplication1.dto;
using WebApplication1.entity;
using WebApplication1.service;
using WebApplication1.utils;

namespace WebApplication1.Controllers
{
    [RoutePrefix("api/student")]
    public class StudentController : ApiController
    {

        private StudentService studentService = new StudentService();

        [Route("get")]
        [HttpGet, HttpPost]
        public Object get(int id, String callback = null)
        {
            CommonResult commonResult = new CommonResult();
            Student student = studentService.get(id);            
            commonResult.data = student;
            return JsonpUtils.doJsonP(commonResult, callback);
        }

        [Route("getall")]
        [HttpGet, HttpPost]
        public Object getAll(String callback = null)
        {
            List<Student> students = studentService.getAll();            
            CommonResult commonResult = new CommonResult();
            if (students == null)
            {
                commonResult.code = 500;
                commonResult.message = "查询错误";
            }
            commonResult.data = students;
            return JsonpUtils.doJsonP(commonResult, callback);
        }

        [Route("getallpage")]
        [HttpGet, HttpPost]
        public Object getAll(int page = 0, int size = 10, String callback = null)
        {
            MyPage<Student> myPage = studentService.getAllPage(page, size);
            List<Student> list = myPage.content;
            CommonResult commonResult = new CommonResult();
            if (list == null)
            {
                commonResult.code = 500;
                commonResult.message = "查询错误";
            }
            commonResult.data = myPage;
            return JsonpUtils.doJsonP(commonResult, callback);
        }

        [Route("delete")]
        [HttpGet, HttpPost]
        public Object delete(int id, String callback = null)
        {
            int num = studentService.delete(id);
            CommonResult commonResult = new CommonResult();
            if (num == -1)
            {
                commonResult.code = 500;
                commonResult.message = "删除失败";
            } else if (num == 0)
            {
                commonResult.code = 200;
                commonResult.message = "未发现要删除的记录";
            }
            return JsonpUtils.doJsonP(commonResult, callback);
        }

        [Route("add")]
        [HttpGet, HttpPost]
        public Object add([FromUri] Student student, String callback)
        {
            Student student2 = studentService.add(student);
            CommonResult commonResult = new CommonResult();
            if (student2 == null)
            {
                commonResult.code = 500;
                commonResult.message = "新增失败";
            }
            commonResult.data = student2;
            return JsonpUtils.doJsonP(commonResult, callback);
        }

        [Route("update")]
        [HttpGet, HttpPost]
        public Object update(int id, String updatedParams, String callback = null)
        {
            CommonResult commonResult = new CommonResult();
            if (updatedParams == null || updatedParams == "")
            {
                commonResult.code = 432;
                commonResult.message = "updatedParams不能为空";
                return JsonpUtils.doJsonP(commonResult, callback);
            }

            Student student = studentService.get(id);
            if (student == null)
            {
                commonResult.code = 500;
                commonResult.message = "要更新的对象并不存在";
                return JsonpUtils.doJsonP(commonResult, callback);
            }

            Dictionary<string, object> map = JsonConvert.DeserializeObject<Dictionary<string, object>>(updatedParams);

            Student student2 = studentService.update(student, map);
            
            if (student2 == null)
            {
                commonResult.code = 500;
                commonResult.message = "更新失败";
            }
            commonResult.data = student2;
            return JsonpUtils.doJsonP(commonResult, callback);
        }

    }
}

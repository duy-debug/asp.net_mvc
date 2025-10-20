using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BaiTap3._1_65130650.Models
{
    public class EmpModel
    {
        [Required(ErrorMessage = "Vui lòng nhập Mã nhân viên")]
        public string EmpID { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Họ tên")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Năm sinh")]
        [DataType(DataType.Date)]
        public DateTime BirthOfDate { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Email")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập Đơn vị")]
        public string Department { get; set; }

        public string Avatar { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace asp.Models
{
    public class User
    {
        /// <summary>
        ///  사용자 번호
        /// </summary>
        [Key] // PK 설정(기본키)
        public int UserNo { get; set; }
        /// <summary>
        /// 사용자 이름
        /// </summary>
        [Required]// Not Null 설정
        public string UserName { get; set; }
        /// <summary>
        /// 사용자 아이디
        /// </summary>
        [Required]// Not Null 설정
        public string UserId { get; set; }
        /// <summary>
        /// 사용자 비밀번호
        /// </summary>
        [Required]// Not Null 설정
        public int UserPassword { get; set; }

    }
}

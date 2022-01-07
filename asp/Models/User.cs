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
        [Required(ErrorMessage = "사용자 이름을 입력하세요")]// Not Null 설정
        public string UserName { get; set; }
        /// <summary>
        /// 사용자 아이디
        /// </summary>
        [Required(ErrorMessage = "아이디를 입력하세요")]// Not Null 설정
        public string UserId { get; set; }
        /// <summary>
        /// 사용자 비밀번호
        /// </summary>
        [Required(ErrorMessage = "비밀번호를 입력하세요")]// Not Null 설정
        public string UserPassword { get; set; }

    }
}

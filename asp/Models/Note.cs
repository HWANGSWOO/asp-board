using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace asp.Models
{
    public class Note
    {

        /// <summary>
        /// 개시판 번호
        /// </summary>
        [Key]
        public int NoteNo { get; set; }
        /// <summary>
        /// 게시판 제목
        /// </summary>
        [Required(ErrorMessage = "게시판 제목을 입력하세요")]// Not Null 설정
        public string NoteTitle { get; set; }
        /// <summary>
        /// 게시물 내용
        /// </summary>
        [Required(ErrorMessage = "게시물 내용을 입력하세요")]// Not Null 설정
        public string NoteContents { get; set; }
        /// <summary>
        /// 작성자 번호
        /// </summary>
        [Required] // not null
        public int UserNo { get; set; }
        
        [ForeignKey("UserNo")]//외래키 //UserNo를 사용안하면 외래키가 뭔지를 모르기 때문에 작성을 해야함
        public virtual User User { get; set; } //User와 조인
    }
}

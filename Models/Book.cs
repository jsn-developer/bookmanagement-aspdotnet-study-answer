using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BookManagementApp.Models{
    /// <summary>
    /// 書籍モデル
    /// </summary>
    public class Book{

        /// <summary>
        /// 主キーとなる書籍のID
        /// </summary>
        /// <value></value>
        public int BookId{ get; set; }

        /// <summary>
        /// 書籍タイトル
        /// </summary>
        /// <value></value>
        [Required(ErrorMessage="タイトルを入力してください。")]
        [Display(Name="タイトル")]
        public string Title{ get; set; }

        /// <summary>
        /// 著者名
        /// </summary>
        [Required(ErrorMessage="著者名を入力してください。")]
        [Display(Name="著者名")]
        public string Writer{ get; set; }

        /// <summary>
        /// 出版社名
        /// </summary>
        [Required(ErrorMessage="出版社を入力してください。")]
        [Display(Name="出版社")]
        public string Company{ get; set; }

        /// <summary>
        /// 価格
        /// </summary>
        [Range(1,100000,ErrorMessage="1以上100000以上の値を入力してください。"), Required(ErrorMessage="価格を入力してください。")]
        [Display(Name="価格")]
        public int? Price{ get; set; }

        /// <summary>
        /// 購入日
        /// </summary>
        [DataType(DataType.Date),Required(ErrorMessage="購入日を入力してください。")]
        [Display(Name="購入日")]
        public DateTime? Buydate { get; set; }

        /// <summary>
        /// 書籍管理部門
        /// </summary>
        [Required(ErrorMessage="書籍管理部門を入力してください。")]
        [Display(Name="書籍管理部門")]
        public string ManagementDepartment{ get; set; }

        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime Updatedate{ get; set; }

        /// <summary>
        /// モデルの連携を行うためのPersonId
        /// </summary>
        public int PersonId{ get; set; }
        /// <summary>
        /// 連携を行ったモデルのデータを保管するためのもの
        /// </summary>
        public Person Person { get; set; }

    }
}
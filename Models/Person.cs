using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookManagementApp.Models{
    /// <summary>
    /// ユーザーモデル
    /// </summary>
    public class Person{
        /// <summary>
        /// 主キーとなるユーザーのID
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// ログインID
        /// </summary>
        public string UserId{ get; set; }

        /// <summary>
        /// パスワード
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 所属部署
        /// </summary>
        public string DepartmentName{ get; set; }
    }
}
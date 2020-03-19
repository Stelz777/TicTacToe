using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NETCoreTicTacToe.Models
{
    public class Captcha
    {
        public string UserEnteredCaptchaCode { get; set; }
        public string CaptchaId { get; set; }
    }
}

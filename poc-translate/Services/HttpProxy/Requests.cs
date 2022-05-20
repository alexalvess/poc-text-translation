using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace poc_translate.Services.HttpProxy
{
    public static class Requests
    {
        public record TextToTranslate(string Text);
    }
}

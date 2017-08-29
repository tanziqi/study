using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataStruct.TextSearch
{
    class RabinKarp
    {
        private string _pat;
        private ulong _patHash;
        private int _patLen;

        private ulong _H;
        private const uint _R = 128;
        private const ulong _Q = 9999991;

        public RabinKarp(string pat)
        {
            _pat = pat;
            _patLen = pat.Length;
            
            _H = 1;
            for(int i = 1; i < _patLen; i++)
            { 
                _H = (_R * _H) % _Q;
            }
            _patHash = hash(pat, _patLen);
        }

        public int Search(string text)
        {
            int textLen = text.Length;
            ulong textHash = 0;

            textHash = hash(text, _patLen);
            if (textHash == _patHash && check(0))
            {
                return 0;
            }

            for(int i = _patLen; i < textLen; i++)
            {
#if true
                textHash = (textHash - _H * text[i - _patLen] % _Q + _Q);  // 在《算法-第四版》中这个式子的外面还取模了一次, 变为: textHash = (textHash - _H * text[i - _patLen] % _Q + _Q) % Q; 
                                                                           // 但是即便不取模, 这个式子也能保证正数, 所以不清楚再取模一次的用意?  参考:http://blog.csdn.net/u013679882/article/details/68955535
                textHash = (textHash * _R + text[i]) % _Q;
#else
                /*
                    原始的公式2如下面两句代码. 但若tmp的值是负的, 匹配结果就会不正确.
                    为了确保这一式子值为正数, 
                */
                long tmp = textHash - text[i - _patLen] * _H;
                textHash = (tmp * _R + text[i]) % _Q;
#endif

                if (textHash == _patHash && check(i - _patLen + 1))
                {
                    return i - _patLen + 1;
                }
            }

            return -1;
        }

        private ulong hash(string key, int m)
        {
            ulong hash_int = 0;
            for (int j = 0; j < m; j++)
            {
                hash_int = (_R * hash_int + key[j]) % _Q;
            }
            return hash_int;
        }

        /*
         * 因为散列的结果可能有冲突, 还需要一个函数来检查是不是真正匹配的子串.
         * 两种方法: 蒙特卡洛与拉斯维加斯方法 
         */
        private bool check(int i)//蒙特卡洛算法: 将散列表大小(_htSize)设置得非常大, 使冲突概率极低, 并忽略冲突.
        {
            return true;
        }
    }
}

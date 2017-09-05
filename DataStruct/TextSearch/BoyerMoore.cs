using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStruct.TextSearch
{
    ///Boyer-Moore的主要思想: 从模式串的尾部自右往左匹配.
    ///"坏字符"<1> ababc
    ///            ababababc  此时a即为坏字符, 此时对应patt串比较的位置为4, 而坏字符a在patt串从后往前找下标为2, 则后退步长=4-2=2 
    ///                     
    ///        <2> ababc
    ///            ababdababc 此时d为坏字符, 但d在模式串中从未出现, 则后退步长=4 - (-1) = 5//其实也就是右移模式串的长度。 下一次直接ababc比对了
    ///        
    ///         坏字符表是可能出现负数的。如下例中坏字符P[2], M=8, 右数第一个e字符在P[6]处, 步长=2-6=-4; bad[Text[i]](也就是bad[e](Patt[6]处))=m-1-6=1
    ///                                                                                                  bad[Patt[i]](也就是bad[a])           =m-1-2=5
    ///          here is a semplei examplei     -------\      here is a semplei examplei
    ///                   examplei              -------/           examplei
    ///        
    ///"好后缀" 设模式串P长度为m, 某坏字符对应串下标j, j 取值范围为[0, m]， 那么好后缀为 P[j+1 ,m-1]
    ///        <1> 若P中  存在  P[k, i] == P[j+1 ,m-1]                即从j开始至右往左， 有1个或多个子串与好后缀完全匹配, 则右移子串 m-1-i; 如串 aabcaab  公式:gs[m-1-suff[i]] = m-1-i; 
    ///                                                                                                                                   sufflen 0030007  作用于 [0, m-1-i) 区间; i即为等于好后缀的前缀的右边界
    ///                                                                                                                                        gs 4444771  /*有趣的是 gs[m-1-i, m-1-1]在这里等于整个模式串的长度7. 
    ///                                                                                                                                                    比如与aabcabb匹配, 既然索引5处处于相等前后缀串中,那么
    ///                                                                                                                                                    自然前缀与其也不会匹配, 所以直接移动一个模式串的距离*/
    ///                                                                                                                                 
    ///        <2> 若P中  存在  P[k, i] 为 P[j+1 ,m-1] 子集           即从j开始至右往左， 有1个或多个子串与好后缀部分匹配, 则右移子串 m-1-i; 如串 abccaab  公式与作用范围同上
    ///                                                                                                                                   sufflen 0211007  i即为部分等于好后缀的前缀的右边界
    ///                                                                                                                                        gs 5555571  
    ///                                                                                                                                 
    ///        <3> 若P中 不存在 P[k, i] 为 P[j+1 ,m-1] 子集           即从j开始至右往左， 毫无子串与好后缀哪怕部分匹配, 则右移整个模式串 m;  如串 example  
    ///                                                                                                                                           0000007
    ///                                                                                                                                           7777771


    class BoyerMoore
    {
        private int[] _badCharTab;     //坏字符表
        private int[] _goodSuffixTab;  //好后缀表
        private int[] _suffixLenArr;   //模式串公共后缀长度数组，计算好后缀表要用到(从后往前比较)
        private string _patt;          //模式串

        private int _R;

        void InitBadCharTab()
        {
            int i = 0;
            for (; i < _R; i++)
            {
                _badCharTab[i] = _patt.Length;//先假定所有的坏字符在模式串中都不会出现
            }
            for (i = 0; i < _patt.Length; i++)//遍历patt串, 因为patt中的字符以后在搜索中如果被认定为坏字符那肯定不是第一次出现.
            {
                _badCharTab[_patt[i]] = _patt.Length - 1 - i;
            }
        }

        void GetSuffixLen()
        {
            int i = 0;
            int j = 0;
            _suffixLenArr[_patt.Length - 1] = _patt.Length;
            for (i = _patt.Length - 2; i >= 0; i--)
            {
                j = i;
                while (j >= 0 && _patt[j] == _patt[_patt.Length - 1 - i + j])//假设模式串bbbbb的话, 那么这个j就一致循环减到-1; suffixlen{1,2,3,4,5}
                {                                                            //假设模式串ababc的话, suffixlen{0,2,0,0,5}
                    j--;
                }
                _suffixLenArr[i] = i - j;
            }
        }
        void InitGoodSuffixTab()
        {
            GetSuffixLen();

            // 先全部赋值为m，包含Case3
            int i = 0;
            for (i = 0; i < _patt.Length; i++)
            {
                _goodSuffixTab[i] = _patt.Length;
            }

            // Case2
            int j = 0;
            for (i = _patt.Length - 1; i >= 0; i--)
            {
                if (_suffixLenArr[i] == i + 1)//这个if在suffixlen[i]>0的时候进, i也就是模式串后缀的右边界。 比如模式串abccaab, suffixlen{0,2,0,0,0,0,7} 经过case2后得 bs{5,5,5,5,5,7,7}
                {
                    for (; j < _patt.Length - 1 - i; j++)
                    {
                        if (_goodSuffixTab[j] == _patt.Length)
                        {
                            _goodSuffixTab[j] = _patt.Length - 1 - i;
                        }
                    }
                }
            }

            // Case1
            for (i = 0; i <= _patt.Length - 2; i++)//
            {
                _goodSuffixTab[_patt.Length - 1 - _suffixLenArr[i]] = _patt.Length - 1 - i;
            }


        }

        public BoyerMoore(string pattern)
        {
            _R = 256;//ascii size
            _patt = pattern;

            _badCharTab = new int[_R];     //坏字符表
            _goodSuffixTab = new int[_patt.Length];  //好后缀表
            _suffixLenArr = new int[_patt.Length];   //计算好后缀表要用到的模式串的公共后缀长度数组(从后往前比较)

            InitBadCharTab();
            InitGoodSuffixTab();
        }

        public int Search(string text)
        {
            int j = 0;//jump len
            int i = 0;//pattern index
            while (j <= text.Length - _patt.Length)
            {
                for (i = _patt.Length - 1; i >= 0 && _patt[i] == text[i + j]; --i)
                { }
                if (i < 0)
                {
                    return j;//Match;
                }
                else
                {
                    //按坏字符规则的话 偏移: 
                    int badoff = _badCharTab[text[i + j]] - (_patt.Length - 1 - i);
                    //按好后缀规则的话 偏移: _goodSuffixTab[i]
                    int goodoff = _goodSuffixTab[i];

                    //取两条规则中较大的值进行偏移
                    j += badoff > goodoff ? badoff : goodoff;
                }
            }
            return 0;
        }



    }
}

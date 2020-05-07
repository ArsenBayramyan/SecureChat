using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.Core.Helper
{
    public static class CodingDecodingHelper
    {
        public static string CodeingCaesar(this string value)
        {
            if (value==null)
            {
                return null;
            }
            List<char> originList = new List<char>()
                        { 'A','B','C','D','E','F','G','H','I','J','K','L','M',
                          'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',' ',',',':',';',')','(','.','@','?','!',
                          'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o',
                          'p','q','r','s','t','u','v','w','x','y','z','1','2','3','4','5','6','7','8','9','0',
                          'Ա','Բ','Գ','Դ','Ե','Զ','Է','Ը','Թ','Ժ','Ի','Լ','Խ','Ծ','Կ','Հ','Ձ','Ղ','Ճ','Մ','Յ','Ն','Շ','Ո','Չ','Պ','Ջ','Ռ','Ս','Վ',
                          'Տ','Ր','Ց','Ւ','Փ','Ք','Օ','Ֆ','ա','բ','գ','դ','ե','զ','է','ը','թ','ժ','ի','լ','խ','ծ','կ','հ','ձ','ղ','ճ','մ','յ','ն','շ','ո','չ','պ','ջ','ռ','ս','վ','տ',
                          'ր','ց','ւ','փ','ք','և','օ','ֆ'
                        };
            List<char> codeList = new List<char>()
                               { 'D','E','F','G','H','I','J','K','L','M',
                                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z','A','B','C','$','#','*',')','/','%','(','-',
                                 'd','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','~','!',
                                 'v','w','x','y','z','a','b','c','4','5','6','7','8','9','0','1','2','3',
                                 'Ե','Զ','Է','Ը','Թ','Ժ','Ի','Լ','Խ','Ծ','Կ','Հ','Ձ','Ղ','Ճ','Մ','Յ','Ն','Շ','Ո','Չ','Պ','Ջ','Ռ','Ս','Վ',
                                 'Տ','Ր','Ց','Ւ','Փ','Ք','Օ','Ֆ','Ա','Բ','Գ','Դ','ե','զ','է','ը','թ','ժ','ի','լ','խ','ծ','կ','հ','ձ','ղ','ճ','մ','յ','ն','շ','ո','չ','պ','ջ','ռ','ս','վ','տ',
                                 'ր','ց','г','փ','ք','և','օ','ֆ','ա','բ','գ','դ'
                               };
            int index = 0;
            List<char> outputList = new List<char>();
            for (int i = 0; i < value.Length; i++)
            {
                index = originList.IndexOf(value[i]);
                outputList.Add(codeList[index]);
            }
            return CodingWithMatrix(new string(outputList.ToArray()));
        }
        private static string CodingWithMatrix(string value)
        {
            int MATRIX_ROWS = Convert.ToInt32(Math.Ceiling(Math.Sqrt(value.Length)));
            string[,] matrix = new string[MATRIX_ROWS, MATRIX_ROWS];
            int inpCounter = 0;

            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {

                    if (inpCounter >= value.Length)
                    {
                        matrix[i, j] = "0";
                    }
                    else
                    {
                        matrix[i, j] = value[inpCounter].ToString();
                        inpCounter++;
                    }
                }
            }
            string word = null;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {
                    word += matrix[i, j];
                }
            }
            int zeroCount = word.Length - value.Length;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = i; j < MATRIX_ROWS; j++)
                {
                    string c = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = c;
                }
            }
            string _outPut = null;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {
                    _outPut += matrix[i, j];
                }
            }
            _outPut += zeroCount;
            return _outPut;
        }
        private static string DecodingCaesar(string output)
        {
            List<char> originList = new List<char>()
                        { 'A','B','C','D','E','F','G','H','I','J','K','L','M',
                          'N','O','P','Q','R','S','T','U','V','W','X','Y','Z',' ',',',':',';',')','(','.','@','?','!',
                          'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o',
                          'p','q','r','s','t','u','v','w','x','y','z','1','2','3','4','5','6','7','8','9','0',
                          'Ա','Բ','Գ','Դ','Ե','Զ','Է','Ը','Թ','Ժ','Ի','Լ','Խ','Ծ','Կ','Հ','Ձ','Ղ','Ճ','Մ','Յ','Ն','Շ','Ո','Չ','Պ','Ջ','Ռ','Ս','Վ',
                          'Տ','Ր','Ց','Ւ','Փ','Ք','Օ','Ֆ','ա','բ','գ','դ','ե','զ','է','ը','թ','ժ','ի','լ','խ','ծ','կ','հ','ձ','ղ','ճ','մ','յ','ն','շ','ո','չ','պ','ջ','ռ','ս','վ','տ',
                          'ր','ց','ւ','փ','ք','և','օ','ֆ'
                        };
            List<char> codeList = new List<char>()
                               { 'D','E','F','G','H','I','J','K','L','M',
                                 'N','O','P','Q','R','S','T','U','V','W','X','Y','Z','A','B','C','$','#','*',')','/','%','(','-',
                                 'd','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','~','!',
                                 'v','w','x','y','z','a','b','c','4','5','6','7','8','9','0','1','2','3',
                                 'Ե','Զ','Է','Ը','Թ','Ժ','Ի','Լ','Խ','Ծ','Կ','Հ','Ձ','Ղ','Ճ','Մ','Յ','Ն','Շ','Ո','Չ','Պ','Ջ','Ռ','Ս','Վ',
                                 'Տ','Ր','Ց','Ւ','Փ','Ք','Օ','Ֆ','Ա','Բ','Գ','Դ','ե','զ','է','ը','թ','ժ','ի','լ','խ','ծ','կ','հ','ձ','ղ','ճ','մ','յ','ն','շ','ո','չ','պ','ջ','ռ','ս','վ','տ',
                                 'ր','ց','г','փ','ք','և','օ','ֆ','ա','բ','գ','դ'
                               };
            List<char> word = new List<char>();
            int index = 0;
            for (int i = 0; i < output.Length; i++)
            {
                index = codeList.IndexOf(output[i]);
                word.Add(originList[index]);
            }
            return new string(word.ToArray());
        }
        public static string DecodingMatrix(this string value)
        {
            if (value==null)
            {
                return null;
            }
            var code = value.Substring(0, value.Length - 1);
            int MATRIX_ROWS = Convert.ToInt32(Math.Ceiling(Math.Sqrt(code.Length)));
            string[,] matrix = new string[MATRIX_ROWS, MATRIX_ROWS];
            int inpCounter = 0;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {

                    if (inpCounter >= value.Length)
                    {
                        matrix[i, j] = "0";
                    }
                    else
                    {
                        matrix[i, j] = value[inpCounter].ToString();
                        inpCounter++;
                    }
                }
            }

            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = i; j < MATRIX_ROWS; j++)
                {
                    string c = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = c;
                }
            }
            string outcode = null;
            for (int i = 0; i < MATRIX_ROWS; i++)
            {
                for (int j = 0; j < MATRIX_ROWS; j++)
                {
                    outcode += matrix[i, j];
                }
            }
            int x = (int)(value[value.Length - 1] - '0');
            var decode = outcode.Substring(0, outcode.Length - x);
            return DecodingCaesar(decode);
        }
    }
}

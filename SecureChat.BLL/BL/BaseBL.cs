using System;
using System.Collections.Generic;
using System.Text;

namespace SecureChat.BLL.BL
{
    public class BaseBL<T>
    {
        protected T repository { get; private set; }
        public BaseBL(T repository)
        {
            this.repository = repository;
        }
    }
}

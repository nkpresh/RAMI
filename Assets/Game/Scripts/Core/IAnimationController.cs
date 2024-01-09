using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public interface IAnimationController
    {
        public void OnAnimationStart(string animation);
        public void OnAnimationEnd(string animation);
    }
}
